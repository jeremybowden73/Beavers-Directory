using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class EnquireRepository : IEnquireRepository
    {
        private readonly BeaversDbContext _beaversDbContext;

        public EnquireRepository(BeaversDbContext beaversDbContext)
        {
            _beaversDbContext = beaversDbContext;
        }

        public void AddEnquire(Enquire enquire)
        {
            _beaversDbContext.Enquires.Add(enquire);
            _beaversDbContext.SaveChanges();
        }
    }
}
