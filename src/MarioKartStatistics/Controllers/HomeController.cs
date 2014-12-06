using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            _mkContext.Heats.RemoveRange(_mkContext.Heats.ToList());
            _mkContext.Heats.Add(new Heat
            {
                Date = DateTime.Now,
                Scores = new List<HeatScore>
                {
                    new HeatScore
                    {
                        Score = 5,
                        Player = new Player
                        {
                            Name = "A"
                        }
                    },
                    new HeatScore
                    {
                        Score = 10,
                        Player = new Player
                        {
                            Name = "B"
                        }
                    }
                },
            });
            _mkContext.SaveChanges();
        }
        public IActionResult Index()
        {
            var heats = _mkContext.Heats
                .ToList()
                .Select(x => Convert(x))
                .ToList();

            return View(heats);
        }

        [HttpPost]
        public void AddResult(NewHeatResult result)
        {

        }

        private HeatModel Convert(Heat heat)
        {
            return new HeatModel()
            {
                Date = heat.Date,
                Scores = heat.Scores
                .Select(x => new HeatModel.HeatScoreModel()
                {
                    PlayerName = x.Player.Name,
                    Score = x.Score,
                })
                .OrderByDescending(x => x.Score)
                .ToList()
            };
        }
    }

    public class NewHeatResult
    {
        [Required]
        IList<NewHeatScore> Scores { get; set; }
    }

    public class NewHeatScore
    {
        [Required]
        public int? Player { get; set; }
        [Required]
        public int? Points { get; set; }
    }

    public class HeatModel
    {
        public DateTime Date { get; set; }
        public List<HeatScoreModel> Scores { get; set; }
        public class HeatScoreModel
        {
            public string PlayerName { get; set; }
            public int Score { get; set; }
        }
    }
}
