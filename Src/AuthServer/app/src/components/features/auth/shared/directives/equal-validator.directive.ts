import {Directive, forwardRef, Attribute} from "@angular/core";
import {Validator, AbstractControl, NG_VALIDATORS} from "@angular/forms";


@Directive({
    selector: '[equalsto][formControlName],[equalsto][formControl],[equalsto][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => EqualValidator), multi: true }
    ]
})
export class EqualValidator implements Validator {
    constructor( @Attribute('equalsto') public validateEqual: string) {}

    validate(c: AbstractControl): { [key: string]: boolean } {
        // self value (e.g. retype password)
        let v = c.value;

        // control value (e.g. password)
        let e = c.root.get(this.validateEqual);

        // value not equal
        if (e && v !== e.value) return {
            equalsto: true
        };
        return null;
    }
}