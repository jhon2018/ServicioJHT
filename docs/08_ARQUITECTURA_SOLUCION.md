# 08_ARQUITECTURA_SOLUCION.md

# ARQUITECTURA OFICIAL DEL PROYECTO

## Nombre

Portal de Gestión y Trazabilidad de Servicios Logísticos JHT

---

# OBJETIVO

Implementar una solución escalable basada en servicios, preparada para crecimiento progresivo, integraciones futuras y aplicación móvil.

La arquitectura deberá soportar:

* Portal Web Administrativo.
* Portal Público de Tracking.
* Aplicación Flutter.
* Integraciones futuras.
* Alto volumen de servicios.
* Múltiples clientes.

---

# ARQUITECTURA GENERAL

┌─────────────────────┐
│ React Frontend │
└──────────┬──────────┘
│
┌──────────▼──────────┐
│ ASP.NET Core API │
└──────────┬──────────┘
│
├─────────────┐
│
▼ ▼
PostgreSQL MinIO

---

# COMPONENTES

## Frontend Administrativo

Tecnología:

* React
* Vite
* TypeScript

Responsabilidades:

* Gestión operativa.
* Administración.
* Consulta.
* Auditoría.
* Configuración.

---

## Portal Público

Tecnología:

* React

Responsabilidades:

* Consulta de estados.
* Historial.
* Descarga de documentos.

No requiere autenticación.

---

## Backend

Tecnología:

* ASP.NET Core 8

Responsabilidades:

* API REST.
* Seguridad.
* Reglas de negocio.
* Auditoría.
* Integraciones.

---

## Aplicación Móvil

Tecnología:

* Flutter

Responsabilidades:

* Consultar servicios asignados.
* Actualizar estados.
* Recibir notificaciones.

---

## Base de Datos

Tecnología:

PostgreSQL

Responsabilidades:

* Persistencia.
* Integridad.
* Auditoría.

---

## Repositorio de Archivos

Tecnología:

MinIO

Alternativas futuras:

* AWS S3


Responsabilidades:

* PDF.
* Imágenes.
* Evidencias.

---

# CLEAN ARCHITECTURE

## Capa Domain

Contiene:

* Entidades.
* Reglas de negocio.
* Interfaces.

No depende de ninguna tecnología.

---

## Capa Application

Contiene:

* Casos de uso.
* Servicios.
* Validaciones.

---

## Capa Infrastructure

Contiene:

* PostgreSQL.
* MinIO.
* JWT.
* Servicios externos.

---

## Capa API

Contiene:

* Controllers.
* Middleware.
* Seguridad.

---

# SEGURIDAD

Autenticación:

JWT

Autorización:

Role Based Access Control

Roles:

* Administrador
* Operador
* Conductor

---

# NOTIFICACIONES FUTURAS

Proveedor inicial:

Firebase Cloud Messaging

Casos:

* Nuevo servicio asignado.
* Cambio de estado.
* Recordatorios.

---

# ESCALABILIDAD

La solución deberá soportar:

Fase 1

* 100 conductores.

Fase 2

* 500 conductores.

Fase 3

* Miles de servicios diarios.

Sin rediseñar la arquitectura principal.

---

# INTEGRACIONES FUTURAS

GPS

WhatsApp

ERP

Facturación Electrónica

Servicios SUNAT

Power BI

---

# PRINCIPIOS OBLIGATORIOS

* No acceso directo a la base desde Flutter.
* No lógica de negocio en React.
* No lógica de negocio en Flutter.
* Toda regla de negocio reside en API.
* Toda comunicación mediante API REST.
* Eliminación lógica obligatoria.
* Auditoría obligatoria.
