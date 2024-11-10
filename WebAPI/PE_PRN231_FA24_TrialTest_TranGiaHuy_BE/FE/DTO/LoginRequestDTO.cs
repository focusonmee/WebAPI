using System.ComponentModel.DataAnnotations;

namespace PE_PRN231_FA24_TrialTest_TranGiaHuy_OdataAPI
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage ="Email Is not empty")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is not empty")]
        public string Password { get; set; }
    }

}
