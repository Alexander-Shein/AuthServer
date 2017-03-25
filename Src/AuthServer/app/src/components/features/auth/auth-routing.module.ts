import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {LogInPageComponent} from "./log-in/log-in-page.component";
import {SignUpPageComponent} from "./sign-up/sign-up-page.component";
import {ForgotPasswordPageComponent} from "./manage-passwords/forgot-password-page.component";
import {ResetPasswordPageComponent} from "./manage-passwords/reset-password-page.component";
import {EmailConfirmationPageComponent} from "./external-log-in/email-confirmation-page.component";
import {TwoFactorPageComponent} from "./two-factor/two-factor-page.component";
import {ChangePasswordPageComponent} from "./manage-passwords/change-password-page.component";
import {AddPasswordPageComponent} from "./manage-passwords/add-password-page.component";
import {AddPhonePageComponent} from "./manage-phones/add-phone-page.component";
import {ManageExternalProvidersPageComponent} from "./external-log-in/manage-external-providers-page.component";
import {TwoFactorProvidersResolver} from "./two-factor/services/two-factor-providers-resolver.service";
import {ExternalProvidersSettingsResolver} from "./external-log-in/services/external-providers-settings-resolver.service";
import {AppByUrlResolver} from "../business/apps/services/app-by-url-resolver.service";
import {LogOutPageComponent} from "./log-out/log-out-page.component";
import {ExternalLogInCallbackPageComponent} from "./external-log-in/external-log-in-callback-page.component";
import {LoggedInGuard} from "./services/logged-in.guard";


const appRoutes: Routes = [
    {
        path: 'log-in',
        component: LogInPageComponent,
        resolve:
        {
            app: AppByUrlResolver
        }
    },
    {path: 'log-out', component: LogOutPageComponent},
    {
        path: 'sign-up',
        component: SignUpPageComponent,
        resolve:
        {
            app: AppByUrlResolver
        }
    },
    {path: 'forgot-password', component: ForgotPasswordPageComponent},
    {path: 'reset-password', component: ResetPasswordPageComponent},
    {path: 'change-password', component: ChangePasswordPageComponent, canActivate: [LoggedInGuard]},
    {path: 'add-password', component: AddPasswordPageComponent, canActivate: [LoggedInGuard]},
    {path: 'add-phone', component: AddPhonePageComponent, canActivate: [LoggedInGuard]},
    {path: 'email-confirmation', component: EmailConfirmationPageComponent},
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
            externalProvidersSettings: ExternalProvidersSettingsResolver
        },
        canActivate: [LoggedInGuard]
    },
    {path: 'external-log-in-callback', component: ExternalLogInCallbackPageComponent}
];

@NgModule({
    imports: [
        RouterModule.forChild(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AuthRoutingModule {}
