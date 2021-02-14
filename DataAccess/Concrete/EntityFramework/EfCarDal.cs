using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapDBContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapDBContext context= new ReCapDBContext())
            {
                var result = from c in filter == null ? context.Cars : context.Cars.Where(filter)
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join a in context.Colors on c.ColorId equals a.ColorId
                             select new CarDetailDto { 
                                 BrandName = b.BrandName, 
                                 ColorName = a.ColorName, 
                                 DailyPrice = c.DailyPrice, 
                                 ModelYear = c.ModelYear };
                return result.ToList();
            }
        }
    }
}
