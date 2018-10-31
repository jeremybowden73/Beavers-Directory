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

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<BeaverLiteAPI>> GetAll()
        {
            var allBeavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id).ToList();

            // BeaverAPI is a simplified version of Beaver that is suitable for serving from
            // the API (i.e. does not contain private information)
            List<BeaverLiteAPI> beavers = new List<BeaverLiteAPI>();

            // for each Beaver in allBeavers, create a BeaverAPI object (by populating
            // it with data from the Beaver object) then add it to the List "beavers" 
            for (int i = 0; i < allBeavers.Count; i++)
            {
                BeaverLiteAPI nextBeaverToAdd = new BeaverLiteAPI
                {
                    FirstName = allBeavers[i].FirstName,
                    LastName = allBeavers[i].LastName,
                    Lodge = allBeavers[i].Lodge,
                    IsLodgeLeader = allBeavers[i].IsLodgeLeader
                };

                beavers.Add(nextBeaverToAdd);
            }

            return beavers;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<Beaver> Get(int id)
        {
            var beaver = _beaversRepository.GetBeaverById(id);
            if (beaver == null)
                return NotFound();

            return beaver;
        }


    }
}
