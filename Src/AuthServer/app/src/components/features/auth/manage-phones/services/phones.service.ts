import {Injectable} from "@angular/core";
import {IPhonesService} from "./i-phones.service";
import {Phone} from "../models/phone";
import {NewPhone} from "../models/new-phone";


@Injectable()
export class PhonesService implements IPhonesService {

    public sendVerificationCode(phone: Phone): Promise<void> {
        return Promise.resolve();
    }

    public remove(): Promise<void> {
        return Promise.resolve();
    }

    public add(newPhone: NewPhone): Promise<void> {
        return Promise.resolve();
    }

}