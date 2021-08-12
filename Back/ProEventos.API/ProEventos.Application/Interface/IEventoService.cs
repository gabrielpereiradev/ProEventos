using ProEventos.Application.DTOS;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);

        Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes);
        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes);
        Task<EventoDto> GetEventoByIdAsync(int eventoId, bool includePalestrantes);

    }
}
