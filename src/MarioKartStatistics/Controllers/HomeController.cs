﻿using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

namespace MarioKartStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly MKContext _mkContext;
        public HomeController(MKContext mkContext)
        {
            _mkContext = mkContext;
            Trace.TraceError("TRACING ERROR!");
        }
        public IActionResult Index()
        {
            var players = _mkContext.Players
                .Select(x => new PlayerModel { Id = x.Id, Name = x.Name })
                .ToList();

            var heats = _mkContext.Heats
                .Include(x => x.Scores)
                .OrderByDescending(x => x.Date)
                .Take(50)
                .ToList()
                .Select(x => Convert(x))
                .ToList();

            var homeViewModel = new HomeViewModel
            {
                Heats = heats,
                Players = players
            };

            return View(homeViewModel);
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddResult(NewHeatResult heat)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var newHeat = new Heat
            {
                Date = DateTime.Now,
            };

            _mkContext.Heats.Add(newHeat);
            _mkContext.SaveChanges();

            var scores = heat.Scores
            .Where(x => x.Player.HasValue)
            .Select(x => new HeatScore
            {
                PlayerId = _mkContext.Players.Single(p => p.Id == x.Player).Id,
                Score = x.Points.Value,
                HeatId = newHeat.Id
            })
            .ToList();

            _mkContext.HeatScores.AddRange(scores);
            _mkContext.SaveChanges();

            return RedirectToAction("Index");
        }

        private HeatModel Convert(Heat heat)
        {
            return new HeatModel()
            {
                Date = heat.Date.ToString(),
                Scores = heat.Scores
                .Select(x => new HeatModel.HeatScoreModel
                {
                    PlayerName = _mkContext.Players.Single(p => p.Id == x.PlayerId).Name,
                    Score = x.Score,
                })
                .OrderByDescending(x => x.Score)
                .ToList()
            };
        }
    }

    public class NewHeatResult : IValidatableObject
    {
        public IList<NewHeatScore> Scores { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Scores == null)
            {
                yield break;
            }

            var scoresWithPlayers = Scores
                .Where(x => x.Player.HasValue)
                .ToList();
            if (scoresWithPlayers.Count == 0)
            {
                yield return new ValidationResult("A heat must contain at least one player");
            }

            if (scoresWithPlayers.Count > 4)
            {
                yield return new ValidationResult("A heat can't contain more than four players");
            }

            var players = scoresWithPlayers
                .Select(x => x.Player)
                .ToList();

            var numberOfUniquePlayers = players.Distinct().Count();
            if (numberOfUniquePlayers != players.Count)
            {
                yield return new ValidationResult("A player can only score once in a heat");
            }
        }
    }

    public class NewHeatScore : IValidatableObject
    {
        public int? Player { get; set; }

        [Range(0, 60)]
        public int? Points { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Player.HasValue && !Points.HasValue)
            {
                yield return new ValidationResult("Points must be provided");
            }
        }
    }

    public class HomeViewModel
    {
        public IEnumerable<HeatModel> Heats { get; set; }
        public IEnumerable<PlayerModel> Players { get; set; }
    }

    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class HeatModel
    {
        public string Date { get; set; }
        public List<HeatScoreModel> Scores { get; set; }
        public class HeatScoreModel
        {
            public string PlayerName { get; set; }
            public int Score { get; set; }
        }
    }
}
