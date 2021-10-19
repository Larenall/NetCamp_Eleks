using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CryptoAPI.DTO
{
    class LunarAssetDataWrapperDTO
    {
        public object config { get; set; }
        public object usage { get; set; }
        public List<LunarAssetDataDTO> data { get; set; }
        
    }
}
