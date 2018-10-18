using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public interface IEnquireRepository
    {
        void AddEnquire(Enquire enquire);
    }
}
