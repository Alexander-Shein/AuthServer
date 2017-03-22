import {IMessagesService} from "./i-messages.service";
import {Injectable} from "@angular/core";
import {Message} from "../models/message";

@Injectable()
export class MessagesService implements IMessagesService {

    public send(message: Message): Promise<void> {
        return new Promise<void>((resolve) =>
            setTimeout(() => resolve(), 500)
        );
    }

}