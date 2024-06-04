using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Data;
using WebApiMusicalLibrary.Models.Sales;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        private readonly ApplicationDbContext _userRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public OrderController(IOrderRepository orderRepo, ApplicationDbContext userRepo, ILogger<GenreController> logger, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrders()
        {
            try
            {
                _logger.LogInformation("Get all Orders");
                IEnumerable<Order> albunList = await _orderRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<OrderDto>>(albunList);
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

        [HttpGet("{id}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetOrder(int id)
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

                var order = await _orderRepo.GetOne(v=>v.IdOrder==id);
                if (order==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<OrderDto>(order);
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
        public async Task<ActionResult<APIResponse>> CreateOrder([FromBody] OrderCreateDto createDto)
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

                Order modelo = _mapper.Map<Order>(createDto);

                // //Exista Grupo o Cantante Asociado
                // var bandSinger= await _bandSingerRepo.GetOne(b=>b.IdBandSinger == modelo.IdBandSinger);
                // if (bandSinger==null) {
                //     ModelState.AddModelError("ValidationOfBandSinger","The entered Band or Singer does not exist" );
                //     return BadRequest(ModelState);      
                // }

                // //Exista Genero Asociado
                // var genre = await _genreRepo.GetOne(g=>g.IdGenre == modelo.IdGenre);
                // if (genre == null) {
                //     ModelState.AddModelError("ValidationOfGenre","The entered Genre does not exist" );
                //     return BadRequest(ModelState);    
                // }
                
                var keyParent = await _orderRepo.GetAll();
                var idx =0;
                if (keyParent.Count==0) {
                    idx = 1;
                } else {
                    idx = keyParent.OrderByDescending(v=>v.IdOrder).FirstOrDefault().IdOrder +1;
                }

                modelo.IdOrder =idx;
                await _orderRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetOrder", new { id = modelo.IdOrder}, _response);
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
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                if (id==0)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var bandSinger= await _orderRepo.GetOne(v=>v.IdOrder == id);
                if (bandSinger==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // //Que Albun no tenga asociado Canciones
                // var existSongs = await _songsRepo.GetOne(s=> s.IdAlbun == bandSinger.IdAlbun);
                // if (existSongs != null)
                // {
                //     ModelState.AddModelError("ValidationOfSongs", "You cannnot Delete this Albun because has Songs asociated");
                //     return BadRequest(ModelState);
                // }

                await _orderRepo.Delete(bandSinger);
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
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto updateDto, int id)
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

                Order modelo = _mapper.Map<Order>(updateDto);

                await _orderRepo.Update(modelo);
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