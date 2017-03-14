import {Component, OnInit} from "@angular/core";
import {MenuItem} from "./models/menu-item";
import {Params, ActivatedRoute} from "@angular/router";
import {Consts} from "../../consts";


@Component({
    selector: 'au-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    public menuItems: MenuItem[] = [
        new MenuItem('home', 'home', 'home'),
        new MenuItem('support', 'contact_phone', 'support'),
        new MenuItem('log in', '', 'log-in')
    ];

    constructor(private route: ActivatedRoute) {
    }

    public display: boolean = true;

    public ngOnInit(): void {
        this.route
            .queryParams
            .subscribe((params: Params) => {
                this.display = !params[Consts.RedirectUrl];
            });
    }
}