using BookManagerAPI.Web.Contracts.Book;
using FluentValidation;

namespace BookManagerAPI.Web.Validations
{
    public class AddBookRequestValidator : AbstractValidator<BookRequestModel>
    {
        public AddBookRequestValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEmpty();

            // We need image size and image format validations
        }
    }
}
