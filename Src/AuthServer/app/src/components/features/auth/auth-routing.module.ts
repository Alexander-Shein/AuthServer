import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {ForgotPasswordPageComponent} from "./manage-passwords/forgot-password-page.component";
import {ResetPasswordPageComponent} from "./manage-passwords/reset-password-page.component";
import {EmailConfirmationPageComponent} from "./external-log-in/email-confirmation-page.component";
import {TwoFactorPageComponent} from "./two-factor/two-factor-page.component";
import {ChangePasswordPageComponent} from "./manage-passwords/change-password-page.component";
import {AddPasswordPageComponent} from "./manage-passwords/add-password-page.component";
import {AddPhonePageComponent} from "./manage-phones/add-phone-page.component";
import {ManageExternalProvidersPageComponent} from "./external-log-in/manage-external-providers-page.component";
import {TwoFactorProvidersResolver} from "./two-factor/services/two-factor-providers-resolver.service";
import {AppByUrlResolver} from "../business/apps/services/app-by-url-resolver.service";
import {LogOutPageComponent} from "./log-out/log-out-page.component";
import {ExternalLogInCallbackPageComponent} from "./external-log-in/external-log-in-callback-page.component";
import {LoggedInGuard} from "./services/logged-in.guard";
import {SearchableProvidersResolver} from "./external-log-in/services/searchable-providers-resolver";
import {SignInUpPageComponent} from "./sign-in-up/sign-in-up-page.component";
import {AddEmailPageComponent} from "./manage-emails/add-email-page.component";
import {CurrentUserResolver} from "./services/current-user-resolver.service";
import {ExternalProvidersResolver} from "./external-log-in/services/external-providers-resolver.service";
import {AccountConfirmationPageComponent} from "./sign-up/account-confirmation-page.component";


let authResolve = {
    app: AppByUrlResolver,
    searchableProviders: SearchableProvidersResolver
};

export const AuthRoutes: Routes = [
    {path: 'log-in', component: SignInUpPageComponent, resolve: authResolve},
    {path: 'sign-up', component: SignInUpPageComponent, resolve: authResolve},
    {path: 'log-out', component: LogOutPageComponent},
    {path: 'forgot-password', component: ForgotPasswordPageComponent, resolve: authResolve},
    {path: 'reset-password', component: ResetPasswordPageComponent},
    {path: 'change-password', component: ChangePasswordPageComponent, canActivate: [LoggedInGuard]},
    {path: 'add-password', component: AddPasswordPageComponent, canActivate: [LoggedInGuard]},
    {path: 'add-phone', component: AddPhonePageComponent, canActivate: [LoggedInGuard]},
    {path: 'add-email', component: AddEmailPageComponent, canActivate: [LoggedInGuard]},
    {path: 'email-confirmation', component: EmailConfirmationPageComponent},
    {path: 'account-confirmation', component: AccountConfirmationPageComponent},
    {
        path: 'two-factor',
        component: TwoFactorPageComponent,
        resolve:
        {
            providers: TwoFactorProvidersResolver
        }
    },
    {
        path: 'external-providers',
        component: ManageExternalProvidersPageComponent,
        resolve:
        {
            user: CurrentUserResolver,
            externalProviders: ExternalProvidersResolver
        },
        canActivate: [LoggedInGuard]
    },
    {path: 'external-log-in-callback', component: ExternalLogInCallbackPageComponent}
];

@NgModule({
    imports: [
        RouterModule.forChild(AuthRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AuthRoutingModule {}
