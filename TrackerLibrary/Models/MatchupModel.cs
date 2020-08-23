using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    ///  represents one match in the tournament 
    /// </summary>
    public class MatchupModel
    {
        public int Id { get; set; }
        /// <summary>
        /// the set of team that were involve in this match.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// the winner of the match.
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// which round this match is a part of.
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
