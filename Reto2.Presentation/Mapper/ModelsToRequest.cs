using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Reto2.Infrastructure.Entities;
using Reto2.Presentation.Publishing.Request;

namespace Reto2.Presentation.Mapper;

public class ModelsToRequest : Profile
{
    public ModelsToRequest()
    {
        //Conversión de datos de Pedido a PedidoRequest
        CreateMap<Pedido, PedidoRequest>();
        
        //Conversión de datos de Cliente a ClienteRequest
        CreateMap<Cliente, ClienteRequest>();
    }
}