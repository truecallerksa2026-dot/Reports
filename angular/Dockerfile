FROM node:20-alpine AS build

WORKDIR /app
COPY package*.json /app/

RUN yarn
COPY . /app

RUN yarn build:prod

FROM nginx:alpine
COPY nginx.conf /etc/nginx/conf.d/default.conf
COPY entrypoint.sh /entrypoint.sh
RUN chmod +x /entrypoint.sh
COPY --from=build /app/dist/ReportBuilder/browser /usr/share/nginx/html

EXPOSE 80
ENTRYPOINT ["/entrypoint.sh"]
