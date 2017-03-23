import {Injectable} from '@angular/core';
import {Router, CanActivate} from '@angular/router';
import {AuthenticationService} from './authentication.service';


@Injectable()
export class LoggedInGuard implements CanActivate {

    constructor(
        private authenticationService: AuthenticationService,
        private router: Router
    ) {}

    public canActivate(): boolean {
        let isLoggedIn = this.authenticationService.isLoggedIn();

        if (!isLoggedIn) {
            this.router
                .navigate(['/log-in']);
        }

        return isLoggedIn;
    }
}
