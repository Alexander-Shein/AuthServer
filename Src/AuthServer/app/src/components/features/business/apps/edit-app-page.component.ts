import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AppsService} from "./services/apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";
import {ExtendedAppVm} from "./models/extended-app-vm";


@Component({
    selector: 'au-edit-app',
    templateUrl: './put-app-page.component.html',
    styleUrls: ['./put-app-page.component.scss']
})
export class EditAppPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private appsService: AppsService,
        private spinnerService: SpinnerService
    ) { }

    public vm: ExtendedAppVm;
    public externalProviders: ExternalProvider[];
    public title: string = 'Edit app';
    public inProgress = false;
    public appKeyErrorMessage: string = '';
    public currentAppKey: string;

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { app: ExtendedAppVm, externalProviders: ExternalProvider[] }) => {
                this.vm = data.app;
                this.externalProviders = data.externalProviders;
                this.removeSelectedExternalProviders();
                this.currentAppKey = this.vm.key;
            });
    }

    public addExternalProvider(externalProvider: ExternalProvider): boolean {
        let array = this.externalProviders,
            index = array.indexOf(externalProvider);

        array.splice(index, 1);
        this.vm.externalProviders.push(externalProvider);
        return false;
    }

    public removeExternalProvider(externalProvider: ExternalProvider): void {
        let array = this.vm.externalProviders,
            index = array.indexOf(externalProvider);

        array.splice(index, 1);
        this.externalProviders.push(externalProvider);
    }

    public save() {
        this.spinnerService
            .show();

        this.appsService
            .put(this.vm.id, this.vm)
            .subscribe(() => {
                this.router
                    .navigate(['/business-apps/' + this.vm.id]);
            },
            () => this.spinnerService.hide());
    }

    public validateKey(isFieldValid: boolean): void {
        if (!isFieldValid) return;
        if (this.currentAppKey === this.vm.key.toLocaleLowerCase()) return;
        if (this.inProgress) return;
        if (!this.vm.key) return;

        this.inProgress = true;

        this.appsService
            .isAppExist(this.vm.key)
            .subscribe(
                () => {
                    this.appKeyErrorMessage = 'App key already exists.';
                    this.inProgress = false;
                },
                () => {
                    this.appKeyErrorMessage = '';
                    this.inProgress = false;
                });
    }

    private removeSelectedExternalProviders() {
        this.externalProviders = this.externalProviders.filter((el) => {
            return !this.vm.externalProviders.some((x) => {
                return x.authenticationScheme === el.authenticationScheme;
            });
        });
    }

}