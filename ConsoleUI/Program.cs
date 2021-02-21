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
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            UserManager userManager = new UserManager(new EfUserDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("ARABA KİRALAMA SİSTEMİNE HOŞGELDİNİZ");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Yapmak istediğiniz işlemi seçin: \n1-Araba ekleme \n2-Araba silme \n3-Araba güncelleme \n4-Arabaların detaylı listesi \n5-Arabaların fiyat aralığına göre listelenmesi\n6-Arabaların model yılına göre Listelenmesi \n7-Müşteri ekleme \n8-Müşterilerin Listelenmesi \n9- Kullanıcı ekleme \n10-Kullanıcıların listelenmesi \n11-Araba Kiralama \n12-Araba Teslimi");
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
                case 7:
                    GetAllUsers(userManager);
                    CustomerAddition(customerManager);
                    break;
                case 8:
                    ListCustomers(customerManager);
                    break;
                case 9:
                    UserAddition(userManager);
                    break;
                case 10:
                    GetAllUsers(userManager);
                    break;
                case 11:
                    GetCarDetails(carManager);
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("\n\n");
                    ListCustomers(customerManager);
                    Console.WriteLine("-----------------------------");
                    Console.WriteLine("\n\n");
                    RentalAddition(rentalManager);
                    break;
                case 12:
                    ReturnRental(rentalManager);
                    break;

                default:
                    break;
            }
            
        }

        private static void ReturnRental(RentalManager rentalManager)
        {
            Console.WriteLine("Kiraladığınız araba hangi Car Id'ye sahip?");
            int carId = Convert.ToInt32(Console.ReadLine());
            var returnedRental = rentalManager.GetRentalDetails(I => I.CarId == carId);
            foreach (var rental in returnedRental.Data)
            {
                rental.ReturnDate = DateTime.Now;
                Console.WriteLine("Araba teslim edildi.");
            }
            
        }

        private static void RentalAddition(RentalManager rentalManager)
        {
            Console.WriteLine("Car id:");
            int carIdForRentCar = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Customer id:");
            int customerIdForRentCar = Convert.ToInt32(Console.ReadLine());
            Rental rental = new Rental
            {
                CarId = carIdForRentCar,
                CustomerId = customerIdForRentCar,
                RentDate = DateTime.Now,
                ReturnDate = null,
            };
            Console.WriteLine(rentalManager.Add(rental).Message);
            
        }

        private static void UserAddition(UserManager userManager)
        {
            Console.WriteLine("First Name:");
            string firstNameForAddititon = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string lastNameForAddititon = Console.ReadLine();
            Console.WriteLine("Email:");
            string emailForAddititon = Console.ReadLine();
            Console.WriteLine("Password:");
            string passwordForAddititon = Console.ReadLine();
            User userForAdd = new User { FirstName = firstNameForAddititon, LastName = lastNameForAddititon, Email = emailForAddititon, Password = passwordForAddititon };
            userManager.Add(userForAdd);
        }

        private static void ListCustomers(CustomerManager customerManager)
        {
            Console.WriteLine("Müşterilerin Listesi:");
            Console.WriteLine("User id - Company Name");
            foreach (var customer in customerManager.GetAll().Data)
            {

                Console.WriteLine(customer.UserId + " - " + customer.CompanyName);
            }
        }

        private static void CustomerAddition(CustomerManager customerManager)
        {
            Console.WriteLine("Eklemek istediğiniz müşterinin kullanıcı id'si:");
            int idForAddCustomer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Company name:");
            string companyNameForAdd = Console.ReadLine();
            Customer customerForAdd = new Customer { UserId = idForAddCustomer, CompanyName = companyNameForAdd };
            customerManager.Add(customerForAdd);
        }

        private static void GetAllUsers(UserManager userManager)
        {
            Console.WriteLine("Kullanıcı Listesi");
            foreach (var user in userManager.GetAll().Data)
            {
                Console.WriteLine(user.Id + " " + user.FirstName + " " + user.LastName + " " + user.Email);
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
            carManager.Delete(carManager.GetById(carIdForDelete).Data);
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
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.BrandId + "-" + brand.BrandName);
            }
        }

        private static void GetAllColor(ColorManager colorManager)
        {
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorId + "--" + color.ColorName);
            }
        }

        private static void CarByDailyPrice(CarManager carManager)
        {
            Console.WriteLine("Minimum fiyat: ");
            double min = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Maximum fiyat: ");
            double max = Convert.ToDouble(Console.ReadLine());

            var result = carManager.GetCarDetails(I => I.DailyPrice > min & I.DailyPrice < max);
            if (result.Success == true)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine("Model Year-{0} Brand-{1} Color-{2} Daily Price-{3}", item.ModelYear, item.BrandName, item.ColorName, item.DailyPrice);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
          }
        private static void CarByModelYear(CarManager carManager)
        {
            Console.WriteLine("Bir model yılı giriniz.");
            int modelyear = Convert.ToInt32(Console.ReadLine());
           
            foreach (var item in carManager.GetCarDetails(I => I.ModelYear == modelyear).Data)
            {
                Console.WriteLine("Daily price-{0} Brand-{1} Color-{2}", item.DailyPrice, item.BrandName, item.ColorName);
            }
        }

        private static void GetCarDetails(CarManager carManager)
        {
            var result = carManager.GetCarDetails();
            if (result.Success==true)
            {
                foreach (var item in result.Data)
            {
                Console.WriteLine("Car id -- Model Year -- Brand -- Color -- Daily Price");
                Console.WriteLine(item.CarId+"/" + item.ModelYear + "/" + item.BrandName + "/ " + item.ColorName + "/" + item.DailyPrice);
            }

            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
        
    }
}
