using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RecapProjectDatabaseContext>, IRentalDal
    {
        public List<RentalDto> GetRentalDetails(/*Expression<Func<Rental, bool>> filter = null*/)
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from r in context.Rentals
                    join ca in context.Cars
                        on r.CarId equals ca.Id
                    join b in context.Brands
                        on ca.BrandId equals b.BrandId
                    join c in context.Customers
                        on r.CustomerId equals c.Id
                    join u in context.Users
                        on c.UserId equals u.Id

                    select new RentalDto { Id = r.Id, BrandName = b.BrandName, CarName = ca.CarName, FirstName = u.FirstName, LastName = u.LastName, RentDate = r.RentDate, ReturnDate = r.ReturnDate };
                return result.ToList();
            }
        }
    }
}
