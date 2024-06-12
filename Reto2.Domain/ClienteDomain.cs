using System.Data;
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

    public async Task<int> SaveClienteAsync(Cliente cliente)
    {
        var existingCliente = await _clienteData.GetByEmailAsync(cliente.Correo); 
        if (existingCliente != null) 
        { 
            throw new Exception("Cliente with this email already exists"); 
        }
        var result = await _clienteData.SaveClienteAsync(cliente); 
        return result; 
    }
    
    public async Task<int> SaveAsync(Cliente cliente, Pedido pedido)
    {
        var existingPedido = await _clienteData.GetByNameAsync(cliente.Nombre);
        if (existingPedido == null) throw new DuplicateNameException("Cliente Not Found");
        
        var total = (await _clienteData.GetAllPedidosAsync()).Count;
        if (total > 5) throw new ConstraintException("Max pedidos reached " + 5); 
        
        return await _clienteData.SavePedidoAsync(pedido);
    }
    
    public bool UpdatePedidoAsync(Pedido pedido)
    {
        var existingPedido = _clienteData.GetById(pedido.Id);
        if (existingPedido == null) throw new DuplicateNameException("Pedido Not Found");
        if (existingPedido.Id != pedido.ClienteId)
        {
            throw new ConstraintException("The clientId can not be change");
        }
        return _clienteData.Update(pedido, pedido.Id);
    }
}