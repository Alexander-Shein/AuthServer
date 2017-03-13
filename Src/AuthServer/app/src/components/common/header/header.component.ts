import {Component} from '@angular/core';
import {MenuItem} from './models/menu-item';

@Component({
    selector: 'au-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

    public menuItems: MenuItem[] = [
        new MenuItem('home', 'home', 'home'),
        new MenuItem('support', 'contact_phone', 'support'),
        new MenuItem('log in', '', 'log-in')
    ];

    constructor() {
    }
}