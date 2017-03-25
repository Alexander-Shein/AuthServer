import {Response} from '@angular/http';
import {Observable} from "rxjs";
import {NotificationsService} from "angular2-notifications";


export abstract class ServiceBase {

    constructor(
        protected notificationsService: NotificationsService
    ) {}

    protected extractData(res: Response) {
        let body = res.json();
        return body || { };
    }

    protected handleError(error: Response | any) {
        debugger;
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }

        this.notificationsService
            .error('Failed.', errMsg);

        return Observable.throw(errMsg);
    }

}