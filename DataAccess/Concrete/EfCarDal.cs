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
                
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                    join cl in context.Colors
                        on c.ColorId equals cl.ColorId
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    select new CarDetailDto
                    {
                        Id=c.Id,
                        CarName = c.CarName,
                        BrandName = b.BrandName,
                        ColorName = cl.ColorName,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        CarImages = context.CarImages.Where(x => x.CarId == c.Id).ToList()
                    };
                return result.Single();
            }

        }
        public List<CarDetailDto> GetCarsDetail(Expression<Func<Car, bool>> filter = null)
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                    join cl in context.Colors
                        on c.ColorId equals cl.ColorId
                    join b in context.Brands
                        on c.BrandId equals b.BrandId
                    select new CarDetailDto
                    {
                        Id = c.Id,
                        CarName = c.CarName,
                        BrandName = b.BrandName,
                        ColorName = cl.ColorName,
                        ModelYear = c.ModelYear,
                        DailyPrice = c.DailyPrice,
                        Description = c.Description,
                        ImagePath = context.CarImages.Where(x => x.CarId == c.Id).FirstOrDefault().ImagePath
                    };
                return result.ToList();
            }
        }
    }
}
