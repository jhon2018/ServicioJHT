using JHT.Logistics.Application.Interfaces.Repositories;
using MediatR;

namespace JHT.Logistics.Application.Features.Clientes.Commands;

public class DeleteClienteCommand : IRequest<bool>
{
    public int CliId { get; set; }
}

public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, bool>
{
    private readonly IClienteRepository _repository;

    public DeleteClienteCommandHandler(IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await _repository.GetByIdAsync(request.CliId, cancellationToken);

        if (cliente == null)
            throw new Exception($"Cliente con ID {request.CliId} no encontrado.");

        await _repository.DeleteAsync(cliente, cancellationToken);

        return true;
    }
}
