using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using Models.SyncfusionViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/CourseType")]
    public class CourseTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerType
        [HttpGet]
        public async Task<IActionResult> GetCourseType()
        {
            try
            {
                List<CourseType> Items = await _context.CourseType.ToListAsync();
                int Count = Items.Count();
                return Ok(new { Items, Count });
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }



        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<CourseType> payload)
        {
            CourseType CourseType = payload.value;
            _context.CourseType.Add(CourseType);
            _context.SaveChanges();
            return Ok(CourseType);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<CourseType> payload)
        {
            CourseType CourseType = payload.value;
            _context.CourseType.Update(CourseType);
            _context.SaveChanges();
            return Ok(CourseType);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<CourseType> payload)
        {
            CourseType CourseType = _context.CourseType
                .Where(x => x.CourseTypeId == (int)payload.key)
                .FirstOrDefault();
            _context.CourseType.Remove(CourseType);
            _context.SaveChanges();
            return Ok(CourseType);

        }
    }
}