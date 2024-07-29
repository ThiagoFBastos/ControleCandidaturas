using ControleCandidaturas.DTOs;
using FluentValidation;

namespace ControleCandidaturas.Validators
{
    public class RelatorioValidators: AbstractValidator<RelatorioRequestDTO>
    {
        public RelatorioValidators()
        {
            RuleFor(e => e.Titulo)
                .NotNull().WithMessage("O título não pode ser nulo.")
                .Length(min: 1, max: 255).WithMessage("O título deve conter entre 1 e 255 caracteres.");

            RuleFor(e => e.Descricao)
                .NotNull().WithMessage("A descrição não pode ser nula.")
                .NotEmpty().WithMessage("A descrição não pode ser vazia.");
        }
    }
}
