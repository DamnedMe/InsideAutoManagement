using AutoMapper;
using InsideAutoManagement.Data;
using InsideAutoManagement.DTO;
using InsideAutoManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.DAO
{
    /// <summary>
    /// used for crud operations on CarDealers
    /// </summary>
    public class CarDealersDAO
    {
        private readonly InsideAutoManagementContext _context = null!;
        private readonly IMapper _mapper;

        public CarDealersDAO(InsideAutoManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private void DeleteOpeningHoursShifts(CarDealer existingCarDealer)
        {
            if (existingCarDealer.OpeningHoursShifts != null && existingCarDealer.OpeningHoursShifts.Count > 0)
                _context.OpeningHoursShifts.RemoveRange(existingCarDealer.OpeningHoursShifts);
        }
        

        public async Task<IEnumerable<CarDealer>> GetCarDealers()
        {
            List<CarDealer> carDealers = await _context.CarDealers
                .Include(cd => cd.OpeningHoursShifts)
                .ToListAsync();

            if (carDealers == null || carDealers.Count == 0)
                return new List<CarDealer>();

            return carDealers;
        }

        public async Task<CarDealer?> GetCarDealer(long id)
        {
            if (_context.CarDealers == null || _context.CarDealers.Count() == 0)
                return null;

            var carDealer = await _context.CarDealers
                .Include(cd => cd.OpeningHoursShifts)
                .Where(cd => cd.Id == id)
                .FirstOrDefaultAsync();

            if (carDealer == null)
                return null;

            return carDealer;
        }

        public async Task<CarDealer?> GetCarDealer(string name)
        {
            if (_context.CarDealers == null || _context.CarDealers.Count() == 0)
                return null;

            var carDealer = await _context.CarDealers
               .Include(cd => cd.OpeningHoursShifts)
               .Where(cd => cd.Name == name)
               .FirstOrDefaultAsync();

            if (carDealer == null)
                return null;

            return carDealer;
        }

        public async Task EditCarDealer(long id, CarDealer carDealer)
        {

            if (id != carDealer.Id)
                throw new ArgumentException("Diverget id between argument");

            if (CarDealerExists(id) == false)
                throw new ArgumentException($"CarDealers with id {id} doesn't exists");

            _context.Entry(carDealer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SaveCarDealer(CarDealer carDealer)
        {
            try
            {
                if (carDealer == null)
                    throw new ArgumentNullException();

                var existingCarDealer = await GetCarDealer(carDealer.Name);

                if (existingCarDealer != null)
                {
                    DeleteOpeningHoursShifts(existingCarDealer);

                    //update if already exists
                    #region update
                    existingCarDealer.Name = carDealer.Name;
                    existingCarDealer.PIVA = carDealer.PIVA;
                    existingCarDealer.Phone = carDealer.Phone;
                    existingCarDealer.Email = carDealer.Email;
                    existingCarDealer.City = carDealer.City;
                    existingCarDealer.Country = carDealer.Country;
                    existingCarDealer.Adress = carDealer.Adress;
                    existingCarDealer.OpeningHoursShifts = carDealer.OpeningHoursShifts;
                    #endregion

                    _context.Entry(existingCarDealer).State = EntityState.Modified;
                }
                else
                    _context.CarDealers.Add(carDealer);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCarDealer(long id)
        {
            var carDealer = await _context.CarDealers
                .Include(cd => cd.OpeningHoursShifts)
                .Where(cd => cd.Id == id)
                .FirstOrDefaultAsync();
            if (carDealer == null)
                throw new InvalidOperationException($"there aren't carDealers with id = {id}");

            DeleteOpeningHoursShifts(carDealer);
            _context.CarDealers.Remove(carDealer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarDealer(string name)
        {
            var carDealer = await _context.CarDealers
                .Include(cd=>cd.OpeningHoursShifts)
                .Where(cd => cd.Name == name)
                .FirstAsync();
            if (carDealer == null)
                throw new InvalidOperationException($"there aren't carDealers with name = {name}");

            DeleteOpeningHoursShifts(carDealer);
            _context.CarDealers.Remove(carDealer);
            await _context.SaveChangesAsync();
        }

        public bool CarDealerExists(long id)
        {
            return _context.CarDealers.Any(e => e.Id == id);
        }
        public bool CarDealerExists(string name)
        {
            return _context.CarDealers.Any(e => e.Name == name);
        }
    }
}
