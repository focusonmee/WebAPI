using BOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public interface IPremierLeagueAccountRepo
    {
        Task<PremierLeagueAccount> Login(string email, string password);
        
    }
}
