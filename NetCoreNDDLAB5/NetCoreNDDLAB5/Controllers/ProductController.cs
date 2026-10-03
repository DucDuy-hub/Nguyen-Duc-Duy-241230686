using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreNDDLAB5.Models;

namespace NetCoreNDDLAB5.Controllers
{
    public class ProductController : Controller
    {
        private static List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Điện thoại" },
            new Category { Id = 2, Name = "Laptop" },
            new Category { Id = 3, Name = "Tai nghe" },
            new Category { Id = 4, Name = "Phụ kiện" }
        };

        private static List<Product> products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "iPhone 17 Pro Max",
                Image = "",
                Price = 30000000,
                SalePrice = 27000000,
                Description = "Điện thoại cao cấp",
                CategoryId = 1
            },

            new Product
            {
                Id = 2,
                Name = "Laptop Lenovo IdeaPad",
                Image = "",
                Price = 20000000,
                SalePrice = 18000000,
                Description = "Laptop phục vụ học tập",
                CategoryId = 2
            }
        };

        // GET: Product
        public IActionResult Index()
        {
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            Product product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories =
                new SelectList(categories, "Id", "Name");

            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public async Task<IActionResult> Create(
            Product product,
            IFormFile ImageFile)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError(
                    "ImageFile",
                    "Vui lòng chọn ảnh sản phẩm");
            }

            if (ModelState.IsValid)
            {
                product.Id = products.Count == 0
                    ? 1
                    : products.Max(x => x.Id) + 1;

                string folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "products");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =
                    Guid.NewGuid().ToString()
                    + Path.GetExtension(ImageFile.FileName);

                string filePath =
                    Path.Combine(folder, fileName);

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                product.Image = "/products/" + fileName;

                products.Add(product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories =
                new SelectList(
                    categories,
                    "Id",
                    "Name",
                    product.CategoryId);

            return View(product);
        }

        // GET: Product/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product product =
                products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories =
                new SelectList(
                    categories,
                    "Id",
                    "Name",
                    product.CategoryId);

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(
            Product product,
            IFormFile ImageFile)
        {
            Product oldProduct =
                products.FirstOrDefault(x => x.Id == product.Id);

            if (oldProduct == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string folder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "products");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName =
                        Guid.NewGuid().ToString()
                        + Path.GetExtension(ImageFile.FileName);

                    string filePath =
                        Path.Combine(folder, fileName);

                    using (var stream = new FileStream(
                        filePath,
                        FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    product.Image = "/products/" + fileName;
                }
                else
                {
                    product.Image = oldProduct.Image;
                }

                oldProduct.Name = product.Name;
                oldProduct.Price = product.Price;
                oldProduct.SalePrice = product.SalePrice;
                oldProduct.Description = product.Description;
                oldProduct.CategoryId = product.CategoryId;
                oldProduct.Image = product.Image;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories =
                new SelectList(
                    categories,
                    "Id",
                    "Name",
                    product.CategoryId);

            return View(product);
        }

        // GET: Product/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product product =
                products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/DeleteConfirmed
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            Product product =
                products.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                products.Remove(product);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}