import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {App} from "../models/app";
import {AppsService} from "./apps.service";
import {Observable} from "rxjs/Observable";


@Injectable()
export class AppsResolver implements Resolve<App[]> {

    public reject:(p1:Error)=>void;

    constructor(private appsService: AppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<App[]> {
        return this.appsService.getAll();
    }

}