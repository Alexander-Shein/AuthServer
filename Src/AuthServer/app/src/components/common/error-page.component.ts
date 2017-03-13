import {Component, OnInit} from "@angular/core";
import {ActivatedRoute, Params} from "@angular/router";


@Component({
    template: '<div class="text-center"><h1>An error has occurred.</h1><h2>{{message}}</h2></div>'
})
export class ErrorPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute
    ) {}

    public ngOnInit(): void {
        this.route.params.subscribe((params: Params) => {
            this.message = params['message'] || '';
        });
    }

    public message: string;
}
