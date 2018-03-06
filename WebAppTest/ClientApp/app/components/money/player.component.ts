import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { DataService } from './data.service';


@Component({
    templateUrl: './player.component.html'
})
export class PlayerComponent implements OnInit {
    pageTitle: string = 'Create bet';
    bet: any;
    allEvents= [{ id:102, name: "a vs b" }, { id: 115, name: "a vs c" }];

    errorMessage: string;

    constructor(private route: ActivatedRoute, private router: Router,private dataService:DataService) {
        console.log('constructing player route');
    }


    ngOnInit() {
        console.log('selected player');
        let playerId = +this.route.snapshot.params.id; //'+' converts the string to numeric id
        this.pageTitle += `: ${playerId}`;

    }

    saveBet(formBet: any) {
        alert(formBet.event);
        //this.dataService.saveBet(formBet);
        alert('bet successfully saved into the database...');
        this.router.navigate(['/bet']);
    }


    gotoToPlayers() {
        this.router.navigate(['/bet']);
    }

    //this.dataService.getPlayers().
        //    subscribe(p => {
        //            this.players = p;
                    
        //        },
        //        errors => this.errorMessage = <any>errors);
    
}
