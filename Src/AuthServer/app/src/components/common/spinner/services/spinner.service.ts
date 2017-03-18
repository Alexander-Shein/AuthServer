import {Injectable, EventEmitter} from "@angular/core";
import {ISpinnerService} from "./i-spinner.service";


@Injectable()
export class SpinnerService implements ISpinnerService {

    private visible: boolean = false;
    private visibility: EventEmitter<boolean> = new EventEmitter();

    public observeVisibility(): EventEmitter<boolean> {
        return this.visibility;
    }

    public  show(): void {
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
}