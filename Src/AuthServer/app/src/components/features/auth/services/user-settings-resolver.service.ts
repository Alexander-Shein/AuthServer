import { Injectable } from "@angular/core";
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from "@angular/router";
import { UserSettings } from "../models/user-settings";
import {AuthenticationService} from "./authentication.service";


@Injectable()
export class UserSettingsResolver implements Resolve<UserSettings> {

    public reject:(p1:Error)=>void;

    public constructor(private authenticationService: AuthenticationService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<UserSettings> {
        return this.authenticationService.getUserSettings();
    }

}