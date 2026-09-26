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
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            if(dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var product = _mapper.Map<Product>(dto);
            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product =await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("The Product is not found.");
            }
            _unitOfWork.Product.Delete(product);
            await _unitOfWork.SaveChangesAsync(); 
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _unitOfWork.Product.GetAllAsync();
            if (products == null)
            {
                throw new Exception("Please you create a one new Product now.");
            }
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product =await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Not found.");
            }
            return _mapper.Map<ProductDto>(product );
        }

        public async Task UpdateAsync(ProductUpdateDto dto)
        {
            var updated=_mapper.Map<Product>(dto);
            _unitOfWork.Product.Update(updated);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
