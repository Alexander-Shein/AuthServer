import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {PhonesService} from "./services/phones.service";
import {NewPhone} from "./models/new-phone";


@Component({
    selector: 'au-add-phone',
    templateUrl: './add-phone-page.component.html',
    styleUrls: ['../auth.scss', './add-phone-page.component.scss']
})
export class AddPhonePageComponent extends AuthBaseComponent {

    constructor(
        private phonesService: PhonesService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public im: NewPhone = new NewPhone();
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

    public verifyCode(): void {
        this.spinnerService.show();

        this.phonesService
            .add(this.im)
            .then(() => this.redirectAfterLogin())
            .catch(() => this.spinnerService.hide());
    }
}