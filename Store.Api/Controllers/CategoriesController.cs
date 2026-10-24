using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Store.Application.Command;
using Store.Application.Query;
using Store.Domain.Entities;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ISender sender) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        { 
            var result = await sender.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        { 
            var result = await sender.Send(new GetCategoryByIdQuery(id));

            if (result is null)
            {
                return NotFound(new
                {
                    message = "Category not found."
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            var result = await sender.Send(new AddCategoryCommand(category));
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var result = await sender.Send(new UpdateCategoryCommand(category));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        { 
            try
            {
                await sender.Send(new DeleteCategoryCommand(id));

                return Ok(new
                {
                    message = "Category deleted successfully."
                });
            }
            catch (SqlException ex) when (ex.Number == 50001)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
            catch (SqlException ex) when (ex.Number == 50002)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

    }
}
