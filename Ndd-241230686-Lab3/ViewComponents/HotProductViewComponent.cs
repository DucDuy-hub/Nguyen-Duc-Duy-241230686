using Microsoft.AspNetCore.Mvc;
using Ndd_241230686_Lab3.Models;

namespace Ndd_241230686_Lab3.ViewComponents
{
    public class HotProductViewComponent : ViewComponent
    {
        protected Product product = new Product();

        public IViewComponentResult Invoke()
        {
            var products = product.GetProductList();
            return View(products);
        }
    }
}
