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
    public class EfCarImageDal:EfEntityRepositoryBase<CarImage,RecapProjectDatabaseContext>,ICarImageDal
    {
       
        public List<CarImageDto> GetImagesDetail(Expression<Func<CarImage, bool>> filter = null)
        {
            using (RecapProjectDatabaseContext context = new RecapProjectDatabaseContext())
            {
                var result = from ci in filter == null ? context.CarImages : context.CarImages.Where(filter)
                             join c in context.Cars
                                 on ci.CarId equals c.Id
                             join cl in context.Colors
                                 on c.ColorId equals cl.ColorId
                                 join b in context.Brands
                                     on c.BrandId equals b.BrandId
                             select new CarImageDto
                             {
                                 Id = ci.Id,
                                 CarId = ci.CarId,
                                 BrandId=b.BrandId,
                                 ColorId = cl.ColorId,
                                 CarName = c.CarName,
                                 BrandName = b.BrandName,
                                 ColorName = cl.ColorName,
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 Date = ci.Date,
                                 ImagePath = ci.ImagePath
                             };
                return result.ToList();
            }
        }
    }
}
