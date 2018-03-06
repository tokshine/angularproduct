using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTestStandAlone.Models
{
    public class Player
    {
    }

    public class Bet
    {
    }

    public class PlayerInfo
    {
        public long  Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public int TotalBets { get; set; }
        public int TotalBetsWon { get; set; }

        public int TotalBetsLost { get; set; }

        public double AmountWon { get; set; }
        public double AmountLost { get; set; }

    }

    public class Event
    {

        public System.DateTime KickoffTime { get; set; }

  
        public long EventId { get; set; }


        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public float HomeOdds { get; set; }

        public float AwayOdds { get; set; }

        public float DrawOdds { get; set; }

        public byte Result { get; set; }
    }
}
