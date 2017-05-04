import {Component} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {AuthBaseComponent} from "../auth-base.component";
import {ExternalProvidersService} from "./services/external-providers.service";
import {ExternalProvider} from "./models/external-provider";
import {UserExternalProvider} from "./models/user-external-provider";
import {User} from "../models/user";


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
        private spinnerService: SpinnerService
    ) {
        super(route, router);
    }

    public canDeleteExternalLogIn: boolean;
    public externalProviders: ExternalProvider[];
    public user: User;

    public ngOnInit(): void {
        this.route.data
            .subscribe((data: {externalProviders: ExternalProvider[], user: User}) => {
                this.canDeleteExternalLogIn = data.user.hasPassword || data.user.externalProviders.length > 1;

                this.externalProviders =
                    data.externalProviders
                        .filter(
                            (x: ExternalProvider) =>
                                !data.user.externalProviders.some(
                                    c => c.authenticationScheme === x.authenticationScheme));
                this.user = data.user;
            });
    }

    public deleteExternalLogIn(userExternalProvider: UserExternalProvider): void {
        this.spinnerService.show();

        this.externalProvidersService
            .deleteExternalLogIn(userExternalProvider)
            .then(() => {
                let userProviders = this.user.externalProviders;
                this.externalProviders.push(userExternalProvider);

                this.user.externalProviders = userProviders.filter((x: ExternalProvider) => x.authenticationScheme !== userExternalProvider.authenticationScheme);
                this.canDeleteExternalLogIn = this.user.hasPassword || this.user.externalProviders.length > 1;

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