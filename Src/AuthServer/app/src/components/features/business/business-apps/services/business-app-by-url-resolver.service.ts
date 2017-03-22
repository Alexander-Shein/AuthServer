import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {BusinessAppsService} from "./business-apps.service";
import {BusinessAppVm} from "../models/business-app-vm";
import {Consts} from "../../../../consts";


@Injectable()
export class BusinessAppByUrlResolver implements Resolve<BusinessAppVm> {

    public reject:(p1:Error)=>void;

    constructor(private businessApps: BusinessAppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<BusinessAppVm> {
        let returnUrl = route.params[Consts.RedirectUrl];

        return this.businessApps.getByUrl(returnUrl);
    }

}

