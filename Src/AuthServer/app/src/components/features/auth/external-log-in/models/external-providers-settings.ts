import {UserExternalProvider} from './user-external-provider';
import {ExternalProvider} from './external-provider';


export class ExternalProvidersSettings {

    public currentLogIns: UserExternalProvider[];
    public otherLogIns: ExternalProvider[];
    public hasPassword: boolean;

}