using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Repository.Interface;
using ProEventos.Repository.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Repository
{
    public class EventoRepository : IEventoRepository
    {
        private readonly ProEventosDbContext _dbContext;
        public EventoRepository(ProEventosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _dbContext.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();

        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _dbContext.Eventos
               .Include(e => e.Lotes)
               .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                         .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _dbContext.Eventos
               .Include(e => e.Lotes)
               .Include(e => e.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestranteEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
               .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
