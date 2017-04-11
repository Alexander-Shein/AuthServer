import {Injectable} from "@angular/core";
import {IEmailsService} from "./i-emails.service";
import {Email} from "../models/email";


@Injectable()
export class EmailsService implements IEmailsService {

    public sendVerificationCode(email: Email): Promise<void> {
        return Promise.resolve();
    }

}