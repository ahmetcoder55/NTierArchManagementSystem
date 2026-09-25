namespace Entities.Concrete.DTOs
{
    public record OrderItemDto(int Id, int ProductId, string ProductName, int Quantity, decimal UnitPrice);
}
