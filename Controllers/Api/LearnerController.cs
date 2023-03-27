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
    [Route("api/Learner")]
    public class LearnerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LearnerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            List<Learner> Items = await _context.Learner.ToListAsync();
            int Count = Items.Count();
            return Ok(new { Items, Count });
        }


        [HttpPost("[action]")]
        public IActionResult Insert([FromBody]CrudViewModel<Learner> payload)
        {
            Learner Learner = payload.value;
            _context.Learner.Add(Learner);
            _context.SaveChanges();
            return Ok(Learner);
        }

        [HttpPost("[action]")]
        public IActionResult Update([FromBody]CrudViewModel<Learner> payload)
        {
            Learner Learner = payload.value;
            _context.Learner.Update(Learner);
            _context.SaveChanges();
            return Ok(Learner);
        }

        [HttpPost("[action]")]
        public IActionResult Remove([FromBody]CrudViewModel<Learner> payload)
        {
            Learner Learner = _context.Learner
                .Where(x => x.LearnerId == (int)payload.key)
                .FirstOrDefault();
            _context.Learner.Remove(Learner);
            _context.SaveChanges();
            return Ok(Learner);

        }
    }
}