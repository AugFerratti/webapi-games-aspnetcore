using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiGames_Demo.Migrations
{
    public partial class PopulateDb : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categories(Name,ImageURL) VALUES('FPS','https://logopond.com/logos/08ad4f45777f9f7210ad2a340a8f8bd6.png')");
            mb.Sql("INSERT INTO Categories(Name,ImageURL) VALUES('RPG','https://ovelhinhodorpg.files.wordpress.com/2015/06/rpg-logo.png')");
            mb.Sql("INSERT INTO Categories(Name,ImageURL) VALUES('Horror','https://www.logolynx.com/images/logolynx/f5/f55544e39b8921cc1a5db8c5ca35f10e.jpeg')");

            mb.Sql("INSERT INTO Games(Name, Description, Score, ImageURL, RegisterDate, CategoryId)" +
                "VALUES ('Battlefield 4','Battlefield 4 is a first-person shooter video game developed by video game developer EA DICE and published by Electronic Arts.', 81," +
                "'https://image.api.playstation.com/cdn/UP0006/CUSA00110_00/VaulrBDwbGorU7Ykfjg5sNrJ5X9resKm.png'," +
                "now(), (SELECT CategoryId FROM Categories WHERE Name = 'FPS'))");
            mb.Sql("INSERT INTO Games(Name, Description, Score, ImageURL, RegisterDate, CategoryId)" +
                "VALUES ('Skyrim','The Elder Scrolls V: Skyrim is an open world action role-playing video game developed by Bethesda Game Studios and published by Bethesda Softworks.', 94," +
                "'https://upload.wikimedia.org/wikipedia/en/thumb/1/15/The_Elder_Scrolls_V_Skyrim_cover.png/220px-The_Elder_Scrolls_V_Skyrim_cover.png'," +
                "now(), (SELECT CategoryId FROM Categories WHERE Name = 'RPG'))");
            mb.Sql("INSERT INTO Games(Name, Description, Score, ImageURL, RegisterDate, CategoryId)" +
                "VALUES ('Resident Evil HD Remaster','Resident Evil is a survival horror video game developed by Capcom Production Studio 4 and published by Capcom.', 82," +
                "'https://steamcdn-a.akamaihd.net/steam/apps/304240/header.jpg?t=1554399707'," +
                "now(), (SELECT CategoryId FROM Categories WHERE Name = 'Horror'))");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");
            mb.Sql("Delete from Games");
        }
    }
}
