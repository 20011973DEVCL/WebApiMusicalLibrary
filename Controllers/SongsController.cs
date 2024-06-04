using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ISongsRepository _songsRepo;
        private readonly IAlbunRepository _albunRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly ISingerRepository _bandSingerRepo;
        private readonly IMusicGenreRepository _genreRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public SongsController(ISongsRepository songsRepo, IAlbunRepository albunRepo, ILogger<GenreController> logger, IMapper mapper, ICountryRepository countryRepo, ISingerRepository bandSingerRepo, IMusicGenreRepository genreRepo)
        {
            _songsRepo = songsRepo;
            _albunRepo = albunRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
            _countryRepo = countryRepo;
            _bandSingerRepo = bandSingerRepo;
            _genreRepo = genreRepo;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSongs()
        {
            try
            {
                _logger.LogInformation("Get all Songs");
                IEnumerable<Songs> songsList = await _songsRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<SongsDto>>(songsList);
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

        [HttpGet("{id}", Name = "GetSong")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSong(int id)
        {
            try
            {
                if (id==0)
                {
                    _logger.LogError("Error searching for Songs with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var songs = await _songsRepo.GetOne(v=>v.IdSong==id);
                if (songs==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<SongsDto>(songs);
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

        // [HttpGet("search")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<ActionResult<APIResponse>> Search(int id) {
        //         var pais = await _countryRepo.GetAll();
        //         var banda = await _bandSingerRepo.GetAll();
        //         var albun = await _albunRepo.GetAll();
        //         var genero = await _genreRepo.GetAll(); 
        //         var songs = await _songsRepo.GetAll();

        //         var query = 
        //         from varAlbun in albun 
        //         join varBanda in banda on 
        //         varAlbun.IdBandSinger equals varBanda.IdBandSinger join varPais in pais 
        //             on varBanda.IdCountry equals varPais.IdCountry
        //         join varGenero in genero on varAlbun.IdGenre equals varGenero.IdGenre
        //         join varSongs in songs on varAlbun.is equals varSongs.
        //         select new {
        //                     varAlbun.IdAlbun,
        //                     varAlbun.AlbunName,
        //                     varAlbun.AlbunYear,
        //                     varBanda.IdBandSinger,
        //                     varBanda.Name,
        //                     varBanda.StarDate,
        //                     varAlbun.Notes,
        //                     varPais.IdCountry,
        //                     varPais.CountryName,
        //                     varGenero.IdGenre,
        //                     varGenero.GenreName,
        //                     varSongs.IdSong,
        //                     varSongs.Track,
        //                     //varSongs.Name,
        //                     };
        // }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSongs([FromBody] SongsCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                Songs modelo = _mapper.Map<Songs>(createDto);

                //Albun Existe para la cancion
                var albunExist= await _albunRepo.GetOne(a=>a.IdAlbun == modelo.IdAlbun);
                if (albunExist==null) {
                    ModelState.AddModelError("ValidationOfAlbun","The entered Albun does not exist" );
                    return BadRequest(ModelState);      
                }

                //Existe el mismo nombre de la cancion
                var songName = await _songsRepo.GetOne(v=> v.SongName.ToLower().Trim()== createDto.SongName.ToLower().Trim());
                if (songName!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Name already exists");
                    return BadRequest(ModelState);
                }

                //IdAlbun y Track no deben repetirse
                var existAlbunTrack = await _songsRepo.GetOne(s=>s.IdAlbun == modelo.IdAlbun && s.Track == modelo.Track);
               if (existAlbunTrack!=null)
                {
                    ModelState.AddModelError("ValidationOfAlbunTrack", "The entered Albun and Number of Track already exists");
                    return BadRequest(ModelState);
                }

                var keyParent = await _songsRepo.GetAll();
                var idx =0;
                if (keyParent.Count==0) {
                    idx = 1;
                } else {
                    idx = keyParent.OrderByDescending(v=>v.IdSong).FirstOrDefault().IdSong +1;
                }

                modelo.IdSong =idx;
                await _songsRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetSong", new { id = modelo.IdSong}, _response);
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
        public async Task<IActionResult> DeleteSong(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var song= await _songsRepo.GetOne(v=>v.IdSong == id);
                if (song==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                await _songsRepo.Delete(song);
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
        public async Task<IActionResult> UpdateSong([FromBody] SongsUpdateDto updateDto, int id)
        {
            try
            {
                if (updateDto == null || id== 0)
                {
                    _response.Successful=false;
                    _response.ErrorMessages =new List<string>() {"The Id cannot be 0"};
                    _response.statusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                Songs modelo = _mapper.Map<Songs>(updateDto);

                await _songsRepo.Update(modelo);
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