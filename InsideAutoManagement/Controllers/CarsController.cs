using AutoMapper;
using InsideAutoManagement.DAO;
using InsideAutoManagement.Data;
using InsideAutoManagement.DTO;
using InsideAutoManagement.Model;
using InsideAutoManagement.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsideAutoManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        //private readonly InsideAutoManagementContext _context;
        private ConfigurationsDAO _configurationsDAO;
        private CarsDAO _carsDAO;
        private DocumentsDAO _documentsDAO;
        private IMapper _mapper;

        public CarsController(InsideAutoManagementContext context, IMapper mapper, CarDealer carDealer)
        {
            _mapper = mapper;
            _configurationsDAO = new ConfigurationsDAO(context);
            _carsDAO = new CarsDAO(context,carDealer);
            _documentsDAO = new DocumentsDAO(context,carDealer);
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
        public async Task<IActionResult> UploadFile(string plate, Document document, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            if (CarExists(plate) == false)
                return BadRequest($"There are no car with plate {plate}");

            using (var memoryStream = new MemoryStream())
            {
                await _documentsDAO.SaveDocuments( [ 
                    new RequestSaveDocuments { 
                        Document = document, 
                        DocumentBytes = GetBytesFromMemoryStream(file)
                    } ]);
            }

            Car car = await _carsDAO.GetCar(plate) ?? throw new Exception("Car not found");
           if(car.Documents == null) 
                car.Documents = new List<Document>();

            car.Documents.Add(document);

            await _carsDAO.EditCar(car.Id, car);

            return Ok($"Document {file.Name} uploaded successfully");
        }

        private byte[] GetBytesFromMemoryStream(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }


        private bool CarExists(Guid id)
        {
            return _carsDAO.CarExists(id);
        }
        private bool CarExists(string plate)
        {
            return _carsDAO.CarExists(plate);
        }

        

    }
}
