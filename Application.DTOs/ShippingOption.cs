namespace Application.DTOs
{
    public class ShippingOption
    {
        public int ShippingOptionId { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public EstimatedDelivery EstimatedDelivery { get; set; }
    }
}