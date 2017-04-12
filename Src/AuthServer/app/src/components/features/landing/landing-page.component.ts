import { Component } from "@angular/core";
import {AuthenticationService} from "../auth/services/authentication.service";


@Component({
    selector: 'au-app',
    templateUrl: './landing-page.component.html',
    styleUrls: ['./landing-page.component.scss']
})
export class LandingPageComponent {

    constructor(
        private authenticationService: AuthenticationService
    ) {
        this.isLoggedIn = authenticationService.isLoggedIn();
    }

    public isLoggedIn: boolean;
}