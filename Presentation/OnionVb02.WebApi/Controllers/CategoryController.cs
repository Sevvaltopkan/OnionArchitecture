using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionVb02.Application.DTOClasses;
using OnionVb02.Application.ManagerInterfaces;
using OnionVb02.WebApi.RequestModels.Categories;
using OnionVb02.WebApi.ResponseModels.Categories;

namespace OnionVb02.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;
        

        public CategoryController(ICategoryManager categoryManager,IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            List<CategoryDto> values = await _categoryManager.GetAllAsync();
            return Ok(_mapper.Map<List<CategoryResponseModel>>(values));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            CategoryDto value = await _categoryManager.GetByIdAsync(id);
            return Ok(_mapper.Map<CategoryResponseModel>(value));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestModel model)
        {
            CategoryDto category = _mapper.Map<CategoryDto>(model);
            await _categoryManager.CreateAsync(category);
            return Ok("Veri eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequestModel model)
        {
            CategoryDto category = _mapper.Map<CategoryDto>(model);
            await _categoryManager.UpdateAsync(category);
            return Ok("Veri güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            string result = await _categoryManager.SoftDeleteAsync(id);
            return Ok(result);
        }

    }
}
