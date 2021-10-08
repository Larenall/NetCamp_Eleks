using System.Collections.Generic;

namespace Domain.DTO
{
    public class AssetDataWrapperDTO
    {
        public object config { get; set; }
        public object usage { get; set; }
        public List<AssetDataDTO> data { get; set; }

    }
}
