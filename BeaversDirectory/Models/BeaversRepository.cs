using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class BeaversRepository : IBeaversRepository
    {
        private readonly BeaversDbContext _beaversDbContext;

        // constructor injection
        public BeaversRepository(BeaversDbContext beaversDbContext)
        {
            _beaversDbContext = beaversDbContext;
        }

        public IEnumerable<Beaver> AllBeavers()
        {
            // return the Pies collection from the context
            return _beaversDbContext.Beavers;
        }

        public Beaver GetBeaverById(int beaverId)
        {
            // return the first object from the collection Beavers which has the Id that is passed
            // in to the method, or if there isn't one, return a 'default'
            return _beaversDbContext.Beavers.FirstOrDefault(p => p.Id == beaverId);
        }

        public void UpdateBeaver(Beaver beaver)
        {
            // update the Beaver model in the db that has been passed to this method
            _beaversDbContext.Beavers.Update(beaver);
            _beaversDbContext.SaveChanges();
        }
    }
}
