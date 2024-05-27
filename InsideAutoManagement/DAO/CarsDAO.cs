using InsideAutoManagement.Data;
using InsideAutoManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.DAO
{
    /// <summary>
    /// used for crud operations on Cars
    /// </summary>
    public class CarsDAO
    {
        private readonly InsideAutoManagementContext _context = null!;
        private CarDealer _carDealer = null!;

        public CarsDAO(InsideAutoManagementContext context, CarDealer carDealer)
        {
            _context = context;
            _carDealer = carDealer;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return new List<Car>();

            return await _context.Cars.Where(c=>c.CarDealer == _carDealer).ToListAsync();
        }

        public async Task<Car?> GetCar(Guid id)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return null;

            var car = await _context.Cars.Where(c => c.CarDealer == _carDealer && c.Id == id).FirstOrDefaultAsync();

            return car;
        }

        public async Task<Car?> GetCar(string plate)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return null;

            var car = await _context.Cars.Where(c=> c.CarDealer == _carDealer && c.Plate == plate).FirstOrDefaultAsync();

            return car;
        }

        public async Task EditCar(Guid id, Car car)
        {

            if (id != car.Id)
                throw new ArgumentException("Invalid car");

            if (_context.Cars == null || _context.Cars.Count() == 0)
                throw new InvalidOperationException("There are no cars");

            car.CarDealer = _carDealer;

            if(CarExists(car.Plate) == false)
                throw new ArgumentException($"The input car plate {car.Plate} doesn't exist");

            try
            {
                _context.Entry(car).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
       
        public async Task SaveCar(Car car)
        {
            try
            {
                if(car==null)
                    throw new ArgumentNullException();

                car.CarDealer = _carDealer;

                var existingCars = await GetCar(car.Plate);

                if (existingCars != null)
                {
                    Guid lastId = existingCars.Id;
                    existingCars = car;
                    existingCars.Id = lastId;

                    _context.Entry(existingCars).State = EntityState.Modified;
                }
                else
                    _context.Cars.Add(car);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }           
        }
       
        public async Task DeleteCar(Guid id)
        {
            var car = await _context.Cars.Where(c => c.CarDealer == _carDealer && c.Id == id).FirstOrDefaultAsync();
            if (car == null)
                throw new InvalidOperationException($"there aren't cars with id = {id}");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCar(string plate)
        {
            var car = await _context.Cars.Where(c=> c.CarDealer == _carDealer && c.Plate == plate).FirstAsync();
            if (car == null)
                throw new InvalidOperationException($"there aren't cars with plate = {plate}");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public bool CarExists(Guid id)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return false;

            return _context.Cars.Any(c => c.CarDealer == _carDealer && c.Id == id);
        }
        public bool CarExists(string plate)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return false;

            return _context.Cars.Any(c => c.CarDealer == _carDealer && c.Plate == plate);
        }
    }
}
