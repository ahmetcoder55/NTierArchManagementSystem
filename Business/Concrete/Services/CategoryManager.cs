using AutoMapper;
using Business.Abstract.Services;
using DataAccess.Abstract.UnitOfWorks;
using Entities.Concrete;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Services
{
    public class CategoryManager : ICategoryService
    {
        //Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryCreateDto> CreateAsync(CategoryCreateDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var category = _mapper.Map<Category>(dto);
            //if (category == null)
            //{

            //}
            if (category.Name.Length > 5||category.Name.Length<50)
            {
                await _unitOfWork.Category.AddAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return dto;
            }
            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category is null)
            {
                throw new Exception("Not Found.");
            }
            _unitOfWork.Category.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _unitOfWork.Category.GetAllAsync());
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(CategoryUpdateDto dto)
        {
            _unitOfWork.Category.Update(_mapper.Map<Category>(dto));
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
