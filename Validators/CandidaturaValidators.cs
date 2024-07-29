using FluentValidation;
using ControleCandidaturas.DTOs;
using System;

namespace ControleCandidaturas.Validators
{
    public class CandidaturaValidators: AbstractValidator<CandidaturaRequestDTO>
    {
        public CandidaturaValidators()
        {
            RuleFor(e => e.Cargo)
                .NotNull().WithMessage("O cargo não pode ser nulo.")
                .NotEmpty().WithMessage("O nome do cargo não pode ser vazio.")
                .Length(min: 1, max: 255).WithMessage("O nome do cargo deve conter entre 1 e 255 caracteres.");

            RuleFor(e => e.Empresa)
                .NotNull().WithMessage("A empresa não pode ser nulo.")
               .NotEmpty().WithMessage("O nome da empresa não pode ser vazia.")
               .Length(min: 1, max: 255).WithMessage("O nome da empresa deve conter entre 1 e 255 caracteres.");

            RuleFor(e => e.Plataforma)
                .Length(min: 0, max: 255).WithMessage("O nome da plataforma deve conter no máximo 255 caracteres.")
                .When(e => e.Plataforma != null);

            RuleFor(e => e.Url)
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("A Url deve representar um endereço eletrônico válido.")
                .When(e => e.Url != null);

            RuleFor(e => e.Salario)
                .GreaterThanOrEqualTo(0).WithMessage("O salário deve corresponder a uma quantia em dinheiro.")
                .When(e => e.Salario != null);

            RuleFor(e => e.Descricao)
                .NotNull().WithMessage("A descrição não pode ser nula.")
                .NotEmpty().WithMessage("A descrição não pode ser vazia.");

            RuleFor(e => e.Status)
                .NotNull().WithMessage("O status não pode ser nulo.")
                .InclusiveBetween(0, 6).WithMessage("O status é inválido");

            RuleFor(e => e.Modo)
                .InclusiveBetween(0, 2).WithMessage("O modo de trabalho é inválido.")
                .When(e => e.Modo != null);

            RuleFor(e => e.Grau)
                .InclusiveBetween(0, 6).WithMessage("A senioridade é inválida.")
                .When(e => e.Grau != null);
        }
    }
}
