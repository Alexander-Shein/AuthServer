import {Component} from "@angular/core";
import {Message} from "./models/message";
import {MessagesService} from "./services/messages.service";
import {SpinnerService} from "../../common/spinner/services/spinner.service";


@Component({
    selector: 'au-contact',
    templateUrl: './contact-page.component.html',
    styleUrls: ['./contact-page.component.scss']
})
export class ContactPageComponent {

    constructor(
        private messagesService: MessagesService,
        private spinnerService: SpinnerService
    ) { }

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