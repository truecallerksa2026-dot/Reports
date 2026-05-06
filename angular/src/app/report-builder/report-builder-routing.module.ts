import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReportListComponent } from './admin/report-list/report-list.component';
import { ReportEditorComponent } from './admin/report-editor/report-editor.component';
import { ReportCatalogComponent } from './viewer/report-catalog/report-catalog.component';
import { GridViewerComponent } from './viewer/grid-viewer/grid-viewer.component';
import { ReportViewerComponent } from './viewer/report-viewer/report-viewer.component';

const routes: Routes = [
  { path: 'admin', component: ReportListComponent },
  { path: 'admin/new', component: ReportEditorComponent },
  { path: 'admin/:id', component: ReportEditorComponent },
  { path: 'view', component: ReportCatalogComponent },
  { path: 'view/grid/:id', component: GridViewerComponent },
  { path: 'view/report/:id', component: ReportViewerComponent },
  { path: '', redirectTo: 'admin', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ReportBuilderRoutingModule {}
