using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using FE.DTO;
using System.Text;

namespace FE.Pages.FootballPlayerPages
{
    public class CreateModel : PageModel
    {
        public List<FootballClubDTO> FootballClubs { get; set; } = new List<FootballClubDTO>();

        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync($"http://localhost:5098/api/FootballClubs");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    FootballClubs = JsonConvert.DeserializeObject<List<FootballClubDTO>>(content);

                }
                return Page();
            }
        }
        [BindProperty]
        public FootballPlayerDTO FootballPlayer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToPage("/Index");
            }
            using (var httpClient = new HttpClient()) {
                httpClient.DefaultRequestHeaders.Authorization =
                  new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                 var json = JsonConvert.SerializeObject(FootballPlayer);
                 var content = new StringContent(json, Encoding.UTF8,"application/json");        
                 var response = await httpClient.PostAsync("http://localhost:5098/api/FootballPlayers", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Create successfully!";
                    return RedirectToPage("/FootballPlayerPages/Index");
                }
                else
                {
                    TempData["Message"] = "Create failed!";
                    return await OnGet();
                }
            }
        }
    }
}
