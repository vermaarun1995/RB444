export class TournamentEventsList {
    sportId: string;
    exEventId: string;
    eventName: string;
    
    constructor(private tournamentEventsLists : TournamentEventsList){
        this.sportId = tournamentEventsLists.sportId
        this.exEventId = tournamentEventsLists.exEventId
        this.eventName = tournamentEventsLists.eventName
    }

}