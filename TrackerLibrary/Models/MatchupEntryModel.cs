using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// represents one team in Matchup
    /// </summary>
    public class MatchupEntryModel
    {
        public int Id { get; set; }
        /// <summary>
        /// Represents one team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Represents the score for this particular team
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// Represents the matchup that this team came
        /// from as the winner
        /// </summary>
        public MatchupModel Parentmatchup { get; set; }
    }
}
