using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Repository.Interface;
using ProEventos.Repository.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Repository
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly ProEventosDbContext _dbContext;
        public PalestranteRepository(ProEventosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _dbContext.Palestrantes
                 .Include(p => p.RedeSociais);
            
            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _dbContext.Palestrantes
                .Include(p => p.RedeSociais);

            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(e => e.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _dbContext.Palestrantes
                .Include(p => p.RedeSociais);

            if(includeEventos)
            {
                query = query
                    .Include(p => p.PalestranteEventos)
                    .ThenInclude(pe => pe.Evento);
            }
            query = query.OrderBy(e => e.Id)
                .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

    }
}
