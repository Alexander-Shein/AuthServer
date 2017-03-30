import {AbstractControl, NG_VALIDATORS, Validator, Validators} from "@angular/forms";
import {Directive} from "@angular/core";
let phoneUtil = require('google-libphonenumber').PhoneNumberUtil.getInstance();


@Directive({
    selector: '[emailOrPhone]',
    providers: [{provide: NG_VALIDATORS, useExisting: EmailOrPhoneValidatorDirective, multi: true}]
})
export class EmailOrPhoneValidatorDirective implements Validator {

    public validate(control: AbstractControl): {[key: string]: any} {
        if (!control.value) return null;

        let emailValidationResult = Validators.email(control);

        if (!emailValidationResult) {
            return null;
        }

        try {
            let phoneNumber = phoneUtil.parse('+' + control.value);

            if (phoneUtil.isValidNumber(phoneNumber)) {
                return null;
            }

        } catch(e) {
        }

        return {'emailOrPhone': true};
    }

}