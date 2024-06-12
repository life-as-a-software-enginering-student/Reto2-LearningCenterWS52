using Reto2.Infrastructure.Entities;

namespace Reto2.Domain;

public interface IClienteDomain 
{ 
    bool UpdatePedidoAsync(Pedido pedido);
    Task<int> SaveClienteAsync(Cliente cliente);
    Task<int> SaveAsync(Cliente cliente, Pedido pedido);
}
    