//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using DataAccess.Abstract;
//using Entities.Concrete;

//namespace DataAccess.Concrete
//{
//    public class InMemoryCarDal : ICarDal
//    {
//        List<Car> _cars = new List<Car>()
//        {
//            new Car{ Id=1, BrandId=1, ColorId=1, ModelYear=2010, DailyPrice=350, Description="Az hasarlı bir araba."},
//            new Car{ Id=2, BrandId=1, ColorId=2, ModelYear=2005, DailyPrice=250, Description="Kullanışlı bir araba."},
//            new Car{ Id=3, BrandId=2, ColorId=3, ModelYear=2017, DailyPrice=600, Description="Hasarsız bir araba."},
//            new Car{ Id=4, BrandId=3, ColorId=2, ModelYear=2020, DailyPrice=1000, Description="Son model bir araba."},
//            new Car{ Id=5, BrandId=3, ColorId=3, ModelYear=2014, DailyPrice=500, Description="Hasarsız bir araba."},
//        };
//        public void Add(Car car)
//        {
//            _cars.Add(car);
//        }

//        public void Delete(Car car)
//        {
//            Car carToDelete = _cars.SingleOrDefault(p => p.Id == car.Id);
//            _cars.Remove(carToDelete);
//        }

//        public Car Get(Expression<Func<Car, bool>> filter)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Car> GetAll()
//        {
//            return _cars;
//        }

//        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Car> GetById(int id)
//        {
//            return _cars.Where(p => p.Id == id).ToList();
//        }

//        public void Update(Car car)
//        {
//            Car carToUpdate = _cars.SingleOrDefault(p => p.Id == car.Id);
//            carToUpdate.DailyPrice = car.DailyPrice;
//            carToUpdate.Description = car.Description;
//        }
//    }
//}
