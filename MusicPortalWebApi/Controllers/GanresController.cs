using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace MusicPortalWebApi.Controllers
{

    [ApiController]
    [Route("api/ganres")]
    public class GanresController : ControllerBase
    {
        private readonly MusicClubContext context;

        public GanresController(MusicClubContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGanres()
        {
            return await context.Genres.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGanre(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var ganre = await context.Genres.FindAsync(id);
            if (ganre == null)
            {
                return NotFound();
            }
            return new ObjectResult(ganre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> PostGanre(Genre genre)
        {
            if (ModelState.IsValid && genre.Genre_name != "" && genre.Genre_name != string.Empty)
            {
                var g = new Genre();
                g.Genre_name = genre.Genre_name;
                await context.Genres.AddAsync(g);
                await context.SaveChangesAsync();
                return Ok(true);
            }
            return new ObjectResult(new Genre() { Genre_name = genre.Genre_name });
        }


        // GET: Ganres/Edit/5
        [HttpPut]
        public async Task<ActionResult<Genre>> Edit(Genre genre)
        {
            if (genre == null && genre.Id == 0)
            {
                return new ObjectResult(genre);
            }
            var editgenre = await context.Genres.FindAsync(genre.Id);
            editgenre.Genre_name = genre.Genre_name;
            await context.SaveChangesAsync();
            return Ok(true);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var genre = await context.Genres.FindAsync(id);
            
            if (genre == null)
            {
                return new ObjectResult(genre);
            }
            context.Remove(genre);
            await context.SaveChangesAsync();

            return Ok(true);
        }

       
    }
}
