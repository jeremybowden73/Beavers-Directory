using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeaversDirectory.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeaversDirectory.APIcontrollers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IBeaversRepository _beaversRepository;

        // constructor injection
        public ValuesController(IBeaversRepository beaversRepository)
        {
            _beaversRepository = beaversRepository;
        }



        // define routes

        // GET: api/values/
        // returns the complete List of Beaver objects in the db, but in simplified form (sensitive data omitted)
        //[HttpGet]
        //public ActionResult<List<BeaverLiteAPI>> GetAll()
        //{
        //    var allBeavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id).ToList();

        //    if (allBeavers == null)
        //    {
        //        return NotFound();
        //    }

        //    // BeaverAPI is a simplified version of Beaver that is suitable for serving from
        //    // the API (i.e. does not contain Toen or DOB information)
        //    List<BeaverLiteAPI> beavers = new List<BeaverLiteAPI>();

        //    // for each Beaver in allBeavers, create a BeaverAPI object (by populating
        //    // it with data from the Beaver object) then add it to the List "beavers" 
        //    for (int i = 0; i < allBeavers.Count; i++)
        //    {
        //        BeaverLiteAPI nextBeaverToAdd = new BeaverLiteAPI
        //        {
        //            FirstName = allBeavers[i].FirstName,
        //            LastName = allBeavers[i].LastName,
        //            Lodge = allBeavers[i].Lodge,
        //            IsLodgeLeader = allBeavers[i].IsLodgeLeader
        //        };

        //        beavers.Add(nextBeaverToAdd);
        //    }

        //    return beavers;
        //}


        // GET: api/values/?querystring-options
        [HttpGet]
        public ActionResult<BeaverMoreAPI> Get(string FirstName, string LastName)
        {
            var allBeavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id).ToList();

            // create a list of all Beaver objects that match the "FirstName"
            // and "LastName" values in the supplied querystring
            List<Beaver> lastnameMatches = allBeavers.FindAll(x => (x.FirstName == FirstName)
                    && (x.LastName == LastName));

            if (lastnameMatches == null)
            {
                return NotFound();
            }

            if (lastnameMatches.Count() > 1)
            {
                return BadRequest();
            }

            Beaver foundBeaver = lastnameMatches[0];

            BeaverMoreAPI beaver = new BeaverMoreAPI
            {
                FirstName = foundBeaver.FirstName,
                LastName = foundBeaver.LastName,
                Town = foundBeaver.Town,
                Dob = foundBeaver.Dob,
                Lodge = foundBeaver.Lodge,
                IsLodgeLeader = foundBeaver.IsLodgeLeader
            };

            return beaver;
        }
    }
}
