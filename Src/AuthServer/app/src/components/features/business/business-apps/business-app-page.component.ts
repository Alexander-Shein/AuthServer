import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {BusinessApp} from "./models/business-app";
import {BusinessAppsService} from "./services/business-apps.service";
import {MdDialog} from "@angular/material";
import {ConfirmationDialogComponent} from "../../../common/pop-ups/confirmation-dialog.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {BaseComponent} from "../../../common/base.component";


@Component({
    selector: 'au-business-app',
    templateUrl: './business-app-page.component.html',
    styleUrls: ['./business-app-page.component.scss']
})
export class BusinessAppPageComponent extends BaseComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private businessAppsService: BusinessAppsService,
        private dialog: MdDialog,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(notificationsService, spinnerService);
    }

    public vm: BusinessApp;

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { businessApp: BusinessApp }) => {
                this.vm = data.businessApp;
            });
    }

    public removeBusinessApp() {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService
                    .show();

                this.businessAppsService
                    .remove(this.vm.name)
                    .then(() => {
                        this.router
                            .navigate(['dashboard']);

                        this.spinnerService
                            .hide();
                    })
                    .catch((e) => this.handleError(e));
            }
        });
    }

}