using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILojaRepositorio _lojaRepositorio;

        public HomeController(ILogger<HomeController> logger, ILojaRepositorio lojaRepositorio)
        {
            _lojaRepositorio = lojaRepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sterm = "", int categoriaId = 0)
        {

            IEnumerable<Produto> produtos = await _lojaRepositorio.GetProdutos(sterm, categoriaId);
            IEnumerable<Categoria> categorias = await _lojaRepositorio.GetCategorias();
            ProdutoDisplayModel produtoModel = new()
            {
                Produtos = produtos,
                Categorias = categorias,
                sTerm = sterm,
                CategoriaId = categoriaId
            };
            return View(produtoModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
