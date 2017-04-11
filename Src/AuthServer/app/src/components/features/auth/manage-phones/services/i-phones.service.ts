import {Phone} from "../models/phone";


export interface IPhonesService {

    sendVerificationCode(phone: Phone): Promise<void>;

}