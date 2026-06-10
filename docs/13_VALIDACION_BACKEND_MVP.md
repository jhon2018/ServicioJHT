# Validación Integral de Arquitectura Backend MVP

Este documento representa la auditoría final y el cierre técnico del desarrollo del Backend (Fases 1 a 6) para el MVP del Sistema Logístico JHT. Se detalla el estado actual, capacidades y preparación para las fases de frontend y producción.

---

## 1. Inventario Final de Tablas

El contexto `JhtDbContext` administra actualmente las siguientes tablas en PostgreSQL:

**Seguridad y Accesos**
* `TBL_UUSUARIO`: Autenticación y datos básicos del usuario.
* `TBL_CROL`: Catálogo de roles (ADMINISTRADOR, OPERADOR, CONDUCTOR).
* `TBL_RUSUARIO_ROL`: Relación N:M para control de accesos.

**Módulos Maestros**
* `TBL_TCLIENTE`: Empresas o personas que contratan los servicios.
* `TBL_TCONDUCTOR`: Choferes y motorizados (Internos y Externos).
* `TBL_TVEHICULO`: Unidades de transporte.
* `TBL_CTIPO_DOCUMENTO`: Catálogo Maestro (Facturas, Guías, Evidencias).
* `TBL_CESTADO_SERVICIO`: Catálogo Maestro de flujo lógico (Recepcionado, En Ruta, etc.).

**Operaciones (Servicios)**
* `TBL_TSERVICIO`: Cabecera principal de la orden de trabajo.
* `TBL_DSERVICIO_DESTINO`: Paradas/entregas individuales asociadas a un servicio.
* `TBL_DSERVICIO_DOCUMENTO`: Archivos integrados con S3/MinIO.
* `TBL_HSERVICIO_ESTADO`: Línea de tiempo cronológica de lo que ocurre en el servicio.
* `TBL_TTRACKING_PUBLICO`: Tokens encriptados para exposición de tracking sin autenticación.
* `TBL_RSERVICIO_CONDUCTOR`: Tabla pivote de asignación de choferes.
* `TBL_RSERVICIO_VEHICULO`: Tabla pivote de asignación de unidades vehiculares.

---

## 2. Inventario Final de Endpoints

Todos los endpoints (excepto Auth Login y Tracking) están protegidos vía `[Authorize]`.

* **Auth**: `POST /api/auth/login`
* **Clientes**: `GET /api/clientes`, `GET /api/clientes/{id}`, `POST`, `PUT`, `DELETE`.
* **Conductores**: `GET /api/conductores`, `GET /api/conductores/{id}`, `POST`, `PUT`, `DELETE`.
* **Vehículos**: `GET /api/vehiculos`, `GET /api/vehiculos/{id}`, `POST`, `PUT`, `DELETE`.
* **Servicios**: `GET /api/servicios`, `POST /api/servicios` (Incluye destinos y autogenera tracking).
* **Tracking Público**: `GET /api/tracking/{token}` (`AllowAnonymous`).

---

## 3. Relaciones entre Entidades

El diseño respeta estrictamente la 3FN y las reglas de Clean Architecture:

* **Independencia Total (Desacoplamiento):** `Conductor` y `Vehículo` no están atados entre sí de forma nativa. Su relación es circunstancial y dinámica a través de un `Servicio` (`TBL_RSERVICIO_CONDUCTOR` y `TBL_RSERVICIO_VEHICULO`). Esto permite reemplazar un camión averiado a media ruta sin alterar el historial.
* **Agregación (Padre - Hijo):** El `Servicio` es el Aggregate Root. Controla a sus `Destinos`, `Documentos` e `Historial`.
* **Auditoría Transversal:** Todos heredan de `AuditableEntity`, utilizando un Interceptor de Entity Framework que inyecta el `UsuarioId` y `Fecha` en cada Insert/Update de manera automática, garantizando rastreabilidad pasiva en toda la base de datos.

---

## 4. Flujo Completo Operativo

