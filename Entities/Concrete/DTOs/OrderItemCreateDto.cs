namespace Entities.Concrete.DTOs
{
    public record OrderItemCreateDto(int ProductId, int Quantity, decimal UnitPrice);
}
