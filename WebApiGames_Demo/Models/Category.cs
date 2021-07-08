using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiGames_Demo.Models
{
    [Table("Categories")]
    public class Category
    {
        public Category()
        {
            Games = new Collection<Game>();
        }
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string ImageURL { get; set; }
        public ICollection<Game> Games { get; set; }


    }
}
