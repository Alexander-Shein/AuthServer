import {Injectable, EventEmitter} from "@angular/core";
import {ISpinnerService} from "./i-spinner.service";
import {
    Router, Event as RouterEvent, NavigationStart, NavigationEnd, NavigationCancel,
    NavigationError
} from "@angular/router";


@Injectable()
export class SpinnerService implements ISpinnerService {

    private visible: boolean = false;
    private visibility: EventEmitter<boolean> = new EventEmitter();

    constructor(private router: Router) {

        router.events.subscribe((event: RouterEvent) => {
            this.navigationInterceptor(event);
        });
    }

    public observeVisibility(): EventEmitter<boolean> {
        return this.visibility;
    }

    public show(): void {
        if (this.visible) return;

        this.visible = true;
        this.visibility.emit(this.visible);
    }

    public hide(): void {
        if (!this.visible) return;

        this.visible = false;
        this.visibility.emit(this.visible);
    }

    public isInProgress(): boolean {
        return this.visible;
    }

    private navigationInterceptor(event: RouterEvent): void {
        if (event instanceof NavigationStart) {
            this.show();
        }
        if (event instanceof NavigationEnd) {
            this.hide();
        }

        if (event instanceof NavigationCancel) {
            this.hide();
        }
        if (event instanceof NavigationError) {
            this.hide();
        }
    }
}