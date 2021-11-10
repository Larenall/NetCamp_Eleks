namespace Infrastructure.CryptoAPI.DTO
{
    public class LunarAssetPriceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double? Price { get; set; }
        public double? Price_btc { get; set; }
        public double? Market_cap { get; set; }
    }

}
