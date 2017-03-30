import {Injectable} from "@angular/core";
import {Resolve, RouterStateSnapshot, ActivatedRouteSnapshot} from "@angular/router";
import {ExternalProvidersService} from "./external-providers.service";
import {SearchableExternalProvider} from "../models/searchable-external-provider";


@Injectable()
export class SearchableProvidersResolver implements Resolve<SearchableExternalProvider[]> {

    public reject:(p1:Error)=>void;

    constructor(private externalProvidersService: ExternalProvidersService) {}

    public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<SearchableExternalProvider[]> {
        return this.externalProvidersService.getSearchableProviders();
    }

}