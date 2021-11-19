using System.Collections.Generic;
using AutoMapper;
using DemoService.Data;
using DemoService.Models;
using Microsoft.AspNetCore.Mvc;


namespace DemoService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MyServiceController : ControllerBase
    {
        private readonly IMyServiceRepo _repository;

        public MyServiceController(IMyServiceRepo repository)
        {
            _repository = repository;
        }
       
        //GET api/MyService
        [HttpGet]
        public ActionResult <IEnumerable<MyService>> GetAllMyServices()
        {
            var MyServiceItems = _repository.GetAllMyServices();

            return Ok(MyServiceItems);
        }

        //GET api/MyService/{id}
        [HttpGet("{id}", Name="GetMyServiceById")]
        public ActionResult <MyService> GetMyServiceById(string id)
        {
            var MyServiceItem = _repository.GetMyServiceById(id);
            if(MyServiceItem != null)
            {
                return Ok(MyServiceItem);
            }
            return NotFound();
        }

        //POST api/MyService
        [HttpPost]
        public ActionResult <MyService> CreateMyService(MyService MyServiceCreate)
        {
            _repository.CreateMyService(MyServiceCreate);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetMyServiceById), new {Id = MyServiceCreate.Id}, MyServiceCreate);      
        }

        //DELETE api/MyService/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMyService(string id)
        {
            var MyServiceModelFromRepo = _repository.GetMyServiceById(id);
            if(MyServiceModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteMyService(MyServiceModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}