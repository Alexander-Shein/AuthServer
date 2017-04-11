import {Email} from "../models/email";


export interface IEmailsService {

    sendVerificationCode(email: Email): Promise<void>;

}