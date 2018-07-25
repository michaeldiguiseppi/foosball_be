using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoosballApi.Models
{
    public class GameItem
    {
        public int Id { get; set; }

        [Required]
        public int Player1Id { get; set; }

        [Required]
        [Range(0, 10)]
        public int Player1Score { get; set; }

        [Required]
        public int Player2Id { get; set; }

        [Required]
        [Range(0, 10)]
        public int Player2Score { get; set; }
    }
}
