using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Event = WebApiTestStandAlone.Models.Event;

namespace WebApiTestStandAlone.Controllers
{
    [Route("api/bet")]
    public class BetController : Controller
    {
        private IBetRepository _repository;

        public BetController(IBetRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }

        [HttpGet("sportevent/{id}", Name = "GetEvent")]
        public IActionResult Get(int id)
        {
            IActionResult ret = null;

            var sportEvent = _repository.GetEvent(id);

            ret = Ok(sportEvent);

            return ret;
        }


        [HttpGet("players", Name = "GetPlayers")]
        public IActionResult Getplayers()
        {
            var players  = _repository.GetPlayers();
            IActionResult ret = NoContent(); 
            if (players.Any())
            {
               ret  = Ok(players);
            }


            return ret;
        }


        [HttpPut("sportEvent/{id}")]
        public IActionResult UpdateEvent(int id,
            Event sportEvent)
        {
       
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_repository.SportEventExists(id))
            {
                return NotFound();
            }

            _repository.UpdateEvent(id, sportEvent);
            return NoContent();
        }


        [HttpPost("sportevent")]
        public IActionResult Post(
           Event sportEvent)
        {
           
            IActionResult ret = null;
            if (ModelState.IsValid)
            {
                var saveEvent = _repository.SaveEvent(sportEvent);

                return CreatedAtRoute("GetEvent", new
                    { id = saveEvent.EventId }, saveEvent);

            }
            return BadRequest(ModelState);

        }
    }
}
