namespace WebApiGames_Demo.DTOs
{
    public class GameDTO
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Score { get; set; }
        public string ImageURL { get; set; }
        public int CategoryId { get; set; }
    }
}
