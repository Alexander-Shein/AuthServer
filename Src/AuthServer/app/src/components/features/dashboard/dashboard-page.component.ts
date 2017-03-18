import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {UserSettings} from "../auth/models/user-settings";
import {AuthenticationService} from "../auth/services/authentication.service";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";


@Component({
    selector: 'au-dashboard',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private notificationsService: NotificationsService,
        private spinnerService: SpinnerService) {}

    public userSettings: UserSettings;

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: { userSettings: UserSettings }) => {
                this.userSettings = data.userSettings;
            });
    }

    public deletePhoneNumber(): void {
        this.spinnerService.show();

        this.authenticationService
            .deletePhoneNumber()
            .then(() => {
                this.userSettings.phoneNumber = '';
                this.spinnerService.hide();
            })
            .catch((e) => this.handleError(e));
    }

    private handleError(error: any): void {
        let message = error || error.message || '';

        this.notificationsService
            .error('Failed.', message);

        this.spinnerService.hide();
    }

    public toggleTwoFactor(): void {
        this.spinnerService.show();

        let request;

        if (this.userSettings.twoFactor) {
            request =
                this.authenticationService
                    .disableTwoFactor();
        } else {
            request =
                this.authenticationService
                    .enableTwoFactor();
        }

        request
            .then(() => {
                this.spinnerService.hide();
            })
            .catch((e) => this.handleError(e));
    }

}