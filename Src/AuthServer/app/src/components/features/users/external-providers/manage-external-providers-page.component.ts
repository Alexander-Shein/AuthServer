import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router'
import { ExternalProvidersSettings } from '../models/external-providers-settings';

@Component({
    selector: 'au-manage-external-providers',
    templateUrl: './manage-external-providers-page.component.html',
    styleUrls: ['./manage-external-providers-page.component.scss']
})
export class ManageExternalProvidersPageComponent implements OnInit {
    constructor(
        private route: ActivatedRoute) {
    }
    public externalProvidersSettings:ExternalProvidersSettings;
    public canDeleteExternalLogin: boolean;

    public ngOnInit():void {
        this.route.data
            .subscribe((data: { externalProvidersSettings: ExternalProvidersSettings }) => {
                this.externalProvidersSettings = data.externalProvidersSettings;

                this.canDeleteExternalLogin =
                    this.externalProvidersSettings.hasPassword ||
                    this.externalProvidersSettings.currentLogins.length > 1;
            });
    }

    public deleteExternalLogin():void {

    }

}