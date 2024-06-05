using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiMusicalLibrary.Models.Login;
using WebApiMusicalLibrary.Repository.IRepository;

namespace WebApiMusicalLibrary.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MenuOptionController : ControllerBase
    {
        private readonly IMenuOptionRepository _menuOptRepo;
        private readonly ILogger<MenuOptionController> _logger;
        private readonly IMapper _mapper;
        private APIResponse _response;

        public MenuOptionController(IMenuOptionRepository menuOptRepo, 
                                    ILogger<MenuOptionController> logger, 
                                    IMapper mapper)
        {
            _menuOptRepo = menuOptRepo;
            _logger = logger;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetMenuOptions()
        {
            try
            {
                IEnumerable<MenuOptions> menuOptList = await _menuOptRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<MenuOptionsDto>>(menuOptList.OrderBy(c =>c.OptionOrder));
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

        [HttpGet("{id}", Name = "GetMenuOption")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetMenuOption(string id)
        {
            try
            {
                if (id.Trim()=="" || id ==null)
                {
                    _logger.LogError("Error searching for Menu Option with ID "+ id);
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var menuOpt = await _menuOptRepo.GetOne(v=>v.IdOption.ToUpper().Trim()==id);
                if (menuOpt==null)
                {
                    _response.Successful = false;
                    _response.statusCode= HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                
                _response.Result = _mapper.Map<MenuOptionsDto>(menuOpt);
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
        public async Task<ActionResult<APIResponse>> CreateMenuOption([FromBody] MenuOptionsCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var menuOpt = await _menuOptRepo.GetOne(v=> v.Description.ToLower().Trim()== createDto.Description.ToLower().Trim());
                if (menuOpt!=null)
                {
                    ModelState.AddModelError("ValidationOfNames", "The entered Menu/Option already exists");
                    return BadRequest(ModelState);
                }

                if (createDto==null)
                {
                    return BadRequest(createDto);
                }

                MenuOptions modelo = _mapper.Map<MenuOptions>(createDto);
                modelo.IdOption = modelo.IdOption.ToUpper().Trim();

                await _menuOptRepo.Create(modelo);
                _response.Result = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetMenuOption", new { id = modelo.IdOption}, _response);
            }
            catch (Exception ex)
            {
                _response.Successful=false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }

            return _response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] 
        public async Task<IActionResult> DeleteMenuOption(string id)
        {
            try
            {
                if (id.Trim()=="" || id==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                //Existe Menu/Opcion
                var menuOpt= await _menuOptRepo.GetOne(v=>v.IdOption == id);
                if (menuOpt==null)
                {
                    _response.Successful=false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _menuOptRepo.Delete(menuOpt);
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
        public async Task<IActionResult> UpdateGenre([FromBody] MenuOptionsUpdateDto updateDto, string id)
        {
            try
            {
                if (updateDto == null || id!= updateDto.IdOption)
                {
                    _response.Successful=false;
                    _response.statusCode =HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                MenuOptions modelo = _mapper.Map<MenuOptions>(updateDto);
                modelo.IdOption = modelo.IdOption.ToUpper().Trim();
                
                await _menuOptRepo.Update(modelo);
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