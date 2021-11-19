using DemoService.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;
namespace DemoService.Service{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient cosmosDbClient,
            string databaseName,
            string containerName)
        {
            _container = cosmosDbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(MyService myservice)
        {
            await _container.CreateItemAsync(myservice, new PartitionKey(myservice.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<MyService>(id, new PartitionKey(id));
        }

        public async Task<MyService> GetAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<MyService>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException) //For handling MyService not found and other exceptions
            {
                return null;
            }
        }

        public async Task<IEnumerable<MyService>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<MyService>(new QueryDefinition(queryString));

            var results = new List<MyService>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, MyService MyService)
        {
            await _container.UpsertItemAsync(MyService, new PartitionKey(id));
        }
    }    
}