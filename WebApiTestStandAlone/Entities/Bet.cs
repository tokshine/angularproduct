using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestStandAlone.Entities
{


    public class Bet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BetId { get; set; }

       
        public long PlayerId { get; set; }

        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }

        public long EventId { get; set; }

        public byte Type { get; set; }

        public double Amount { get; set; }
    }



    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlayerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Bet> Bets { get; set; }

    }



 


    public class Event

    {

        public System.DateTime KickoffTime { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EventId { get; set; }

        public virtual List<Bet> Bets { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public float HomeOdds { get; set; }

        public float AwayOdds { get; set; }

        public float DrawOdds { get; set; }

        public byte Result { get; set; }

    }




}
