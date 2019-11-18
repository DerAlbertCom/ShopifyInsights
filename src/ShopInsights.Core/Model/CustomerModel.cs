namespace ShopInsights.Model
{
    public class CustomerModel
    {
        public CustomerModel()
        {
        }

        public string Id { get; set; }
        public PositionModel Position { get; set; } = new PositionModel();
    }
}