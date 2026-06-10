# BACKLOG TÉCNICO DE IMPLEMENTACIÓN

## OBJETIVO
Definir la ruta de construcción progresiva (Roadmap) para el MVP del Portal de Gestión y Trazabilidad de Servicios Logísticos JHT. 

Las tareas están agrupadas en fases incrementales para asegurar una entrega continua de valor y reducir riesgos de integración.

---

## FASE 1: BASE DEL PROYECTO
**Objetivo:** Establecer la infraestructura fundacional, los repositorios y la arquitectura de los proyectos base.
**Dependencias:** Ninguna.

### Tareas de Backend (ASP.NET Core)
* `[BE-1.1]` Inicializar solución bajo Clean Architecture (Domain, Application, Infrastructure, API).
* `[BE-1.2]` Configurar inyección de dependencias centralizada.
* `[BE-1.3]` Configurar base de datos y DbContext inicial (Entity Framework Core + PostgreSQL).
* `[BE-1.4]` Configurar cliente de conexión a MinIO/S3 para manejo futuro de archivos.
* `[BE-1.5]` Configurar manejo global de excepciones y respuestas HTTP estandarizadas.

### Tareas de Frontend (React)
* `[FE-1.1]` Inicializar proyecto Portal Administrativo (Vite + React + TS).
* `[FE-1.2]` Inicializar proyecto Portal de Tracking (Vite + React + TS).
* `[FE-1.3]` Configurar enrutador global (React Router) y cliente HTTP (Axios).
* `[FE-1.4]` Configurar estructura base de estilos, paleta de colores JHT (DOC-012) y fuentes (Inter/Roboto).
* `[FE-1.5]` Crear componentes atómicos base (Botones, Inputs, DataGrids, Modales).

---

## FASE 2: SEGURIDAD
**Objetivo:** Implementar la autenticación y autorización para restringir el acceso a los módulos operativos.
**Dependencias:** Fase 1.

### Tareas de Backend
* `[BE-2.1]` Crear entidades y migraciones: `TBL_UUSUARIO`, `TBL_CROL`, `TBL_RUSUARIO_ROL`.
* `[BE-2.2]` Implementar lógica de generación y validación de tokens JWT.
* `[BE-2.3]` Crear endpoints REST: `/api/auth/login` y políticas de autorización por roles (Administrador, Operador, Conductor).
* `[BE-2.4]` Sembrar (Seed) usuario Administrador por defecto.

### Tareas de Frontend (Administrativo)
* `[FE-2.1]` Construir vista de Login Administrativo.
* `[FE-2.2]` Implementar estado global (Store/Context) para la sesión del usuario.
* `[FE-2.3]` Implementar interceptor HTTP para inyectar token JWT.
* `[FE-2.4]` Configurar protección de rutas en React Router (Redirección a login si no hay token).
* `[FE-2.5]` Construir la maqueta principal (Sidebar, Topbar y Layout).

---

## FASE 3: CLIENTES
**Objetivo:** Permitir el registro y gestión de los clientes emisores de los servicios.
**Dependencias:** Fase 1, Fase 2.

### Tareas de Backend
* `[BE-3.1]` Crear entidad y migración: `TBL_TCLIENTE`.
* `[BE-3.2]` Implementar Patrón Repositorio y Casos de Uso (CRUD) de Clientes.
* `[BE-3.3]` Exponer Endpoints REST protegidos (GET, POST, PUT, DELETE Lógico).

### Tareas de Frontend (Administrativo)
* `[FE-3.1]` Construir vista de Listado de Clientes (Tabla con paginación/búsqueda).
* `[FE-3.2]` Construir formularios de Creación y Edición de Clientes.

---

## FASE 4: CONDUCTORES
**Objetivo:** Gestionar el personal de transporte interno y externo.
**Dependencias:** Fase 1, Fase 2.

### Tareas de Backend
* `[BE-4.1]` Crear entidad y migración: `TBL_TCONDUCTOR`.
* `[BE-4.2]` Implementar Repositorios y Casos de Uso (CRUD) con validación de Tipos (Interno/Externo).
* `[BE-4.3]` Exponer Endpoints REST.

### Tareas de Frontend (Administrativo)
* `[FE-4.1]` Construir vista de Listado de Conductores.
* `[FE-4.2]` Construir formularios de Creación y Edición (Manejo del selector Interno/Externo).

---

## FASE 5: VEHÍCULOS
**Objetivo:** Gestionar el parque automotor disponible para asignación.
**Dependencias:** Fase 1, Fase 2.

