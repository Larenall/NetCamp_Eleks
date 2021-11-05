using System.Collections.Generic;

namespace Infrastructure.CryptoAPI.DTO
{
    class LunarAssetPriceWrapperDTO
    {
        public object config { get; set; }
        public List<LunarAssetPriceDTO> data { get; set; }

    }
}
