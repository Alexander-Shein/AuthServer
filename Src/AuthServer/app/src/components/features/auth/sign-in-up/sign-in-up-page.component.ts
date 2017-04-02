import {Component, OnInit} from "@angular/core";
import {AppVm} from "../../business/apps/models/app-vm";
import {ActivatedRoute} from "@angular/router";


@Component({
    selector: 'au-sign-in-up',
    template: `
        <div class="container-fluid mt-2">

            <div class="row">
                <div class="col-12">
                    <h3 class="text-center"><strong>{{app.name}}</strong>.</h3>
                </div>
            </div>

            <div class="row justify-content-center">
                <md-tab-group class="col-12 col-md-6 col-lg-5 col-xl-4" style="max-width: 320px;">
                    <md-tab label="log in">
                        <au-log-in></au-log-in>
                    </md-tab>
                    <md-tab label="sign up">
                        <au-sign-up></au-sign-up>
                    </md-tab>
                </md-tab-group>
            </div>
            
        </div>
    `
})
export class SignInUpPageComponent implements OnInit {

    constructor(
        private route: ActivatedRoute
    ) {
    }

    public ngOnInit(): void {
        this.route
            .data
            .subscribe((data: {app: AppVm}) => {
                this.app = data.app;
            });
    }

    public app: AppVm;

}