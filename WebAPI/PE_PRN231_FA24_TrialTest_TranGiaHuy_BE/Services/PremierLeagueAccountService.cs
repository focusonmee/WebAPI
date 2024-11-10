using BOs;
using Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PremierLeagueAccountService : IPremierLeagueAccountService
    {
        private readonly IPremierLeagueAccountRepo _premierLeagueAccountRepo;

        public PremierLeagueAccountService (IPremierLeagueAccountRepo premierLeagueAccountRepo)
        {
            _premierLeagueAccountRepo = premierLeagueAccountRepo;
        }

        public async Task<PremierLeagueAccount> Login(string email, string password)
        {
            return await _premierLeagueAccountRepo.Login(email, password);
        }
    }
}
