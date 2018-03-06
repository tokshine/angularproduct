using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbEvent = WebApiTestStandAlone.Entities;
using WebApiTestStandAlone.Models;
using Event = WebApiTestStandAlone.Models.Event;

namespace WebApiTestStandAlone
{
    public class BetRepository : IBetRepository
    {
        private DbEvent.BetContext _context;

        public BetRepository(DbEvent.BetContext context)
        {
            _context = context;
        }

        public IEnumerable<PlayerInfo> GetPlayers()
        {
            var players = new List<PlayerInfo>();

            DateTime cutOffTime = DateTime.UtcNow.AddHours(2);

            //var betIds = _context.Bet.Where(x => x.Event.KickoffTime <= cutOffTime).Select(b=>b.BetId);
            


            var playerIds = _context.Player.Select(p => p.PlayerId);

            foreach (var id in playerIds)
            {
                var player = new PlayerInfo();
                //it seems lazy loading is not supported in ef7 including nested objects should be explicit
                var dbPlayer = _context.Player
                                .Include(b=>b.Bets).ThenInclude(c => c.Event)
                                .First(p=>p.PlayerId==id);

                // var bets = _context.Bet.Where(x => x.PlayerId == id);
                
                player.Id = dbPlayer.PlayerId;
                player.Email = dbPlayer.Email;
                player.Name = dbPlayer.Name;

                player.TotalBets = dbPlayer.Bets.Count;
                //player.TotalBets = dbPlayer.Bets.Count(c=>c.Event.KickoffTime <=cutOffTime);

                //Is the event result null if so ,equation changes below
                //player.TotalBetsWon = dbPlayer.Bets.Count(s => s.Type == s.Event.Result && betIds.Contains(s.BetId));
                player.TotalBetsWon = dbPlayer.Bets.Count(s => s.Type == s.Event.Result && s.Event.KickoffTime <= cutOffTime );
                player.TotalBetsLost = dbPlayer.Bets.Count(s => s.Type != s.Event.Result && s.Event.KickoffTime <= cutOffTime);
                //player.TotalBetsLost = dbPlayer.Bets.Count(s => s.Type != s.Event.Result && betIds.Contains(s.BetId));


                player.AmountWon = dbPlayer.Bets.Where(s => s.Type == s.Event.Result && s.Event.KickoffTime <= cutOffTime).Sum(a => a.Amount * a.Event.Result);
                player.AmountLost = dbPlayer.Bets.Where(s => s.Type != s.Event.Result && s.Event.KickoffTime <= cutOffTime).Sum(a => a.Amount);


                //player.AmountWon = dbPlayer.Bets.Where(s => s.Type == s.Event.Result && s.Event.KickoffTime <= cutOffTime).Sum(a=>a.Amount);
                //player.AmountLost = dbPlayer.Bets.Where(s => s.Type != s.Event.Result && s.Event.KickoffTime <= cutOffTime).Sum(a => a.Amount);
                players.Add(player);
            }


            return players;


        }

        public Event SaveEvent(Event sportEvent)
        {

            var dbEvent = new DbEvent.Event
            {
                KickoffTime = DateTime.UtcNow,
                HomeTeam = sportEvent.HomeTeam,
                AwayTeam = sportEvent.AwayTeam,
                HomeOdds = sportEvent.HomeOdds,
                AwayOdds = sportEvent.AwayOdds,
                Result = sportEvent.Result,
                DrawOdds =sportEvent.DrawOdds
            };


            _context.Event.Add(dbEvent);
            _context.SaveChanges();


            sportEvent.EventId = dbEvent.EventId;

            return sportEvent;
        }


        public void UpdateEvent(int id ,Event sportEvent)
        {
            var dbEvent = _context.Event.Find(id);

            if (dbEvent != null)
            {
                dbEvent.AwayOdds = sportEvent.AwayOdds;
                dbEvent.HomeOdds = sportEvent.HomeOdds;
                dbEvent.Result = sportEvent.Result;
                dbEvent.AwayTeam = sportEvent.AwayTeam;
                dbEvent.KickoffTime = sportEvent.KickoffTime;
                dbEvent.DrawOdds = sportEvent.DrawOdds;
                dbEvent.HomeTeam = sportEvent.HomeTeam;
                _context.SaveChanges();
            }
            
        }


        public bool SportEventExists(int id)
        {
            return _context.Event.Any(c => c.EventId == id);
        }

        public Event GetEvent(int id)
        {
            throw new NotImplementedException();
        }
    }



    public interface IBetRepository
    {
        Event SaveEvent(Event sportEvent);

        Event GetEvent(int id);

        bool SportEventExists(int id);
        void UpdateEvent(int id, Event sportEvent);

        IEnumerable<PlayerInfo> GetPlayers();
    }
}
