using AutoMapper;
using Reto2.Infrastructure.Entities;
using Reto2.Presentation.Publishing.Request;

namespace Reto2.Presentation.Mapper;

public class ResquestToModels:Profile
{
    public ResquestToModels()
    {
        //Conversión de datos de ClienteRequest a Cliente
        CreateMap<ClienteRequest, Cliente>();
        
        //Conversión de datos de PedidoRequest a Pedido
        CreateMap<PedidoRequest, Pedido>();
    }
}