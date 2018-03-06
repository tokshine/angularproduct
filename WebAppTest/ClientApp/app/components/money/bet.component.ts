import { Component } from '@angular/core';
import { DataService } from './data.service';
import { IPlayerInfo } from './player';


@Component({
    selector: 'players',
    templateUrl: './bet.component.html',
    styleUrls: ['./bet.component.css']
    
})
export class BetComponent {
    players: any[];
    errorMessage: string;
    constructor(private dataService: DataService) {


    }
    ngOnInit() {
        this.dataService.getPlayers().
            subscribe(p => {
                    this.players = p;
                    
                },
                errors => this.errorMessage = <any>errors);
    }
}
