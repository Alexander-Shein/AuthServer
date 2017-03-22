import {Component, ViewEncapsulation} from "@angular/core";


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
