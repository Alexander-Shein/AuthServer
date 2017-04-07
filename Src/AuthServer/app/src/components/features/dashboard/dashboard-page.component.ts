import {Component, OnInit} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {UserSettings} from "../auth/models/user-settings";
import {SpinnerService} from "../../common/spinner/services/spinner.service";
import {PhonesService} from "../auth/manage-phones/services/phones.service";
import {TwoFactorService} from "../auth/two-factor/services/two-factor.service";
import {App} from "../business/apps/models/app";
import {ConfirmationDialogComponent} from "../../common/pop-ups/confirmation-dialog.component";
import {MdDialog} from "@angular/material";
import {TwoFactorSettings} from "../auth/two-factor/models/two-factor-settings";


@Component({
    selector: 'au-dashboard',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private spinnerService: SpinnerService,
        private phonesService: PhonesService,
        private dialog: MdDialog,
        private twoFactorService: TwoFactorService) {}

    public userSettings: UserSettings;
    public apps: App[];

    public ngOnInit(): void {
        this.route.data
            .subscribe((data:
                            {
                                userSettings: UserSettings,
                                apps: App[]
                            }) => {
                this.userSettings = data.userSettings;
                this.apps = data.apps;
            });
    }

    public deletePhoneNumber(): boolean {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService.show();

                this.phonesService
                    .remove()
                    .then(() => {
                        this.userSettings.phoneNumber = '';
                        this.spinnerService.hide();
                    })
                    .catch(() => this.spinnerService.hide());
            }
        });

        return false;
    }

    public toggleTwoFactor(): void {
        this.spinnerService.show();

        let twoFactorSettings: TwoFactorSettings = {
            enabled: this.userSettings.twoFactor
        };

        this.twoFactorService
            .updateTwoFactorSettings(twoFactorSettings)
            .subscribe(
                () => this.spinnerService.hide(),
                () => this.spinnerService.hide());
    }

}