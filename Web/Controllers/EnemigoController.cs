using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnemigoController : ControllerBase
    {
       
        private IEnemigoService _servicio;
        public EnemigoController(IEnemigoService enemigoService) 
        {
            _servicio = enemigoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enemigo>>> Get()
        {
            var Enemigos = await _servicio.GetAll();

            return Ok(Enemigos);
        }

        [HttpPost]
        public async Task<ActionResult<Enemigo>> Post([FromBody] Enemigo enemigo)
        {
            try
            {
                var createdEnemigo =
                    await _servicio.Create(enemigo);

                return Ok(createdEnemigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Enemigo>>> Delete(int id)
        {
            try
            {
                await _servicio.Delete(id);
                return Ok("Enemigo eliminado");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Enemigo>> Update(int id, [FromBody] Enemigo enemigo)
        {
            try
            {
                await _servicio.Update(id, enemigo);
                return Ok("Enemigo Actualizado!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}