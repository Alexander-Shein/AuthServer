import {Component, Input} from "@angular/core";
import "../../../../assets/icons/facebook.svg";
import "../../../../assets/icons/google.svg";
import "../../../../assets/icons/microsoft.svg";
import {MdIconRegistry} from "@angular/material";
import {DomSanitizer} from "@angular/platform-browser";
import {ExternalProvider} from "./models/external-provider";


@Component({
    selector: 'au-social-network-button',
    template: `
        <button
                [ngClass]="getIconName()"
                class="w-100 mb-1 text-left"
                md-raised-button
                color="primary">
            <md-icon svgIcon="{{getIconName()}}"></md-icon>
            <span class="ml-2">{{namePrefix}} {{provider.displayName}} {{namePostfix}}</span>
        </button>
    `,
    styleUrls: ['./social-network-button.component.scss']
})
export class SocialNetworkButtonComponent {

    @Input()
    public provider: ExternalProvider;

    @Input()
    public namePrefix: string = '';

    @Input()
    public namePostfix: string = '';

    constructor(
        iconRegistry: MdIconRegistry, sanitizer: DomSanitizer
    ) {
        iconRegistry.addSvgIcon(
            'facebook',
            sanitizer.bypassSecurityTrustResourceUrl('/assets/facebook.svg'));

        iconRegistry.addSvgIcon(
            'google',
            sanitizer.bypassSecurityTrustResourceUrl('/assets/google.svg'));

        iconRegistry.addSvgIcon(
            'microsoft',
            sanitizer.bypassSecurityTrustResourceUrl('/assets/microsoft.svg'));
    }

    public getIconName() {
        let scheme = this.provider.authenticationScheme;

        if (scheme === 'Negotiate') {
            return 'microsoft';
        }

        return scheme.toLowerCase();
    }

}