using System.ComponentModel.DataAnnotations;

namespace TechChallengue.Models
{
    public class Player
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo del jugador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre completo no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

    }

    public class Players
    {
        public Player player1 { get; set; }
        public Player player2 { get; set; }
    }
}
