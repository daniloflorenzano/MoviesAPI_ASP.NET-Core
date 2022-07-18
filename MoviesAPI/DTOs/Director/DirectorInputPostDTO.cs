using FluentValidation;

namespace MoviesAPI.DTOs.Director
{
    public class DirectorInputPostDTO
    {
        public string Name { get; set; }
    }

    public class DirectorInputPostDTOValidator : AbstractValidator<DirectorInputPostDTO>
    {
        public DirectorInputPostDTOValidator()
        {
            RuleFor(director => director.Name).NotNull().NotEmpty();
            RuleFor(director => director.Name).Length(2, 250).
                WithMessage("Tamanho {TotalLength} e invalido");
        }
    }
}
