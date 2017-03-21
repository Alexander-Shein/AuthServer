import {UserLogInInfo} from './user-log-in-info';
import {ExternalProvider} from './external-provider';


export class ExternalProvidersSettings {

    public currentLogIns: UserLogInInfo[];
    public otherLogIns: ExternalProvider[];
    public hasPassword: boolean;

}