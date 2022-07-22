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
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("O nome do diretor eh obrigatorio.");
        }
    }
}
