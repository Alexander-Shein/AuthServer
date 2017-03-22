import {Component} from "@angular/core";
import {Message} from "./models/message";
import {MessagesService} from "./services/messages.service";
import {BaseComponent} from "../../common/base.component";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../common/spinner/services/spinner.service";


@Component({
    selector: 'au-contact',
    templateUrl: './contact-page.component.html',
    styleUrls: ['./contact-page.component.scss']
})
export class ContactPageComponent extends BaseComponent {

    constructor(
        private messagesService: MessagesService,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(notificationsService, spinnerService);
    }

    public im: Message = new Message();
    public isMessageSent: boolean = false;

    public send() {
        this.spinnerService
            .show();

        this.messagesService
            .send(this.im)
            .then(() => {
                this.spinnerService
                    .hide();

                this.isMessageSent = true;
            })
            .catch((e) => this.handleError(e));
    }

}