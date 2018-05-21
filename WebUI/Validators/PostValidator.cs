using DAO.Models;
using FluentValidation;

namespace WebUI.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(m => m.Title).NotEmpty();
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.UserId).NotEmpty();
        }
    }
}
