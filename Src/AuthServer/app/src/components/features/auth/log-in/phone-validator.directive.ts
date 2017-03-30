import {AbstractControl, NG_VALIDATORS, Validator} from "@angular/forms";
import {Directive} from "@angular/core";
let phoneUtil = require('google-libphonenumber').PhoneNumberUtil.getInstance();


@Directive({
    selector: '[phone]',
    providers: [{provide: NG_VALIDATORS, useExisting: PhoneValidatorDirective, multi: true}]
})
export class PhoneValidatorDirective implements Validator {

    public validate(control: AbstractControl): {[key: string]: any} {
        if (!control.value) return null;

        try {
            let phoneNumber = phoneUtil.parse('+' + control.value);

            if (phoneUtil.isValidNumber(phoneNumber)) {
                return null;
            }

        } catch(e) {
        }

        return {'phone': true};
    }

}