using System.ComponentModel.DataAnnotations;

namespace roulettegame.Models
{
    public class PlayerModel: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Amount { get; set; } = 10.00m;
    }
}
