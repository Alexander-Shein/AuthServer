import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {ClientsService} from "../../clients/services/clients.service";
import {ClientSettings} from "../../clients/models/client-settings";


@Injectable()
export class ClientSettingsResolver implements Resolve<ClientSettings> {

    reject: (p1:Error)=>void;

    constructor(private clientsService: ClientsService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<ClientSettings> {
        let returnUrl = route.params['returnUrl'];

        return this.clientsService.getClientSettings(returnUrl);
    }

}