using Reto2.Infrastructure.Entities;

namespace Reto2.Infrastructure.Repositories;

public interface IClienteData
{
    Task<List<Cliente>> GetAllClientesAsync();
    Task<List<Pedido>> GetAllPedidosAsync();
    Task<List<Cliente>> GetSearchAsync(string name);
    Cliente GetById(int id);
    Task<Cliente> GetByNameAsync(string name);
    Task<Cliente> GetByEmailAsync(string email);

    Task<int> SaveClienteAsync(Cliente cliente);    
    Task<int> SavePedidoAsync(Pedido pedido);
    bool Update(Pedido pedido, int id);
    bool Delete(int id);
}