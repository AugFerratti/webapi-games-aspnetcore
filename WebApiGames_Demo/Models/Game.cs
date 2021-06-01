using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGames_Demo.Models
{
    [Table("Games")]
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Score { get; set; }
        [Required]
        [MaxLength(500)]
        public string ImageURL { get; set; }
        public DateTime RegisterDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
