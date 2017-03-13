import {Component} from '@angular/core';
import {SpinnerService} from './services/spinner.service';


@Component({
    selector: 'au-spinner',
    templateUrl: './spinner.component.html',
    styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent {

    public showSpinner: boolean = false;

    constructor(
        private spinnerService: SpinnerService
    ) {
        spinnerService
            .observeVisibility()
            .subscribe((value:boolean) => {
                this.showSpinner = value;
            });
    }
}