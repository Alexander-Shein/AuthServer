import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {BusinessApp} from "../models/business-app";
import {BusinessAppsService} from "./business-apps.service";


@Injectable()
export class BusinessAppResolver implements Resolve<BusinessApp> {

    public reject:(p1:Error)=>void;

    constructor(private businessApps: BusinessAppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<BusinessApp> {
        let appName: string = route.params['name'];

        return this.businessApps.get(appName);
    }

}