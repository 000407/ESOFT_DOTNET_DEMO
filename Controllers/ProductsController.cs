using demo_crud.Context;
using demo_crud.Models;
using demo_crud.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace demo_crud.Controllers
{
    [Route("products")] // Attribute routing; note the text "products" become a prefix segment in the URL
    public class ProductsController : Controller
    {
        private readonly StoreContext _context;

        [ActivatorUtilitiesConstructor]
        public ProductsController(StoreContext context) => 
            _context = context; // Note: the field _context is injected with the instance in the application container

        [HttpGet("", Name = "all_products")]
        public IActionResult Index()
        {
            return View(_context.Products);
        }

        [HttpGet("{id:int}/edit", Name = "product_edit_form")] // Exposes an endpoint: GET /products/<PRODUCT_ID>/edit
        public IActionResult Edit(int id)
        {
            Product? product = _context.Products.Find(id); // Find the record by the ID: NOTE that we don't write ANY SQL. All of that is handled by the entity framework
            ViewBag.Id = id;
            return View(product);
        }

        [HttpPost("update", Name = "product_update")] // Exposes an endpoint: POST /products/update
        public IActionResult Edit(Product product) // the parameter `product` will be populated from the form data that we submit
        {
            var entity = _context.Products.Find(product.Id);

            if (entity != null) {
                entity.Name = product.Name; // Set updates
                entity.Price = product.Price;

                _context.SaveChanges(); // Save the changes done to the entity

                ViewBag.Message = new Message(Status.INFO, $"Successfully updated product with ID: {product.Id}"); // Put data about the "MESSAGE" in the ViewBag, which can be read from the view
            } else {
                ViewBag.Message = new Message(Status.ERROR, $"No product was found for ID: {product.Id}"); // Put data about the "MESSAGE" in the ViewBag, which can be read from the view
            }

            return View(product);
        }
        
        [HttpGet("new", Name = "new_product_view")] // Exposes an endpoint: GET /products/nex
        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpDelete("{id:int}/delete", Name = "delete_product")] // Exposes an endpoint: DELETE /products/<PRODUCT_ID>/delete
        public IActionResult DeleteProduct(int id)
        {
            Product? p = _context.Products.Find(id);

            if (p != null) {
               _context.Products.Remove(p);
               _context.SaveChanges(); 

               return Ok();
            } else {
                return NotFound();
            }
        }
        
        [HttpPost("new", Name = "create_new_product")] // Exposes an endpoint: POST /products/new
        public IActionResult NewProduct(Product product) // the parameter `product` will be populated from the form data that we submit
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToRoute("product_view", new { id = product.Id });
        }

        [HttpGet("{id:int}/view", Name = "product_view")] // Exposes an endpoint: GET /products/<PRODUCT_ID>/view
        public IActionResult ViewProduct(int id)
        {
            Product? product = _context.Products.Find(id);
            ViewBag.Id = id;
            
            if (product == null) {
                ViewBag.Message = new Message(Status.ERROR, $"No product was found for ID: {id}");
            }
            
            return View(product);
        }
    }
}