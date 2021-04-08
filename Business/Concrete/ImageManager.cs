using Business.Abstract;
using Core.Utilities.Helpers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Business.Concrete
{
    public class ImageManager : IImageService
    {
        private IImageDal _imageDal;

        public ImageManager(IImageDal imageDal)
        {
            _imageDal = imageDal;
        }

        public IResult Add(CarImage carImage, IFormFile formFile)
        {
            //var result = CheckImageLimit(carImage.CarId);
            //if (!result.Success)
            //{
            //    return result;
            //}

            var result = BusinessRules.Run(CheckImageLimit(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            if (formFile != null)
            {
                var filePath = FileHelper.AddFile(formFile);
                if (filePath.Success)
                {
                    _imageDal.Add(new CarImage
                    {
                        CarId = carImage.CarId,
                        ImagePath = filePath.Data
                    });
                    return new SuccessResult();
                }
            }
            else
            {
                _imageDal.Add(new CarImage
                {
                    CarId = carImage.CarId,
                    ImagePath = SetDefaultImage()
                });
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(CarImage carImage)
        {
            //if (carImage.ImagePath.Contains("default.jpg"))
            //{
            //    _imageDal.Delete(carImage);
            //}
            //else
            //{
            //    FileHelper.DeleteFile(carImage.ImagePath);
            //    _imageDal.Delete(carImage);
            //}
            FileHelper.DeleteFile(carImage.ImagePath);
            _imageDal.Delete(carImage);

            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_imageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_imageDal.Get(i => i.Id == id));
        }

        public IResult Update(CarImage carImage, IFormFile formFile)
        {
            var oldFilePath = carImage.ImagePath;
            var newFilePath = FileHelper.UpdateFile(oldFilePath, formFile);

            if (newFilePath.Success)
            {
                _imageDal.Update(new CarImage
                {
                    Id = carImage.Id,
                    CarId = carImage.CarId,
                    ImagePath = newFilePath.Data
                });
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult CheckImageLimit(int carId)
        {
            var result = _imageDal.GetAll(x => x.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult("Resim sayısı belirlenen limiti geçti.");
            }
            return new SuccessResult();
        }

        public string SetDefaultImage()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\default.png");
            return path;
        }
    }
}

