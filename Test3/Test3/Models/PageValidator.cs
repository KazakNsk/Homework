using FluentValidation;

namespace Test3.Models
{
    public class PageValidator : AbstractValidator<Page>
        {
            private const int maxTitleLength = 128;
            public PageValidator()
            {
                RuleFor(p => p.title).NotEmpty();
                RuleFor(p => p.title).MaximumLength(maxTitleLength);
                RuleFor(p => p.id).GreaterThanOrEqualTo(0);
            }
        }
    }

