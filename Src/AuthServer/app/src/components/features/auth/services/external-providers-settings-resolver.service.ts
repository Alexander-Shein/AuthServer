import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {AuthenticationService} from "./authentication.service";


@Injectable()
export class ExternalProvidersSettingsResolver implements Resolve<ExternalProvidersSettings> {

    reject:(p1:Error)=>void;

    constructor(private authenticationService: AuthenticationService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<ExternalProvidersSettings> {
        return this.authenticationService.getExternalProvidersSettings();
    }

}