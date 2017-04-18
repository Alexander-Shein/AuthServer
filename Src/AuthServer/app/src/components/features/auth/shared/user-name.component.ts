import {Component, EventEmitter, Input, Output} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {UserName} from "../models/user-name";
import {ExternalProvider} from "../external-log-in/models/external-provider";
import {AuthenticationService} from "../services/authentication.service";
import {SpinnerService} from "../../../common/spinner/services/spinner.service";
import {SearchableExternalProvider} from "../external-log-in/models/searchable-external-provider";
import {AuthBaseComponent} from "../auth-base.component";
import {ActivatedRoute, Router} from "@angular/router";
import {UsersService} from "../services/users.service";


@Component({
    selector: 'au-user-name',
    styleUrls: ['./user-name.component.scss'],
    templateUrl: './user-name.component.html'
})
export class UserNameComponent extends AuthBaseComponent {

    constructor(
        private authenticationService: AuthenticationService,
        private usersService: UsersService,
        route: ActivatedRoute,
        router: Router,
        spinnerService: SpinnerService
    ) {
        super(route, router, spinnerService);
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm, searchableProviders: SearchableExternalProvider[]}) => {
                this.app = data.app;
                this.searchableProviders = data.searchableProviders;
            });

        super.ngOnInit();
    }

    @Input()
    public im: UserName;

    @Input()
    public unique: boolean = false;

    @Output()
    public next: EventEmitter<any> = new EventEmitter();

    public app: AppVm;
    public searchableProviders: SearchableExternalProvider[];
    public userNameMessage: string = '';
    public inProgress: boolean = false;
    public searchResult: ExternalProvider;

    public validateUserName(isFormValid: boolean): void {
        if (!isFormValid) return;
        if (this.inProgress) return;
        if (!this.im.userName) return;

        this.inProgress = true;

        this.usersService
            .isUserNameExists(this.im.userName)
            .subscribe(
                () => {
                    if (this.unique) {
                        this.userNameMessage = 'UserName already exists.';
                    } else {
                        this.userNameMessage = '';
                    }
                    this.inProgress = false;
                },
                () => {
                    if (this.unique) {
                        this.userNameMessage = '';
                    } else {
                        this.userNameMessage = 'UserName doesn\'t exist.';
                    }
                    this.inProgress = false;
                });
    }

    public externalLogIn(externalProvider: ExternalProvider): void {
        this.authenticationService
            .externalLogIn(
                {
                    returnUrl: this.redirectUrl,
                    authenticationScheme: externalProvider.authenticationScheme
                });
    }

    public searchProvider(): void {
        if(!this.app.emailSettings.isSearchRelatedProviderEnabled) return;
        this.searchResult = null;

        if (!this.im.userName) return;
        if (!this.searchableProviders) return;

        let userName = this.im.userName.toLowerCase();

        for(let item of this.searchableProviders) {
            for(let match of item.matches) {
                if(userName.indexOf(match) != -1) {
                    this.searchResult = item;
                    return;
                }
            }
        }
    }

    public onSubmit(): boolean {
        this.next.emit();
        return false;
    }
}