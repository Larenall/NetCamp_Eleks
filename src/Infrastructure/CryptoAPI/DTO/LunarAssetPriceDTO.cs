namespace Infrastructure.CryptoAPI.DTO
{
    public class LunarAssetPriceDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public float? price { get; set; }
        public float? price_btc { get; set; }
        public float? market_cap { get; set; }
    }

}
