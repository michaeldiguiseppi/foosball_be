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
    public class GamesController : ControllerBase
    {
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;

            if (_context.Games.Count() == 0)
            {
                _context.Games.Add(new GameItem { Player1Id = 1, Player2Id = 2, Player1Score = 9, Player2Score = 0 });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public ActionResult<List<GameItem>> GetGames()
        {
            return _context.Games.ToList();
        }

        [HttpGet("{id}", Name = "GetGame")]
        public ActionResult<GameItem> GetGameById(int id)
        {
            var game = _context.Games.Find(id);
            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        [HttpPost]
        public IActionResult CreateGame(GameItem game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGame(int id, GameItem game)
        {
            var foundGame = _context.Games.Find(id);
            if (foundGame == null)
            {
                return NotFound();
            }

            foundGame.Player1Id = game.Player1Id;
            foundGame.Player1Score = game.Player1Score;
            foundGame.Player2Id = game.Player2Id;
            foundGame.Player2Score = game.Player2Score;

            _context.Games.Update(foundGame);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGame(int id)
        {
            var foundGame = _context.Games.Find(id);
            if (foundGame == null)
            {
                return NotFound();
            }

            _context.Games.Remove(foundGame);
            _context.SaveChanges();

            return NoContent();
        }
    }
}