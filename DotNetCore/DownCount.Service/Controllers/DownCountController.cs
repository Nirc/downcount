using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DownCount.Service.Models;
using DownCount.Engine;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DownCount.Service.Controllers
{
    [Route("api/[controller]")]
    public class DownCountController : Controller
    {
        public DownCountController()
        {
        }

        /// <summary>
        /// Example json input
        /// {"TargetNumber":11, "Numbers": [1,2,3,4]}
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] DownCountGame game)
        {
            if (game == null)
            {
                return BadRequest();
            }

            Entities.DownCountGame downCountGame = new Entities.DownCountGame(game.TargetNumber, game.Numbers.ToArray());
            var solutions = DownCountSolver.Solve(downCountGame, false);
            DownCountGameResult gameResult = new DownCountGameResult();

            if (solutions.Count == 0)
            {
                gameResult.ExactResult = false;
                gameResult.Result = string.Empty;
            }
            else
            {
                var solution = solutions[0];
                gameResult.ResultValue = solution.Value;
                gameResult.Result = solution.ToString();
                if (solution.Value == downCountGame.TargetNumber)
                {
                    gameResult.ExactResult = true;
                }
                else
                {
                    gameResult.ExactResult = false;
                }
            }

            return new ObjectResult(gameResult);
        }
    }
}
