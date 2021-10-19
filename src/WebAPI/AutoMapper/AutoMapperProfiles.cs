using AutoMapper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AssetPrice, AssetPriceDTO>();
            CreateMap<AssetData, AssetDataDTO>();
            CreateMap<GroupedUserSubscription, GroupedUserSubscriptionDTO>();
        }
    }
}
