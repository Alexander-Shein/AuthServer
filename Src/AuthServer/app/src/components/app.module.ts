import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule}   from "@angular/material";
import {AppComponent} from "./app.component";
import {AppRoutingModule} from "./app-routing.module";
import {PageNotFoundComponent} from "./common/not-found.component";
import {HeaderComponent} from "./common/header/header.component";
import {HomePageComponent} from "./features/home/home-page.component";
import {LandingPageComponent} from "./features/landing/landing-page.component";
import {SupportPageComponent} from "./features/support/support-page.component";
import {AuthModule} from "./features/auth/auth.module";
import {SimpleNotificationsModule} from "angular2-notifications";
import {SpinnerComponent} from "./common/spinner/spinner.component";
import {SpinnerService} from "./common/spinner/services/spinner.service";
import {ErrorPageComponent} from "./common/error-page.component";
import {DashboardPageComponent} from "./features/dashboard/dashboard-page.component";
import {UserSettingsResolver} from "./features/auth/services/user-settings-resolver.service";
import {AuthenticationService} from "./features/auth/services/authentication.service";
import {PasswordsService} from "./features/auth/manage-passwords/services/passwords.service";
import {PhonesService} from "./features/auth/manage-phones/services/phones.service";
import {TwoFactorService} from "./features/auth/two-factor/services/two-factor.service";
import {ClientSettingsResolver} from "./features/business/services/client-settings-resolver.service";
import {ClientsService} from "./features/business/services/clients.service";
import {TwoFactorProvidersResolver} from "./features/auth/two-factor/services/two-factor-providers-resolver.service";
import {ExternalProvidersSettingsResolver} from "./features/auth/external-log-in/services/external-providers-settings-resolver.service";
import {ExternalProvidersService} from "./features/auth/external-log-in/services/external-providers.service";
import {BusinessAppsService} from "./features/business/business-apps/services/business-apps.service";
import {BusinessAppsResolver} from "./features/business/business-apps/services/business-apps-resolver.service";
import {UsersService} from "./features/auth/services/users.service";
import {BusinessModule} from "./features/business/business.module";
import {BusinessAppResolver} from "./features/business/business-apps/services/business-app-resolver.service";
import {ConfirmationDialogComponent} from "./common/pop-ups/confirmation-dialog.component";
import {ExternalProvidersResolver} from "./features/auth/external-log-in/services/external-providers-resolver.service";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        AuthModule,
        BusinessModule,
        AppRoutingModule,
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [
        AppComponent,
        ConfirmationDialogComponent,
        PageNotFoundComponent,
        ErrorPageComponent,
        HeaderComponent,
        HomePageComponent,
        LandingPageComponent,
        SupportPageComponent,
        SpinnerComponent,
        DashboardPageComponent
    ],
    providers: [
        SpinnerService,
        AuthenticationService,
        PasswordsService,
        UserSettingsResolver,
        PhonesService,
        TwoFactorService,
        BusinessAppsService,
        BusinessAppsResolver,
        BusinessAppResolver,
        ClientSettingsResolver,
        ClientsService,
        TwoFactorProvidersResolver,
        ExternalProvidersSettingsResolver,
        ExternalProvidersService,
        UsersService,
        ExternalProvidersResolver
    ],
    bootstrap: [ AppComponent, ConfirmationDialogComponent ]
})
export class AppModule { }
