import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {LogInPageComponent} from "./log-in/log-in-page.component";
import {SignUpPageComponent} from "./sign-up/sign-up-page.component";
import {ClientSettingsResolver} from "./services/client-settings-resolver.service";
import {ForgotPasswordPageComponent} from "./password/forgot-password-page.component";
import {ResetPasswordPageComponent} from "./password/reset-password-page.component";
import {EmailConfirmationPageComponent} from "./external-log-in/email-confirmation-page.component";
import {TwoFactorProvidersResolver} from "./services/two-factor-providers-resolver.service";
import {TwoFactorPageComponent} from "./two-factor/two-factor-page.component";
import {ChangePasswordPageComponent} from "./password/change-password-page.component";
import {AddPasswordPageComponent} from "./password/add-password-page.component";
import {AddPhonePageComponent} from "./phone/add-phone-page.component";
import {ManageExternalProvidersPageComponent} from "./external-log-in/manage-external-providers-page.component";
import {ExternalProvidersSettingsResolver} from "./services/external-providers-settings-resolver.service";


const appRoutes: Routes = [
    {
        path: 'log-in',
        component: LogInPageComponent,
        resolve:
        {
            clientSettings: ClientSettingsResolver
        }
    },
    {path: 'sign-up', component: SignUpPageComponent},
    {path: 'forgot-password', component: ForgotPasswordPageComponent},
    {path: 'reset-password', component: ResetPasswordPageComponent},
    {path: 'change-password', component: ChangePasswordPageComponent},
    {path: 'add-password', component: AddPasswordPageComponent},
    {path: 'add-phone', component: AddPhonePageComponent},
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
        }
    }
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
