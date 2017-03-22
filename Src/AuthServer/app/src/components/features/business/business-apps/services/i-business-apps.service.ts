import {BusinessApp} from "../models/business-app";
import {BusinessAppVm} from "../models/business-app-vm";


export interface IBusinessAppsService {

    getAll(): Promise<BusinessApp[]>;
    put(businessApp: BusinessApp): Promise<BusinessApp>;
    get(name: string): Promise<BusinessApp>;
    remove(name: string): Promise<void>;
    getByUrl(url: string): Promise<BusinessAppVm>;

}