using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TrackerLibrary.Models;
using Dapper;
namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";

        // TODO - make the create person method actually save to the database
        /// <summary>
        /// save a new person to the database
        /// </summary>
        /// <param name="model">the person information</param>
        /// <returns>the person information include the unique identifier.</returns>
        public PersonModel CreatePreson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();

                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);
                p.Add("@id",0,dbType:DbType.Int32,direction:ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", p,commandType:CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        // TODO - make the create prize method actually save to the database
        /// <summary>
        /// save a new prize to the database
        /// </summary>
        /// <param name="model">the prize information</param>
        /// <returns>the prize information include the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();

                p.Add("@PlaceNumber",model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id",0,dbType:DbType.Int32,direction:ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert",p,commandType: CommandType.StoredProcedure);
                
                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection=new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@TeamName",model.TeamName);
                p.Add("@id",0,dbType:DbType.Int32,direction:ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", p,commandType:CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    p= new DynamicParameters();
                    p.Add("@TeamId",model.Id);
                    p.Add("@PersonId",tm.Id);

                    connection.Execute("dbo.spTeamMembers_Insert",p,commandType:CommandType.StoredProcedure);
                }

                return model;
            }
        }

        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(connection,model);

                SaveTournamentPrizes(connection, model);

                SaveTournamentEntries(connection,model);

                SaveTournamentRounds(connection, model);

            }

            
        }

        private void SaveTournament(IDbConnection connection,TournamentModel model)
        {
                var p = new DynamicParameters();
                p.Add("@TournamentName", model.TournamentName);
                p.Add("@EntryFee", model.EntryFee);
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournaments_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");   
        }

        private void SaveTournamentPrizes(IDbConnection connection, TournamentModel model)
        {
            foreach (PrizeModel pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@PrizeId", pz.Id);

                connection.Execute("dbo.spTournamntPrizes_Insert", p, commandType: CommandType.StoredProcedure);
            }
        }
        private void SaveTournamentEntries(IDbConnection connection,TournamentModel model)
        {
            foreach (TeamModel tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add("@TournamentId", model.Id);
                p.Add("@TeamId", tm.Id);

                connection.Execute("dbo.spTournamntEntries_Insert", p, commandType: CommandType.StoredProcedure);
            }

        }
        private void SaveTournamentRounds(IDbConnection connection,TournamentModel model)
        {
            //List<List<MatchupModel>> Rounds
            //List<MatchupEntryModel> Entries

            //Loop throught the rounds
            //Loop throught the matchups
            //Save the matchup
            //Loop throught the entries and save them

            foreach (List<MatchupModel> round in model.Rounds)
            {
                foreach (MatchupModel matchup in round)
                {
                    var p = new DynamicParameters();
                    p.Add("@TournamentId", model.Id);
                    p.Add("@MatchupRound", matchup.MatchupRound);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                    connection.Execute("dbo.spMatchups_Insert", p, commandType: CommandType.StoredProcedure);

                    matchup.Id = p.Get<int>("@id");

                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        p = new DynamicParameters();
                        p.Add("@MatchupId", matchup.Id);
                        if (entry.Parentmatchup==null)
                        {
                            p.Add("@ParentMatchupId", null);
                        }
                        else
                        {
                            p.Add("@ParentMatchupId", entry.Parentmatchup.Id);
                        }
                        if (entry.TeamCompeting==null)
                        {
                            p.Add("@TeamCompetingId", null);
                        }
                        else
                        {
                            p.Add("@TeamCompetingId", entry.TeamCompeting.Id);
                        }
                        p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                        connection.Execute("dbo.spMatchupEntries_Insert", p, commandType: CommandType.StoredProcedure);
                    }
                }
            }
        }
        // TODO - Load Data Get All Person Information
        /// <summary>
        /// Get List<PersonModel> From DataBase
        /// </summary>
        /// <returns>List<PersonModel></returns>
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection=new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;

            using (IDbConnection connection=new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();

                foreach (TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId",team.Id);
                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam",p,commandType:CommandType.StoredProcedure).ToList();
                }
            }

            return output;
        }
    }
}
