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
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public string Winner { get; set; }
        public int NumRounds { get; set; }

        [ForeignKey("User1Id")]
        public AppUser User1 { get; set; }
        [ForeignKey("User2Id")]
        public AppUser User2 { get; set; }


    }
}
