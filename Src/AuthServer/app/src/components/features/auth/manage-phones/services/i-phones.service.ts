import {Phone} from "../models/phone";
import {NewPhone} from "../models/new-phone";


export interface IPhonesService {

    sendVerificationCode(phone: Phone): Promise<void>;
    remove(): Promise<void>;
    add(newPhone: NewPhone): Promise<void>;

}