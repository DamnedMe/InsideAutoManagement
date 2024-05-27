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
    public class CarDealersController : ControllerBase
    {
        //private readonly InsideAutoManagementContext _context;
        private CarDealersDAO _carDealersDAO;
        private IMapper _mapper;

        public CarDealersController(InsideAutoManagementContext context, IMapper mapper)
        {
            _mapper = mapper;
            _carDealersDAO = new CarDealersDAO(context, mapper);
        }

        // GET: api/CarDealers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDealerDTO>>> GetCarDealer()
        {
            try
            {
                IEnumerable<CarDealerDTO> carDealers = (await _carDealersDAO.GetCarDealers())
                    .Select(cd => _mapper.Map<CarDealerDTO>(cd!));

                if (carDealers == null)
                    return NoContent();

                return Ok(carDealers);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        // GET: api/CarDealers/GetCarDealerById/5
        [HttpGet("GetCarDealerById/{id}")]
        public async Task<ActionResult<CarDealerDTO>> GetCarDealerById(Guid id)
        {
            var carDealerDTO = _mapper.Map<CarDealerDTO>(await _carDealersDAO.GetCarDealer(id));
            if (carDealerDTO == null)
            {
                return NotFound();
            }

            return Ok(carDealerDTO);
        }

        // GET: api/CarDealers/pippo
        [HttpGet("{name}")]
        public async Task<ActionResult<CarDealerDTO>> GetCarDealer(string name)
        {
            var carDealerDTO = _mapper.Map<CarDealerDTO>(await _carDealersDAO.GetCarDealer(name));

            if (carDealerDTO == null)
            {
                return NotFound();
            }

            return Ok(carDealerDTO);
        }

        // PUT: api/CarDealers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarDealer(Guid id, CarDealerDTO carDealerDTO)
        {
            if (id != carDealerDTO.Id)
                return BadRequest();

            try
            {
                await _carDealersDAO.EditCarDealer(id, _mapper.Map<CarDealer>(carDealerDTO));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarDealerExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/CarDealers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarDealerDTO>> PostCarDealer(CarDealerDTO carDealer)
        {
            try
            {
                await _carDealersDAO.SaveCarDealer(_mapper.Map<CarDealer>(carDealer));
            }catch(DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
            return await GetCarDealer(carDealer.Name);
        }

        // DELETE: api/CarDealers/DeleteCarDealerById/5
        [HttpDelete("DeleteCarDealerById/{id}")]
        public async Task<IActionResult> DeleteCarDealerById(Guid id)       
        {            
            if (!CarDealerExists(id))
                return NotFound();            
            await _carDealersDAO.DeleteCarDealer(id);

            return NoContent();
        }

        // DELETE: api/CarDealers/pippo
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCarDealer(string name)
        {
            if (!CarDealerExists(name))
                return NotFound();

            await _carDealersDAO.DeleteCarDealer(name);

            return NoContent();
        }

        private bool CarDealerExists(Guid id)
        {
            return _carDealersDAO.CarDealerExists(id);
        }
        private bool CarDealerExists(string name)
        {
            return _carDealersDAO.CarDealerExists(name);
        }
    }
}