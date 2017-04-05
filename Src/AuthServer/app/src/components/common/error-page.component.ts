import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Params} from "@angular/router";
import {Consts} from "../consts";


@Component({
    template: `
        <div class="text-center">
            <h3>An error has occurred.</h3>
            <p *ngIf="errorMessage">Error message: <span style="color: red;">{{errorMessage}}</span></p>
        </div>`
})
export class ErrorPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute
    ) {}

    public ngOnInit(): void {
        this.route
            .params
            .subscribe((params: Params) => {
                this.errorMessage = params[Consts.ErrorMessage] || '';
            });
    }

    public errorMessage: string;
}
