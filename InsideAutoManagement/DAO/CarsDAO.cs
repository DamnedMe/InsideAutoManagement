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

        public CarsDAO(InsideAutoManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetCars()
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return new List<Car>();

            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCar(Guid id)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return null;

            var car = await _context.Cars.FindAsync(id);            

            return car;
        }

        public async Task<Car?> GetCar(string plate)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return null;

            var car = await _context.Cars.Where(c=>c.Plate == plate).FirstOrDefaultAsync();

            return car;
        }

        public async Task EditCar(Guid id, Car car)
        {

            if (id != car.Id)
                throw new ArgumentException("Invalid car");

            if (_context.Cars == null || _context.Cars.Count() == 0)
                throw new InvalidOperationException("There are no cars");

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
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
                throw new InvalidOperationException($"there aren't cars with id = {id}");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCar(string plate)
        {
            var car = await _context.Cars.Where(cd=> cd.Plate == plate).FirstAsync();
            if (car == null)
                throw new InvalidOperationException($"there aren't cars with name = {plate}");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public bool CarExists(Guid id)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return false;

            return _context.Cars.Any(e => e.Id == id);
        }
        public bool CarExists(string plate)
        {
            if (_context.Cars == null || _context.Cars.Count() == 0)
                return false;

            return _context.Cars.Any(e => e.Plate == plate);
        }
    }
}
