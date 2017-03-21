import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {BusinessAppResolver} from "./business-apps/services/business-app-resolver.service";
import {EditBusinessAppPageComponent} from "./business-apps/edit-business-app-page.component";
import {ExternalProvidersResolver} from "../auth/external-log-in/services/external-providers-resolver.service";
import {CreateBusinessAppPageComponent} from "./business-apps/create-business-app-page.component";
import {BusinessAppPageComponent} from "./business-apps/business-app-page.component";


const appRoutes: Routes = [

    {
        path: 'business-apps/create',
        component: CreateBusinessAppPageComponent,
        resolve:
            {
                externalProviders: ExternalProvidersResolver
            }
    },
    {
        path: 'business-apps/:name',
        component: BusinessAppPageComponent,
        resolve:
            {
                businessApp: BusinessAppResolver
            }
    },
    {
        path: 'business-apps/:name/edit',
        component: EditBusinessAppPageComponent,
        resolve:
            {
                businessApp: BusinessAppResolver,
                externalProviders: ExternalProvidersResolver
            }
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class BusinessRoutingModule {}
