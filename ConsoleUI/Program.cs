using System;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("ARABA KİRALAMA SİSTEMİNE HOŞGELDİNİZ");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Yapmak istediğiniz işlemi seçin: \n1-Araba ekleme \n2-Araba silme \n3-Araba güncelleme \n4-Arabaların detaylı listesi \n5-Arabaların fiyat aralığına göre listelenmesi\n6-Arabaların model yılına göre Listelenmesi");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    CarAddition(carManager, brandManager, colorManager);
                    break;
                case 2:
                    CarDeletion(carManager);
                    break;
                case 3:
                    CarForUpdate(carManager);
                    break;
                case 4:
                    GetCarDetails(carManager);
                    break;
                case 5:
                    CarByDailyPrice(carManager);
                    break;
                case 6:
                    CarByModelYear(carManager);
                    break;

                default:
                    break;
            }
            
        }

        private static void CarForUpdate(CarManager carManager)
        {
            Console.WriteLine("Güncellemek istediğiniz arabanın bilgilerini giriniz.");
            Console.WriteLine("Car Id:");
            int carIdForUpdate = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Color Id:");
            int colorIdForUpdate = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Brand Id:");
            int brandIdForUpdate = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Model Year:");
            int modelYearForUpdate = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Daily Price:");
            double dailyPriceForUpdate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Description:");
            string descriptionForUpdate = Console.ReadLine();
            Car carForUpdate = new Car { BrandId = brandIdForUpdate, ColorId = colorIdForUpdate, DailyPrice = dailyPriceForUpdate, Description = descriptionForUpdate, ModelYear = modelYearForUpdate, Id = carIdForUpdate };
            carManager.Update(carForUpdate);
        }

        private static void CarDeletion(CarManager carManager)
        {
            Console.WriteLine("Hangi Id'ye sahip arabayı silmek istiyorsunuz? ");
            int carIdForDelete = Convert.ToInt32(Console.ReadLine());
            carManager.Delete(carManager.GetById(carIdForDelete));
        }

        private static void CarAddition(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            Console.WriteLine("Renklerin Listesi");
            GetAllColor(colorManager);
            Console.WriteLine("Markaların Listesi");
            GetAllBrand(brandManager);

            Console.WriteLine("Eklemek istediğiniz arabanın bilgileri:");
            Console.WriteLine("Color Id:");
            int colorIdForAddition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Brand Id:");
            int brandIdForAddition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Model Year:");
            int modelYearForAddition = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Daily Price:");
            double dailyPriceForAddition = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Description:");
            string descriptionForAddition = Console.ReadLine();
            Car carForAdd = new Car { BrandId = brandIdForAddition, ColorId = colorIdForAddition, DailyPrice = dailyPriceForAddition, Description = descriptionForAddition, ModelYear = modelYearForAddition };
            carManager.Add(carForAdd);
        }

        private static void GetAllBrand(BrandManager brandManager)
        {
            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine(brand.BrandId + "-" + brand.BrandName);
            }
        }

        private static void GetAllColor(ColorManager colorManager)
        {
            foreach (var color in colorManager.GetAll())
            {
                Console.WriteLine(color.ColorId + "--" + color.ColorName);
            }
        }

        private static void CarByDailyPrice(CarManager carManager)
        {
            Console.WriteLine("Minimum fiyat: ");
            int min = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Maximum fiyat: ");
            int max = Convert.ToInt32(Console.ReadLine());
            foreach (var item in carManager.GetCarDetails(p=>p.DailyPrice>min & p.DailyPrice<max))
            {
                Console.WriteLine("Model Year-{0} Brand-{1} Color-{2}", item.ModelYear, item.BrandName, item.ColorName);
            }
        }
        private static void CarByModelYear(CarManager carManager)
        {
            Console.WriteLine("Bir model yılı giriniz.");
            int modelyear = Convert.ToInt32(Console.ReadLine());
           
            foreach (var item in carManager.GetCarDetails(I => I.ModelYear == modelyear))
            {
                Console.WriteLine("Daily price-{0} Brand-{1} Color-{2}", item.DailyPrice, item.BrandName, item.ColorName);
            }
        }

        private static void GetCarDetails(CarManager carManager)
        {
            foreach (var item in carManager.GetCarDetails())
            {
                Console.WriteLine("Model Year -- Brand -- Color -- Daily Price");
                Console.WriteLine(item.ModelYear + "/" + item.BrandName + "/ " + item.ColorName + "/" + item.DailyPrice);
            }
        }
        
    }
}
