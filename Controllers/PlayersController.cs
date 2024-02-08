using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechChallengue.Context;
using TechChallengue.Models;


namespace TechChallengue.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public Player p1;
        public Player p2;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;

        }


        public IActionResult Winner()
        {

            var ultimosDosRegistros = _context.Player
                    .OrderByDescending(player => player.Id)
                    .Take(2)
                    .ToList();

            var firstPlayer = ultimosDosRegistros[1];
            var secondPlayer = ultimosDosRegistros[0];

            string winner = GetPlayerScore(firstPlayer.Id) > GetPlayerScore(secondPlayer.Id) ? firstPlayer.Name : secondPlayer.Name;

            ViewData["Winner"] = winner;


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePlayer([Bind("player1, player2")] Players players)
        {
            if (ModelState.IsValid)
            {
                _context.Add(players.player1);
                _context.Add(players.player2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(FirstMove));
            }
            return View(players);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateSecond([Bind("Id,Name")] Player player)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(player);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(FirstMove));
        //    }
        //    return View(player);
        //}
        ////First Player move






        // GET: Players/FirstMove
        public IActionResult FirstMove()
        {
            var ultimosDosRegistros = _context.Player
            .OrderByDescending(player => player.Id)
            .Take(2)
            .ToList();

            Player firstPlayer = ultimosDosRegistros[1];

            if (firstPlayer != null)
            {
                ViewData["FirstPlayerName"] = firstPlayer.Name;
                ViewData["FirstPlayerId"] = firstPlayer.Id;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FirstMove(string moveType)
        {
            var ultimosDosRegistros = _context.Player
            .OrderByDescending(player => player.Id)
            .Take(2)
            .ToList();

            Player firstPlayer = ultimosDosRegistros[1];

            if (firstPlayer != null)
            {
                var move = new Move
                {
                    PlayerId = firstPlayer.Id,
                    MoveType = moveType,
                };

                _context.Add(move);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("SecondMove");
        }

        public IActionResult SecondMove()
        {
            var ultimosDosRegistros = _context.Player
                .OrderByDescending(player => player.Id)
                .Take(2)
                .ToList();

            var secondPlayer = ultimosDosRegistros[0];

            if (secondPlayer != null)
            {
                ViewData["SecondPlayerName"] = secondPlayer.Name;
                ViewData["SecondPlayerId"] = secondPlayer.Id; // Añadir el Id del segundo jugador
            }
            else
            {
                return RedirectToAction("CreateSecond");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SecondMove(string moveType)
        {
            var ultimosDosRegistros = _context.Player
                 .OrderByDescending(player => player.Id)
                 .Take(2)
                 .ToList();

            var secondPlayer = ultimosDosRegistros[0];

            if (secondPlayer != null)
            {
                var move = new Move
                {
                    PlayerId = secondPlayer.Id,
                    MoveType = moveType,
                };

                _context.Add(move);
                await _context.SaveChangesAsync();
            }

            // Redirigir directamente a la página "Result"
            return RedirectToAction("Result");
        }

        

        public IActionResult Result()
        {
            var ultimosDosRegistros = _context.Player
                    .OrderByDescending(player => player.Id)
                    .Take(2)
                    .ToList();

            var firstPlayer = ultimosDosRegistros[1];
            var secondPlayer = ultimosDosRegistros[0];

            if (firstPlayer != null && secondPlayer != null)
            {
                var ultimosDosmovimientos = _context.Moves
                    .OrderByDescending(moves => moves.Id)
                    .Take(2)
                    .ToList();

                var firstPlayerMove = ultimosDosmovimientos[1];
                var secondPlayerMove = ultimosDosmovimientos[0];

                // Lógica para determinar el ganador
                var result = DetermineWinner(firstPlayerMove?.MoveType, secondPlayerMove?.MoveType);

                // Verificar si hay un empate
                if (result == "Empate")
                {
                    ViewData["Winner"] = "Empate. Ningún jugador gana puntos.";
                    ViewData["FirstPlayerScore"] = GetPlayerScore(firstPlayer.Id);
                    ViewData["SecondPlayerScore"] = GetPlayerScore(secondPlayer.Id);
                }
                else
                {
                    UpdateScores(result, firstPlayer.Id, secondPlayer.Id);

                    ViewData["ResultMessage"] = result;
                    ViewData["FirstPlayerScore"] = GetPlayerScore(firstPlayer.Id);
                    ViewData["SecondPlayerScore"] = GetPlayerScore(secondPlayer.Id);
                }

                ViewData["LastMoveFP"] = firstPlayerMove.MoveType;
                ViewData["LastMoveSP"] = secondPlayerMove.MoveType;
                ViewData["FirstPlayerName"] = firstPlayer.Name;
                ViewData["SecondPlayerName"] = secondPlayer.Name;

                
                Console.WriteLine($"Result: {result}");
                Console.WriteLine($"FirstPlayer Score: {GetPlayerScore(firstPlayer.Id)}");
                Console.WriteLine($"SecondPlayer Score: {GetPlayerScore(secondPlayer.Id)}");
                string winner = GetPlayerScore(firstPlayer.Id) > GetPlayerScore(secondPlayer.Id) ? firstPlayer.Name : secondPlayer.Name;
                ViewData["Winner"] = winner;
                if (GetPlayerScore(firstPlayer.Id) >= 3 || GetPlayerScore(secondPlayer.Id) >=3)
                {
                    return RedirectToAction("Winner", "Players");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        private string DetermineWinner(string moveType1, string moveType2)
        {
            if (moveType1 == moveType2)
            {
                return "Empate";
            }

            if ((moveType1 == "Rock" && moveType2 == "Scissors") ||
                (moveType1 == "Paper" && moveType2 == "Rock") ||
                (moveType1 == "Scissors" && moveType2 == "Paper"))
            {
                return "FirstPlayer";
            }

            return "SecondPlayer";
        }

        // actualiza el score
        private void UpdateScores(string result, int playerId1, int playerId2)
        {
            if (result == "FirstPlayer")
            {
                UpdateScoreHistory(playerId1);
            }
            else if (result == "SecondPlayer")
            {
                UpdateScoreHistory(playerId2);
            }

            _context.SaveChanges(); // Guarda los cambios en la base de datos
        }


        // Incrementa  el score
        private void UpdateScoreHistory(int playerId)
        {
            var scoreHistory = _context.ScoreHistories.FirstOrDefault(s => s.PlayerId == playerId);

            if (scoreHistory != null)
            {
                scoreHistory.Victories++;
            }
            else
            {
                _context.ScoreHistories.Add(new ScoreHistory { PlayerId = playerId, Victories = 1 });
            }
        }


        private int GetPlayerScore(int playerId)
        {
            // Obtén el puntaje del jugador desde la base de datos.
            var scoreHistory = _context.ScoreHistories.FirstOrDefault(s => s.PlayerId == playerId);
            return scoreHistory?.Victories ?? 0;
        }

       
    }
}
