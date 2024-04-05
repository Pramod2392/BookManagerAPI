using BookManagerAPI.Web.Contracts.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagerAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IValidator<UserRequestModel> _validator;

        public UsersController(IValidator<UserRequestModel> validator)
        {
            _validator = validator;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<UserResponseModel> Post(UserRequestModel userRequestModel)
        {
            // Model validation using fluent validation

            await _validator.ValidateAsync(userRequestModel);

            // Map controller model to service model


            // Call service model method


            // Map service model to controller response model


            // return the response
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
