using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using FE.DTO;

namespace FE.Pages.FootballPlayerPages
{
    public class DeleteModel : PageModel
    {

        [BindProperty]
        public FootballPlayerDTO FootballPlayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {

                var token = HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Login");
                }
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.DeleteAsync($"http://localhost:5098/api/FootballPlayers/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Delete successfully";
                    return RedirectToPage("/FootballPlayerPages/Index");
                }
                else
                {
                    return Page();

                }

            }


        }
    }
}
