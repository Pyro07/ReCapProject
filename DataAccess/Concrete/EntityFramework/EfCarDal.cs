using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (CarRentalContext context=new CarRentalContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.Id equals b.Id
                             join clr in context.Colors on c.ColorId equals clr.Id
                             select new CarDetailDto
                             {
                                 CarName = c.ModelName,
                                 BrandName = b.Name,
                                 DailyPrice = c.DailyPrice,
                                 ColorName = clr.Name
                             };
                return result.ToList();
            }
        }
    }
}
