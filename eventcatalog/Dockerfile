FROM node:22-alpine AS build

WORKDIR /app

# Alpine doesn't include bash, which EventCatalog's build requires
RUN apk add --no-cache bash

# Copy package files
COPY package.json package-lock.json ./

# Install dependencies
RUN npm ci

# Copy all EventCatalog content
COPY . .

# Build the static site
RUN npx @eventcatalog/core build

# Production stage - serve with lightweight nginx
FROM nginx:alpine

# Copy built static files to nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Copy custom nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 3000

CMD ["nginx", "-g", "daemon off;"]