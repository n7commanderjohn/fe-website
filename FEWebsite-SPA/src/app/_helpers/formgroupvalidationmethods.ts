import { FormGroup } from '@angular/forms';

export const FormStrings = {
    gender: 'gender',
    name: 'name',
    description: 'description',
    birthday: 'birthday',
    required: 'required',
    minlength: 'minlength',
    maxlength: 'maxlength',
    email: 'email',
    username: 'username',
    password: 'password',
    passwordConfirm: 'passwordConfirm',
    mismatch: 'mismatch'
};

export class FormGroupValidatorMethods {
    email = {
        hasErrors(fg: FormGroup) {
            return fg.get(FormStrings.email).errors && fg.get(FormStrings.email).touched;
        },
    };
    username = {
        hasErrors(fg: FormGroup) {
            return fg.get(FormStrings.username).errors && fg.get(FormStrings.username).touched;
        },
    };
    password = {
        hasErrors(fg: FormGroup) {
            return fg.get(FormStrings.password).errors && fg.get(FormStrings.password).touched;
        },
        required(fg: FormGroup) {
            return fg.get(FormStrings.password).hasError(FormStrings.required) && fg.get(FormStrings.password).touched;
        },
        minLength(fg: FormGroup) {
            return fg.get(FormStrings.password).hasError(FormStrings.minlength) && fg.get(FormStrings.password).touched;
        },
        maxLength(fg: FormGroup) {
            return fg.get(FormStrings.password).hasError(FormStrings.maxlength) && fg.get(FormStrings.password).touched;
        },
    };
    passwordConfirm = {
        hasErrors(fg: FormGroup) {
            return (fg.get(FormStrings.passwordConfirm).errors || fg.hasError(FormStrings.mismatch))
                && fg.get(FormStrings.passwordConfirm).touched;
        },
        required(fg: FormGroup) {
            return fg.get(FormStrings.passwordConfirm).hasError(FormStrings.required) && fg.get(FormStrings.passwordConfirm).touched;
        },
        matches(fg: FormGroup) {
            return fg.get(FormStrings.password).value === fg.get(FormStrings.passwordConfirm).value ?
                null : { mismatch: true };
        },
        mismatch(fg: FormGroup) {
            return fg.get(FormStrings.passwordConfirm).value
                && fg.hasError(FormStrings.mismatch)
                && fg.get(FormStrings.passwordConfirm).touched;
        }
    };
    birthday = {
        hasErrors(fg: FormGroup) {
            return fg.get(FormStrings.birthday).touched && fg.get(FormStrings.birthday).errors;
        },
        required(fg: FormGroup) {
            return fg.get(FormStrings.birthday).touched && fg.get(FormStrings.birthday).hasError(FormStrings.required);
        }
    };
}
