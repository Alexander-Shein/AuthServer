import {Component} from "@angular/core";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {EmailsService} from "./services/emails.service";
import {Consts} from "../../../consts";
import {UserIm} from "../models/user-im";
import {UsersService} from "../services/users.service";


@Component({
    selector: 'au-add-email',
    templateUrl: './add-email-page.component.html',
    styleUrls: ['../auth.scss', './add-email-page.component.scss']
})
export class AddEmailPageComponent extends AuthBaseComponent {

    constructor(
        private emailsService: EmailsService,
        private usersService: UsersService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit() {
        this.route
            .params
            .subscribe((params: Params) => {
                let email = params[Consts.Email];

                if (email) {
                    this.im.email = email;
                    this.sendCode();
                }
            });

        super.ngOnInit();
    }

    public im: UserIm = new UserIm();
    public isCodeSent: boolean = false;

    public sendCode(): void {
        this.emailsService
            .sendVerificationCode({emailAddress: this.im.email})
            .then(() => {
                this.isCodeSent = true;
                this.spinnerService.hide();
            })
            .catch(() => this.spinnerService.hide());
    }

    public addEmail(): void {
        this.spinnerService.show();

        this.usersService
            .update(this.im)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide()
            );
    }
}