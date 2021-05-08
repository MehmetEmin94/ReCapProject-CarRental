﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
       ICarDal _carDal;

       public CarManager(ICarDal carDal)
       {
           _carDal = carDal;
       }
        //[SecuredOperation("car.add,admin")]
        //[ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            //if (car.CarName.Length>2&&car.DailyPrice>0)
            //{
            //    return new ErrorResult(Messages.CarNameInvalid);
            //}
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }
        [CacheAspect]
        //[PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
       {
           return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarsListed);
       }
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<List<Car>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));
        }

        public IDataResult<CarDetailDto> GetCarDetail(int id)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetail(c => c.Id == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetail());
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetail(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId)
        {
            
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetail(c => c.ColorId == colorId));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.DailyPrice < 10)
            {
                throw new Exception("TransactionNotSucceed");
            }

            Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

    }
}
