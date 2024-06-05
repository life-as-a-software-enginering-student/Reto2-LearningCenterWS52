using System.ComponentModel.DataAnnotations;

namespace Reto2.Presentation.Publishing.Request;

public class ClienteRequest
{
    [Required]
    public string Nombre { get; set; }
    
    [Required]
    //[EmailAddress]
    public string Correo { get; set; }
}