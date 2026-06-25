using EduHome.DTOs.Course;
using FluentValidation;

namespace EduHome.Validations.Course
{
    public class CourseCreateDtoValidation : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidation()
        {
            RuleFor(c => c.Info).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(c => c.Desc).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(c => c.Title).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(c => c.Price).GreaterThanOrEqualTo(0);
            RuleFor(c => c.CategoryID).NotEqual(Guid.Empty);
        }
    }
}
