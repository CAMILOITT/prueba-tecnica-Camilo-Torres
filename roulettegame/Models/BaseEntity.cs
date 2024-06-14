using System.ComponentModel.DataAnnotations;

namespace roulettegame.Models
{
    public class BaseEntity
    {
        [Required, Key ]
        public int Id { get; set; }
        public DateTime created_at { get; set; }  = DateTime.Now;
        public DateTime updated_at { get; set; }  = DateTime.Now;

    }
}
