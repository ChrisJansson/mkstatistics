using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MarioKartStatistics.Controllers
{   
    public class HomeController : Controller
    {
        private readonly MKContext _mkContext;
        public HomeController(MKContext mkContext)
        {
            var context = new MKContext();
            _mkContext = mkContext;
        }
        public IActionResult Index()
        {
            _mkContext.Heats.Add(new Heat { A = 1 });
            _mkContext.SaveChanges();
            return View();
        }
    }
}
