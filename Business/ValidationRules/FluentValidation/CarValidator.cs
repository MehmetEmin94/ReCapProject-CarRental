using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        //public CarValidator()
        //{
        //    RuleFor(c => c.BrandId).NotEmpty();
        //    RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(50).When(c => c.BrandId == 2);
        //    RuleFor(c => c.Description).Must(StartWithA);
        //    RuleFor(c => c.ModelYear).GreaterThan(1995);
        //}

        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
