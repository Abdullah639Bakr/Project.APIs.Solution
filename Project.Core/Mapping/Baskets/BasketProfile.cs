using AutoMapper;
using Project.Core.Dtos.Baskets;
using Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
        }
    }
}
