import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {ClientSettings} from "../models/client-settings";
import {ClientsService} from "./clients.service";
import {Consts} from "../../../consts";


@Injectable()
export class ClientSettingsResolver implements Resolve<ClientSettings> {

    public reject: (p1:Error)=>void;

    constructor(private clientsService: ClientsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<ClientSettings> {
        let returnUrl = route.params[Consts.RedirectUrl];

        return this.clientsService.getClientSettings(returnUrl);
    }

}