using AutoMapper;
using PurchaseOrder.Domain.DTO;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Application.Shared;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CustomerDto, Customer>();

        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductDto, Product>();

        CreateMap<PuchaseOrder, PuchaseOrderDto>().ReverseMap();
        CreateMap<PuchaseOrderDto, PuchaseOrder>();

        CreateMap<PurchaseOrderItem, PurchaseOrderItemDto>().ReverseMap();
        CreateMap<PurchaseOrderItemDto, PurchaseOrderItem>();

        CreateMap<State, StateDto>().ReverseMap();
        CreateMap<StateDto, State>();
    }
}
