using System;
using System.Collections.Generic;

namespace ShopInsights.Model
{
    public class OrderModel
    {
        readonly List<LineModel> _lines = new List<LineModel>();

        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal SalesPrice
        {
            get
            {
                if (DiscountAmount.HasValue)
                {
                    return TotalPrice - DiscountAmount.Value;
                }

                return TotalPrice;
            }
        }

        public IEnumerable<LineModel> Lines => _lines;

        public void AddLines(params LineModel[] lines)
        {
            throw new NotImplementedException();
        }

        public PositionModel Position { get; set; } = new PositionModel();
    }
}
