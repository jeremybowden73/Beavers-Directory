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

        //
        // define routes

        // GET: api/values/?querystring-options
        [HttpGet]
        public ActionResult<List<BeaverLiteAPI>> Get(
            [FromQuery]string FirstName,
            [FromQuery]string LastName,
            [FromQuery]string Town,
            [FromQuery]string Lodge,
            [FromQuery]string Leader)
        {
            var allBeavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id).ToList();

            if (FirstName != null)
            {
                allBeavers = allBeavers.FindAll(x => x.FirstName == FirstName);
            }
          
            if (LastName != null)
            {
                allBeavers = allBeavers.FindAll(x => x.LastName == LastName);
            }

            if (Town != null)
            {
                allBeavers = allBeavers.FindAll(x => x.Town == Town);
            }

            if (Lodge != null)
            {
                allBeavers = allBeavers.FindAll(x => x.Lodge == Lodge);
            }

            if (Leader != null && Leader.ToLower() == "true")
            {
                allBeavers = allBeavers.FindAll(x => x.IsLodgeLeader == true);
            }

            // return a status code 404 if there are no objects to return to the API consumer
            if (allBeavers.Count == 0)
            {
                return NotFound();
            }

            // BeaverLiteAPI is a simplified version of Beaver that is suitable for serving to
            // the API consumer (it does not contain DOB or Parent information)
            List<BeaverLiteAPI> beavers = new List<BeaverLiteAPI>();

            // for each Beaver in allBeavers, create a BeaverLiteAPI object (by populating
            // it with data from the Beaver object) then add it to the List "beavers" 
            for (int i = 0; i < allBeavers.Count; i++)
            {
                BeaverLiteAPI nextBeaverToAdd = new BeaverLiteAPI
                {
                    FirstName = allBeavers[i].FirstName,
                    LastName = allBeavers[i].LastName,
                    Town = allBeavers[i].Town,
                    Lodge = allBeavers[i].Lodge,
                    IsLodgeLeader = allBeavers[i].IsLodgeLeader
                };

                beavers.Add(nextBeaverToAdd);
            }

            return beavers;
        }
    }
}
