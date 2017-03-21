import {BusinessApp} from "../models/business-app";


export interface IBusinessAppsService {

    getAll(): Promise<BusinessApp[]>;
    put(businessApp: BusinessApp): Promise<BusinessApp>;
    get(name: string): Promise<BusinessApp>;
    remove(name: string): Promise<void>;

}