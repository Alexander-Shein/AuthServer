import {Component, ViewEncapsulation} from "@angular/core";
import {MdIconRegistry} from "@angular/material";


@Component({
    selector: 'au-app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AppComponent {

    public notificationOptions = {
        position: ['top', 'right'],
        timeOut: 8000
    };

}
