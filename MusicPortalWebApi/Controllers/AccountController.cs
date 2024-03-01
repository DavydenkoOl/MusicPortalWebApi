using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortalWebApi.Filters;
using MusicPortalWebApi.Models;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortalWebApi.Controllers
{
    [Culture]
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
	{

		private readonly MusicClubContext context;

		public AccountController(MusicClubContext context)
		{
			this.context = context;
		}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return new ObjectResult(user);
        }

        [HttpPut]
        public async Task<ActionResult<Users>> Edit(Users user)
        {
            if (user == null && user.Id == 0)
            {
                return new ObjectResult(user);
            }
            var edituser = await context.Users.FindAsync(user.Id);
            edituser.IsСonfirm = true;
            await context.SaveChangesAsync();
            return Ok(true);
        }

        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Club/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }


    }
}
