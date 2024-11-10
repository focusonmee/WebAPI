using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOs;
using FE.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FE.Pages.FootballPlayerPages
{
    public class IndexModel : PageModel
    {
        public IList<FootballPlayerDTO> FootballPlayers { get; set; } = new List<FootballPlayerDTO>();

        [BindProperty(SupportsGet = true)]
        public string SearchName { get; set; } = default;

        public string Message { get; set; } = default;


        public async Task<IActionResult> OnGetAsync()
        {
            if (TempData["Message"] != null) {
                Message = TempData["Message"].ToString();
            }

            try
            {
                var token = HttpContext.Session.GetString("Token");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Login");
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var query = new List<string>();
                    query.Add("$expand=FootballClub");
                    query.Add("$count=true");

                    if (!string.IsNullOrEmpty(SearchName))
                    {
                        query.Add($"filter=contains(Nomination,'{SearchName}') or contains(Achievements,'{SearchName}')");
                    }

                    var queryString = string.Join("&", query);

                    var response = await httpClient.GetAsync($"http://localhost:5098/odata/FootballPlayers?{queryString}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<OdataResponse<FootballPlayerDTO>>(content).Value;
                        FootballPlayers = result;
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
    }
}
