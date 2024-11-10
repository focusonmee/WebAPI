using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PE_PRN231_FA24_TrialTest_TranGiaHuy_OdataAPI;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class IndexModel : PageModel
{
    // Đảm bảo LoginRequestDTO được khai báo đúng
    [BindProperty]
    public LoginRequestDTO LoginRequestDTO { get; set; }

    public string Message { get; set; } // Để hiển thị thông báo lỗi nếu có

    // Hàm OnPostAsync để xử lý đăng nhập
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            // Tạo HttpClient để gửi yêu cầu POST
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync("http://localhost:5098/api/PremierLeagueAccounts/Login", LoginRequestDTO);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc phản hồi từ API
                    var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();

                    // Lưu Token, Role và AccountId vào Session
                    HttpContext.Session.SetString("Token", result.Token);
                    HttpContext.Session.SetString("Role", result.Role);
                    HttpContext.Session.SetString("AccountId", result.AccountId);

                    // Chuyển hướng đến trang Index của FootballPlayerPages sau khi đăng nhập thành công
                    return RedirectToPage("/FootballPlayerPages/Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    if (errorContent.Length != 0)
                    {
                        throw new Exception(errorContent);
                    }
                    else
                    {
                        throw new Exception("An error occurred during login.");
                    }
                }
            }
        }
        catch (Exception e)
        {
            // Hiển thị thông báo lỗi nếu có
            Message = e.Message;
            return Page();
        }
    }
}
