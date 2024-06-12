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

    public async Task<List<Cliente>> GetAllClientesAsync()
    {
        var result = await _reto2Context.Clientes
            .Where(c=>c.IsActive)
            .Include(c => c.Pedidos)
            .ToListAsync();
        return result;
    }
    public async Task<List<Pedido>> GetAllPedidosAsync()
    {
        var result = await _reto2Context.Pedidos
            .Where(p=>p.IsActive)
            .ToListAsync();
        return result;
    }

    public async Task<List<Cliente>> GetSearchAsync(string name)
    {
        var result = await _reto2Context.Clientes
            .Where(c => c.Nombre.Contains(name) && c.IsActive)
            .Include(c=>c.Pedidos)
            .ToListAsync();
        return result;
    }

    public Cliente GetById(int id)
    {
        var result=_reto2Context.Clientes
            .Where(c=>c.Id==id && c.IsActive)
            .Include(c => c.Pedidos)
            .FirstOrDefault();

        if (result==null)
        {
            throw new Exception("Cliente no encontrado");
        }
        
        return result;
    }
    
    public async Task<Cliente> GetByNameAsync(string name)
    {
        var result = await _reto2Context.Clientes
            .Where(c => c.Nombre == name && c.IsActive)
            .Include(c => c.Pedidos)
            .FirstOrDefaultAsync();
        
        if (result == null)
        {
            throw new Exception("Cliente no encontrado");
        }

        return result;
    }

    public async Task<Cliente> GetByEmailAsync(string email)
    {
        var result = await _reto2Context.Clientes
            .Where(c => c.Correo== email && c.IsActive)
            .Include(c => c.Pedidos)
            .FirstOrDefaultAsync();
        
        if (result == null)
        {
            throw new Exception("Cliente no encontrado");
        }

        return result;
    }

    public async Task<int> SaveClienteAsync(Cliente cliente)
    {
        using (var transaction = await _reto2Context.Database.BeginTransactionAsync())
        {
            try
            {
                cliente.IsActive = true;
                _reto2Context.Clientes.Add(cliente);
                await _reto2Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        } 
        return cliente.Id;
    }
    
    public async Task<int> SavePedidoAsync(Pedido pedido)
    {
        using (var transaction = await _reto2Context.Database.BeginTransactionAsync())
        {
            try
            {
                pedido.IsActive = true;
                _reto2Context.Pedidos.Add(pedido);
                await _reto2Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }
        return pedido.Id;
    }

    public bool Update(Pedido pedido, int id)
    {
        var existingPedido = _reto2Context.Pedidos
            .Where(p => p.Id == id)
            .FirstOrDefault();

        if (existingPedido == null)
        {
            throw new Exception("Pedido not found");
        }

        existingPedido.Fecha = pedido.Fecha;
        existingPedido.Monto = pedido.Monto;

        _reto2Context.Pedidos.Update(existingPedido);
        _reto2Context.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var existingPedifo = _reto2Context.Pedidos
            .Where(t => t.Id == id)
            .FirstOrDefault();

        // _learningCenterContext.Tutorials.Remove(exitingTutorial);
        existingPedifo.IsActive = false;

        _reto2Context.Pedidos.Update(existingPedifo);
        _reto2Context.SaveChanges();
        return true;
    }
}
