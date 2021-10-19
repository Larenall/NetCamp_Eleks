using AutoMapper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.AutoMapper
{
    public class APIAutoMapperProfiles : Profile
    {
        public APIAutoMapperProfiles()
        {
            CreateMap<AssetPrice, AssetPriceDTO>();
            CreateMap<AssetData, AssetDataDTO>();
            CreateMap<GroupedUserSubscription, GroupedUserSubscriptionDTO>();
        }
    }
}
