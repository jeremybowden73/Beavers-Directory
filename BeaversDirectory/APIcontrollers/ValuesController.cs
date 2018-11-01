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

                foreach (Beaver element in allBeavers)
                {
                    BeaverLiteAPI nextBeaverToAdd = new BeaverLiteAPI
                    {
                        FirstName = element.FirstName,
                        LastName = element.LastName,
                        Town = element.Town,
                        Lodge = element.Lodge,
                        IsLodgeLeader = element.IsLodgeLeader
                    };

                    beavers.Add(nextBeaverToAdd);
                }

                return beavers;
            }


        // GET: api/values/names
        [HttpGet("names")]
        public ActionResult<List<BeaverName>> Names()
        {
            var allBeavers = _beaversRepository.AllBeavers().OrderBy(b => b.LastName).ToList();

            List<BeaverName> beaverNames = new List<BeaverName>();

            // for each object in allBeavers, add a new BeaverName object to beaverNames,
            // which contains the Id and the concatanated FirstName and LastName properties
            foreach (Beaver element in allBeavers)
            {
                BeaverName nextBeaverName = new BeaverName
                {
                    Name = element.LastName + " " + element.FirstName
                };

                beaverNames.Add(nextBeaverName);
            }




            return beaverNames;
        }
    }
}
