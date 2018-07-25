using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoosballApi.Models;

namespace FoosballApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly GameContext _context;

        public PlayersController(GameContext context)
        {
            _context = context;

            if (_context.Players.Count() == 0)
            {
                _context.Players.AddRange(
                        new Player
                        {
                            FirstName = "Mike",
                            LastName = "DiGuiseppi"
                        },
                        new Player
                        {
                            FirstName = "Tasia",
                            LastName = "DiGuiseppi"
                        }
                    );
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<Player>> GetPlayers()
        {
            return _context.Players.ToList();
        }

        [HttpGet("{id}", Name = "GetPlayer")]
        public ActionResult<Player> GetPlayerById(int id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        [HttpPost]
        public IActionResult CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();

            return CreatedAtRoute("GetPlayer", new { id = player.Id });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlayer(int id, Player player)
        {
            var foundPlayer = _context.Players.Find(id);
            if (foundPlayer == null)
            {
                return NotFound();
            }

            foundPlayer.FirstName = player.FirstName;
            foundPlayer.LastName = player.LastName;

            _context.Players.Update(foundPlayer);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            var foundPlayer = _context.Players.Find(id);
            if (foundPlayer == null)
            {
                return NotFound();
            }

            _context.Players.Remove(foundPlayer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}