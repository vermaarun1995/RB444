export class SportList {
    sportId: string;
    sportName: string;
    iconUrl: string;
    highlight: boolean;
    sequence: number;
    popular: boolean;
    status: boolean;
    others: boolean;
    
    constructor(private sportLists : SportList){
        this.sportId = sportLists.sportId
        this.sportName = sportLists.sportName
        this.iconUrl = sportLists.iconUrl
        this.highlight = sportLists.highlight
        this.sequence = sportLists.sequence
        this.popular = sportLists.popular
        this.status = sportLists.status
        this.others = sportLists.others
    }

}