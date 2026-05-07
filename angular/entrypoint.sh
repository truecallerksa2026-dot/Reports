#!/bin/sh
set -e

# Inject Railway environment variables into dynamic-env.json at container startup.
# Set these in your Railway service's environment variables:
#   API_URL  — public URL of the API service  (e.g. https://reports-api.up.railway.app)
#   APP_URL  — public URL of this frontend    (e.g. https://reports.up.railway.app)

API_URL="${API_URL:-https://localhost:44356}"
APP_URL="${APP_URL:-http://localhost:4200}"

# Railway assigns a dynamic port via $PORT. Update nginx to listen on it.
LISTEN_PORT="${PORT:-80}"
sed -i "s/listen\s*80;/listen ${LISTEN_PORT};/g; s/listen\s*\[::\]:80;/listen [::]:${LISTEN_PORT};/g" /etc/nginx/conf.d/default.conf

cat > /usr/share/nginx/html/dynamic-env.json << ENVEOF
{
  "production": true,
  "application": {
    "baseUrl": "${APP_URL}",
    "name": "ReportBuilder",
    "logoUrl": ""
  },
  "oAuthConfig": {
    "issuer": "${API_URL}/",
    "redirectUri": "${APP_URL}",
    "clientId": "ReportBuilder_App",
    "responseType": "code",
    "scope": "offline_access openid profile email phone ReportBuilder",
    "requireHttps": false
  },
  "apis": {
    "default": {
      "url": "${API_URL}",
      "rootNamespace": "ReportBuilder"
    },
    "AbpAccountPublic": {
      "url": "${API_URL}",
      "rootNamespace": "AbpAccountPublic"
    }
  }
}
ENVEOF

exec nginx -g 'daemon off;'
