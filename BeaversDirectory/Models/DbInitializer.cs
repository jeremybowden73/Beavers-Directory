using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.Models
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // if the AppDbContext that was passed to it is empty, initialize it with this data
            if (!context.Beavers.Any())
            {
                context.AddRange
                (
                    new Beaver { FirstName = "Adam", LastName = "Axelrod", Town = "Houghton", Dob = "01 Jan 2012", Lodge = "Blue", IsLodgeLeader = true, ParentEmail = "", ParentPassword = "" },
                    new Beaver { FirstName = "Bert", LastName = "Beaverboy", Town = "Billesdon", Dob = "02 Feb 2012", Lodge = "Red", IsLodgeLeader = false, ParentEmail = "", ParentPassword = "" },
                    new Beaver { FirstName = "Colin", LastName = "Cumberbatch", Town = "Houghton", Dob = "03 Mar 2012", Lodge = "Blue", IsLodgeLeader = false, ParentEmail = "", ParentPassword = "" },
                    new Beaver { FirstName = "Dave", LastName = "Diggler", Town = "Billesdon", Dob = "04 Apr 2012", Lodge = "Yellow", IsLodgeLeader = true, ParentEmail = "", ParentPassword = "" }
                );
            }

            // submit changes to the underlying database
            context.SaveChanges();
        }
    }
}
