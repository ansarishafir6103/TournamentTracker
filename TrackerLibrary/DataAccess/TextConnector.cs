using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;
namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels.csv";

        // TODO - wire up the CreatePerson for text files.
        public PersonModel CreatePreson(PersonModel model)
        {
            //load text file and convert text file to List<PersonModel>
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            //find the max ID
            int currentId = 1;

            if (people.Count>0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //add the new record with the new ID(max +1)
            people.Add(model);

            //convert the prizes to list<string>
            //save the list<string> to the text file
            people.SaveToPersonFile(PeopleFile);

            return model;
            
        }

        // TODO - wire up the CreatePrize for text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //load text file and convert text file to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //find the max ID
            int currentId = 1;

            if (prizes.Count>0)
            {
                currentId= prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //add the new record with the new ID(max +1)
            prizes.Add(model);

            //convert the prizes to list<string>
            //save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            //find the max ID
            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            //add the new record with the new ID(max +1)
            teams.Add(model);

            //convert the prizes to list<string>
            //save the list<string> to the text file
            teams.SaveToTeamFile(TeamFile);

            return model;
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamntModels(TeamFile,PeopleFile,PrizesFile);

            int currentId = 0;

            if (tournaments.Count>0)
            {
                currentId = tournaments.OrderByDescending(x =>x.Id).First().Id + 1;
            }

            model.Id = currentId;
            
            model.SaveRoundsToFile(MatchupFile,MatchupEntryFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(TournamentFile);
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }
    }
}
