using FluentValidation;
using JHT.Logistics.Application.Features.Clientes.Commands;

namespace JHT.Logistics.Application.Features.Clientes.Validators;

public class UpdateClienteCommandValidator : AbstractValidator<UpdateClienteCommand>
{
    public UpdateClienteCommandValidator()
    {
        RuleFor(v => v.CliId)
            .GreaterThan(0).WithMessage("El ID del cliente es requerido.");

        RuleFor(v => v.CliTipoDocumento)
            .NotEmpty().WithMessage("El tipo de documento es requerido.")
            .MaximumLength(20).WithMessage("El tipo de documento no debe exceder los 20 caracteres.");

        RuleFor(v => v.CliNumeroDocumento)
            .NotEmpty().WithMessage("El número de documento es requerido.")
            .MaximumLength(50).WithMessage("El número de documento no debe exceder los 50 caracteres.");

        RuleFor(v => v.CliRazonSocial)
            .NotEmpty().WithMessage("La razón social es requerida.")
            .MaximumLength(255).WithMessage("La razón social no debe exceder los 255 caracteres.");

        RuleFor(v => v.CliNombreComercial)
            .MaximumLength(255).WithMessage("El nombre comercial no debe exceder los 255 caracteres.");

        RuleFor(v => v.CliDireccion)
            .MaximumLength(500).WithMessage("La dirección no debe exceder los 500 caracteres.");

        RuleFor(v => v.CliTelefono)
            .MaximumLength(50).WithMessage("El teléfono no debe exceder los 50 caracteres.");

        RuleFor(v => v.CliEmail)
            .MaximumLength(150).WithMessage("El email no debe exceder los 150 caracteres.")
            .EmailAddress().WithMessage("El email debe tener un formato válido.")
            .When(v => !string.IsNullOrEmpty(v.CliEmail));
    }
}
