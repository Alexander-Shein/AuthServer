import {NgModule} from '@angular/core';
import {RouterModule} from '@angular/router';
import {PageNotFoundComponent} from './common/not-found.component';
import {LandingPageComponent} from './features/landing/landing-page.component';
import {HomePageComponent} from './features/home/home-page.component';
import {SupportPageComponent} from './features/support/support-page.component';
import {ErrorPageComponent} from "./common/error-page.component";
import {DashboardPageComponent} from "./features/dashboard/dashboard-page.component";
import {UserSettingsResolver} from "./features/auth/services/user-settings-resolver.service";


const appRoutes = [
    {path: 'home', redirectTo: '', pathMatch: 'full'},
    {path: '', component: HomePageComponent},
    {path: 'landing', component: LandingPageComponent},
    {path: 'support', component: SupportPageComponent},
    {
        path: 'dashboard',
        component: DashboardPageComponent,
        resolve:
            {
                userSettings: UserSettingsResolver
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
