import {Component} from "@angular/core";
import {Message} from "./models/message";
import {MessagesService} from "./services/messages.service";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {AuthenticationService} from "../auth/services/authentication.service";
import {ActivatedRoute} from "@angular/router";
import {User} from "../auth/models/user";


@Component({
    selector: 'au-contact',
    templateUrl: './contact-page.component.html',
    styleUrls: ['./contact-page.component.scss']
})
export class ContactPageComponent {

    constructor(
        private route: ActivatedRoute,
        private messagesService: MessagesService,
        private spinnerService: SpinnerService,
        private authenticationService: AuthenticationService
    ) {
        this.isLoggedIn = authenticationService.isLoggedIn();

        this.route.data
            .subscribe((data: {user: User}) => {
                if(data.user) {
                    this.im.fromEmail = data.user.email;
                }
            });
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