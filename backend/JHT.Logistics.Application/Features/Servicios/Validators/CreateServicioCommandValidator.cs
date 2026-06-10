using FluentValidation;
using JHT.Logistics.Application.Features.Servicios.Commands;

namespace JHT.Logistics.Application.Features.Servicios.Validators;

public class CreateServicioCommandValidator : AbstractValidator<CreateServicioCommand>
{
    public CreateServicioCommandValidator()
    {
        RuleFor(v => v.CliId)
            .GreaterThan(0).WithMessage("El ID del cliente es requerido.");

        RuleFor(v => v.SerTipoServicio)
            .NotEmpty().WithMessage("El tipo de servicio es requerido.")
            .MaximumLength(50).WithMessage("El tipo de servicio no debe exceder los 50 caracteres.");

        RuleFor(v => v.Destinos)
            .NotEmpty().WithMessage("El servicio debe tener al menos un destino.");

        RuleForEach(v => v.Destinos).SetValidator(new CreateDestinoDtoValidator());
    }
}

public class CreateDestinoDtoValidator : AbstractValidator<CreateDestinoDto>
{
    public CreateDestinoDtoValidator()
    {
        RuleFor(v => v.Secuencia)
            .GreaterThan(0).WithMessage("La secuencia debe ser mayor a 0.");

        RuleFor(v => v.Destinatario)
            .NotEmpty().WithMessage("El destinatario es requerido.")
            .MaximumLength(255).WithMessage("El destinatario no debe exceder los 255 caracteres.");

        RuleFor(v => v.Direccion)
            .NotEmpty().WithMessage("La dirección es requerida.")
            .MaximumLength(500).WithMessage("La dirección no debe exceder los 500 caracteres.");
    }
}
