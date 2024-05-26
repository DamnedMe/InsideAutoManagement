using InsideAutoManagement.Data;
using InsideAutoManagement.Models;
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
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car?> GetCar(long id)
        {
            var car = await _context.Cars.FindAsync(id);            

            return car;
        }

        public async Task<Car?> GetCar(string plate)
        {
            var car = await _context.Cars.Where(c=>c.Plate == plate).FirstOrDefaultAsync();

            return car;
        }

        public async Task EditCar(long id, Car car)
        {

            if (id != car.Id)
                throw new ArgumentException();

            _context.Entry(car).State = EntityState.Modified;

            try
            {
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
                    long lastId = existingCars.Id;
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
       
        public async Task DeleteCar(long id)
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

        public bool CarExists(long id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
        public bool CarExists(string plate)
        {
            return _context.Cars.Any(e => e.Plate == plate);
        }
    }
}
