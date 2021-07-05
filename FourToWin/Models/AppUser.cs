using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FourToWin.Models
{
    public class AppUser : IdentityUser
    {
        public string UserImage { get; set; }
        public List<Match> MatchesU1 { get; set; }
        public List<Match> MatchesU2 { get; set; }
    }
}
