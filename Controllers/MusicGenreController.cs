using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;
using System.Net;

namespace WebApiMusicalLibrary.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IMusicGenreRepository _musicGenreRepo;
        private readonly IAlbunRepository _albunRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public GenreController(IMusicGenreRepository genreRepo, IAlbunRepository albunRepo, ILogger<GenreController> logger, IMapper mapper)
        {
            _musicGenreRepo = genreRepo;
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
                IEnumerable<MusicGenre> genreList = await _musicGenreRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<MusicGenreDto>>(genreList);
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

                var genre = await _musicGenreRepo.GetOne(v=>v.IdMusicGenre==id);
                if (genre==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<MusicGenreDto>(genre);
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
        public async Task<ActionResult<APIResponse>> CreateGenre([FromBody] MusicGenreCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var genre = await _musicGenreRepo.GetOne(v=> v.GenreName.ToLower().Trim()== createDto.GenreName.ToLower().Trim());
                if (genre!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Genre already exists");
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                MusicGenre modelo = _mapper.Map<MusicGenre>(createDto);

                var idx = await _musicGenreRepo.GetAll();
                modelo.IdMusicGenre =  idx.OrderByDescending(v=>v.IdMusicGenre).FirstOrDefault().IdMusicGenre +1;
                 
                await _musicGenreRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetGenre", new { id = modelo.IdMusicGenre}, _response);
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
                var genre= await _musicGenreRepo.GetOne(v=>v.IdMusicGenre == id);
                if (genre==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //Genero tiene asociados Albunes
                var existAlbun = await _albunRepo.GetOne(b=>b.IdGenre == genre.IdMusicGenre);
                if (existAlbun != null)
                {
                    ModelState.AddModelError("ValidationOfAlbun", "You cannnot Delete this Genre because has Albuns asociated");
                    return BadRequest(ModelState);
                }

                await _musicGenreRepo.Delete(genre);
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
        public async Task<IActionResult> UpdateGenre([FromBody] MusicGenreUpdateDto updateDto, int id)
        {
            try
            {
                if (updateDto == null || id!= updateDto.IdGenre)
                {
                    _response.Successful=false;
                    _response.statusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                MusicGenre modelo = _mapper.Map<MusicGenre>(updateDto);

                await _musicGenreRepo.Update(modelo);
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