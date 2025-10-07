# Etap 1: build Angulara
FROM node:20 AS build
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build -- --configuration=docker

# Etap 2: serwowanie przez http-server
FROM node:20-alpine AS runtime
WORKDIR /app
RUN npm install -g http-server
COPY --from=build /app/dist/stacktags.client /app/dist
EXPOSE 80
CMD ["http-server", "dist", "-p", "80"]

