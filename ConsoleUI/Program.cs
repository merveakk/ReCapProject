using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            
            Console.WriteLine(carManager.GetCarsByBrandId(3).Count);

            //foreach (Car car in carManager./*GetCarsByColorId(3)*/)
            //{
            //    Console.WriteLine(car.Id);
            //}

        }
    }
}
