import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {ExternalProvidersSettings} from "../models/external-providers-settings";
import {ExternalProvidersService} from "./external-providers.service";


@Injectable()
export class ExternalProvidersSettingsResolver implements Resolve<ExternalProvidersSettings> {

    public reject:(p1:Error)=>void;

    constructor(private externalProvidersService: ExternalProvidersService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<ExternalProvidersSettings> {
        return this.externalProvidersService.getExternalProvidersSettings();
    }

}