import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {App} from "./models/app";
import {AppsService} from "./services/apps.service";
import {MdDialog} from "@angular/material";
import {ConfirmationDialogComponent} from "../../../common/pop-ups/confirmation-dialog.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {BaseComponent} from "../../../common/base.component";


@Component({
    selector: 'au-app',
    templateUrl: './app-page.component.html',
    styleUrls: ['./app-page.component.scss']
})
export class AppPageComponent extends BaseComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private appsService: AppsService,
        private dialog: MdDialog,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(notificationsService, spinnerService);
    }

    public vm: App;

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { app: App }) => {
                this.vm = data.app;
            });
    }

    public removeBusinessApp() {
        let dialogRef = this.dialog.open(ConfirmationDialogComponent);
        dialogRef.afterClosed().subscribe((result: boolean) => {
            if (result) {
                this.spinnerService
                    .show();

                this.appsService
                    .remove(this.vm.name)
                    .then(() => {
                        this.router
                            .navigate(['dashboard'])
                            .then(() => this.spinnerService.hide());
                    })
                    .catch((e) => this.handleError(e));
            }
        });
    }

}