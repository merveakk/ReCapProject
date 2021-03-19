using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            if (rental.ReturnDate==null && _rentalDal.GetRentalDetails(p=>p.CarId==rental.CarId).Count>0)
            {
                return new ErrorResult(Messages.Error);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.EntityAdded);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.EntityDeleted);
        }
        [PerformanceAspect(10)]
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [PerformanceAspect(10)]
        [CacheAspect]
        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(p => p.Id== id));
        }
        [PerformanceAspect(10)]
        public IDataResult<List<RentalDetailDto>> GetRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(filter));
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}
