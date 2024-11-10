using System;
using System.ComponentModel.DataAnnotations;

namespace FE.DTO
{
    public class FootballPlayerDTO
    {
        [Required(ErrorMessage = "FootballPlayerId is required.")]
        public string FootballPlayerId { get; set; } = null!;

        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^(?:A[a-zA-Z0-9@#]*\s?)+$", ErrorMessage = "Each word in the Full Name must start with the letter 'A' and can only contain letters, numbers, or the symbols '@' and '#'.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Achievements are required.")]
        [StringLength(100, MinimumLength = 9, ErrorMessage = "Achievements must be between 9 and 100 characters.")]
        public string Achievements { get; set; } = null!;

        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Player Experiences are required.")]
        [StringLength(400, ErrorMessage = "Player Experiences cannot exceed 400 characters.")]
        public string PlayerExperiences { get; set; } = null!;

        [Required(ErrorMessage = "Nomination is required.")]
        [StringLength(400, MinimumLength = 9, ErrorMessage = "Nomination must be between 9 and 400 characters.")]
        public string Nomination { get; set; } = null!;


        public string? FootballClubId { get; set; }

        public virtual FootballClubDTO? FootballClub { get; set; }
    }
}
