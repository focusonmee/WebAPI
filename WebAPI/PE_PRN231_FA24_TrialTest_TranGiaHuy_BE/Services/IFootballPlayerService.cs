using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public interface IFootballPlayerService
    {
        Task <List<FootballPlayer>> GetFootballPlayers();
        Task<FootballPlayer> GetFootballPlayer(string id);

        Task<FootballPlayer> AddFootballPlayer(FootballPlayer footballPlayer);

        Task<FootballPlayer> UpdateFootballPlayer(FootballPlayer footballPlayer);

        Task <FootballPlayer> DeleFootballPlayer(string id);

        public Task<List<FootballClub>> GetFootballClubs();

    }
}
