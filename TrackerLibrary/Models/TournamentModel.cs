using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// represents one tournament,with all of the rounds,matchups,prizes and outcomes. 
    /// </summary>
   public class TournamentModel
    {
        public int Id { get; set; }
        /// <summary>
        /// the name give to this tournament.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// the amount of money each team need to put up to enter.
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// the set of team that have been entred.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();
        /// <summary>
        /// the list of prize for the various place.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// the match per round.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
