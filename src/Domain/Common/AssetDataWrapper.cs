using System.Collections.Generic;

namespace Domain.Common
{
    public class AssetDataWrapper
    {
        public object config { get; set; }
        public object usage { get; set; }
        public List<AssetData> data { get; set; }

    }
}
