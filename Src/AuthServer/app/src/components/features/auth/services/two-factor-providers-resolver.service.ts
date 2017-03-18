import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {Provider} from "../models/provider";
import {AuthenticationService} from "./authentication.service";


@Injectable()
export class TwoFactorProvidersResolver implements Resolve<Provider[]> {

    reject:(p1:Error)=>void;

    constructor(private authenticationService: AuthenticationService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<Provider[]> {
        return this.authenticationService.getTwoFactorProviders();
    }

}