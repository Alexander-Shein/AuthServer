import {Component} from "@angular/core";
import {MdDialogRef} from "@angular/material";


@Component({
    selector: 'au-confirmation-dialog',
    templateUrl: './confirmation-dialog.component.html',
    styleUrls: ['./confirmation-dialog.component.scss']
})
export class ConfirmationDialogComponent {
    constructor(public dialogRef: MdDialogRef<ConfirmationDialogComponent>) {}
}