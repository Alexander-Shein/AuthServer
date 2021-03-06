import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {HttpModule} from '@angular/http';
import {MaterialModule, MdDialogModule}   from "@angular/material";
import {AppComponent} from "./app.component";
import {AppRoutingModule} from "./app-routing.module";
import {PageNotFoundComponent} from "./common/not-found.component";
import {HeaderComponent} from "./common/header/header.component";
import {LandingPageComponent} from "./features/landing/landing-page.component";
import {AuthModule} from "./features/auth/auth.module";
import {SimpleNotificationsModule} from "angular2-notifications";
import {SpinnerComponent} from "./common/spinner/spinner.component";
import {SpinnerService} from "./common/spinner/services/spinner.service";
import {ErrorPageComponent} from "./common/error-page.component";
import {DashboardPageComponent} from "./features/dashboard/dashboard-page.component";
import {CurrentUserResolver} from "./features/auth/services/current-user-resolver.service";
import {AuthenticationService} from "./features/auth/services/authentication.service";
import {PasswordsService} from "./features/auth/manage-passwords/services/passwords.service";
import {TwoFactorService} from "./features/auth/two-factor/services/two-factor.service";
import {TwoFactorProvidersResolver} from "./features/auth/two-factor/services/two-factor-providers-resolver.service";
import {ExternalProvidersService} from "./features/auth/external-log-in/services/external-providers.service";
import {AppsService} from "./features/business/apps/services/apps.service";
import {AppsResolver} from "./features/business/apps/services/apps-resolver.service";
import {UsersService} from "./features/auth/services/users.service";
import {BusinessModule} from "./features/business/business.module";
import {AppResolver} from "./features/business/apps/services/app-resolver.service";
import {ConfirmationDialogComponent} from "./common/pop-ups/confirmation-dialog.component";
import {ExternalProvidersResolver} from "./features/auth/external-log-in/services/external-providers-resolver.service";
import {AppByUrlResolver} from "./features/business/apps/services/app-by-url-resolver.service";
import {ContactPageComponent} from "./features/contact/contact-page.component";
import {MessagesService} from "./features/contact/services/messages.service";
import {Ng2Webstorage} from "ng2-webstorage";
import {LoggedInGuard} from "./features/auth/services/logged-in.guard";
import {JwtService} from "./features/auth/services/jwt.service";
import {SearchableProvidersResolver} from "./features/auth/external-log-in/services/searchable-providers-resolver";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {PasswordlessService} from "./features/auth/passwordless/services/passwordless.service";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        MaterialModule,
        MdDialogModule,
        AuthModule,
        BusinessModule,
        AppRoutingModule,
        SimpleNotificationsModule.forRoot(),
        Ng2Webstorage.forRoot({ prefix: 'au', separator: '-' }),
        BrowserAnimationsModule
    ],
    declarations: [
        AppComponent,
        ConfirmationDialogComponent,
        PageNotFoundComponent,
        ErrorPageComponent,
        HeaderComponent,
        LandingPageComponent,
        ContactPageComponent,
        SpinnerComponent,
        DashboardPageComponent
    ],
    providers: [
        SpinnerService,
        AuthenticationService,
        PasswordsService,
        CurrentUserResolver,
        TwoFactorService,
        AppsService,
        AppsResolver,
        AppResolver,
        AppByUrlResolver,
        TwoFactorProvidersResolver,
        ExternalProvidersService,
        UsersService,
        ExternalProvidersResolver,
        SearchableProvidersResolver,
        MessagesService,
        LoggedInGuard,
        JwtService,
        PasswordlessService
    ],
    entryComponents: [ ConfirmationDialogComponent ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
