using System.Collections.Generic;
using DemoService.Models;

namespace DemoService.Data
{
    public interface IMyServiceRepo
    {
        bool SaveChanges();

        IEnumerable<MyService> GetAllMyServices();
        MyService GetMyServiceById(int id);
        void CreateMyService(MyService cmd);
        void UpdateMyService(MyService cmd);
        void DeleteMyService(MyService cmd);
    }
}