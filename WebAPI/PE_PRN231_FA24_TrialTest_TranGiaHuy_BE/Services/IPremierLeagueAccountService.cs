using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPremierLeagueAccountService
    {
        Task<PremierLeagueAccount> Login(string email, string password);

    }
}
