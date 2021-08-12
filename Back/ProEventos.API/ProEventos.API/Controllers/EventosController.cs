using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.DTOS;
using ProEventos.Application.Interface;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;
        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);

                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await _eventoService.GetAllEventosByTemaAsync(tema, true);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao recuperar evento. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEvento(EventoDto model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, EventoDto model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);

                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento(int id)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) ?  Ok("Evento Deletado") : BadRequest("Erro ao deletar evento");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao remover evento. Erro: {ex.Message}");
            }
        }
    }
}
