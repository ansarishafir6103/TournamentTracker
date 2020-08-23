using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// represents the one team who can play tournament.
    /// </summary>
   public class TeamModel
    {
        /// <summary>
        /// the unique identity for Tea.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// the team name of the  team.
        /// </summary>
        public string TeamName  { get; set; }
        /// <summary>
        /// the set of team member were involve in this tournament.
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
    }
}
