namespace ShopInsights.Model
{
    public class LineModel
    {
        public string Sku { get; set; }

        public string Name { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public decimal Vat { get; set; }

        public LineModel Parent { get; set; }
    }
}