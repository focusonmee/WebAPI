using BOs;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class FootballPlayerRepo : IFootballPlayerRepo
    {
        public async Task<FootballPlayer> AddFootballPlayer(FootballPlayer footballPlayer)
        {
            return await FootballPlayerDAO.Instance.AddPlayer(footballPlayer);
        }

        public async Task<FootballPlayer> DeleFootballPlayer(string id)
        {
            return await FootballPlayerDAO.Instance.DeletePlayer(id);
        }

        public async Task<FootballPlayer> GetFootballPlayer(string id)
        {
            return await FootballPlayerDAO.Instance.GetPlayerById(id);
        }

        public async Task<List<FootballPlayer>> GetFootballPlayers()
        {
            return await FootballPlayerDAO.Instance.GetPlayers();
        }

        public async Task<FootballPlayer> UpdateFootballPlayer(FootballPlayer footballPlayer)
        {
            return await FootballPlayerDAO.Instance.UpdatePlayer(footballPlayer);
        }
        public async Task<List<FootballClub>> GetFootballClubs()
        {
            return await FootballPlayerDAO.Instance.GetFootballClubs();
        }
    }
}
