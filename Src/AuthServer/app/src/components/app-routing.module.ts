import {NgModule} from "@angular/core";
import {RouterModule} from "@angular/router";
import {PageNotFoundComponent} from "./common/not-found.component";
import {LandingPageComponent} from "./features/landing/landing-page.component";
import {ErrorPageComponent} from "./common/error-page.component";
import {DashboardPageComponent} from "./features/dashboard/dashboard-page.component";
import {AppsResolver} from "./features/business/apps/services/apps-resolver.service";
import {ContactPageComponent} from "./features/contact/contact-page.component";
import {LoggedInGuard} from "./features/auth/services/logged-in.guard";
import {CurrentUserResolver} from "./features/auth/services/current-user-resolver.service";


const appRoutes = [
    {path: '', redirectTo: 'landing', pathMatch: 'full'},
    {path: 'landing', component: LandingPageComponent},
    {
        path: 'contact',
        component: ContactPageComponent,
        resolve:
            {
                user: CurrentUserResolver
            }
    },
    {
        path: 'dashboard',
        component: DashboardPageComponent,
        resolve:
        {
            user: CurrentUserResolver,
            apps: AppsResolver
        },
        canActivate: [LoggedInGuard]
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
