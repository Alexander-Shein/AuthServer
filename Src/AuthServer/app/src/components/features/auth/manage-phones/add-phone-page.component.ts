import {Component} from "@angular/core";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {PhonesService} from "./services/phones.service";
import {Consts} from "../../../consts";
import {UserIm} from "../models/user-im";
import {UsersService} from "../services/users.service";


@Component({
    selector: 'au-add-phone',
    templateUrl: './add-phone-page.component.html',
    styleUrls: ['../auth.scss', './add-phone-page.component.scss']
})
export class AddPhonePageComponent extends AuthBaseComponent {

    constructor(
        private phonesService: PhonesService,
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
                let phone = params[Consts.Phone];

                if (phone) {
                    this.im.phoneNumber = phone;
                    this.sendCode();
                }
            });

        super.ngOnInit();
    }

    public im: UserIm = new UserIm();
    public isCodeSent: boolean = false;

    public sendCode(): void {
        this.phonesService
            .sendVerificationCode(this.im)
            .then(() => {
                this.isCodeSent = true;
                this.spinnerService.hide();
            })
            .catch(() => this.spinnerService.hide());
    }

    public addPhone(): void {
        this.spinnerService.show();

        this.usersService
            .update(this.im)
            .subscribe(
                () => this.redirectAfterLogin(),
                () => this.spinnerService.hide()
            );
    }
}