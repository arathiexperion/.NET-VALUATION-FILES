using Bill.Models;
using Bill.viewmodel;
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
    public class gstC : ControllerBase
    {
        private readonly IBillRepository _BillRepository;
        public gstC(IBillRepository BillRepository)
        {

            _BillRepository = BillRepository;

        }
        [HttpGet("gstcatpro")]

        public async Task<List<productgst>> Getproductgstdetails()
        {
            return await _BillRepository.Getproductgstdetails();

        }
        [HttpGet]
        public async Task<List<Gst>> GetGst()
        {
            return await _BillRepository.GetGst();

        }
        [HttpPost]
        [Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> AddGst([FromBody] Gst gst)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.AddGst(gst);
                    if (gst.GstId > 0)
                    {
                        return Ok(gst.GstId);
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

        public async Task<ActionResult> UpdateGst([FromBody] Gst gst)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _BillRepository.UpdateGst(gst);

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
        public async Task<IActionResult> DeleteGst(int? id)
        {
            int result = 0;
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var gst = await _BillRepository.DeleteGst(id);
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
