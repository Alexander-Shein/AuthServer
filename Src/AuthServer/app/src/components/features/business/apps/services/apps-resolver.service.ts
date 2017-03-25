import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {App} from "../models/app";
import {AppsService} from "./apps.service";


@Injectable()
export class AppsResolver implements Resolve<App[]> {

    public reject:(p1:Error)=>void;

    constructor(private appsService: AppsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<App[]> {
        return this.appsService.getAll();
    }

}