# Base Técnica del Backend - Portal JHT

## Objetivo de la Solución
Este repositorio contiene la solución base en **ASP.NET Core 8** para el Portal de Gestión y Trazabilidad de Servicios Logísticos JHT. Sigue estrictamente los principios de **Clean Architecture** para asegurar una evolución limpia hacia futuras fases (integración con Flutter, GPS, WhatsApp) según los documentos `DOC-001` a `DOC-015`.

## Tecnologías Implementadas
* **Framework:** .NET 8.0 (C# 12)
* **Base de Datos:** PostgreSQL
* **ORM:** Entity Framework Core
* **Autenticación:** JWT (JSON Web Tokens)
* **Logging:** Serilog
* **Validación:** FluentValidation
* **CQRS / Mediador:** MediatR

---

## 1. Estructura de la Solución

La solución `JHT.Logistics.sln` se compone de 4 proyectos principales que aíslan las responsabilidades:

### `JHT.Logistics.Domain`
* **Capa:** Dominio (Core).
* **Responsabilidad:** Contener las entidades de negocio, enumeraciones, objetos de valor e interfaces de los repositorios.
* **Dependencias:** Ninguna. **No debe tener referencias a bases de datos ni frameworks web**.

### `JHT.Logistics.Application`
* **Capa:** Aplicación (Casos de Uso).
* **Responsabilidad:** Lógica de negocio orquestada. Aquí vivirán los Handlers (CQRS vía MediatR), los DTOs, y las reglas de validación (FluentValidation).
* **Dependencias Internas:** Referencia a `JHT.Logistics.Domain`.
* **Paquetes NuGet:** `MediatR`, `FluentValidation.DependencyInjectionExtensions`.

### `JHT.Logistics.Infrastructure`
* **Capa:** Infraestructura (Tecnología y Persistencia).
* **Responsabilidad:** Implementar el acceso a datos (`DbContext`), migraciones, adaptadores externos (como MinIO/S3), e interceptores.
* **Dependencias Internas:** Referencia a `JHT.Logistics.Application`.
* **Paquetes NuGet:** `Microsoft.EntityFrameworkCore.Design`, `Npgsql.EntityFrameworkCore.PostgreSQL`, `Microsoft.AspNetCore.Authentication.JwtBearer`.

### `JHT.Logistics.API`
* **Capa:** Presentación (Punto de entrada).
* **Responsabilidad:** Exponer los endpoints REST (Controllers), inyectar dependencias y manejar la configuración base (Middlewares, CORS, Swagger).
* **Dependencias Internas:** Referencia a `JHT.Logistics.Application` y `JHT.Logistics.Infrastructure`.
* **Paquetes NuGet:** `Serilog.AspNetCore`, `Swashbuckle.AspNetCore`.

---

## 2. Dependencias NuGet Justificadas

Las siguientes librerías han sido instaladas como pilares arquitectónicos:

| Paquete | Proyecto | Justificación Estratégica |
|---------|----------|---------------------------|
| `Npgsql.EntityFrameworkCore.PostgreSQL` | Infrastructure | Provider oficial y optimizado para interactuar con PostgreSQL en .NET 8. |
| `Microsoft.EntityFrameworkCore.Design` | Infrastructure | Necesario para la generación de migraciones (Code-First) por comandos CLI. |
| `MediatR` | Application | Implementación del patrón CQRS para separar comandos (escrituras) de consultas (lecturas). Aísla los casos de uso. |
| `FluentValidation` | Application | Permite extraer la lógica de validación de los modelos y controladores hacia clases específicas, manteniendo el código limpio. |
| `Microsoft.AspNetCore.Authentication.JwtBearer` | Infrastructure | Habilita la protección de las APIs para el Admin Web y futura app Flutter mediante tokens seguros. |
| `Serilog.AspNetCore` | API | Proveedor de logging estructurado que reemplaza al logger genérico de .NET, permitiendo en un futuro inyectar logs a Seq, Elasticsearch o archivos de texto enriquecidos. |

---

## 3. Estrategia de Configuración (appsettings / .env)

Para cumplir con las normas de seguridad del proyecto y evitar exponer secretos:

1. **Desarrollo Local:** Se utilizará `appsettings.Development.json` y User Secrets para datos sensibles.
2. **Entornos / Producción:** La inyección de configuración se sobreescribirá mediante **Variables de Entorno** obligatorias.
3. Secciones clave que existirán en el futuro `appsettings.json`:
   * `ConnectionStrings:PostgresDb` (La cadena de PostgreSQL).
   * `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`.
   * `MinIO:Endpoint`, `MinIO:AccessKey`, `MinIO:SecretKey`.

*(Los secretos NUNCA se consolidarán en el código fuente).*

---

## 4. Estrategia de Logging (Serilog)

La observabilidad es clave para la auditoría y debugging:
* Se configura **Serilog** desde el archivo `Program.cs` para capturar todos los logs del Host.
* **Formato Estructurado:** Los logs se emitirán en formato JSON (configurable) para facilitar su indexación futura.
* **Sinks (Destinos):** Inicialmente consola asíncrona y un archivo de rolling diario (ej. `logs/jht-log-.txt`).

---

## 5. Estrategia de Auditoría

Para satisfacer el requerimiento de registro histórico sin polucionar los Casos de Uso:
* Se habilitará un **`SaveChangesInterceptor`** (Intercepción a nivel de Entity Framework Core).
* En la etapa `SavingChangesAsync`, el interceptor detectará cualquier entidad añadida o modificada.
* Leerá el `USU_ID` del `HttpContext` inyectado (proveniente del JWT).
* Serializará automáticamente los cambios (Valor Anterior -> Valor Nuevo) y generará un registro en la tabla `TBL_AAUDITORIA` de manera transparente.

---

## 6. Convenciones de Desarrollo (Fase 1)

1. **Clean Architecture:** Ningún código en `API` debe acceder a la base de datos directamente. Todo entra por `Controllers` -> `MediatR` -> `Application` -> `Repository`.
2. **Nomenclatura DB:** `TBL_T...`, `TBL_D...`, etc., mapeadas utilizando el `OnModelCreating` mediante Fluent API (No usando Data Annotations en las entidades).
3. **Soft Delete:** Se implementará en una interfaz `IAuditableEntity` con un campo booleano o enum de Estado, filtrado automáticamente usando Global Query Filters de EF Core.

*(Este repositorio está preparado estructuralmente para iniciar el desarrollo de la Fase 2: Seguridad y Entidades. No se han generado artefactos de código de negocio en esta fase a petición).*




La URL para acceder a Swagger y verificar si tu backend está activo correctamente es:

👉 http://localhost:5251/swagger

(Si por alguna razón se está ejecutando bajo el perfil seguro HTTPS, la URL sería: https://localhost:7060/swagger)