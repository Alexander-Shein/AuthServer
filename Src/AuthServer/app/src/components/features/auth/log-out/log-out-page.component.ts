import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {AuthenticationService} from "../services/authentication.service";


@Component({
    selector: 'au-log-out',
    template: '<div class="text-center">Logging out...</div>'
})
export class LogOutPageComponent {

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router
    ) {
        authenticationService
            .logOut();

        router.navigate(['/landing']);
    }
}