import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule} from "@angular/material";
import {BusinessRoutingModule} from "./business-routing.module";
import {AppPageComponent} from "./apps/app-page.component";
import {EditAppPageComponent} from "./apps/edit-app-page.component";
import {CreateAppPageComponent} from "./apps/create-app-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        BusinessRoutingModule
    ],
    declarations: [
        AppPageComponent,
        CreateAppPageComponent,
        EditAppPageComponent
    ],
    providers: [
    ]
})
export class BusinessModule { }
