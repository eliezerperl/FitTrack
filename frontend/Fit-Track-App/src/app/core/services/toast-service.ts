import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  constructor(private toastr: ToastrService) {}

  successToast(desc: string, headline: string = 'Success') {
    this.toastr.success(desc, headline, {
      timeOut: 3000,
      progressBar: true,
      positionClass: 'toast-top-left',
    });
  }

  failToast(desc?: string, headline: string = 'Something went wrong') {
    this.toastr.error(desc, headline, {
      timeOut: 2000,
      progressBar: true,
      positionClass: 'toast-top-right',
    });
  }
}
