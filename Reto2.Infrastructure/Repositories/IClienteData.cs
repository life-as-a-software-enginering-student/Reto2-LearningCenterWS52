using Reto2.Infrastructure.Entities;

namespace Reto2.Infrastructure.Repositories;

public interface IClienteData
{
    Task<List<Cliente>> GetAllAsync();
    Task<List<Cliente>> GetSearchAsync(string name);
    
    Cliente GetById(int id);
    
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Pedido> CreateAsync(Pedido pedido);
    
    Task<int> CountByClienteIdAndDateAsync(int clienteId, DateTime date);

    Task<bool> ExistsByEmailAsync(string email);
    
    Task<Pedido> UpdateAsync(Pedido pedido);
    Task<Pedido> GetByIdAsync(int id);
    Task<int> SaveAsync(Cliente cliente);
}