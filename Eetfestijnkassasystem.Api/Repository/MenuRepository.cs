//using Eetfestijnkassasystem.Shared.Interface;
//using Eetfestijnkassasystem.Shared.Model;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Eetfestijnkassasystem.Api.Repository
//{
//    public class MenuRepository : IRepository<MenuItem>
//    {
//        public MenuRepository()
//        {


//        }

//        public Task AddAsync(MenuItem entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<MenuItem>> GetAllAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MenuItem> GetAsync(int id)
//        {
//            return Task.Run(() => new MenuItem() { Id = 1, Cost = 20.00, DateTimeCreated = DateTime.Now, Name = "Coke" });
//        }

//        public Task RemoveAsync(MenuItem entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task UpdateAsync(MenuItem entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
