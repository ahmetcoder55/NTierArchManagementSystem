using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryCreateDto> CreateAsync(CategoryCreateDto dto);
        Task UpdateAsync(CategoryUpdateDto dto);
        Task DeleteAsync(int id);
    }
}
