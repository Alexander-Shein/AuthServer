import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {ExternalProvidersService} from "./services/external-providers.service";
import {UserLogInInfo} from "./models/user-log-in-info";
import {ExternalProvider} from "./models/external-provider";
import {ExternalProvidersSettings} from "./models/external-providers-settings";
import {AuthenticationService} from "../services/authentication.service";


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
        authenticationService: AuthenticationService,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, authenticationService, notificationsService, spinnerService);
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

    public deleteExternalLogIn(userLogInInfo: UserLogInInfo): void {
        this.spinnerService.show();

        this.externalProvidersService
            .deleteExternalLogIn(userLogInInfo)
            .then(() => {
                let stgs = this.externalProvidersSettings;

                stgs.currentLogIns = stgs.currentLogIns.filter((x: UserLogInInfo) => x !== userLogInInfo);
                this.canDeleteExternalLogIn = stgs.hasPassword || stgs.currentLogIns.length > 1;

                this.spinnerService.hide();
            })
            .catch((e) => this.handleError(e));
    }

    public linkExternalLogIn(externalProvider: ExternalProvider): void {
        this.spinnerService.show();

        this.externalProvidersService
            .linkExternalLogIn(externalProvider.authenticationScheme);

        this.spinnerService.hide();
    }

}