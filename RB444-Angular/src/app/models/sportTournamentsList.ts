export class SportTournamentsList {
    tournamentId: string;
    tournamentName: string;
    
    constructor(private sportTournamentsLists : SportTournamentsList){
        this.tournamentId = sportTournamentsLists.tournamentId
        this.tournamentName = sportTournamentsLists.tournamentName
    }

}