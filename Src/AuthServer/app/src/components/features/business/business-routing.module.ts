import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {AppResolver} from "./apps/services/app-resolver.service";
import {EditAppPageComponent} from "./apps/edit-app-page.component";
import {ExternalProvidersResolver} from "../auth/external-log-in/services/external-providers-resolver.service";
import {CreateAppPageComponent} from "./apps/create-app-page.component";
import {AppPageComponent} from "./apps/app-page.component";


const appRoutes: Routes = [

    {
        path: 'business-apps/create',
        component: CreateAppPageComponent,
        resolve:
            {
                externalProviders: ExternalProvidersResolver
            }
    },
    {
        path: 'business-apps/:id',
        component: AppPageComponent,
        resolve:
            {
                app: AppResolver
            }
    },
    {
        path: 'business-apps/:id/edit',
        component: EditAppPageComponent,
        resolve:
            {
                app: AppResolver,
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
