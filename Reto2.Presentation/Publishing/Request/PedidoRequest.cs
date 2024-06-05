using System.ComponentModel.DataAnnotations;

namespace Reto2.Presentation.Publishing.Request;

public class PedidoRequest
{
    [Required]
    public int ClientId { get; set; }
    [Required]
    public int CreatedPedido { get; set; }
    public int? UpdatedPedido { get; set; }
    
    [Required]
    public DateTime Fecha { get; set; }
    
    [Required]
    public decimal Monto { get; set; }
}