using AutoMapper;
using InsideAutoManagement.DAO;
using InsideAutoManagement.Data;
using InsideAutoManagement.DTO;
using InsideAutoManagement.Model;
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
        private ConfigurationsDAO _configurationsDAO;
        private IMapper _mapper;

        public CarsController(InsideAutoManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _carsDAO = new CarsDAO(context);
            _carDealersDAO = new CarDealersDAO(context);
            _configurationsDAO = new ConfigurationsDAO(context);
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetCar()
        {
            IEnumerable<CarDTO> cars = (await _carsDAO.GetCars())
                 .Select(c => _mapper.Map<CarDTO>(c!));

            if (cars == null)
                return NoContent();
            return Ok(cars);
        }

        // GET: api/Cars/5
        [HttpGet("GetCarById/{id}")]
        public async Task<ActionResult<CarDTO>> GetCarById(Guid id)
        {
            var car = _mapper.Map<CarDTO>(await _carsDAO.GetCar(id));

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
            var car = _mapper.Map<CarDTO>(await _carsDAO.GetCar(plate));

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(Guid id, CarDTO car)
        {
            if (id != car.Id)
                return BadRequest();

            try
            {
                CarDealerDTO? carDealer =  _mapper.Map<CarDealerDTO>(await _carDealersDAO.GetCarDealer(car.CarDealer.Id));

                if (carDealer != null)
                    car.CarDealer = carDealer;

                await _carsDAO.EditCar(id, _mapper.Map<Car>(car));
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
        public async Task<ActionResult<CarDTO>> PostCar(CarDTO car)
        {
            CarDealerDTO? carDealer = _mapper.Map<CarDealerDTO>(await _carDealersDAO.GetCarDealer(car.CarDealer.Id));

            if (carDealer != null)
                car.CarDealer = carDealer;

            

            await _carsDAO.SaveCar(_mapper.Map<Car>(car));

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/DeleteCarById/5
        [HttpDelete("DeleteCarById/{id}")]
        public async Task<IActionResult> DeleteCarById(Guid id)       
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

        [HttpPost("UploadImate/{plate}")]
        public IActionResult UploadFile(string plate, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            // Specify the path where you want to save the image
            string filePath = Path.Combine("path/to/images", imageFile.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return Ok("Image uploaded successfully");
        }

        private bool CarExists(Guid id)
        {
            return _carsDAO.CarExists(id);
        }
        private bool CarExists(string plate)
        {
            return _carsDAO.CarExists(plate);
        }

        private string GetDocumentPath(Guid? imageId = null)
        {
            string path = string.Empty;

            if (imageId == null)
                path = _configurationsDAO.GetDocumentPath();
            return path;
        }


        

    }
}
