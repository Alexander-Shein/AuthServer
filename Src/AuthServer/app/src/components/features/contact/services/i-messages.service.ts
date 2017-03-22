import {Message} from "../models/message";


export interface IMessagesService {

    send(message: Message): Promise<void>;

}