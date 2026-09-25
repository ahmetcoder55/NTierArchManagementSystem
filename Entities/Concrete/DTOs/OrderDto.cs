namespace Entities.Concrete.DTOs
{
    public record OrderDto(int Id, string CustomerId, decimal TotalAmount, DateTime CreatedDate, List<OrderItemDto> OrderItems);
}
