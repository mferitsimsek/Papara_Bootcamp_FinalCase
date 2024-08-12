namespace Papara.CaptainStore.Domain.DTOs.OrderDTOs
{
    public class OrderBasketDetailsResultDTO
    {
        public bool IsSuccess { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal PointsPercentageTotal { get; set; }
        public decimal MaxEarnedPoints { get; set; }
        public List<string> Errors { get; set; }
    }
}
