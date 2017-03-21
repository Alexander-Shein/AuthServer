import { Injectable } from "@angular/core";
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from "@angular/router";
import { UserSettings } from "../models/user-settings";
import {UsersService} from "./users.service";


@Injectable()
export class UserSettingsResolver implements Resolve<UserSettings> {

    public reject:(p1:Error)=>void;

    public constructor(private usersService: UsersService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<UserSettings> {
        return this.usersService.getUserSettings();
    }

}