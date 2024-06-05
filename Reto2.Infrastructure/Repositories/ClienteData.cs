using Microsoft.EntityFrameworkCore;
using Reto2.Infrastructure.Contexts;
using Reto2.Infrastructure.Entities;

namespace Reto2.Infrastructure.Repositories;

public class ClienteData : IClienteData
{
    private readonly Reto2Context _reto2Context;

    public ClienteData(Reto2Context reto2Context)
    {
        _reto2Context = reto2Context;
    }
    
    public async Task<List<Cliente>> GetAllAsync()
    {
        var result = await _reto2Context.Clientes
            .Include(c => c.Pedidos)
            .ToListAsync();
        return result;
    }

    public async Task<List<Cliente>> GetSearchAsync(string name)
    {
        var result = await _reto2Context.Clientes
            .Where(c => c.Nombre.Contains(name))
            .Include(c=>c.Pedidos)
            .ToListAsync();
        return result;
    }

    public Cliente GetById(int id)
    {
        var result=_reto2Context.Clientes
            .Where(c=>c.Id==id)
            .Include(c => c.Pedidos)
            .FirstOrDefault();

        if (result==null)
        {
            throw new Exception("Cliente no encontrado");
        }
        
        return result;
    }

    public async Task<Cliente> CreateAsync(Cliente clienteData)
    {
         _reto2Context.Clientes.Add(clienteData);
         await _reto2Context.SaveChangesAsync();
         return clienteData;
    }

    public async Task<int> CountByClienteIdAndDateAsync(int clienteId, DateTime date)
    {
        var count = await _reto2Context.Pedidos
            .Where(p => p.ClienteId == clienteId && p.Fecha == date)
            .CountAsync();
        return count;
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        return _reto2Context.Clientes.AnyAsync(c => c.Correo == email);
    }
    
    public async Task<Pedido> UpdateAsync(Pedido pedido)
    {
        var existingPedido = await _reto2Context.Pedidos
            .Where(p => p.Id == pedido.Id)
            .FirstOrDefaultAsync();
        
        if (existingPedido == null)
        {
            throw new Exception("Pedido no encontrado");
        }

        existingPedido.ClienteId = pedido.ClienteId;
        existingPedido.Fecha = pedido.Fecha;
        
        await _reto2Context.SaveChangesAsync();
        return existingPedido;
    }
    
    public async Task<Pedido> GetByIdAsync(int id)
    {
        var result = await _reto2Context.Pedidos
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        
        if (result == null)
        {
            throw new Exception("Pedido no encontrado");
        }

        return result;
    }
    
    public async Task<Pedido> CreateAsync(Pedido pedido)
    {
        _reto2Context.Pedidos.Add(pedido);
        await _reto2Context.SaveChangesAsync();
        return pedido;
    }
    
    public async Task<int> SaveAsync(Cliente cliente)
    {
        _reto2Context.Clientes.Add(cliente);
        await _reto2Context.SaveChangesAsync();
        return cliente.Id;
    }
}