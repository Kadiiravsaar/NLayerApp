using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
  
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _service;

        private readonly IMapper _mapper;

        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _service = productService;
        }

        [HttpGet("GetProductWithCategory")] // [HttpGet("[action]")] aynı şeydir
        public async Task<IActionResult> GetProductWithCategory()
        {
            // Generic olmadığı için Dtoya dönüşüm yapmıyorum service de olur zaten 
            return CreateActionResult(await _service.GetProductWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Generic olduğu için Dtoya dönüşüm yapıyorum
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products);
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            //return Ok(CustomResponseDto<ProductDto>.Success(200, productDto));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            //return Ok(CustomResponseDto<ProductDto>.Success(productsDto));
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productUpdateDto));
            //return Ok(CustomResponseDto<NoContentDto>.Success(204));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")] // delete api/product/5
        public async Task<IActionResult> Delete(int id)
        {   
            var product = await _service.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest();

            }
            await _service.RemoveAsync(product);
            //return Ok(CustomResponseDto<NoContentDto>.Success(204));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
