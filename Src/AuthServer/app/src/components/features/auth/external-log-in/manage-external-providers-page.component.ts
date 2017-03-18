import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router"
import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {AuthenticationService} from "../services/authentication.service";
import {UserLoginInfo} from "../models/user-login-info";
import {NotificationsService} from "angular2-notifications";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {ExternalProvider} from "../models/external-provider";


@Component({
    selector: 'au-manage-external-providers',
    templateUrl: './manage-external-providers-page.component.html',
    styleUrls: ['../auth.scss', './manage-external-providers-page.component.scss']
})
export class ManageExternalProvidersPageComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        route: ActivatedRoute,
        router: Router,
        notificationsService: NotificationsService,
        spinnerService: SpinnerService
    ) {
        super(route, router, notificationsService, spinnerService);
    }

    public externalProvidersSettings: ExternalProvidersSettings;
    public canDeleteExternalLogin: boolean;

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: { externalProvidersSettings: ExternalProvidersSettings }) => {
                let stgs = this.externalProvidersSettings = data.externalProvidersSettings;

                this.canDeleteExternalLogin = stgs.hasPassword || stgs.currentLogIns.length > 1;
            });
    }

    public deleteExternalLogin(userLoginInfo: UserLoginInfo): void {
        this.spinnerService.show();

        this.authenticationService
            .deleteExternalLogin(userLoginInfo)
            .then(() => {
                let stgs = this.externalProvidersSettings;

                stgs.currentLogIns = stgs.currentLogIns.filter(x => x !== userLoginInfo);
                this.canDeleteExternalLogin = stgs.hasPassword || stgs.currentLogIns.length > 1;

                this.spinnerService.hide();
            })
            .catch((e) => this.handleError(e));
    }

    public linkExternalLogin(externalProvider: ExternalProvider): void {
        this.spinnerService.show();

        this.authenticationService
            .linkExternalLogin(externalProvider.authenticationScheme);

        this.spinnerService.hide();
    }

}