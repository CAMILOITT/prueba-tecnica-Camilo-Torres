using Microsoft.AspNetCore.Mvc;
using roulettegame.Models;
using roulettegame.Data;
using roulettegame.utils;
using Microsoft.EntityFrameworkCore;

namespace roulettegame.Controllers
{
    public interface IValueGame : IBetClient
    {
        string Name { get; }
    }

    public class ValueGameDT : IValueGame
    {
        public string Name { get; set; }
        public ETypeBet TypeBet { get; set; }
        public int Number { get; set; }
        public string Color { get; set; }
        public decimal Bet { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly RouletteGameContext _context;

        public GameController(RouletteGameContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Game([FromBody] ValueGameDT valueGame)
        {

            if (valueGame == null) return BadRequest("Todos los valores son necesarios");
            if(valueGame.Bet <= 0 ) return BadRequest("No puedes apostar un valor negativo o cero");
            if(valueGame.Color != "red" && valueGame.Color != "black" ) return BadRequest("El color debe ser rojo o negro");
            if(valueGame.Number < 0 || valueGame.Number > 36 ) return BadRequest("El numero debe estar entre 0 y 36");

            if (_context.Players == null) throw new Exception("Erro en la base de datos");

            var player = await _context.Players.Where(ply => ply.Name.Equals(valueGame.Name)).FirstOrDefaultAsync();

            if (player == null)
            {
                var newPlayer = new PlayerModel
                {
                    Name = valueGame.Name,
                };
                _context.Players.Add(newPlayer);
                await _context.SaveChangesAsync();

                player = await _context.Players.Where(ply => ply.Name.Equals(valueGame.Name)).FirstOrDefaultAsync();
            }

            if(valueGame.Bet > player.Amount) return BadRequest("No tienes fondos suficientes");

            var result = new GenerateResultRoulette(valueGame);

            if (player == null) throw new Exception("Error al obtener el jugador");

            player.Amount += result.GetResult().Gain;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    player = new 
                    {
                        name = player.Name,
                        amount = player.Amount
                    },
                    bet = new
                    {
                        amount = valueGame.Bet,
                        type = valueGame.TypeBet,
                        number = valueGame.Number,
                        color = valueGame.Color
                    },
                    resultRoulette = new
                    {
                        gain = result.GetResult().Gain,
                        isWinner = result.GetResult().IsWin,
                        color = result.Color,
                        number = result.Number
                    }
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Error al actualizar los valoes en la base de datos: {ex.Message}");
            }
        }

        [Route("api/[controller]")]
        [ApiController]
        public async Task<ActionResult> saveGain()
        {

            if (_context.Players == null) throw new Exception("Erro en la base de datos");
            var players = await _context.Players.ToListAsync();
            foreach (var player in players)
            {
                player.Amount += player.Gain;
                player.Gain = 0;
            }
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Error al actualizar los valoes en la base de datos: {ex.Message}");
            }
        }
    }
}
