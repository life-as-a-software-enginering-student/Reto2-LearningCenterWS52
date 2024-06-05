using AutoMapper;
using Reto2.Infrastructure.Entities;
using Reto2.Presentation.Publishing.Response;

namespace Reto2.Presentation.Mapper;

public class ModelsToResponse:Profile
{
    public ModelsToResponse()
    {
        //Conversión de datos de Pedido a PedidoResponse
        CreateMap<Pedido, PedidoResponse>();
        
        //Conversión de datos de Cliente a ClienteResponse
        CreateMap<Cliente, ClienteResponse>();
    }
}