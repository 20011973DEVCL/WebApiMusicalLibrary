using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models.Sales;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OderDetailController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderDetailRepository _orderDetailRepo;
        private readonly ILogger<GenreController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public OderDetailController(IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo, ILogger<GenreController> logger, IMapper mapper, APIResponse response)
        {
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
            _logger = logger;
            _mapper = mapper;
            _response = response;
        }

         [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetOrderDatail()
        {
            try
            {
                _logger.LogInformation("Get all Order Details");
                IEnumerable<OrderDetail> albunList = await _orderDetailRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<OrderDetailDto>>(albunList);
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
    }
}