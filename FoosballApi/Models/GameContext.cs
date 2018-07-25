using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoosballApi.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base (options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<GameItem> Games { get; set; }
    }
}
