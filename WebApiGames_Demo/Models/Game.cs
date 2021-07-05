using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApiGames_Demo.Validations;

namespace WebApiGames_Demo.Models
{
    [Table("Games")]
    public class Game : IValidatableObject
    {
        [Key]
        public int GameId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name must have between 1 and 255 characters", MinimumLength = 1)]
        //[FirstLetterCapital]
        public string Name { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Description must have a maximum of {1} characters")]
        public string Description { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "The score must be between {1} and {2}")]
        public decimal Score { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string ImageURL { get; set; }
        public DateTime RegisterDate { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                var firstLetter = this.Name[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new
                        ValidationResult("The first letter of the game must be capital",
                        new[]
                        { nameof(this.Name) }
                        );
                }
            }
            if (this.Score <= 0 || this.Score > 10)
            {
                yield return new
                  ValidationResult("The score must be between 1 and 10",
                  new[]
                  { nameof(this.Score) }
                  );
            }
        }
    }
}
