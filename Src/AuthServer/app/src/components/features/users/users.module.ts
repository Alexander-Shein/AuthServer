import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {MaterialModule}   from '@angular/material';
import {UsersRoutingModule} from './users-routing.module';
import {DashboardPageComponent} from './dashboard/dashboard-page.component';
import {UserSettingsResolver} from './services/user-settings-resolver.service';
import {UsersService} from './services/users.service';
import {ManageExternalProvidersPageComponent} from './external-providers/manage-external-providers-page.component';
import {ExternalProvidersSettingsResolver} from './services/external-providers-settings-resolver.service';


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        UsersRoutingModule
    ],
    declarations: [
        DashboardPageComponent,
        ManageExternalProvidersPageComponent
    ],
    providers: [
        UsersService,
        UserSettingsResolver,
        ExternalProvidersSettingsResolver
    ]
})
export class UsersModule { }