1. **Cliente solicita el traslado:** El Operador ingresa a React, selecciona al cliente y crea el `Servicio` agregando los `Destinos` (Secuencia 1: Recojo, Secuencia 2: Entrega Final).
2. **Registro Automático:** El sistema asigna el código `JHT-2026-...`, el estado `RECEPCIONADO` y genera el token de **Tracking**.
3. **Asignación:** Tráfico (desde React) asocia el `Vehículo` y el `Conductor` al servicio (pasan a `PROGRAMADO` o `UNIDAD ASIGNADA`).
4. **Seguimiento (Tracking):** El conductor (desde Flutter) interactúa. Sube estado a `EN RUTA`. Automáticamente se registra un hito en `TBL_HSERVICIO_ESTADO`. El cliente, viendo el enlace público `/api/tracking/xyz`, ve el estado cambiar en tiempo real.
5. **Entrega y Cierre:** El conductor marca el destino como `ENTREGADO` y sube la foto (Evidencia) que se carga vía streaming a MinIO/S3. El servicio global se cierra.

---

## 5. Riesgos Pendientes

* **Integridad de S3 vs Base de Datos:** Si un error de red corta la conexión con PostgreSQL después de que el archivo subió a MinIO, el archivo quedará "huérfano" en S3. 
* **Control de Concurrencia:** Dos operadores asignando un vehículo al mismo tiempo a servicios distintos que se cruzan en horario.
* **Crecimiento de Históricos:** Las tablas de Tracking e Historial crecerán exponencialmente mes a mes y podrían requerir particionamiento en Postgres a futuro.

---

## 6. Deuda Técnica Pendiente

1. **Paginación Global:** Actualmente los comandos `GetAll` traen listas planas. Para evitar el colapso de memoria cuando hayan 50,000 servicios, se debe implementar paginación lógica (`PageSize`, `PageNumber`) en el patrón CQRS.
2. **Middleware Global de Excepciones:** Refactorizar los `try/catch` de los Controllers hacia un `GlobalErrorHandlerMiddleware` para una respuesta estandarizada (RFC 7807 ProblemDetails).
3. **Políticas de Retry:** Implementar `Polly` para reintentar la subida a MinIO en caso de micro-cortes de red.
4. **Expiración de Tokens de Tracking:** Implementar un campo `TRK_FECHA_EXPIRACION` para limitar el tiempo de vida de los enlaces públicos por seguridad.

---

## 7. Preparación para React (Web Admin)

El Backend MVP está listo para ser consumido por React:
* **Autenticación Resolutiva:** JWT Tokens configurados para ser transmitidos vía cabecera `Authorization: Bearer <token>`.
* **Respuestas Estandarizadas:** Uso puro de Data Transfer Objects (DTOs), ocultando los modelos de base de datos (`CliId` vs la entidad Cliente completa).
* **CORS:** Debe validarse la habilitación de orígenes cruzados (`AddCors`) apuntando a la URL del dominio de React en Program.cs.

---

## 8. Preparación para Flutter (Móvil)

El Backend MVP está diseñado estratégicamente para móvil:
* El modelo de negocio permite que un **Conductor** tenga un usuario asociado al rol `"CONDUCTOR"`.
* Se ha habilitado la subida en crudo (Base64/Multipart) a **MinIO**, ideal para fotos pesadas tomadas desde la cámara del celular.
* Las consultas CQRS pueden ser reutilizadas rápidamente para crear el endpoint `GetMisServiciosQuery` extrayendo la identidad de quien consulta a través de los Claims del Token.

---

## 9. Preparación para Producción

Antes de subir al VPS (DigitalOcean/AWS/Azure), se requiere:
1. Extraer el secreto de JWT y las credenciales MinIO de los `appsettings.json` e inyectarlos por Variables de Entorno (`.env` o Docker Compose).
2. Ejecutar la migración inicial (`dotnet ef database update`) contra la base de datos gestionada.
3. Configurar un Proxy Reverso (Nginx/Traefik) con Certificados SSL para que las peticiones a MinIO y a la API viajen encriptadas en HTTPS.

---

## 10. Checklist Final de MVP

- [x] Arquitectura Limpia Implementada (Domain, Application, Infrastructure, API).
- [x] Roles y Autenticación con JWT finalizada.
- [x] CRUD de Módulos Maestros (Clientes, Conductores, Vehículos) finalizado.
- [x] Estructura CQRS e Inyección de Dependencias.
- [x] Módulo Completo de Servicios y lógica de Destinos Múltiples.
- [x] Historial Cronológico y Auditoría Automática por Interceptor.
- [x] Endpoint Público de Tracking.
- [x] Cliente de Storage S3/MinIO inyectado.
- [x] Scripts de Base de Datos y Fluent API sincronizado.

**Veredicto Final:** El Backend MVP se considera estructural y arquitectónicamente sólido y listo para acoplar las interfaces visuales (Fase Frontend).
