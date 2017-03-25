import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {AppsService} from "./apps.service";
import {AppVm} from "../models/app-vm";
import {Consts} from "../../../../consts";
import {Observable} from "rxjs";


@Injectable()
export class AppByUrlResolver implements Resolve<AppVm> {

    public reject:(p1:Error)=>void;

    constructor(private appsService: AppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<AppVm> {
        let returnUrl = route.params[Consts.RedirectUrl];

        return this.appsService.getByUrl(returnUrl);
    }

}

