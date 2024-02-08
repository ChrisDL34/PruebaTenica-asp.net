namespace TechChallengue.Models
{
    public class Move
    {

        public int Id { get; set; }
        public int PlayerId { get; set; } // Referencia al jugador que hizo el movimiento
        public string MoveType { get; set; } // Puede ser "Rock", "Paper", "Scissors", etc.
        
    }
}
