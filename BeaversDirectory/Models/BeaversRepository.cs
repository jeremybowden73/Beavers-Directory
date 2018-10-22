using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class BeaversRepository : IBeaversRepository
    {
        private readonly AppDbContext _appDbContext;

        // constructor injection
        public BeaversRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Beaver> AllBeavers()
        {
            // return the Pies collection from the context
            return _appDbContext.Beavers;
        }

        public Beaver GetBeaverById(int beaverId)
        {
            // return the first object from the collection Beavers which has the Id that is passed
            // in to the method, or if there isn't one, return a 'default'
            return _appDbContext.Beavers.FirstOrDefault(p => p.Id == beaverId);
        }

        public void UpdateBeaver(Beaver beaver)
        {
            // update the Beaver model in the db that has been passed to this method
            _appDbContext.Beavers.Update(beaver);
            _appDbContext.SaveChanges();
        }
    }
}
