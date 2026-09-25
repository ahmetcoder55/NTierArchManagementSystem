namespace Entities.Concrete.DTOs
{
    public record ProductUpdateDto(int Id, string Name, string Description, decimal Price, int Stock, int CategoryId);
}
