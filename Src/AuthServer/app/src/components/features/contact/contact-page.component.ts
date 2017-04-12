import {Component} from "@angular/core";
import {Message} from "./models/message";
import {MessagesService} from "./services/messages.service";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {AuthenticationService} from "../auth/services/authentication.service";


@Component({
    selector: 'au-contact',
    templateUrl: './contact-page.component.html',
    styleUrls: ['./contact-page.component.scss']
})
export class ContactPageComponent {

    constructor(
        private messagesService: MessagesService,
        private spinnerService: SpinnerService,
        private authenticationService: AuthenticationService
    ) {
        this.isLoggedIn = authenticationService.isLoggedIn();
    }

    public isLoggedIn: boolean;
    public im: Message = new Message();
    public isMessageSent: boolean = false;

    public send() {
        this.spinnerService
            .show();

        this.messagesService
            .send(this.im)
            .subscribe(() => {
                this.spinnerService
                    .hide();

                this.isMessageSent = true;
            },
            () => this.spinnerService.hide());
    }

}