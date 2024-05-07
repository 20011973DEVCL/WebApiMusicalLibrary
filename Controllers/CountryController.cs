using System.Data.SqlTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;
using System.Net;

namespace WebApiMusicalLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public CountryController(ICountryRepository countryRepo, ILogger<GenreController> logger, IMapper mapper)
        {
            _countryRepo = countryRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }
 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetGenres()
        {
            try
            {
                _logger.LogInformation("Get all Countries");
                IEnumerable<Country> countryList = await _countryRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<CountryDto>>(countryList);
                _response.statusCode= HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id}", Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCountry(string id)
        {
            try
            {
                if (id.Trim()=="" || id ==null)
                {
                    _logger.LogError("Error searching for Country with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var genre = await _countryRepo.GetOne(v=>v.IdCountry==id);
                if (genre==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<CountryDto>(genre);
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }
            return _response;
        }
    
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCountry([FromBody] CountryCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var country = await _countryRepo.GetOne(v=> v.CountryName.ToLower().Trim()== createDto.CountryName.ToLower().Trim());
                if (country!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Country already exists");
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                Country modelo = _mapper.Map<Country>(createDto);

                await _countryRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCountry", new { id = modelo.IdCountry}, _response);
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }

            return _response;
        }
    
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteCountry(string id)
        {
            try
            {
                if (id.Trim()=="" || id==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var country= await _countryRepo.GetOne(v=>v.IdCountry == id);
                if (country==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _countryRepo.Delete(country);
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages=new List<string>() {ex.ToString()};
            }
            return BadRequest(_response);
        }
    
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> UpdateGenre([FromBody] CountryUpdateDto updateDto, string id)
        {
            try
            {
                if (updateDto == null || id!= updateDto.IdCountry)
                {
                    _response.Successful=false;
                    _response.statusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Country modelo = _mapper.Map<Country>(updateDto);

                await _countryRepo.Update(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Successful = false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }

            return BadRequest(_response);
        }
    }
}