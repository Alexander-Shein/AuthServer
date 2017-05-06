import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {App} from "./models/app";
import {AppsService} from "./services/apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";


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

    public vm: App;
    public externalProviders: ExternalProvider[];
    public title: string = 'Edit app';

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { app: App, externalProviders: ExternalProvider[] }) => {
                this.vm = data.app;
                this.externalProviders = data.externalProviders;
                this.removeSelectedExternalProviders();
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
            .put(this.vm)
            .subscribe(() => {
                this.router
                    .navigate(['/business-apps/' + this.vm.name]);
            },
            () => this.spinnerService.hide());
    }

    private removeSelectedExternalProviders() {
        this.externalProviders = this.externalProviders.filter((el) => {
            return !this.vm.externalProviders.some((x) => {
                return x.authenticationScheme === el.authenticationScheme;
            });
        });
    }

}