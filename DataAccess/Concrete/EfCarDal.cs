using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RecapProjectDatabaseContext>, ICarDal
    {
        public CarDetailDto GetCarDetail(Expression<Func<Car, bool>> filter)
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    join cl in context.Colors
                        on c.ColorId equals cl.ColorId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        CarName = c.CarName,
                        BrandName = b.BrandName,
                        ColorName = cl.ColorName,
                        DailyPrice = c.DailyPrice
                    };
                return result.Single();
            }
            
        }

        public List<CarDetailDto> GetCarsDetail()
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    join cl in context.Colors
                        on c.ColorId equals cl.ColorId
                    select new CarDetailDto
                    {
                        Id = c.Id, CarName = c.CarName, BrandName = b.BrandName, ColorName = cl.ColorName,
                        DailyPrice = c.DailyPrice
                    };
                return result.ToList();
            }
        }
    }
}
