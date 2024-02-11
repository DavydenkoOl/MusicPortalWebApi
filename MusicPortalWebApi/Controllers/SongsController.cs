using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebApi.Models;
namespace MusicPortalWebApi.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public class SongsController : ControllerBase
	{
		private readonly MusicClubContext context;
		private readonly IWebHostEnvironment _appEnvironment;


		public SongsController(MusicClubContext context, IWebHostEnvironment _appEnvironment)
		{
			this.context = context;
			this._appEnvironment = _appEnvironment;
		}


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MusicClip>>> GetClips()
        {
            return await context.Clips.ToListAsync();
        }



        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [RequestSizeLimit(2147483648)]
        public async Task<ActionResult<MusicClip>> Create(ModelMusicClip clip)
        {
            if (ModelState.IsValid)
            {
                string Path_Video = "/video/" + clip.attachment.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + Path_Video, FileMode.Create))
                {
                    await clip.attachment.CopyToAsync(fileStream);
                }
               

                var newClip = new MusicClip()
                {
                    Title = clip.Title,
                    Artist = clip.Artist,
                    Path_Video = Path_Video,
                    Id_user = clip.Id_user,
                    ReleaseDate = DateTime.Parse(clip.ReleaseDate),
                    Description = clip.Description
                };
                foreach(var g in clip.Genre)
                {
                    newClip.Genre += context.Genres.Find(int.Parse(g)).Genre_name + ", ";
                }
                await context.Clips.AddAsync(newClip);
                await context.SaveChangesAsync();
                return Ok(true);
            }
            return Ok(false);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var clip = await context.Clips.FindAsync(id);

            if (clip == null)
            {
                return new ObjectResult(clip);
            }

            context.Clips.Remove(clip);
            await context.SaveChangesAsync();

            return Ok(true);
        }

    }
}
