import {Response} from '@angular/http';
import {Observable} from "rxjs";
import {NotificationsService} from "angular2-notifications";
import {Error} from "../common/error";


export abstract class ServiceBase {

    constructor(
        protected notificationsService: NotificationsService
    ) {}

    protected extractData(res: Response) {
        let body = res.json();
        return body || { };
    }

    protected handleError(response: Response | any) {
        let errors: Error[];
        let errMsg: string = '';

        if (response instanceof Response) {
            errors = response.json() || [];

            for (let error of errors) {
                errMsg += `Code: ${error.code} - ${error.description} \r\n`;
            }
        } else {
            errMsg = response.toString();
        }

        this.notificationsService
            .error('Failed.', errMsg);

        return Observable.throw(errors);
    }

}