import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { UsersService } from './users.service';
import { UserSettings } from '../models/user-settings';

@Injectable()
export class UserSettingsResolver implements Resolve<UserSettings> {

    reject:(p1:Error)=>void;

    constructor(private usersService: UsersService) {}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<UserSettings> {
        return this.usersService.getUserSettings();
    }

}