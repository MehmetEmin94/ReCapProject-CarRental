using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId));
            if (result!=null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
             FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {


            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId));
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDal.Get(c =>c.Id == carImage.Id).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult();
        }

        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (carImageCount>=5)
            {
                return new ErrorResult(Messages.CarImageLimitExceed);
            }

            return new SuccessResult();
        }
        private List<CarImage> CheckIfCarImageNull(int carId)
        {
            string path = @"\wwwroot\null.jpg";
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
          
                if (!result)
            {
                return new List<CarImage> { new CarImage
                {
                    CarId = carId,
                    ImagePath = path,
                    Date = DateTime.Now
                } };
            }
            

            
            return _carImageDal.GetAll(c => c.CarId == carId);
        }
    }
}
