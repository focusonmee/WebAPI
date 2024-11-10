using BOs;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class PremierLeagueAccountRepo : IPremierLeagueAccountRepo
    {
        public async Task<PremierLeagueAccount> Login(string email, string password)
        {
            return await PremierLeagueAccountDAO.Instance.Login(email, password);
        }
    }
}
