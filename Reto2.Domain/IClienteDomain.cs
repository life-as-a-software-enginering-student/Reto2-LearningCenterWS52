using Reto2.Infrastructure.Entities;

namespace Reto2.Domain;

public interface IClienteDomain 
{ 
    Task<Cliente> CreateClienteAsync(Cliente cliente);
    Task<Pedido> CreatePedidoAsync(Pedido pedido); 
    Task<Pedido> UpdatePedidoAsync(Pedido pedido);
    
    Task<int> SaveAsync(Cliente cliente);
}
    