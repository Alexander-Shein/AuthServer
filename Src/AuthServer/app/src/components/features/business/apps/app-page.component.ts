import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AppsService} from "./services/apps.service";
import {MdDialog} from "@angular/material";
import {ConfirmationDialogComponent} from "../../../common/pop-ups/confirmation-dialog.component";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExtendedAppVm} from "./models/extended-app-vm";


@Component({
    selector: 'au-app',
    templateUrl: './app-page.component.html',
    styleUrls: ['./app-page.component.scss']
})
export class AppPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private appsService: AppsService,
        private dialog: MdDialog,
        private spinnerService: SpinnerService
    ) {}

    public vm: ExtendedAppVm;

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: ExtendedAppVm}) => {
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
                            .navigate(['dashboard']);
                    })
                    .catch(() => this.spinnerService.hide());
            }
        });
    }

}