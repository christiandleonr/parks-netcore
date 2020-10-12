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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public ActionResult<List<NationalPark>> Get() => _nationalParkRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetNationalPark")]
        [Authorize]
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
        [Authorize]
        public ActionResult<NationalPark> Create(NationalPark nationalPark)
        {
            _nationalParkRepository.Create(nationalPark);

            return CreatedAtRoute("GetNationalPark", new {id = nationalPark.Id.ToString()}, nationalPark);
        }

        [HttpPut("id:length(24)")]
        [Authorize]
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
        [Authorize]
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