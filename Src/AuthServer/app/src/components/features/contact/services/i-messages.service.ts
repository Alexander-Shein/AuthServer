import {Message} from "../models/message";
import {Observable} from "rxjs";


export interface IMessagesService {

    send(message: Message): Observable<void>;

}