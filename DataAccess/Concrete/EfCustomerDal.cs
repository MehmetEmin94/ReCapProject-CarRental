using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RecapProjectDatabaseContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails()
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from c in context.Customers
                    join u in context.Users
                        on c.UserId equals u.Id
                    select new CustomerDetailDto { Id = c.Id, FirstName = u.FirstName, LastName = u.LastName, CompanyName = c.CompanyName, Email = u.Email };
                return result.ToList();
            }
        }
    }
}
