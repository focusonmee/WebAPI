using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FE.DTO;
using Newtonsoft.Json;
using System.Text;

namespace FE.Pages.FootballPlayerPages
{
    public class EditModel : PageModel
    {
        public List<FootballClubDTO> FootballClubs { get; set; } = new List<FootballClubDTO>();

        [BindProperty]
        public FootballPlayerDTO FootballPlayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
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

                var currentResponse = await httpClient.GetAsync($"http://localhost:5098/api/FootballPlayers/{id}");
                 
                if(currentResponse.IsSuccessStatusCode)
                {
                    var content = await currentResponse.Content.ReadAsStringAsync();
                    FootballPlayer = JsonConvert.DeserializeObject<FootballPlayerDTO>(content);
                }
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
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

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(FootballPlayer);
                var content = new StringContent(json,Encoding.UTF8,"application/json");
                
                var response = await httpClient.PutAsync($"http://localhost:5098/api/FootballPlayers/{FootballPlayer.FootballPlayerId}",content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    //FootballClubs = JsonConvert.DeserializeObject<FootballClubDTO>(responseData);

                    TempData["Message"] = "Update successfully";
                    return RedirectToPage("/FootballPlayerPages/Index");

                }
                else
                {
                    TempData["Message"] = "Update Failed!";
                    return Page();
                }
                return Page();
            }
            return RedirectToPage("/Index");

        }
    }
}
