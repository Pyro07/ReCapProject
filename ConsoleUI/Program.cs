using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using Entities.Concrete;

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

            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var item in carManager.GetCarDetails())
            {
                Console.WriteLine($"Marka : {item.BrandName}\nModel : {item.CarName}\nRenk : {item.ColorName}\nGünlük Ücret : {item.DailyPrice}");
            }
        }

        private static void GetBrands()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            AddBrand();
            foreach (var item in brandManager.GetAll())
            {
                Console.WriteLine(item.Name);
            }
        }

        private static void GetCars()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            foreach (var item in carManager.GetAll())
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
