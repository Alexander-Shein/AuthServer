import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {App} from "./models/app";
import {AppsService} from "./services/apps.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {ExternalProvider} from "../../auth/external-log-in/models/external-provider";


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

    public vm: App = new App();
    public externalProviders: ExternalProvider[];
    public title: string = 'Create business app.';

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: { externalProviders: ExternalProvider[] }) => {
                this.externalProviders = data.externalProviders;
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
            .put(this.vm)
            .then(() => {
                this.router
                    .navigate(['/business-apps/' + this.vm.name])
                    .then(() => this.spinnerService.hide());
            })
            .catch(() => this.spinnerService.hide());
    }

}