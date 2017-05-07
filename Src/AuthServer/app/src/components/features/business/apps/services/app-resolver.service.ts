import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {AppsService} from "./apps.service";
import {ExtendedAppVm} from "../models/extended-app-vm";
import {Observable} from "rxjs/Observable";


@Injectable()
export class AppResolver implements Resolve<ExtendedAppVm> {

    public reject:(p1:Error)=>void;

    constructor(private appsService: AppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ExtendedAppVm> {
        let id: string = route.params['id'];

        return this.appsService.get(id);
    }

}