using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class EnquireRepository : IEnquireRepository
    {
        private readonly AppDbContext _appDbContext;

        public EnquireRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddEnquire(Enquire enquire)
        {
            _appDbContext.Enquires.Add(enquire);
            _appDbContext.SaveChanges();
        }
    }
}
