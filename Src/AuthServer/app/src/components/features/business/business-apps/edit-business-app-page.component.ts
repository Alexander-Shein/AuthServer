import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {BusinessApp} from "./models/business-app";
import {BusinessAppsService} from "./services/business-apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {NotificationsService} from "angular2-notifications";
import {BaseComponent} from "../../../common/base.component";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";


@Component({
    selector: 'au-edit-business-app',
    templateUrl: './edit-business-app-page.component.html',
    styleUrls: ['./edit-business-app-page.component.scss']
})
export class EditBusinessAppPageComponent extends BaseComponent implements OnInit {

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private businessAppsService: BusinessAppsService,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(notificationsService, spinnerService);
    }

    public vm: BusinessApp;
    public externalProviders: ExternalProvider[];

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { businessApp: BusinessApp, externalProviders: ExternalProvider[] }) => {
                this.vm = data.businessApp;
                this.externalProviders = data.externalProviders;
                this.removeSelectedExternalProviders();
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

        this.businessAppsService
            .put(this.vm)
            .then(() => {
                this.router
                    .navigate(['/business-apps/' + this.vm.name]);

                this.spinnerService
                    .hide();
            })
            .catch((e) => this.handleError(e));
    }

    private removeSelectedExternalProviders() {
        this.externalProviders = this.externalProviders.filter((el) => {
            return !this.vm.externalProviders.some((x) => {
                return x.authenticationScheme === el.authenticationScheme;
            });
        });
    }

}