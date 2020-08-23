--use Tournaments
-------------------------------------------------------------------------------------------0
--alter procedure dbo.spPrizes_Insert
--(
--@PlaceNumber int,
--@PlaceName nvarchar(50),
--@PrizeAmount money,
--@PrizePercentage float,
--@id int=0 output
--)
--as
--begin
--set nocount on
--insert into
--dbo.Prizes (PlaceNumber,PlaceName,PrizeAmount,PrizePercentage)
--values (@PlaceNumber,@PlaceName,@PrizeAmount,@PrizePercentage);
--select @id=SCOPE_IDENTITY();	
--end

-------------------------------------------------------------------------------------------1

--alter procedure dbo.spPeople_Insert
--(
--@FirstName nvarchar(100),
--@LastName nvarchar(100),
--@EmailAddress nvarchar(100),
--@CellPhoneNumber varchar(20),
--@id int=0 output
--)
--as
--begin
--set nocount on
--insert into dbo.People
--(FirstName,LastName,EmailAddress,CellPhoneNumber)
--values
--(@FirstName,@LastName,@EmailAddress,@CellPhoneNumber)
--select @id=SCOPE_IDENTITY();	
--end

-------------------------------------------------------------------------------------------2

--alter procedure dbo.spPeople_GetAll
--as
--begin
--select ph.* from dbo.People ph
--end

-------------------------------------------------------------------------------------------3

--alter procedure dbo.spTeams_Insert
--@TeamName nvarchar(100),
--@id int = 0 output
--as
--begin
--set nocount on
--insert into dbo.Teams (TeamName) values (@TeamName)
--select @id=SCOPE_IDENTITY();
--end

-------------------------------------------------------------------------------------------4

--alter procedure dbo.spTeamMembers_Insert
--@TeamId int,
--@PersonId int,
--@id int = 0 output
--as
--begin
--set nocount on
--insert into dbo.TeamMembers (TeamId,PersonId) values (@TeamId,@PersonId)
--select @id=SCOPE_IDENTITY();
--end

--------------------------------------------------------------------------------------------5

--create procedure dbo.spTeam_GetAll
--as
--begin
--set nocount on
--select t.* from Teams t
--end

---------------------------------------------------------------------------------------------
--create procedure dbo.spTeamMembers_GetByTeam
--@TeamId int
--as
--begin
--set nocount on;
--select p.* from dbo.TeamMembers m
--inner join dbo.People p on m.PersonId=p.id
--where m.TeamId=@TeamId
--end
---------------------------------------------------------------------------------------------
--create procedure dbo.spTeam_GetByTournament
--@TournamentId int
--as
--begin
--set nocount on;
--select t.* from dbo.Teams t
--inner join dbo.TournamentEntries e on t.id=e.teamId
--where e.TournamentId=@TournamentId
--end


-----------------------------------------------------------------------------------------------

--alter procedure dbo.spTournaments_Insert
--@TournamentName nvarchar(200),
--@EntryFee money,
--@id int=0 output
--as
--begin
--set nocount on;
--insert into dbo.Tournaments (TournamentName,EntryFee,Active) values (@TournamentName,@EntryFee,1);
--select @id=SCOPE_IDENTITY();
--end
-----------------------------------------------------------------------------------------------

--create procedure dbo.spTournamntPrizes_Insert
--@TournamentId int,
--@PrizeId int,
--@id int=0 output
--as
--begin
--set nocount on;
--insert into dbo.TournamentPrizes(TournamentId,PrizeId)values(@TournamentId,@PrizeId);
--select @id=SCOPE_IDENTITY();
--end
-----------------------------------------------------------------------------------------
--create procedure dbo.spTournamntEntries_Insert
--@TournamentId int,
--@TeamId int,
--@id int=0 output
--as
--begin
--set nocount on;
--insert into dbo.TournamentEntries(TournamentId,TeamId)values(@TournamentId,@TeamId);
--select @id=SCOPE_IDENTITY();
--end
----------------------------------------------------------------------------------------------

--alter procedure dbo.spMatchups_Insert
--@TournamentId int,
--@MatchupRound int,
--@id int = 0 output
--as
--begin
--set nocount on;
--insert into dbo.Matchups(TournamentId,MatchupRound) values (@TournamentId,@MatchupRound)
--select @id=SCOPE_IDENTITY();
--end
-------------------------------------------------------------------------------------------------
--alter procedure dbo.spMatchupEntries_Insert
--@MatchupId int,
--@ParentMatchupId int,
--@TeamCompetingId int,
--@id int = 0  output
--as
--begin
--set nocount on;
--insert into dbo.MatchupEntries(MatchupId,ParentMatchupId,TeamCompetingId)values(@MatchupId,@ParentMatchupId,@TeamCompetingId)
--select @id=SCOPE_IDENTITY();
--end

--------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------