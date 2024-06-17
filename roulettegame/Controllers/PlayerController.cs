using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roulettegame.Data;
using roulettegame.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace roulettegame.Controllers
{

    

    public class UserDT
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public decimal Gain { get; set; }
    }

    public class NameDT
    {

        public string Name { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {

        private readonly RouletteGameContext _context;

        public PlayerController(RouletteGameContext context)
        {
            _context = context;
        }
       
        [HttpPost, Route("SaveAgain")]
        public async Task<ActionResult> SaveAgain([FromBody] UserDT infoPlayer)
        {
            if (infoPlayer == null) return BadRequest("Todos los valores son necesarios");
            if (_context.Players == null) throw new Exception("Error en la base de datos");

            var player = await _context.Players.Where(ply => ply.Name.Equals(infoPlayer.Name)).FirstOrDefaultAsync();

            if (player == null)
            {
                var newPlayer = new PlayerModel
                {
                    Name = infoPlayer.Name,
                };
                _context.Players.Add(newPlayer);
                await _context.SaveChangesAsync();

                player = await _context.Players.Where(ply => ply.Name.Equals(infoPlayer.Name)).FirstOrDefaultAsync();
                if (player == null) throw new Exception("Error al obtener el jugador");

                player.Amount = infoPlayer.Amount;

            }else
            {
                player.Amount += infoPlayer.Gain;
            }
                
            try
            {
                await _context.SaveChangesAsync();
                return Ok(player);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Error al actualizar los valoes en la base de datos: {ex.Message}");
            }
        }

        [HttpPost, Route("GetPlayer")]
        public async Task<ActionResult> GetPlayer(NameDT namePlayer)
        {
            if (namePlayer == null) return BadRequest("Todos los valores son necesarios");
            if (_context.Players == null) throw new Exception("Error en la base de datos");

            var player = await _context.Players.Where(ply => ply.Name.Equals(namePlayer.Name)).FirstOrDefaultAsync();

            if (player == null)
            {
                var newPlayer = new PlayerModel
                {
                    Name = namePlayer.Name,
                    Amount = 100
                };
                _context.Players.Add(newPlayer);
                await _context.SaveChangesAsync();

                player = await _context.Players.Where(ply => ply.Name.Equals(namePlayer.Name)).FirstOrDefaultAsync();
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(player);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Error al actualizar los valoes en la base de datos: {ex.Message}");
            }
        }

    }
}
