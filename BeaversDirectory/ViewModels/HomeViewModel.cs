using BeaversDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeaversDirectory.ViewModels
{
    public class HomeViewModel
    {
        // generate a list of Beavers
        public List<Beaver> Beavers { get; set; }

        // generate a Title
        public string Title { get; set; }

        // property for which user is logged in
        public string User { get; set; }
    }
}
