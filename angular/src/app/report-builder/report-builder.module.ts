import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DxDataGridModule, DxButtonModule } from 'devextreme-angular';
import { MonacoEditorModule } from 'ngx-monaco-editor-v2';
import { AppMaterialModule } from '../shared/material.module';
import { ReportBuilderRoutingModule } from './report-builder-routing.module';
import { ReportListComponent } from './admin/report-list/report-list.component';
import { ReportEditorComponent } from './admin/report-editor/report-editor.component';
import { ReportCatalogComponent } from './viewer/report-catalog/report-catalog.component';
import { ParameterFormComponent } from './viewer/parameter-form/parameter-form.component';
import { GridViewerComponent } from './viewer/grid-viewer/grid-viewer.component';
import { ReportViewerComponent } from './viewer/report-viewer/report-viewer.component';

@NgModule({
  declarations: [
    ReportListComponent,
    ReportEditorComponent,
    ReportCatalogComponent,
    ParameterFormComponent,
    GridViewerComponent,
    ReportViewerComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    DxDataGridModule,
    DxButtonModule,
    MonacoEditorModule.forRoot(),
    ReportBuilderRoutingModule,
    AppMaterialModule,
  ],
})
export class ReportBuilderModule {}
