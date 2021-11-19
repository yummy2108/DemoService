using System;
using System.Collections.Generic;
using System.Linq;
using DemoService.Models;
using DemoService.Data;

namespace DemoService.Data
{
    public class MyServiceSimpleRepo : IMyServiceRepo
    {
        private readonly MyServiceContext _context;

        public MyServiceSimpleRepo(MyServiceContext context)
        {
            _context = context;
        }

        public void CreateMyService(MyService myservice)
        {
            if(myservice == null)
            {
                throw new ArgumentNullException(nameof(myservice));
            }

            _context.MyServices.Add(myservice);
        }

        public void DeleteMyService(MyService myservice)
        {
            if(myservice == null)
            {
                throw new ArgumentNullException(nameof(myservice));
            }
            _context.MyServices.Remove(myservice);
        }

        public IEnumerable<MyService> GetAllMyServices()
        {
            return _context.MyServices.ToList();
        }

        public MyService GetMyServiceById(string id)
        {
            return _context.MyServices.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateMyService(MyService myservice)
        {
            //Nothing
        }
    }
}