import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { UsersService } from './users.service';
import { ExternalProvidersSettings } from '../models/external-providers-settings';

@Injectable()
export class ExternalProvidersSettingsResolver implements Resolve<ExternalProvidersSettings> {

    reject:(p1:Error)=>void;

    constructor(private usersService: UsersService) {}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<ExternalProvidersSettings> {
        return this.usersService.getExternalProvidersSettings();
    }

}