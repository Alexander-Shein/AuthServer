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


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        AuthModule,
        AppRoutingModule,
        SimpleNotificationsModule.forRoot()
    ],
    declarations: [
        AppComponent,
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
        UserSettingsResolver
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule { }
