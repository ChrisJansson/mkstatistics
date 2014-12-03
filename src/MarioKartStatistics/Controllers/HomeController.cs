using Microsoft.AspNet.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MarioKartStatistics.Controllers
{   
    public class HomeController : Controller
    {
        private readonly MKContext _mkContext;
        public HomeController(MKContext mkContext)
        {
            _mkContext = mkContext;
        }
        public IActionResult Index()
        {
            var a = _mkContext.Heats
                .ToList();
            return View();
        }
    }
}
