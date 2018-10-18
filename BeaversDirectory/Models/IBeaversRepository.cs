using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public interface IBeaversRepository
    {
        IEnumerable<Beaver> AllBeavers();

        // method to retreive Beaver objects by beaverId
        Beaver GetBeaverById(int beaverId);

        // method to update a Beaver entity
        void UpdateBeaver(Beaver beaver);


    }
}
