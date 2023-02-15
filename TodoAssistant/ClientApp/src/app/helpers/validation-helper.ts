import { FormGroup } from '@angular/forms';

export function validateFormGenerally(
  form: FormGroup,
  validationMessages?: any,
  onFail?: (message?: string) => void
): boolean {
  let res = false;

  // Control errors
  for (const controlKey in form.controls) {
    if (form.controls.hasOwnProperty(controlKey)) {
      const control = form.controls[controlKey];
      for (const errorKey in control.errors) {
        if (control.errors.hasOwnProperty(errorKey)) {
          if (onFail) {
            onFail(
              validationMessages &&
                validationMessages[controlKey] &&
                validationMessages[controlKey][errorKey]
            );
          }

          return res;
        }
      }
    }
  }

  // Form generic errors
  for (const errorKey in form.errors) {
    if (form.errors.hasOwnProperty(errorKey)) {
      if (onFail) {
        onFail(validationMessages && validationMessages[errorKey]);
      }
      return res;
    }
  }

  res = true;
  return res;
}
