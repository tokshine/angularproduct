import { Injectable, Inject } from '@angular/core'
import { Http, Response } from '@angular/http'
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import {IPlayerInfo} from './player';



@Injectable()
export class DataService {
    constructor(private http: Http,@Inject('BASE_URL') private baseUrl: string) {

    }
    getPlayers(): Observable<any[]> {
        var address = this.baseUrl + '/api/bet/players';
        //alert(address);
        return this.http.get(address)
            .map((response: Response) => <any[]>response.json())
            .do(data => console.log('All' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    saveBet(bet: any) {
        //this.http.post(bet);
        //return todo
    }

    private handleError(error: Response) {
        console.log('resource unavailable');
        console.error(error);
        return Observable.throw(error.json().error || 'server error');
    }
}