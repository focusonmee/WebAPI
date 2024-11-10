using BOs;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DAOs
{
    public class FootballPlayerDAO
    {
        private static FootballPlayerDAO instance = null;
        private readonly EnglishPremierLeague2024DbContext context;

        private FootballPlayerDAO()
        {
            context = new EnglishPremierLeague2024DbContext();
        }

        public static FootballPlayerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FootballPlayerDAO();
                }
                return instance;
            }

        }

        public async Task<List<FootballPlayer>> GetPlayers()
        {
            var players  = await context.FootballPlayers.Include(p=>p.FootballClub).ToListAsync();
            return players;
        }

        public async Task<FootballPlayer> GetPlayerById(string id)
        {
            var player = await context.FootballPlayers
                                      .Where(p => p.FootballPlayerId == id)
                                      .Select(p => new FootballPlayer
                                      {
                                          FootballPlayerId = p.FootballPlayerId,
                                          FullName = p.FullName,
                                          Achievements = p.Achievements,
                                          Birthday = p.Birthday,
                                          PlayerExperiences = p.PlayerExperiences,
                                          Nomination = p.Nomination,
                                          FootballClubId= p.FootballClubId,
                                          FootballClub = new FootballClub
                                          {
                                              FootballClubId = p.FootballClub.FootballClubId,
                                              ClubName = p.FootballClub.ClubName,
                                              ClubShortDescription = p.FootballClub.ClubShortDescription,
                                              SoccerPracticeField = p.FootballClub.SoccerPracticeField,
                                              Mascos = p.FootballClub.Mascos
                                          }
                                      })
                                      .FirstOrDefaultAsync();

            return player;
        }



        public async Task<FootballPlayer> AddPlayer(FootballPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Player cannot be null");
            }

            try
            {
                using (var context = new EnglishPremierLeague2024DbContext())
                {
                    await context.FootballPlayers.AddAsync(player);
                    await context.SaveChangesAsync();
                    return player;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
        }



        public async Task<FootballPlayer> UpdatePlayer(FootballPlayer player)
        {
            var playerToUpdate  = await context.FootballPlayers.FirstOrDefaultAsync(p => p.FootballPlayerId==player.FootballPlayerId);
            if(playerToUpdate == null)
            {
                throw new Exception("Player not found");
            }
            playerToUpdate.FullName = player.FullName;
            playerToUpdate.Achievements = player.Achievements;
            playerToUpdate.Birthday = player.Birthday;
            playerToUpdate.PlayerExperiences = player.PlayerExperiences;
            playerToUpdate.Nomination = player.Nomination;
            playerToUpdate.FootballClubId = player.FootballClubId;

             context.Update(playerToUpdate);

            await context.SaveChangesAsync();
            return playerToUpdate;
        }

        public async Task<FootballPlayer> DeletePlayer (string id)
        {
            using (var context = new EnglishPremierLeague2024DbContext())
            {
                var playerToDelete = await context.FootballPlayers.FirstOrDefaultAsync(p => p.FootballPlayerId.Equals(id));
                if (playerToDelete == null)
                {
                    throw new Exception("Player not found");
                }
                context.FootballPlayers.Remove(playerToDelete);
                await context.SaveChangesAsync();
                return playerToDelete;
            }
        }

        public async Task<List<FootballClub>> GetFootballClubs()
        {
            var clubs = await context.FootballClubs.ToListAsync();
            return clubs;
        }

    }
}
