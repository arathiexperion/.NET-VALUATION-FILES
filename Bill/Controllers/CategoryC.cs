using Bill.Models;
using BillRepo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryC : ControllerBase
    {
        private readonly IBillRepository _BillRepository;
        public CategoryC(IBillRepository BillRepository)
        {

            _BillRepository = BillRepository;

        }
        [HttpGet]
        public async Task<List<Category>> GetCategory()
        {
            return await _BillRepository.GetCategory();

        }
        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.AddCategory(category);
                    if (category.CategoryId > 0)
                    {
                        return Ok(category.CategoryId);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult> UpdateCategory([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.UpdateCategory(category);

                    return Ok();

                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var employee = await _BillRepository.DeleteCategory(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
