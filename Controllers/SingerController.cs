using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Repository.IRepository;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using WebApiMusicalLibrary.Models;

namespace WebApiMusicalLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController : ControllerBase
    {
        private readonly ISingerRepository _bandSingerRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IAlbunRepository _albunRepo;
        private readonly ILogger<SingerController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public SingerController(ISingerRepository bandSingerRepo, ICountryRepository countryRepo, IAlbunRepository albunRepo,
            ILogger<SingerController> logger, IMapper mapper)
        {
            _bandSingerRepo = bandSingerRepo;
            _countryRepo = countryRepo;
            _albunRepo = albunRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }
 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBandSinger()
        {
            try
            {
                _logger.LogInformation("Get all Band or Singers");
                IEnumerable<Singer> bandSingerList = await _bandSingerRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<SingerDto>>(bandSingerList);
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

        [HttpGet("{id}", Name = "GetBandSingers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBandSingers(int id)
        {
            try
            {
                if (id ==0)
                {
                    _logger.LogError("Error searching for Country with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var Singer = await _bandSingerRepo.GetOne(v=>v.IdSinger==id);
                if (Singer==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<SingerDto>(Singer);
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
        public async Task<ActionResult<APIResponse>> Search(string? idCountry, string? nameBandSinger)
        {
            try
            {
                _logger.LogInformation("Get all Band or Singers by ID");

                var bandas = await _bandSingerRepo.GetAll();
                var countries = await _countryRepo.GetAll();

                var query =
                from varBanda in bandas
                join varCountry in countries on varBanda.IdCountry equals varCountry.IdCountry
                select new { 
                            varBanda.IdSinger, 
                            varBanda.SingerName, 
                            varBanda.Members, 
                            varCountry.IdCountry, 
                            varCountry.CountryName,
                            varBanda.StarDate 
                            };

                if (idCountry==null && nameBandSinger!=null) 
                {
                    query = query.Where(q=>q.SingerName.ToUpper().Trim()==nameBandSinger.ToUpper().Trim());
                } else if (idCountry!=null && nameBandSinger!=null) {
                    query = query.Where(q=>q.IdCountry.ToUpper()==idCountry.ToUpper() && q.SingerName.ToUpper().Trim()==nameBandSinger.ToUpper().Trim());
                } else if (idCountry!=null && nameBandSinger==null) {
                    query = query.Where(q=>q.IdCountry.ToUpper()==idCountry.ToUpper());
                }

                if (query.Count()> 0) {
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
        public async Task<ActionResult<APIResponse>> CreateBandSinger([FromBody] SingerCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var Singer = await _bandSingerRepo.GetOne(v=> v.SingerName.ToLower().Trim()== createDto.SingerName.ToLower().Trim());
                if (Singer!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Band or Singer already exists");
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                Singer modelo = _mapper.Map<Singer>(createDto);
                if (modelo.IdCountry!=string.Empty)
                {
                    var contrySinger= await _countryRepo.GetOne(c=>c.IdCountry.ToLower().Trim() == modelo.IdCountry.ToLower().Trim());
                    if (contrySinger==null) {
                        ModelState.AddModelError("ValidationOfCountry","The entered country does not exist" );
                        return BadRequest(ModelState);      
                    }
                }

                var keyParent = await _bandSingerRepo.GetAll();
                var idx =0;
                if (keyParent.Count==0) {
                    idx = 1;
                } else {
                    idx = keyParent.OrderByDescending(v=>v.IdSinger).FirstOrDefault().IdSinger +1;
                }

                modelo.IdSinger = idx;
                modelo.DateCreate = DateTime.Now;
                modelo.DateUpdate = DateTime.Now;
            
                await _bandSingerRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetBandSingers", new { id = modelo.IdSinger}, _response);
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
        public async Task<IActionResult> DeleteBandSinger(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //Que exista el Grupo o cantante 
                var Singer= await _bandSingerRepo.GetOne(v=>v.IdSinger == id);
                if (Singer==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                //Que Grupo o cantante  no tenga asociado Albunes
                var existSongs = await _albunRepo.GetOne(s=> s.IdSinger == Singer.IdSinger);
                if (existSongs != null)
                {
                    ModelState.AddModelError("ValidationOfAlbun", "You cannnot Delete this Band or Singer because has Albuns asociated");
                    return BadRequest(ModelState);
                }
                await _bandSingerRepo.Delete(Singer);
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
        public async Task<IActionResult> UpdateBandSinger([FromBody] SingerUpdateDto updateDto, int id)
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

                Singer modelo = _mapper.Map<Singer>(updateDto);

                modelo.DateUpdate = DateTime.Now;
                await _bandSingerRepo.Update(modelo);
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