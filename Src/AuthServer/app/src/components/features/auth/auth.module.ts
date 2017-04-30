import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule}   from "@angular/material";
import {AuthRoutingModule} from "./auth-routing.module";
import {LogInComponent} from "./log-in/log-in.component";
import {SignUpComponent} from "./sign-up/sign-up.component";
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
import {EmailOrPhoneValidatorDirective} from "./log-in/email-or-phone-validator.directive";
import {PhoneValidatorDirective} from "./log-in/phone-validator.directive";
import {SocialNetworkButtonComponent} from "./external-log-in/social-network-button.component";
import {LogInPasswordComponent} from "./log-in/log-in-password.component";
import {UserNameComponent} from "./shared/user-name.component";
import {SignInUpPageComponent} from "./sign-in-up/sign-in-up-page.component";
import {SignUpPasswordComponent} from "./sign-up/sign-up-password.component";
import {AddEmailPageComponent} from "./manage-emails/add-email-page.component";
import {IconUserNameComponent} from "./shared/icon-user-name.component";
import {AccountConfirmationPageComponent} from "./sign-up/account-confirmation-page.component";
import {ShowHidePasswordComponent} from "./shared/show-hide-password.component";
import {SignUpPasswordlessComponent} from "./sign-up/sign-up-passwordless.component";
import {SignUpPasswordlessConfirmationPageComponent} from "./sign-up/sign-up-passwordless-confirmation-page.component";
import {LogInPasswordlessComponent} from "./log-in/log-in-passwordless.component";
import {LogInPasswordlessConfirmationPageComponent} from "./log-in/log-in-passwordless-confirmation-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        AuthRoutingModule
    ],
    declarations: [
        LogInComponent,
        LogOutPageComponent,
        SignUpComponent,
        ForgotPasswordPageComponent,
        ResetPasswordPageComponent,
        EmailConfirmationPageComponent,
        TwoFactorPageComponent,
        ChangePasswordPageComponent,
        AddPasswordPageComponent,
        AddPhonePageComponent,
        AddEmailPageComponent,
        ManageExternalProvidersPageComponent,
        ExternalLogInCallbackPageComponent,
        EmailOrPhoneValidatorDirective,
        PhoneValidatorDirective,
        SocialNetworkButtonComponent,
        LogInPasswordComponent,
        UserNameComponent,
        SignInUpPageComponent,
        SignUpPasswordComponent,
        IconUserNameComponent,
        AccountConfirmationPageComponent,
        ShowHidePasswordComponent,
        SignUpPasswordlessComponent,
        SignUpPasswordlessConfirmationPageComponent,
        LogInPasswordlessComponent,
        LogInPasswordlessConfirmationPageComponent
    ],
    providers: []
})
export class AuthModule { }
