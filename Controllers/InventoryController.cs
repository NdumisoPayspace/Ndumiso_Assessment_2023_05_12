namespace Ndumiso_Assessment_2023_05_12.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
