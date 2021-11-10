using System.Collections.Generic;

namespace Infrastructure.CryptoAPI.DTO
{
    public class LunarAssetPriceWrapperDTO
    {
        public object config { get; set; }
        public List<LunarAssetPriceDTO> data { get; set; }

    }
}
