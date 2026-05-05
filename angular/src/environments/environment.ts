import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44356/',
  redirectUri: baseUrl,
  clientId: 'ReportBuilder_App',
  responseType: 'code',
  scope: 'offline_access ReportBuilder',
  requireHttps: true,
  impersonation: {
    tenantImpersonation: true,
    userImpersonation: true,
  }
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'ReportBuilder',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44356',
      rootNamespace: 'ReportBuilder',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
