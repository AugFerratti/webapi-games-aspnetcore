using System.Collections.Generic;

namespace WebApiGames_Demo.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public ICollection<GameDTO> Games { get; set; }
    }
}
