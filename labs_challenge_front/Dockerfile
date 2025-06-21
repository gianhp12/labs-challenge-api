FROM ghcr.io/cirruslabs/flutter:latest AS build

WORKDIR /app
COPY . .

RUN flutter build web

# Etapa 2: Servir os arquivos usando NGINX
FROM nginx:alpine

# Remove a configuração padrão do NGINX
RUN rm -rf /usr/share/nginx/html/*

# Copia os arquivos gerados do Flutter Web para a pasta padrão do NGINX
COPY --from=build /app/build/web /usr/share/nginx/html

# Copia um arquivo opcional de configuração customizada (se quiser)
# COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
