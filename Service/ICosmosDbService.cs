using DemoService.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Cosmos;
namespace DemoService.Service{
    public interface ICosmosDbService
    {
        Task<IEnumerable<MyService>> GetMultipleAsync(string query);
        Task<MyService> GetAsync(string id);
        Task AddAsync(MyService myservice);
        Task UpdateAsync(string id, MyService myservice);
        Task DeleteAsync(string id);
    }
}