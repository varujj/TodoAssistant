import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private _toastrService: ToastrService) {}

  public handleException(ex: any) {
    const msg = this.tryGetMessage(ex) ?? 'Unknown error on server side';

    this._toastrService.error(msg);
    console.error(ex);
  }

  public tryGetMessage(ex: any): string | undefined {
    let msg;
    if (ex.error) {
      let error = ex.error;
      if (typeof ex.error === 'string') {
        msg = error;
      } else {
        msg = 'Unknown error occured';
      }
    }

    return msg ?? ex.message;
  }
}
