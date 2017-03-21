import {NgModule} from "@angular/core";
import {BrowserModule} from "@angular/platform-browser";
import {FormsModule} from "@angular/forms";
import {MaterialModule}   from "@angular/material";
import {BusinessRoutingModule} from "./business-routing.module";
import {BusinessAppPageComponent} from "./business-apps/business-app-page.component";
import {EditBusinessAppPageComponent} from "./business-apps/edit-business-app-page.component";
import {CreateBusinessAppPageComponent} from "./business-apps/create-business-app-page.component";


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        MaterialModule,
        BusinessRoutingModule
    ],
    declarations: [
        BusinessAppPageComponent,
        CreateBusinessAppPageComponent,
        EditBusinessAppPageComponent
    ],
    providers: [
    ]
})
export class BusinessModule { }
