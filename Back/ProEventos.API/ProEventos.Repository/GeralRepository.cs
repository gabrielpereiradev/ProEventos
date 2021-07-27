using ProEventos.Repository.Interface;
using ProEventos.Repository.Persistence;
using System.Threading.Tasks;

namespace ProEventos.Repository
{
    public class GeralRepository : IGeralRepository
    {
        private readonly ProEventosDbContext _dbContext;
        public GeralRepository(ProEventosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            _dbContext.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _dbContext.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public void DeleteRange<T>(T entityArray) where T : class
        {
            _dbContext.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync()) > 0;
        }


        
    }
}
