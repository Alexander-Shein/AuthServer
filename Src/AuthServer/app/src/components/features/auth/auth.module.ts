import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule}   from "@angular/material";
import {AuthRoutingModule} from "./auth-routing.module";
import {LogInPageComponent} from "./log-in/log-in-page.component";
import {SignUpPageComponent} from "./sign-up/sign-up-page.component";
import {ClientSettingsResolver} from "./services/client-settings-resolver.service";
import {ClientsService} from "../clients/services/clients.service";
import {AuthenticationService} from "./services/authentication.service";
import {ForgotPasswordPageComponent} from "./password/forgot-password-page.component";
import {ResetPasswordPageComponent} from "./password/reset-password-page.component";
import {EmailConfirmationPageComponent} from "./external-log-in/email-confirmation-page.component";
import {TwoFactorProvidersResolver} from "./services/two-factor-providers-resolver.service";
import {TwoFactorPageComponent} from "./two-factor/two-factor-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        AuthRoutingModule
    ],
    declarations: [
        LogInPageComponent,
        SignUpPageComponent,
        ForgotPasswordPageComponent,
        ResetPasswordPageComponent,
        EmailConfirmationPageComponent,
        TwoFactorPageComponent
    ],
    providers: [
        ClientSettingsResolver,
        ClientsService,
        AuthenticationService,
        TwoFactorProvidersResolver
    ]
})
export class AuthModule { }
