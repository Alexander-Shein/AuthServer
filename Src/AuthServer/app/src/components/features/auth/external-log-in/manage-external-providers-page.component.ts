import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {ExternalProvidersService} from "./services/external-providers.service";
import {ExternalProvider} from "./models/external-provider";
import {ExternalProvidersSettings} from "./models/external-providers-settings";
import {UserExternalProvider} from "./models/user-external-provider";


@Component({
    selector: 'au-manage-external-providers',
    templateUrl: './manage-external-providers-page.component.html',
    styleUrls: ['../auth.scss', './manage-external-providers-page.component.scss']
})
export class ManageExternalProvidersPageComponent extends AuthBaseComponent {

    constructor(
        private externalProvidersService : ExternalProvidersService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public externalProvidersSettings: ExternalProvidersSettings;
    public canDeleteExternalLogIn: boolean;

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: { externalProvidersSettings: ExternalProvidersSettings }) => {
                let stgs = this.externalProvidersSettings = data.externalProvidersSettings;

                this.canDeleteExternalLogIn = stgs.hasPassword || stgs.currentLogIns.length > 1;
            });
    }

    public deleteExternalLogIn(userExternalProvider: UserExternalProvider): void {
        this.spinnerService.show();

        this.externalProvidersService
            .deleteExternalLogIn(userExternalProvider)
            .then(() => {
                let stgs = this.externalProvidersSettings;

                stgs.currentLogIns = stgs.currentLogIns.filter((x: UserExternalProvider) => x !== userExternalProvider);
                this.canDeleteExternalLogIn = stgs.hasPassword || stgs.currentLogIns.length > 1;

                this.spinnerService.hide();
            })
            .catch(() => this.spinnerService.hide());
    }

    public linkExternalLogIn(externalProvider: ExternalProvider): void {
        this.spinnerService.show();

        this.externalProvidersService
            .linkExternalLogIn(externalProvider.authenticationScheme);

        this.spinnerService.hide();
    }

}