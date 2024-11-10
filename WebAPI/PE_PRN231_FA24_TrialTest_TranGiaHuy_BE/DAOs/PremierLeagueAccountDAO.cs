using BOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PremierLeagueAccountDAO
    {
        private static PremierLeagueAccountDAO instance = null;
        private readonly EnglishPremierLeague2024DbContext context;

        private PremierLeagueAccountDAO()
        {
            context = new EnglishPremierLeague2024DbContext();
        }

        public static PremierLeagueAccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PremierLeagueAccountDAO();
                }
                return instance;
            }

        }

        public async Task <PremierLeagueAccount> Login(string email, string password)
        {
            var account = await context.PremierLeagueAccounts.FirstOrDefaultAsync(account => account.EmailAddress == email && account.Password == password);
                return account;
        }
    }
    }
