using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using Entities.Concrete;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddCar();
            //GetCars();
            //GetBrands();
            //AddColor();
            GetCarDetails();
            //AddUsers();
            //AddCustomers();
            //GetUsers();
            //GetCustomers();

            //RentACar();

            Console.ReadKey();
        }

        private static void RentACar()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental { CustomerId = 2, CarId = 2 }).Message;
            Console.WriteLine(result);
        }

        private static void GetCarDetails()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var item in carManager.GetCarDetails().Data)
            {
                Console.WriteLine($"Marka : {item.BrandName}\nModel : {item.CarName}\nRenk : {item.ColorName}\nGünlük Ücret : {item.DailyPrice}\n");
            }
        }

        private static void GetCustomers()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var item in customerManager.GetAll().Data)
            {
                Console.WriteLine($"{item.CompanyName} {item.UserId}");
            }
        }

        private static void GetUsers()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            foreach (var item in userManager.GetAll().Data)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
        }

        private static void AddUsers()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            List<User> users = new List<User>
            {
                new User { FirstName = "Ali", LastName = "Aydın", Email = "abc@abc.com", Password = "12345" },
                new User { FirstName = "Hüseyin", LastName = "Yılmaz", Email = "abc1@abc.com", Password = "12345" },
                new User { FirstName = "Mehmet", LastName = "Güner", Email = "abc2@abc.com", Password = "12345" },
            };

            foreach (var item in users)
            {
                userManager.Add(item);
            }
        }

        private static void AddCustomers()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            List<Customer> customers = new List<Customer>
            {
                new Customer{ CompanyName="AB Company", UserId=1 },
                new Customer{ CompanyName="BC Company", UserId=2 },
                new Customer{ CompanyName="CD Company", UserId=3 },
            };
            foreach (var item in customers)
            {
                customerManager.Add(item);
            }
        }

        private static void GetBrands()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            AddBrand();
            foreach (var item in brandManager.GetAll().Data)
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void GetCars()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var item in carManager.GetAll().Data)
            {
                Console.WriteLine($"Id: {item.Id} Model Name: {item.ModelName}");
            }
        }

        private static CarManager AddCar()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            carManager.Add(
                new Car
                {
                    BrandId = 1
                    //ColorId = 1,
                    //DailyPrice = 20.000M,
                    //ModelName = "C-Max",
                    //ModelYear = 2010,
                    //Description = "MPV"
                });
            return carManager;
        }

        private static void AddBrand()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            brandManager.Add(new Brand { Name = "Renault" });
        }

        private static void AddColor()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(new Color { Name = "Bulut Gri" });
        }
    }
}
