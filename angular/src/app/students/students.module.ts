import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DxDataGridModule, DxButtonModule } from 'devextreme-angular';
import { StudentsRoutingModule } from './students-routing.module';
import { StudentListComponent } from './student-list/student-list.component';

@NgModule({
  declarations: [
    StudentListComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    DxDataGridModule,
    DxButtonModule,
    StudentsRoutingModule,
  ],
})
export class StudentsModule {}
