import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {App} from "./models/app";
import {AppsService} from "./services/apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";
import {LocalAccountSettings} from "./models/local-account-settings";


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

    public vm: App;
    public externalProviders: ExternalProvider[] = [];
    public title: string = 'Create app';

    public ngOnInit(): void {
        this.vm = {
            id: null,
            isActive: true,
            name: '',
            key: '',
            externalProviders: [],
            websiteUrl: '',
            usersCount: 0,
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
            .subscribe(() => {
                this.router
                    .navigate(['/business-apps/' + this.vm.name]);
            },
            () => this.spinnerService.hide());
    }

}