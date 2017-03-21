import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule}   from "@angular/material";
import {AuthRoutingModule} from "./auth-routing.module";
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
        TwoFactorPageComponent,
        ChangePasswordPageComponent,
        AddPasswordPageComponent,
        AddPhonePageComponent,
        ManageExternalProvidersPageComponent
    ],
    providers: []
})
export class AuthModule { }
