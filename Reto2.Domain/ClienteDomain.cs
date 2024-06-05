using Reto2.Infrastructure.Entities;
using Reto2.Infrastructure.Repositories;

namespace Reto2.Domain;

public class ClienteDomain : IClienteDomain
{
    private readonly IClienteData _clienteData;
    
    public ClienteDomain(IClienteData clienteData)
    {
        _clienteData = clienteData;
    }
    
    public async Task<Cliente> CreateClienteAsync(Cliente cliente)
    {
        if (await _clienteData.ExistsByEmailAsync(cliente.Correo))
        {
            throw new Exception("El email ya existe");
        }
        
        return await _clienteData.CreateAsync(cliente);
    }

    public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
    {
        var count = await _clienteData.CountByClienteIdAndDateAsync(pedido.ClienteId, pedido.Fecha);
        if (count >= 5)
        {
            throw new Exception("Un cliente no puede tener más de 5 pedidos al día");
        }

        return await _clienteData.CreateAsync(pedido);
    }

    public async Task<Pedido> UpdatePedidoAsync(Pedido pedido)
    {
        var existingPedido = await _clienteData.GetByIdAsync(pedido.Id);
        if (existingPedido.ClienteId != pedido.ClienteId)
        {
            throw new Exception("No se puede cambiar el cliente de un pedido");
        }
        
        return await _clienteData.UpdateAsync(pedido);
    }
    
    public async Task<int> SaveAsync(Cliente cliente)
    {
        return await _clienteData.SaveAsync(cliente);
    }
}