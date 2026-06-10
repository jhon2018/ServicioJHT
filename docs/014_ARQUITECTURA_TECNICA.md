# ARQUITECTURA TÉCNICA DEFINITIVA

## OBJETIVO
Definir la arquitectura técnica actual del proyecto ServicioJHT, basándose estrictamente en la estructura real de carpetas y las configuraciones de despliegue en producción.

---

## 1. ESTRUCTURA GLOBAL DEL MONOREPO

El proyecto utiliza un enfoque de **Monorepo**, conteniendo todo el código en un único lugar:

```text
/
├── /assets        # Recursos gráficos y multimedia
├── /backend       # Solución ASP.NET Core 8 (Clean Architecture)
├── /docs          # Documentación oficial del proyecto
├── /frontend      # Aplicación SPA React + Vite (Portal Web)
```

*(Carpetas auxiliares como `/promt` o `/HashGen` se usan para utilidades locales o generación de scripts/hashes).*

---

## 2. BACKEND (ASP.NET CORE 8)

Sigue el patrón de **Clean Architecture**, dividiendo responsabilidades claramente:

```text
/backend
├── /JHT.Logistics.Domain           # Entidades base, Enums (Sin dependencias externas)
├── /JHT.Logistics.Application      # Casos de Uso, Lógica de Negocio, DTOs
├── /JHT.Logistics.Infrastructure   # Contexto EF Core, PostgreSQL, JWT
└── /JHT.Logistics.API              # Controladores REST, Configuraciones, Program.cs
```

---

## 3. FRONTEND (REACT + VITE)

Estructura modular orientada a simplificar la UI, acorde a lo que realmente existe en el proyecto:

```text
/frontend/src
├── /app           # Configuración base de la aplicación
├── /components    # Componentes UI reutilizables
├── /layouts       # Plantillas maestras (ej. Sidebar, Header)
├── /pages         # Vistas ruteables (Login, Dashboard, etc.)
├── /services      # Integración con la API (Axios, Endpoints)
├── /store         # Estado global (Zustand, AuthStore)
├── App.tsx        # Enrutador principal
└── main.tsx       # Punto de entrada
```

---

## 4. BASE DE DATOS Y ESTÁNDARES

* **Motor de BD:** PostgreSQL (v15+)
* **ORM:** Entity Framework Core (Code-First)
* **Auditoría:** Uso de interceptores en EF Core para guardar logs de inserciones y actualizaciones automáticas.
* **Eliminación Lógica:** No se borran datos físicamente; se usa una columna de Estado.
* **Seguridad (JWT):** Autenticación mediante tokens de corta duración.

---

## 5. ESTRATEGIA DE DESPLIEGUE (RENDER)

Ambos sistemas se despliegan en la plataforma **Render** como servicios independientes a partir de la misma rama de GitHub.

### 5.1 Backend (Web Service)
* **Directorio Raíz:** `backend`
* **Entorno:** Docker (utilizando el `Dockerfile` interno).
* **Variables:** `ASPNETCORE_ENVIRONMENT = Production`.

### 5.2 Frontend (Static Site)
* **Directorio Raíz:** `frontend`
* **Comando Build:** `npm install --legacy-peer-deps && npm run build`
* **Directorio a Publicar:** `dist`
* **Variables:** `VITE_API_URL` apuntando a la URL pública del backend.
