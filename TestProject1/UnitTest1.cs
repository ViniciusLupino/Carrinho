using Moq;
using PF.Data;
using PF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

public class CarrinhoServiceTests
{
    private readonly ApplicationDbContext _dbContext;

    public CarrinhoServiceTests()
    {
        // Configura o DbContext para usar um banco de dados em memória
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new ApplicationDbContext(options);
    }

    [Fact]
    public async Task GetCarrinhoItemCount_WithValidUserId_ReturnsItemCount()
    {
        // Arrange
        string userId = "12345";
        var carrinho = new Carrinho { Id = 1, UserId = userId };
        _dbContext.Carrinhos.Add(carrinho);

        var detalheCarrinho1 = new DetalheCarrinho { Id = 1, CarrinhoId = 1 };
        var detalheCarrinho2 = new DetalheCarrinho { Id = 2, CarrinhoId = 1 };

        _dbContext.DetalheCarrinhos.Add(detalheCarrinho1);
        _dbContext.DetalheCarrinhos.Add(detalheCarrinho2);
        _dbContext.SaveChanges();

        var service = new CarrinhoService(_dbContext);

        // Act
        var itemCount = await service.GetCarrinhoItemCount(userId);

        // Assert
        Assert.Equal(2, itemCount);  // Espera que retorne 2 itens
    }

    [Fact]
    public async Task GetCarrinhoItemCount_WithEmptyUserId_ReturnsZero()
    {
        // Arrange
        string userId = string.Empty;

        var service = new CarrinhoService(_dbContext);

        // Act
        var itemCount = await service.GetCarrinhoItemCount(userId);

        // Assert
        Assert.Equal(0, itemCount);  // Espera que retorne 0 itens
    }
}
