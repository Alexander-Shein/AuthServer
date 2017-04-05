import {IMessagesService} from "./i-messages.service";
import {Injectable} from "@angular/core";
import {Message} from "../models/message";
import {Observable} from "rxjs";
import {ServiceBase} from "../../../common/base.service";
import {Http} from "@angular/http";
import {NotificationsService} from "angular2-notifications";


@Injectable()
export class MessagesService extends ServiceBase implements IMessagesService {

    constructor (
        private http: Http,
        notificationsService: NotificationsService
    ) {
        super(notificationsService);
    }

    private readonly apiUrl: string = 'http://localhost:5000/api/support/messages';

    public send(message: Message): Observable<void> {
        return this.http
            .post(this.apiUrl, message)
            .map((res) => this.extractData(res))
            .catch((error) => this.handleError(error));
    }

}