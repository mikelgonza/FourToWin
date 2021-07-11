using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FourToWin.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Player 1")]
        public string User1Id { get; set; }
        [Display(Name = "Player 2")]
        public string User2Id { get; set; }
        public string Winner { get; set; }
        public int NumRounds { get; set; }
        [Display(Name = "Date and Time")]
        public DateTime DateTime { get; set; }

        [ForeignKey("User1Id")]
        [Display(Name = "Player 1")]
        public AppUser User1 { get; set; }

        [ForeignKey("User2Id")]
        [Display(Name = "Player 2")]
        public AppUser User2 { get; set; }


    }
}
