using FluentValidation;
using JHT.Logistics.Application.Features.Vehiculos.Commands;

namespace JHT.Logistics.Application.Features.Vehiculos.Validators;

public class UpdateVehiculoCommandValidator : AbstractValidator<UpdateVehiculoCommand>
{
    public UpdateVehiculoCommandValidator()
    {
        RuleFor(v => v.VehId)
            .GreaterThan(0).WithMessage("El ID del vehículo es requerido.");

        RuleFor(v => v.VehPlaca)
            .NotEmpty().WithMessage("La placa del vehículo es requerida.")
            .MaximumLength(20).WithMessage("La placa no debe exceder los 20 caracteres.");

        RuleFor(v => v.VehMarca)
            .NotEmpty().WithMessage("La marca es requerida.")
            .MaximumLength(100).WithMessage("La marca no debe exceder los 100 caracteres.");

        RuleFor(v => v.VehModelo)
            .NotEmpty().WithMessage("El modelo es requerido.")
            .MaximumLength(100).WithMessage("El modelo no debe exceder los 100 caracteres.");

        RuleFor(v => v.VehTipo)
            .NotEmpty().WithMessage("El tipo de vehículo es requerido.")
            .MaximumLength(50).WithMessage("El tipo de vehículo no debe exceder los 50 caracteres.");

        RuleFor(v => v.VehCapacidad)
            .MaximumLength(50).WithMessage("La capacidad no debe exceder los 50 caracteres.");
    }
}
