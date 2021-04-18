using Business.Concrete;
using DataAccess.Concrete;
using System;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            //var result = carManager.GetAll();
            //foreach (var car in result.Data)
            //{
            //    Console.WriteLine(car.ModelYear);
            //}
            //foreach (var car in carManager.GetCarsByBrandId(2))
            //{
            //    Console.WriteLine(car.ModelYear);
            //}
            //foreach (var car in carManager.GetCarsByColorId(4))
            //{
            //    Console.WriteLine(car.ModelYear);
            //}

            //carManager.Add(new Car{ BrandId = 5, ColorId = 1, CarName = "Yaris",ModelYear = 2019,DailyPrice = 100,Description ="v",});
            //var result = carManager.GetCarDetails();
            //if (result.Success==true)
            //{
            //     foreach (var car in result.Data)
            //{
            //    Console.WriteLine(car.CarName+" / "+car.BrandName + " / " + car.ColorName + " / " + car.DailyPrice);
            //}
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            //var result1 = rentalManager.Add(new Rental { CarId = 6, CustomerId = 4, RentDate = DateTime.Now, ReturnDate = new DateTime(2022,02,02) });

            //Console.WriteLine(result1.Message);

            var result = rentalManager.GetById(1);
            if (result.Success==true&&result.Data.ReturnDate==null)
            {
                Console.WriteLine("null");
            }
            else if (result.Success==true&&result.Data.ReturnDate!=null)
            {
                Console.WriteLine(result.Data.ReturnDate);
            }

            
            
            

            

        }
    }
}
