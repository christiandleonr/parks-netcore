using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parks.Repositories.IRepositories;
using Parks.Models;
using MongoDB.Driver;
using Parks.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using Parks.Repositories;

namespace Parks.Controllers
{
    [Route("api/parks")]
    [ApiController]

    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepository = nationalParkRepository;
        }

        [HttpGet]
        public ActionResult<List<NationalPark>> Get() => _nationalParkRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetNationalPark")]
        public ActionResult<NationalPark> Get(string id)
        {
            var nationalPark = _nationalParkRepository.Get(id);

            if (nationalPark == null) 
            {
                return NotFound();
            }

            return nationalPark;
        }

        [HttpPost]
        public ActionResult<NationalPark> Create(NationalPark nationalPark)
        {
            _nationalParkRepository.Create(nationalPark);

            return CreatedAtRoute("GetNationalPark", new {id = nationalPark.Id.ToString()}, nationalPark);
        }

        [HttpPut("id:length(24)")]
        public IActionResult Update(string id, NationalPark nationalParkIn)
        {
            var nationalPark = _nationalParkRepository.Get(id);

            if (nationalPark == null)
            {
                return NotFound();
            }

            _nationalParkRepository.Update(id, nationalParkIn);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var nationalPark = _nationalParkRepository.Get(id);

            if (nationalPark == null)
            {
                return NotFound();
            }

            _nationalParkRepository.Remove(id);

            return NoContent();
        }
    }
}