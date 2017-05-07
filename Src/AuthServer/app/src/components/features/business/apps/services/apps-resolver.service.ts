import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {AppsService} from "./apps.service";
import {Observable} from "rxjs/Observable";
import {ExtendedAppVm} from "../models/extended-app-vm";


@Injectable()
export class AppsResolver implements Resolve<ExtendedAppVm[]> {

    public reject:(p1:Error)=>void;

    constructor(private appsService: AppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ExtendedAppVm[]> {
        return this.appsService.getAll();
    }

}