using AutoMapper;
using eWallet.Core.Dtos;
using eWallet.Core.Entities.eWallet;
using eWallet.Core.Repositories;
using eWallet.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eWallet.API.Controllers
{
    public class CategoryController : BaseApiController
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories
            (
            [FromQuery] TransactionType type
            )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var spec = new CategoriesWithUserSpecification(userId,type);
            var categories = await _unitOfWork.Repository<Category>()
                .GetAllWithSpecAsync(spec);

            var categoriesMapper = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoriesMapper);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryDto categoryDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                AppUserId = userId
            };

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.Complete();

            categoryDto.Id = category.Id; 
            return Ok(categoryDto);
        }
    }
}
