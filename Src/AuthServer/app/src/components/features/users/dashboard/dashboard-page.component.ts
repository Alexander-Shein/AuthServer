import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'
import { UserSettings } from '../models/user-settings';

@Component({
    selector: 'au-dashboard',
    templateUrl: './dashboard-page.component.html',
    styleUrls: ['./dashboard-page.component.scss']
})
export class DashboardPageComponent implements OnInit {
    constructor(
        private route: ActivatedRoute,
        private router: Router) {}

    public userSettings:UserSettings;

    public ngOnInit():void {
        this.route.data
            .subscribe((data: { userSettings: UserSettings }) => {
                this.userSettings = data.userSettings;
            });
    }

}