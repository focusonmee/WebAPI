
namespace FE.DTO
{
    public class FootballClubDTO
    {
        public string FootballClubId { get; set; } = null!;

        public string ClubName { get; set; } = null!;

        public string ClubShortDescription { get; set; } = null!;

        public string SoccerPracticeField { get; set; } = null!;

        public string Mascos { get; set; } = null!;

        public virtual ICollection<FootballPlayerDTO> FootballPlayers { get; set; } = new List<FootballPlayerDTO>();
    }
}
