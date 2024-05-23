using AutoMapper;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models.Book;
using BookManagerAPI.Web.Contracts.Book;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagerAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IValidator<Contracts.Book.BookRequestModel> _validator;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public BooksController(IValidator<Contracts.Book.BookRequestModel> validator, IMapper mapper, IBookService bookService)
        {
            _validator = validator;
            this._mapper = mapper;
            this._bookService = bookService;
        }

        // GET: api/<BooksController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            await _bookService.GetAllBooks();
            return new string[] { "value1", "value2" };
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Contracts.Book.BookResponseModel>> Post(Contracts.Book.BookRequestModel bookRequestModel)
        {
            // Add model validation using fluent validation
            var validationResult = await _validator.ValidateAsync(bookRequestModel);

            if (validationResult.IsValid == false)
            {
                return ValidationProblem();
            }

            // Use automapper to map request model to service model
            var serviceLayerBookRequestModel = _mapper.Map<Service.Models.Book.BookRequestModel>(bookRequestModel);

            // Call service layer method
            var serviceResponse = await _bookService.SaveImageToBlobAndAddNewBook(serviceLayerBookRequestModel);

            // Use automapper to map service model to response model

            if (serviceResponse.IsSuccess == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse.ErrorMessage);
            }

            // return the response
            return Ok(serviceResponse);
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
