using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbunController : ControllerBase
    {
        private readonly IAlbunRepository _albunRepo;
        private readonly IBandSingerRepository _bandSingerRepo;
        private readonly IGenreRepository _genreRepo;
        private readonly ISongsRepository _songsRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public AlbunController( IAlbunRepository albunRepo, 
                                IBandSingerRepository bandSingerRepo, 
                                ISongsRepository songsRepo,
                                IGenreRepository genreRepo, 
                                ICountryRepository countryRepo,
                                ILogger<GenreController> logger, IMapper mapper)
        {
            _albunRepo = albunRepo;
            _bandSingerRepo = bandSingerRepo;
            _genreRepo = genreRepo;
            _songsRepo = songsRepo;
            _countryRepo = countryRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAlbun()
        {
            try
            {
                _logger.LogInformation("Get all Albun");
                IEnumerable<Albun> albunList = await _albunRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<AlbunDto>>(albunList);
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
    
        [HttpGet("{id}", Name = "GetAlbun")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAlbun(int id)
        {
            try
            {
                if (id==0)
                {
                    _logger.LogError("Error searching for Albun with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var albun = await _albunRepo.GetOne(v=>v.IdAlbun==id);
                if (albun==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<AlbunDto>(albun);
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

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Search(string? nombreAlbun, string? nombreBandaCantante, int? IdGenero, string? IdPais) 
        {
            try
            {
                _logger.LogInformation("Get all Band or Singers by ID");
                var pais = await _countryRepo.GetAll();
                var banda = await _bandSingerRepo.GetAll();
                var albun = await _albunRepo.GetAll();
                var genero = await _genreRepo.GetAll();

                var query = 
                from varAlbun in albun 
                join varBanda in banda on 
                varAlbun.IdBandSinger equals varBanda.IdBandSinger join varPais in pais on varBanda.IdCountry equals varPais.IdCountry
                join varGenero in genero on varAlbun.IdGenre equals varGenero.IdGenre
                select new {
                            varAlbun.IdAlbun,
                            varAlbun.AlbunName,
                            varAlbun.AlbunYear,
                            varBanda.IdBandSinger,
                            varBanda.Name,
                            varBanda.StarDate,
                            varAlbun.Notes,
                            varPais.IdCountry,
                            varPais.CountryName,
                            varGenero.IdGenre,
                            varGenero.GenreName
                            };

                if  (nombreAlbun!=null && nombreBandaCantante!=null && IdGenero!=null && IdPais!=null) {
                    query = query.Where(q=>q.AlbunName.ToUpper().Trim()==nombreAlbun.ToUpper().Trim() &&
                                        q.Name.ToUpper().Trim()==nombreBandaCantante.ToUpper().Trim() &&
                                        q.IdGenre == IdGenero &&
                                        q.IdCountry.ToUpper()==IdPais.ToUpper());
                } else if (nombreAlbun!=null && nombreBandaCantante!=null && IdGenero!=null) {
                    query = query.Where(q=>q.AlbunName.ToUpper().Trim()==nombreAlbun.ToUpper().Trim() &&
                                        q.Name.ToUpper().Trim()==nombreBandaCantante.ToUpper().Trim() &&
                                        q.IdGenre == IdGenero);
                } else if (nombreAlbun!=null && nombreBandaCantante!=null) {
                    query = query.Where(q=>q.AlbunName.ToUpper().Trim()==nombreAlbun.ToUpper().Trim() &&
                                        q.Name.ToUpper().Trim()==nombreBandaCantante.ToUpper().Trim());
                } else if (nombreAlbun!=null) {
                    query = query.Where(q=>q.AlbunName.ToUpper().Trim()==nombreAlbun.ToUpper().Trim());
                } else if (nombreBandaCantante!=null) {
                    query = query.Where(q=>q.Name.ToUpper().Trim()==nombreBandaCantante.ToUpper().Trim());
                }  else if (IdGenero!=null) {
                    query = query.Where(q=>q.IdGenre==IdGenero);
                } else if (IdPais!=null) {
                    query = query.Where(q=>q.IdCountry.ToUpper().Trim()==IdPais.ToUpper().Trim());
                } 

                if (query.Count()>0) {
                    _response.Result = query.ToList();
                    _response.statusCode= HttpStatusCode.OK;
                    return Ok(_response);
                } else {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);    
                }

  
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateAlbun([FromBody] AlbunCreateDto createDto)
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

                Albun modelo = _mapper.Map<Albun>(createDto);

                //Exista Grupo o Cantante Asociado
                var bandSinger= await _bandSingerRepo.GetOne(b=>b.IdBandSinger == modelo.IdBandSinger);
                if (bandSinger==null) {
                    ModelState.AddModelError("ValidationOfBandSinger","The entered Band or Singer does not exist" );
                    return BadRequest(ModelState);      
                }

                //Exista Genero Asociado
                var genre = await _genreRepo.GetOne(g=>g.IdGenre == modelo.IdGenre);
                if (genre == null) {
                    ModelState.AddModelError("ValidationOfGenre","The entered Genre does not exist" );
                    return BadRequest(ModelState);    
                }
                
                var keyParent = await _albunRepo.GetAll();
                var idx =0;
                if (keyParent.Count==0) {
                    idx = 1;
                } else {
                    idx = keyParent.OrderByDescending(v=>v.IdAlbun).FirstOrDefault().IdAlbun +1;
                }

                modelo.IdAlbun =idx;
                await _albunRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetAlbun", new { id = modelo.IdAlbun}, _response);
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
        public async Task<IActionResult> DeleteAlbun(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var bandSinger= await _albunRepo.GetOne(v=>v.IdAlbun == id);
                if (bandSinger==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //Que Albun no tenga asociado Canciones
                var existSongs = await _songsRepo.GetOne(s=> s.IdAlbun == bandSinger.IdAlbun);
                if (existSongs != null)
                {
                    ModelState.AddModelError("ValidationOfSongs", "You cannnot Delete this Albun because has Songs asociated");
                    return BadRequest(ModelState);
                }

                await _albunRepo.Delete(bandSinger);
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
        public async Task<IActionResult> UpdateAlbun([FromBody] AlbunUpdateDto updateDto, int id)
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

                Albun modelo = _mapper.Map<Albun>(updateDto);

                await _albunRepo.Update(modelo);
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