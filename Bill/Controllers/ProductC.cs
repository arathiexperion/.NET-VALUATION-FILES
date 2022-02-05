using Bill.Models;
using BillRepo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Bill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductC : ControllerBase
    {
        private readonly IBillRepository _BillRepository;
        public ProductC(IBillRepository BillRepository)
        {
            _BillRepository = BillRepository;
        }
        [HttpGet]
        public async Task<List<Product>> GetProduct()
        {
            return await _BillRepository.GetProduct();

        }
        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.AddProduct(product);
                    if (product.CategoryId > 0)
                    {
                        return Ok(product.CategoryId);
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

        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.UpdateProduct(product);

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
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var gst = await _BillRepository.DeleteProduct(id);
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
