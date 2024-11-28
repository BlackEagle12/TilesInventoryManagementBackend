using Core;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoriesController
            (
                ICategoriesService categoriesService
            )
        {
            _categoriesService = categoriesService;
        }

        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            return Ok(
                        new ApiResponse(
                            StatusCodes.Status200OK,
                            await _categoriesService.GetCategoriesDDAsync()
                        )
                    );
        }
    }
}
