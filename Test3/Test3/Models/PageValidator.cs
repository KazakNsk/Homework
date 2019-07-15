using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test3.Models
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

