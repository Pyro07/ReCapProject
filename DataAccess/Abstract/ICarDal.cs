using System;
using System.Collections.Generic;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal : IEntityRepository<Car>
    {
        //void Add(Car car);
        //void Delete(Car car);
        //void Update(Car car);
        //List<Car> GetAll();
        //Car GetById(int id);

        List<CarDetailDto> GetCarDetails();
    }
}