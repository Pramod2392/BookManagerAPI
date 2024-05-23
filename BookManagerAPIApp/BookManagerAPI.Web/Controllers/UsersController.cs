using AutoMapper;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Web.Contracts.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagerAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IValidator<UserRequestModel> _validator;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IValidator<UserRequestModel> validator, IMapper mapper, IUserService userService, ILogger<UsersController> logger)
        {
            _validator = validator;
            this._mapper = mapper;
            _userService = userService;
            this._logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
        public IEnumerable<string> Get()
        {
            var bearerToken = HttpContext?.Request?.Headers?.Authorization.ToString();
                        

            string token = "";
            if (bearerToken != null)
            {
                if( bearerToken.Split(' ').Length == 2 ) 
                {
                    token = bearerToken.Split(' ')[1].ToString();
                }
                else 
                {
                    token = bearerToken;
                }
            }
            return new string[] { token };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<UserResponseModel>> Post(UserRequestModel userRequestModel)
        {
            try
            {
                // Model validation using fluent validation

                var validationResult = await _validator.ValidateAsync(userRequestModel);

                if (validationResult.IsValid == false)
                {
                    ValidationProblem();
                }

                // Map controller model to service model

                var serviceUserModel = _mapper.Map<Service.Models.User.UserRequestModel>(userRequestModel);

                // Call service model method

                var serviceUserResponse = await _userService.AddUserAsync(serviceUserModel);

                if (serviceUserResponse.IsSuccess == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, serviceUserResponse.Message);
                }

                // Map service model to controller response model

                var response = _mapper.Map<UserResponseModel>(serviceUserResponse.Data);

                // return the response

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding new user");
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
