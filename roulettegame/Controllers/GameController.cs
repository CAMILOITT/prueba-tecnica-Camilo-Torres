using Microsoft.AspNetCore.Mvc;
using roulettegame.Models;
using roulettegame.Data;
using roulettegame.utils;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.AspNetCore.Cors;

namespace roulettegame.Controllers
{


    public class ValueBetDT
    {
        public ETypeBet TypeBet { get; set; }
        public string Value { get; set; }
        public int amount { get; set; }
    }

    public class BetDT: IBetClient
    {
        public List<ValueBetDT> Bet { get; set; }
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
        public async Task<ActionResult> Game([FromBody] BetDT valueGame)
        {

            if (valueGame == null) return BadRequest("Todos los valores son necesarios");

            if (_context.Players == null) throw new Exception("Erro en la base de datos");

            var result = new GenerateResultRoulette(valueGame);

            try
            {
                return Ok(new
                {
                    gain = result.GetResult().Gain,
                    isWinner = result.GetResult().IsWin,
                    color = result.Color,
                    number = result.Number
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Error al actualizar los valoes en la base de datos: {ex.Message}");
            }
        }

    }
}
