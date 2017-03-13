import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {LogInPageComponent} from "./log-in/log-in-page.component";
import {SignUpPageComponent} from "./sign-up/sign-up-page.component";
import {ClientSettingsResolver} from "./services/client-settings-resolver.service";
import {ForgotPasswordPageComponent} from "./password/forgot-password-page.component";
import {ResetPasswordPageComponent} from "./password/reset-password-page.component";
import {EmailConfirmationPageComponent} from "./external-log-in/email-confirmation-page.component";


const appRoutes: Routes = [
    {
        path: 'log-in',
        component: LogInPageComponent,
        resolve: {
            clientSettings: ClientSettingsResolver
        }
    },
    { path: 'sign-up', component: SignUpPageComponent },
    { path: 'forgot-password', component: ForgotPasswordPageComponent },
    { path: 'reset-password', component: ResetPasswordPageComponent },
    { path: 'email-confirmation', component: EmailConfirmationPageComponent }
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
