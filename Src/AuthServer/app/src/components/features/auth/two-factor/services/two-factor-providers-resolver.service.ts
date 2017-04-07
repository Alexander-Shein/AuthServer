import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {TwoFactorService} from "../services/two-factor.service";
import {Provider} from "../models/provider";
import {Observable} from "rxjs";


@Injectable()
export class TwoFactorProvidersResolver implements Resolve<Provider[]> {

    public reject:(p1:Error)=>void;

    constructor(private twoFactorService: TwoFactorService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Provider[]> {
        return this.twoFactorService.getTwoFactorProviders();
    }

}