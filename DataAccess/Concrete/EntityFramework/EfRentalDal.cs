using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public List<RentDetailDto> GetRentDetails()
        {
            using (CarRentalContext context=new CarRentalContext())
            {
                var result = from rent in context.Rentals
                             join car in context.Cars on rent.CarId equals car.Id
                             join customer in context.Customers on rent.CustomerId equals customer.Id
                             join user in context.Users on customer.Id equals user.Id
                             select new RentDetailDto
                             {
                                 CarModelName = car.ModelName,
                                 CustomerFullName = $"{user.FirstName} {user.LastName}",
                                 RentDate = rent.RentDate,
                                 ReturnDate = rent.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}
