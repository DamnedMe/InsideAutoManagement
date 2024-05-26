using AutoMapper;
using InsideAutoManagement.DAO;
using InsideAutoManagement.Data;
using InsideAutoManagement.DTO;
using InsideAutoManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //private readonly InsideAutoManagementContext _context;
        private CarsDAO _carsDAO;
        private CarDealersDAO _carDealersDAO;
        private IMapper _mapper;

        public CarsController(InsideAutoManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _carsDAO = new CarsDAO(context);
            _carDealersDAO = new CarDealersDAO(context, mapper);
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCar()
        {
            IEnumerable<Car> cars = await _carsDAO.GetCars();

            if (cars == null)
                return NoContent();
            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("GetCarById/{id}")]
        public async Task<ActionResult<Car>> GetCarById(long id)
        {
            var car = await _carsDAO.GetCar(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // GET: api/Cars/AA123BB
        [HttpGet("{plate}")]
        public async Task<ActionResult<Car>> GetCar(string plate)
        {
            var car = await _carsDAO.GetCar(plate);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(long id, Car car)
        {
            if (id != car.Id)
                return BadRequest();

            try
            {
                CarDealer? carDealer = await _carDealersDAO.GetCarDealer(car.CarDealer.Id);

                if (carDealer != null)
                    car.CarDealer = carDealer;

                await _carsDAO.EditCar(id, car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            CarDealer? carDealer = await _carDealersDAO.GetCarDealer(car.CarDealer.Id);

            if (carDealer != null)
                car.CarDealer = carDealer;

            await _carsDAO.SaveCar(car);

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/DeleteCarById/5
        [HttpDelete("DeleteCarById/{id}")]
        public async Task<IActionResult> DeleteCarById(long id)       
        {            
            if (!CarExists(id))
                return NotFound();

            await _carsDAO.DeleteCar(id);

            return NoContent();
        }

        // DELETE: api/Cars/AA123BB
        [HttpDelete("{plate}")]
        public async Task<IActionResult> DeleteCar(string plate)
        {
            if (!CarExists(plate))
                return NotFound();

            await _carsDAO.DeleteCar(plate);

            return NoContent();
        }

        private bool CarExists(long id)
        {
            return _carsDAO.CarExists(id);
        }
        private bool CarExists(string plate)
        {
            return _carsDAO.CarExists(plate);
        }
    }
}