### Tareas de Backend
* `[BE-5.1]` Crear entidad y migración: `TBL_TVEHICULO`.
* `[BE-5.2]` Implementar Repositorios y Casos de Uso (CRUD).
* `[BE-5.3]` Exponer Endpoints REST.

### Tareas de Frontend (Administrativo)
* `[FE-5.1]` Construir vista de Listado de Vehículos.
* `[FE-5.2]` Construir formularios de Creación y Edición.

---

## FASE 6: SERVICIOS
**Objetivo:** El núcleo operativo del negocio. Creación de rutas, subida de documentos y gestión de estados y asignaciones.
**Dependencias:** Fase 3 (Clientes), Fase 4 (Conductores), Fase 5 (Vehículos).

### Tareas de Backend
* `[BE-6.1]` Migraciones para el núcleo operativo: `TBL_TSERVICIO`, `TBL_DSERVICIO_DESTINO`, `TBL_CTIPO_DOCUMENTO`, `TBL_DSERVICIO_DOCUMENTO`.
* `[BE-6.2]` Migraciones de asignación y rastreo: `TBL_RSERVICIO_CONDUCTOR`, `TBL_RSERVICIO_VEHICULO`, `TBL_CESTADO_SERVICIO`, `TBL_HSERVICIO_ESTADO`.
* `[BE-6.3]` Implementar lógica para generación del Código de Servicio Único (`JHT-YYYY-NNNNN`).
* `[BE-6.4]` Endpoints de Registro Transaccional (Guardar cabecera, múltiples destinos de una vez).
* `[BE-6.5]` Endpoints de Subida de Documentos al bucket en MinIO y registro en base de datos.
* `[BE-6.6]` Endpoints de Asignación (Actualizar Conductor y Vehículo) e Inserción inicial en Historial.
* `[BE-6.7]` Endpoints de Transición de Estados (Ej. Pasar de En_Ruta a Entregado) asegurando trazabilidad en el Historial.

### Tareas de Frontend (Administrativo)
* `[FE-6.1]` Construir vista de Listado de Servicios (Estado actual, Fechas, Cliente).
* `[FE-6.2]` Construir interfaz de Creación de Servicio (Multi-Paso / Acordeón: Datos Principales + Agregar N Destinos).
* `[FE-6.3]` Construir módulo de "Adjuntar Documentos" interactivo con carga de archivos.
* `[FE-6.4]` Construir vista de "Asignación" (Combobox interactivos para asignar Conductor y Vehículo).
* `[FE-6.5]` Construir visor de Detalle de Servicio (Ver Historial acumulativo).

---

## FASE 7: TRACKING
**Objetivo:** Brindar visibilidad pública al cliente final sobre el avance logístico.
**Dependencias:** Fase 6 (Servicios y Documentación terminada).

### Tareas de Backend
* `[BE-7.1]` Endpoint público de consulta (`/api/tracking/{codigo_jht}`) que retorne: Datos del servicio + Timeline de Historial + Links de Documentos públicos.
* `[BE-7.2]` Endpoint de proxy/redirección para descarga segura desde MinIO sin exponer el bucket.

### Tareas de Frontend (Portal Público)
* `[FE-7.1]` Construir la página inicial "Buscador de Guía/Código JHT" (UI/UX según referencia `002_JHT_PUBLIC_TRACKING_REFERENCE`).
* `[FE-7.2]` Construir la UI del "Timeline de Estados" vertical (Skeleton loading mientras se consulta al backend).
* `[FE-7.3]` Mostrar listado de documentos con botón de descarga visual e intuitivo.

---

## FASE 8: AUDITORÍA
**Objetivo:** Transparencia técnica total. Registrar todo evento de modificación para trazabilidad histórica.
**Dependencias:** Fase 1, Fase 2 (Usuario del contexto de sesión). *Esta fase es transversal y aplicará pasivamente a las Fases 3, 4, 5 y 6*.

### Tareas de Backend
* `[BE-8.1]` Crear entidad y migración: `TBL_AAUDITORIA`.
* `[BE-8.2]` Implementar `SaveChangesInterceptor` en Entity Framework para detectar `EntityState.Added`, `Modified`, o `Deleted`.
* `[BE-8.3]` Lógica de serialización a JSON del valor viejo/nuevo y extracción del `USU_ID` actual.
* `[BE-8.4]` Endpoints de Consulta de Auditoría con filtros (Fecha, Tabla, Usuario).

### Tareas de Frontend (Administrativo)
* `[FE-8.1]` Construir módulo visual de "Auditoría" en el panel administrativo.
* `[FE-8.2]` Construir visor JSON/Comparador visual ("Valor Antes" vs "Valor Después").
