import {Injectable} from "@angular/core";
import {IPhonesService} from "./i-phones.service";
import {Phone} from "../models/phone";


@Injectable()
export class PhonesService implements IPhonesService {

    public sendVerificationCode(phone: Phone): Promise<void> {
        return Promise.resolve();
    }

}