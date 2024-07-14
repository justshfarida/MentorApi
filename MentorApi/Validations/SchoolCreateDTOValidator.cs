using FluentValidation;
using MentorApi.DTOs.SchoolDTOs;

namespace MentorApi.Validations
{
    public class SchoolCreateDTOValidator:AbstractValidator<SchoolCreateDTO>
    {
       public SchoolCreateDTOValidator() 
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage("School number can not be empty").GreaterThan(0).WithMessage("School number should be greater than 0").WithName("School Number");

            RuleFor(x => x.Name).NotEmpty().WithMessage("School Name can'te be empty").MaximumLength(30).MinimumLength(5).WithMessage("School name should be 5-30 characters.").WithName("School Name");
        }
    }
}
