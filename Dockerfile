FROM php:7.4-apache

RUN apt-get update && apt-get install -y \
    libmcrypt-dev \
    libzip-dev \
    && pecl install mcrypt-1.0.4 \
    && docker-php-ext-install zip mysqli pdo_mysql \
    && docker-php-ext-enable mcrypt mysqli pdo_mysql

COPY . /var/www/html/

EXPOSE 80