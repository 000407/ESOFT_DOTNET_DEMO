using demo_crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo_crud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDataAccessContext _context;

        [ActivatorUtilitiesConstructor]
        public ProductsController(ProductDataAccessContext context) => 
            _context = context;

        public IActionResult Index()
        {
            return View(_context.GetAllProducts());
        }
    }
}