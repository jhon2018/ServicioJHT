# ARQUITECTURA TÉCNICA DEFINITIVA

## OBJETIVO
Definir los lineamientos técnicos estrictos, estructuras de directorios y estrategias arquitectónicas para el Portal de Gestión y Trazabilidad de Servicios Logísticos JHT. 

Este documento sirve como base técnica inmutable para el desarrollo del MVP y sus futuras fases.

---

## 1. ESTRUCTURA DE CARPETAS GLOBAL

La estructura raíz del repositorio se organizará de la siguiente manera para mantener los componentes aislados y facilitar el despliegue independiente:

```text
/
├── /docs                 # Documentación oficial del proyecto
├── /assets               # Recursos gráficos (branding, ui-assets, ui-reference)
├── /backend              # Solución ASP.NET Core 8 (Clean Architecture)
├── /frontend-admin       # Aplicación React + Vite (Portal Administrativo)
├── /frontend-tracking    # Aplicación React + Vite (Portal Público)
├── /mobile               # Aplicación Flutter (Futura fase)
├── /infrastructure       # Scripts de BD, Docker Compose (MinIO, Postgres)
└── README.md             # Instrucciones de inicialización del proyecto
```

---

## 2. ESTRUCTURA DE SOLUCIÓN ASP.NET CORE (BACKEND)

Implementación estricta de **Clean Architecture** para garantizar el desacoplamiento.

```text
/backend
├── /JHT.Logistics.Domain           # Entidades, Enums, Interfaces Repository (No dependencias)
├── /JHT.Logistics.Application      # Casos de Uso (CQRS / MediatR), DTOs, Validaciones (FluentValidation)
├── /JHT.Logistics.Infrastructure   # EF Core DbContext, PostgreSQL, JWT, MinIO, Repositorios
└── /JHT.Logistics.API              # Controllers REST, Program.cs, Middlewares, Swagger
```

**Principios:**
* **Domain** no conoce nada sobre la base de datos ni la web.
* **API** y **Infrastructure** dependen de **Application**.
* Inyección de dependencias centralizada en la capa de API.

---

## 3. ESTRUCTURA REACT (FRONTEND)

Se utilizará una arquitectura orientada a características (**Feature-Sliced Design / Feature-based**) para facilitar el mantenimiento y la escalabilidad de la UI.

```text
/src
├── /assets           # Imágenes, fuentes, íconos locales
├── /core             # Constantes, configuraciones de Vite/Axios
├── /features         # Módulos del negocio (Clientes, Conductores, Servicios, etc.)
│   └── /servicios
│       ├── /api      # Llamadas a endpoints de servicios
│       ├── /hooks    # Custom hooks (ej. useServicios)
│       ├── /components # Componentes específicos de servicios
│       └── /pages    # Vistas ruteables (ServiciosList, ServicioCreate)
├── /shared           # Componentes reutilizables (Botones, Modales, Tablas genéricas)
├── /layouts          # Plantillas de diseño (DashboardLayout, PublicLayout)
├── /routes           # Configuración de React Router
├── /store            # Estado global (Zustand o Context API)
├── /theme            # Estilos globales, variables CSS, colores JHT
└── App.tsx           # Componente raíz
```

---

## 4. ESTRUCTURA FLUTTER FUTURA (MOBILE)

Para preparar la aplicación del Conductor, se utilizará un enfoque similar guiado por Clean Architecture o modular por Features.

```text
/lib
├── /core             # Red, Manejo de errores, Configuración, Constantes
├── /features
│   └── /tracking
│       ├── /data     # Repositorios, Models, DataSources (API REST)
│       ├── /domain   # Entidades, UseCases, Interfaces de Repository
│       └── /presentation # Pages, Widgets, State Management (BLoC/Cubit)
├── /shared           # Widgets reutilizables, UI genérica
├── /theme            # Paleta de colores JHT, Tipografía
└── main.dart         # Punto de entrada
```

---

## 5. ORGANIZACIÓN POSTGRESQL

* **Motor:** PostgreSQL (versión 15+)
* **ORM:** Entity Framework Core (Code-First)
* **Esquemas:** Uso del esquema por defecto `public`.

**Principios de Diseño (Según DOC-007):**
* Uso obligatorio del prefijo `TBL_` y categorización (T, D, H, C, R, A, U).
* **Eliminación Lógica Obligatoria:** Toda tabla principal debe incluir la columna `ESTADO` (`Activo`/`Inactivo`). Jamás se ejecutará un `DELETE` físico.
* **Claves Primarias:** Uso de UUID (GUID) o enteros Identity autoincrementables según el estándar definido, pero priorizando enteros para IDs y UUIDs para tokens públicos.
* **Trazabilidad:** Inclusión de campos de auditoría base (`USUARIO_CREACION`, `FECHA_CREACION`) en tablas maestras.

