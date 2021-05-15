import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { ToastrModule } from 'ngx-toastr';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { TimeagoModule } from 'ngx-timeago';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FileUploadModule } from 'ng2-file-upload';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    ButtonsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
    }),
    NgxGalleryModule,
    TimeagoModule.forRoot(),
    FileUploadModule,
    FontAwesomeModule,
    ModalModule.forRoot(),
  ],
  exports: [
    BsDropdownModule,
    BsDatepickerModule,
    TabsModule,
    PaginationModule,
    ButtonsModule,
    ToastrModule,
    NgxGalleryModule,
    TimeagoModule,
    FontAwesomeModule,
    FileUploadModule,
    ModalModule,
  ],
})
export class SharedModule {}
