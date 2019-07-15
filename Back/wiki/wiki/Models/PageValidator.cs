using FluentValidation;
using wiki.Models;

namespace wiki
{
    public class PageValidator : AbstractValidator<Page>
    {
        private const int maxTitleLength = 5;
        public PageValidator()
        {
            RuleFor(p => p.title).NotEmpty();
            RuleFor(p => p.title).MaximumLength(maxTitleLength);
            RuleFor(p => p.id).GreaterThanOrEqualTo(0);
        }
    }
}
