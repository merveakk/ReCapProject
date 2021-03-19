using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq.Expressions;
using Entities.DTOs;
using System.Linq;
using Core.Utilities.Results;
using Business.Constants;
using Business.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("product.add,admin")]
        public IResult Add(Car car)
        {

            _carDal.Add(car);
            return new SuccessResult(Messages.EntityAdded);

        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            if (car.DailyPrice < 100)
            {
                throw new Exception(Messages.TransactionCanceled);
            }
            Add(car);
            return new SuccessResult(Messages.EntityAdded);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.EntityDeleted);
        }

        [PerformanceAspect(10)]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 20)
            {
                return new ErrorDataResult<List<Car>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }
        [PerformanceAspect(10)]
        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new DataResult<Car>(_carDal.Get(p => p.Id == id), true);
        }
        [PerformanceAspect(10)]
        public IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(filter));
        }

        [PerformanceAspect(10)]
        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.BrandId == id));
        }
        [PerformanceAspect(10)]
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            
            _carDal.Update(car);
            return new SuccessResult(Messages.EntityUpdated);

        }
    }
}
