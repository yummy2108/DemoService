using System.Collections.Generic;
using AutoMapper;
using DemoService.Data;
using DemoService.Models;
using DemoService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System;

namespace DemoService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CosmosServiceController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        public CosmosServiceController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
        }
        // GET api/CosmosService
        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await _cosmosDbService.GetMultipleAsync("SELECT * FROM c"));
        }
        // GET api/CosmosService/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _cosmosDbService.GetAsync(id));
        }
        // POST api/CosmosService
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MyService myservice)
        {
            myservice.Id = Guid.NewGuid().ToString();
            await _cosmosDbService.AddAsync(myservice);
            return CreatedAtAction(nameof(Get), new { id = myservice.Id }, myservice);
        }
        // PUT api/CosmosService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromBody] MyService myservice)
        {
            await _cosmosDbService.UpdateAsync(myservice.Id, myservice);
            return NoContent();
        }
        // DELETE api/CosmosService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return NoContent();
        }

    }
}