# REGLAS TÉCNICAS DEL PROYECTO

## Backend

ASP.NET Core 8

Arquitectura:

* API REST
* Clean Architecture
* Repository Pattern
* JWT Authentication

## Frontend

React + Vite

## Aplicación Móvil

Flutter

## Base de Datos

PostgreSQL

## Archivos

Documentos almacenados en:

* S3

Nunca almacenar archivos binarios dentro de PostgreSQL.

## Auditoría

Obligatoria para entidades críticas.

## Integraciones Futuras

Preparar interfaces para:

* GPS
* WhatsApp
* Sistemas externos
* Firma digital

## Seguridad

Variables sensibles obligatoriamente en archivos .env o secretos del servidor.

Nunca almacenar credenciales en código fuente.
