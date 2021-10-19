using AutoMapper;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.CryptoAPI.DTO;


namespace Infrastructure.CryptoAPI.AutoMapper
{
    public class InfrastructureAutoMapperProfiles : Profile
    {
        public InfrastructureAutoMapperProfiles()
        {
            CreateMap<LunarAssetDataDTO, AssetData>();
            CreateMap<LunarAssetPriceDTO, AssetPrice>();
        }
    }
}
