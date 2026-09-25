namespace Entities.Concrete.DTOs
{
    public record OrderCreateDto(string CustomerId, List<OrderItemCreateDto> OrderItems);
}
