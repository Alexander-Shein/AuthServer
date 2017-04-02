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
import {LogOutPageComponent} from "./log-out/log-out-page.component";
import {ExternalLogInCallbackPageComponent} from "./external-log-in/external-log-in-callback-page.component";
import {EmailLogInComponent} from "./log-in/email-log-in.component";
import {PhoneLogInComponent} from "./log-in/phone-log-in.component";
import {EmailOrPhoneValidatorDirective} from "./log-in/email-or-phone-validator.directive";
import {PhoneValidatorDirective} from "./log-in/phone-validator.directive";
import {SocialNetworkButtonComponent} from "./external-log-in/social-network-button.component";
import {LogInPasswordComponent} from "./log-in/log-in-password.component";
import {UserNameComponent} from "./shared/user-name.component";
import {SignInUpPageComponent} from "./sign-in-up/sign-in-up-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        AuthRoutingModule
    ],
    declarations: [
        LogInPageComponent,
        EmailLogInComponent,
        PhoneLogInComponent,
        LogOutPageComponent,
        SignUpPageComponent,
        ForgotPasswordPageComponent,
        ResetPasswordPageComponent,
        EmailConfirmationPageComponent,
        TwoFactorPageComponent,
        ChangePasswordPageComponent,
        AddPasswordPageComponent,
        AddPhonePageComponent,
        ManageExternalProvidersPageComponent,
        ExternalLogInCallbackPageComponent,
        EmailOrPhoneValidatorDirective,
        PhoneValidatorDirective,
        SocialNetworkButtonComponent,
        LogInPasswordComponent,
        UserNameComponent,
        SignInUpPageComponent
    ],
    providers: []
})
export class AuthModule { }
