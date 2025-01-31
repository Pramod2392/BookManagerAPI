﻿using AutoMapper;
using BookManagerAPI.Service.Interfaces;
using BookManagerAPI.Service.Models;
using BookManagerAPI.Service.Models.Book;
using BookManagerAPI.Service.Models.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookManagerAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
        public async Task<ActionResult<PagedGetBookModel>> Get([FromQuery]PaginationModel paginationModel, [FromQuery] string? searchText)
        {
            if (paginationModel?.PageNumber < 1 || paginationModel?.PageSize < 1)
            {
                return StatusCode((int)HttpStatusCode.BadRequest,"Page number or page size value is invalid");
            }

            var GetUserBooksResponse = await _bookService.GetAllBooks(paginationModel, searchText);            

            if (GetUserBooksResponse.IsSuccess == false)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, GetUserBooksResponse.Message);
            }
            return Ok(GetUserBooksResponse.Data);
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BooksController>
        [HttpPost]
        [Authorize]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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

            // Check if the book already exists
            //

            // Call service layer method
            var serviceResponse = await _bookService.SaveImageToBlobAndAddNewBook(serviceLayerBookRequestModel);

            // Use automapper to map service model to response model

            if (serviceResponse.IsSuccess == false && serviceResponse.StatusCode == HttpStatusCode.InternalServerError)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse.ErrorMessage);
            }

            if (serviceResponse.IsSuccess == false && serviceResponse.StatusCode == HttpStatusCode.Conflict)
            {
                return StatusCode(StatusCodes.Status409Conflict, serviceResponse.ErrorMessage);
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

        // Get All Categories
        [HttpGet]
        [Route("/api/Books/GetCategories")]
        public async Task<List<Category>> GetAllCategories()
        {
            var response = await _bookService.GetAllCategoriesAsync();
            return response.ToList();
        }

        //Get All Languages
        [HttpGet]
        [Route("/api/Books/GetLanguages")]
        public async Task<List<Language>> GetAllLanguages()
        {
            var response = await _bookService.GetAllLanguages();
            return response.ToList();
        }
    }
}
