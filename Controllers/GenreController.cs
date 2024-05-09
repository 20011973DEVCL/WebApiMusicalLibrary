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
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepo;
        private readonly IAlbunRepository _albunRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public GenreController(IGenreRepository genreRepo, IAlbunRepository albunRepo, ILogger<GenreController> logger, IMapper mapper)
        {
            _genreRepo = genreRepo;
            _albunRepo = albunRepo;
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
                _logger.LogInformation("Get all Musical Types Genres");
                IEnumerable<Genre> genreList = await _genreRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<GenreDto>>(genreList);
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

        [HttpGet("{id}", Name = "GetGenre")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetGenre(int id)
        {
            try
            {
                if (id==0)
                {
                    _logger.LogError("Error searching for Gender with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var genre = await _genreRepo.GetOne(v=>v.IdGenre==id);
                if (genre==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<GenreDto>(genre);
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
        public async Task<ActionResult<APIResponse>> CreateGenre([FromBody] GenreCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var genre = await _genreRepo.GetOne(v=> v.GenreName.ToLower().Trim()== createDto.GenreName.ToLower().Trim());
                if (genre!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Genre already exists");
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                Genre modelo = _mapper.Map<Genre>(createDto);

                var idx = await _genreRepo.GetAll();
                modelo.IdGenre =  idx.OrderByDescending(v=>v.IdGenre).FirstOrDefault().IdGenre +1;
                 
                await _genreRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetGenre", new { id = modelo.IdGenre}, _response);
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
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //Existe genero
                var genre= await _genreRepo.GetOne(v=>v.IdGenre == id);
                if (genre==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //Genero tiene asociados Albunes
                var existAlbun = await _albunRepo.GetOne(b=>b.IdGenre == genre.IdGenre);
                if (existAlbun != null)
                {
                    ModelState.AddModelError("ValidationOfAlbun", "You cannnot Delete this Genre because has Albuns asociated");
                    return BadRequest(ModelState);
                }

                await _genreRepo.Delete(genre);
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
        public async Task<IActionResult> UpdateGenre([FromBody] GenreUpdateDto updateDto, int id)
        {
            try
            {
                if (updateDto == null || id!= updateDto.IdGenre)
                {
                    _response.Successful=false;
                    _response.statusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Genre modelo = _mapper.Map<Genre>(updateDto);

                await _genreRepo.Update(modelo);
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