using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Conductores.Commands;

public class DeleteConductorCommand : IRequest<bool>
{
    public int ConId { get; set; }
}

public class DeleteConductorCommandHandler : IRequestHandler<DeleteConductorCommand, bool>
{
    private readonly IConductorRepository _repository;

    public DeleteConductorCommandHandler(IConductorRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteConductorCommand request, CancellationToken cancellationToken)
    {
        var conductor = await _repository.GetByIdAsync(request.ConId, cancellationToken);

        if (conductor == null)
            throw new Exception($"Conductor con ID {request.ConId} no encontrado.");

        await _repository.DeleteAsync(conductor, cancellationToken);

        return true;
    }
}
