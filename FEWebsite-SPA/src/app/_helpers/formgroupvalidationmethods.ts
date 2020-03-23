import { FormGroup, FormArray, ValidationErrors } from '@angular/forms';
import { Injectable } from '@angular/core';

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
    passwordCurrent: 'passwordCurrent',
    password: 'password',
    passwordConfirm: 'passwordConfirm',
    passwordNewRequired: 'passwordNewRequired',
    passwordCurrentRequired: 'passwordCurrentRequired',
    mismatch: 'mismatch',
    games: 'games',
    genres: 'genres',
};

@Injectable()
export class FormGroupValidatorMethods {
    customValidators = {
        passwordNewRequired(fg: FormGroup): ValidationErrors | null {
            const passwordNew = fg.get(FormStrings.password);

            return !passwordNew.value && passwordNew.enabled ?
                { passwordNewRequired: true } : null;
        },
        passwordCurrentRequired(fg: FormGroup): ValidationErrors | null {
            const passwordCurrentFilled = fg.get(FormStrings.passwordCurrent).value;
            const newPasswordEnabled = fg.get(FormStrings.password).enabled;
            const usernameControl = fg.get(FormStrings.username);
            const emailControl = fg.get(FormStrings.email);

            return (!passwordCurrentFilled && newPasswordEnabled)
                || (!passwordCurrentFilled && usernameControl.value && usernameControl.dirty)
                || (!passwordCurrentFilled && emailControl.value && emailControl.dirty) ?
                { passwordCurrentRequired: true } : null;
        },
        confirmationPasswordMatches(fg: FormGroup): ValidationErrors | null {
            const passwordNewVal = fg.get(FormStrings.password).value;
            const passwordConfirmVal = fg.get(FormStrings.passwordConfirm).value;

            return passwordNewVal !== passwordConfirmVal ?
                { mismatch: true } : null;
        },
    };
    email = {
        hasErrors(fg: FormGroup) {
            return fg.get(FormStrings.email).errors;
        },
        hasErrorsAndTouched(fg: FormGroup) {
            const emailControl = fg.get(FormStrings.email);
            return emailControl.errors && emailControl.touched;
        },
    };
    name = {
        hasErrors(fg: FormGroup) {
            const nameControl = fg.get(FormStrings.name);
            return nameControl.errors && nameControl.touched;
        },
    };
    username = {
        hasErrors(fg: FormGroup) {
            const unControl = fg.get(FormStrings.username);
            return unControl.errors && unControl.touched;
        },
    };
    passwordCurrent = {
        hasErrors(fg: FormGroup) {
            const pwCurrControl = fg.get(FormStrings.passwordCurrent);
            const pwControl = fg.get(FormStrings.password);
            return pwCurrControl.errors
                || fg.hasError(FormStrings.passwordCurrentRequired)
                || (!pwCurrControl.value && pwControl.value);
        },
        toggleOtherPasswordFields(fg: FormGroup, passwordChangeMode: boolean) {
            const pwControl = fg.get(FormStrings.password);
            const pwConfControl = fg.get(FormStrings.passwordConfirm);
            if (passwordChangeMode) {
                const value = { value: null, disabled: false };
                pwControl.reset(value);
                pwConfControl.reset(value);
            } else {
                const value = { value: null, disabled: true };
                pwControl.reset(value);
                pwConfControl.reset(value);
            }
        },
        requiredForUserName(fg: FormGroup) {
            return fg.get(FormStrings.username).value
                && fg.get(FormStrings.username).dirty
                && fg.hasError(FormStrings.passwordCurrentRequired);
        },
        requiredForEmail(fg: FormGroup) {
            return fg.get(FormStrings.email).value
                && fg.get(FormStrings.email).dirty
                && fg.hasError(FormStrings.passwordCurrentRequired);
        },
        requiredForPasswordChange(fg: FormGroup) {
            return fg.get(FormStrings.password).enabled
                && fg.hasError(FormStrings.passwordCurrentRequired);
        },
    };
    password = {
        hasErrors(fg: FormGroup) {
            const pwControl = fg.get(FormStrings.password);
            return (pwControl.errors || fg.hasError(FormStrings.passwordNewRequired))
                && pwControl.touched;
        },
        required(fg: FormGroup) {
            const pwControl = fg.get(FormStrings.password);
            return pwControl.hasError(FormStrings.required) && pwControl.touched;
        },
        minLength(fg: FormGroup) {
            const pwControl = fg.get(FormStrings.password);
            return pwControl.hasError(FormStrings.minlength) && pwControl.touched;
        },
        maxLength(fg: FormGroup) {
            const pwControl = fg.get(FormStrings.password);
            return pwControl.hasError(FormStrings.maxlength) && pwControl.touched;
        },
        passwordNewRequired(fg: FormGroup) {
            return fg.hasError(FormStrings.passwordNewRequired);
        },

    };
    passwordConfirm = {
        hasErrors(fg: FormGroup) {
            const pwConfControl = fg.get(FormStrings.passwordConfirm);
            return (pwConfControl.errors || fg.hasError(FormStrings.mismatch))
                && pwConfControl.touched;
        },
        required(fg: FormGroup) {
            const pwConfControl = fg.get(FormStrings.passwordConfirm);
            return pwConfControl.hasError(FormStrings.required) && pwConfControl.touched;
        },
        confirmationPasswordMatches(fg: FormGroup) {
            return (fg.get(FormStrings.passwordConfirm).touched
                && fg.hasError(FormStrings.mismatch));
        }
    };
    birthday = {
        hasErrors(fg: FormGroup) {
            const bdayControl = fg.get(FormStrings.birthday);
            return bdayControl.touched && bdayControl.errors;
        },
        required(fg: FormGroup) {
            const bdayControl = fg.get(FormStrings.birthday);
            return bdayControl.touched && bdayControl.hasError(FormStrings.required);
        }
    };
    games = {
        getFavoriteGames(fg: FormGroup) {
            return (fg.get(FormStrings.games) as FormArray).controls;
        }
    };
    genres = {
        getFavoriteGenres(fg: FormGroup) {
            return (fg.get(FormStrings.genres) as FormArray).controls;
        }
    };
}
