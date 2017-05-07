import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {AppsService} from "./services/apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";
import {ExtendedAppIm} from "./models/extended-app-im";
import {ExtendedAppVm} from "./models/extended-app-vm";


@Component({
    selector: 'au-create-app',
    templateUrl: './put-app-page.component.html',
    styleUrls: ['./put-app-page.component.scss']
})
export class CreateAppPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private appsService: AppsService,
        private spinnerService: SpinnerService
    ) {}

    public vm: ExtendedAppIm;
    public externalProviders: ExternalProvider[] = [];
    public title: string = 'Create app';
    public inProgress = false;
    public appKeyErrorMessage: string = '';

    public ngOnInit(): void {
        this.vm = {
            isActive: true,
            name: '',
            key: '',
            externalProviders: [],
            websiteUrl: '',
            isLocalAccountEnabled: true,
            isRememberLogInEnabled: true,
            isSecurityQuestionsEnabled: false,
            emailSettings: {
                isEnabled : true,
                isConfirmationRequired: true,
                isPasswordlessEnabled: true,
                isPasswordEnabled: true,
                isSearchRelatedProviderEnabled: true
            },
            phoneSettings: {
                isEnabled : true,
                isConfirmationRequired: true,
                isPasswordlessEnabled: true,
                isPasswordEnabled: true,
                isSearchRelatedProviderEnabled: true
            }
        };

        this.route
            .data
            .subscribe((data: { externalProviders: ExternalProvider[] }) => {
                this.vm.externalProviders = data.externalProviders;
            });
    }

    public addExternalProvider(externalProvider: ExternalProvider): void {
        let array = this.externalProviders,
            index = array.indexOf(externalProvider);

        array.splice(index, 1);
        this.vm.externalProviders.push(externalProvider);
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
            .post(this.vm)
            .subscribe((vm: ExtendedAppVm) => {
                this.router
                    .navigate(['/business-apps/' + vm.id]);
            },
            () => this.spinnerService.hide());
    }

    public validateKey(isFieldValid: boolean): void {
        if (!isFieldValid) return;
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

}