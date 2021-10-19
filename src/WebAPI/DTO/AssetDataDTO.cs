using System.Collections.Generic;

namespace WebAPI.DTO
{
    public class AssetDataDTO
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double? Price { get; set; }
        public double? Percent_Change_24h { get; set; }
        public double? Percent_Change_7d { get; set; }
        public double? Percent_Change_30d { get; set; }
        public int? alt_rank { get; set; }
        public int? percent_change_24h_rank { get; set; }
    }
}
