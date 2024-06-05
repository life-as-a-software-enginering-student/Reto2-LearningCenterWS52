namespace Reto2.Infrastructure.Entities;

public class Pedido :ModelBase
{
    public int ClienteId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
    
    public Cliente Cliente { get; set; }
}
