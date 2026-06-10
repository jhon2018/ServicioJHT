using FluentValidation;
using JHT.Logistics.Application.Features.Conductores.Commands;

namespace JHT.Logistics.Application.Features.Conductores.Validators;

public class UpdateConductorCommandValidator : AbstractValidator<UpdateConductorCommand>
{
    public UpdateConductorCommandValidator()
    {
        RuleFor(v => v.ConId)
            .GreaterThan(0).WithMessage("El ID del conductor es requerido.");

        RuleFor(v => v.ConCodigoExterno)
            .MaximumLength(50).WithMessage("El código externo no debe exceder los 50 caracteres.");

        RuleFor(v => v.ConTipo)
            .NotEmpty().WithMessage("El tipo de conductor es requerido.")
            .Must(t => t.ToUpper() == "I" || t.ToUpper() == "E")
            .WithMessage("El tipo de conductor debe ser 'I' (Interno) o 'E' (Externo).");

        RuleFor(v => v.ConDni)
            .NotEmpty().WithMessage("El DNI o documento es requerido.")
            .MaximumLength(20).WithMessage("El DNI no debe exceder los 20 caracteres.");

        RuleFor(v => v.ConNombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es requerido.")
            .MaximumLength(255).WithMessage("El nombre completo no debe exceder los 255 caracteres.");

        RuleFor(v => v.ConTelefono)
            .NotEmpty().WithMessage("El teléfono es requerido.")
            .MaximumLength(50).WithMessage("El teléfono no debe exceder los 50 caracteres.");

        RuleFor(v => v.ConEmail)
            .MaximumLength(150).WithMessage("El email no debe exceder los 150 caracteres.")
            .EmailAddress().WithMessage("El email debe tener un formato válido.")
            .When(v => !string.IsNullOrEmpty(v.ConEmail));
    }
}
