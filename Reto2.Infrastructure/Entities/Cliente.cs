namespace Reto2.Infrastructure.Entities;

public class Cliente: ModelBase
{
    public string Nombre { get; set; }
    public string Correo { get; set; }
    
    public List<Pedido> Pedidos { get; set; }
}