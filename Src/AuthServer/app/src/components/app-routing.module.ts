import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {PageNotFoundComponent} from './common/not-found.component';
import {LandingPageComponent} from './features/landing/landing-page.component';
import {ErrorPageComponent} from "./common/error-page.component";
import {DashboardPageComponent} from "./features/dashboard/dashboard-page.component";
import {UserSettingsResolver} from "./features/auth/services/user-settings-resolver.service";
import {BusinessAppsResolver} from "./features/business/business-apps/services/business-apps-resolver.service";
import {ContactPageComponent} from "./features/contact/contact-page.component";


const appRoutes = [
    {path: '', redirectTo: 'landing', pathMatch: 'full'},
    {path: 'landing', component: LandingPageComponent},
    {path: 'contact', component: ContactPageComponent},
    {
        path: 'dashboard',
        component: DashboardPageComponent,
        resolve:
            {
                userSettings: UserSettingsResolver,
                businessApps: BusinessAppsResolver
            }
    },
    {path: 'error', component: ErrorPageComponent},
    {path: '**', component: PageNotFoundComponent}
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule {}
