FROM php:7.4-apache

RUN apt-get update && apt-get install -y \
    libmcrypt-dev \
    libicu-dev \
    libxml2-dev \
    libzip-dev \
    unzip \
    git \
    vim \
    nano \
    zlib1g-dev

RUN docker-php-ext-install pdo_mysql mysqli intl soap zip

RUN pecl install mcrypt-1.0.2 && docker-php-ext-enable mcrypt

WORKDIR /var/www/html

COPY . .

RUN chown -R www-data:www-data /var/www/html

EXPOSE 80
