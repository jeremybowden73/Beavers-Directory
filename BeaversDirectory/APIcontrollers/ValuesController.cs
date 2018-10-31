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
        public ActionResult<List<Beaver>> GetAll()
        {
            var beavers = _beaversRepository.AllBeavers().OrderBy(b => b.Id).ToList();
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