---

## 6. ORGANIZACIÓN MINIO (ALMACENAMIENTO S3)

MinIO actuará como el repositorio centralizado de documentos logísticos y futuras evidencias.

**Estructura de Buckets:**
* `jht-documentos-logisticos`: Contendrá Guías, Facturas, Órdenes de Servicio.
* `jht-evidencias-entrega`: (Futuro) Fotografías y firmas.

**Organización de Rutas (Object Keys):**
El patrón de almacenamiento dentro del bucket será:
`/{TIPO_DOCUMENTO}/{AÑO}/{MES}/{CODIGO_SERVICIO}/{NOMBRE_ARCHIVO.pdf}`
Ejemplo: `/GRE/2026/10/JHT-2026-000001/Guia-001-1234.pdf`

**Seguridad (Ajuste MVP):**
* Los buckets de MinIO serán configurados como **Privados** a nivel de infraestructura para evitar indexación masiva.
* El Frontend/Portal de Tracking visualizará los documentos solicitando una ruta del backend, o bien configurando una política de lectura pública (Read-Only) al directorio específico de documentos permitidos (Dado que el Tracking es público sin login por el momento).

---

## 7. CONVENCIONES

**Código (Backend/C#):**
* Clases e Interfaces: `PascalCase`
* Métodos: `PascalCase`
* Variables Locales / Parámetros: `camelCase`
* Constantes: `UPPER_SNAKE_CASE`

**Código (Frontend/React/TS):**
* Componentes y Archivos TSX: `PascalCase.tsx`
* Hooks y Utilidades: `camelCase.ts`
* Interfaces/Tipos: `PascalCase`

**Git / Ramas:**
* `main`: Producción (Estable).
* `develop`: Entorno de Pruebas / QA.
* `feature/nombre-funcionalidad`: Desarrollo de nuevas historias de usuario.
* `hotfix/nombre-error`: Correcciones urgentes.

**API REST:**
* Pluralización de endpoints: `/api/servicios`, `/api/conductores`
* Códigos HTTP semánticos: `200 OK`, `201 Created`, `400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`, `500 Internal Server Error`.

---

## 8. ESTRATEGIA JWT (JSON WEB TOKEN)

Para el Portal Administrativo (React) y la App Flutter (Conductor).

**Flujo:**
1. Autenticación contra el endpoint `/api/auth/login`.
2. Emisión de token JWT firmado simétricamente (HMAC) o asimétricamente (RSA).
3. Tiempo de vida corto (ej. 1 hora) por seguridad.

**Claims sugeridos:**
* `sub`: ID del Usuario (`USU_ID`).
* `role`: Rol del Usuario (Administrador, Operador, Conductor).
* `name`: Nombre completo para la UI.

**Almacenamiento en Cliente (Portal Admin):**
* El token JWT debe almacenarse de preferencia en memoria o localStorage, cuidando la mitigación contra ataques XSS.

*(El Portal de Tracking no utiliza JWT, realiza consultas abiertas mediante el código público del servicio)*.

---

## 9. ESTRATEGIA DE AUDITORÍA

La tabla `TBL_AAUDITORIA` registrará todos los movimientos transaccionales importantes.

**Implementación Técnica:**
1. **EF Core Interceptors (Intercepción de contexto):**
   * Se implementará un `SaveChangesInterceptor` en el `DbContext` de la capa de Infraestructura de ASP.NET Core.
   * Antes de ejecutar el commit de la base de datos, el interceptor detectará todas las entidades modificadas (`Added`, `Modified`, `Deleted`).
2. **Registro de Cambios:**
   * Guardará en `TBL_AAUDITORIA` el nombre de la tabla, la clave primaria (`AUD_REGISTRO`), el tipo de acción (`INSERT`, `UPDATE`), los valores originales en formato JSON (`AUD_VALOR_ANTERIOR`) y los nuevos valores (`AUD_VALOR_NUEVO`).
3. **Usuario de Contexto:**
   * A través de la inyección de `IHttpContextAccessor`, se obtendrá el `USU_ID` del token JWT para atribuir la acción al usuario responsable.
   * Para operaciones del sistema, se usará un identificador de usuario `SYSTEM`.

Esta estrategia garantiza el cumplimiento estricto del **CU-013 (Consultar Auditoría)** de forma pasiva y desacoplada de la lógica de negocio.
