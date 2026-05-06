import { RoutesService, eLayoutType } from '@abp/ng.core';
import { inject, provideAppInitializer } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  provideAppInitializer(() => {
    configureRoutes();
  }),
];

function configureRoutes() {
  const routes = inject(RoutesService);
  routes.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/dashboard',
        name: '::Menu:Dashboard',
        iconClass: 'fas fa-chart-line',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'ReportBuilder.Dashboard.Host  || ReportBuilder.Dashboard.Tenant',
      },
      {
        path: '/students',
        name: '::Menu:Students',
        iconClass: 'fas fa-user-graduate',
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy: 'ReportBuilder.Students',
      },
      {
        path: '/report-builder',
        name: '::Menu:ReportBuilder',
        iconClass: 'fas fa-table',
        order: 3,
        layout: eLayoutType.application,
        requiredPolicy: 'ReportBuilder.Reports',
      },
      {
        path: '/report-builder/view',
        name: '::Menu:ReportBuilder:Catalog',
        iconClass: 'fas fa-list',
        order: 1,
        parentName: '::Menu:ReportBuilder',
        layout: eLayoutType.application,
        requiredPolicy: 'ReportBuilder.Reports.Admin || ReportBuilder.Reports.Viewer',
      },
      {
        path: '/report-builder/admin',
        name: '::Menu:ReportBuilder:Admin',
        iconClass: 'fas fa-tools',
        order: 2,
        parentName: '::Menu:ReportBuilder',
        layout: eLayoutType.application,
        requiredPolicy: 'ReportBuilder.Reports.Admin',
      },
  ]);
}
