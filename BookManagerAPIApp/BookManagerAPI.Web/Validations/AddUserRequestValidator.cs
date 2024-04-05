using BookManagerAPI.Web.Contracts.User;
using FluentValidation;

namespace BookManagerAPI.Web.Validations
{
    public class AddUserRequestValidator : AbstractValidator<UserRequestModel>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.FirstName).NotEmpty();            
        }
    }
}
