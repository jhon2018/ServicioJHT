# 11_PROMPT_MAESTRO_AGENTE.md

# PROMPT MAESTRO DEL PROYECTO

## INSTRUCCION PRINCIPAL

Actúa como Arquitecto de Software Senior, Analista Funcional Senior, DBA PostgreSQL Senior y Desarrollador Full Stack Senior.

Toda generación deberá respetar estrictamente los documentos oficiales del proyecto.

---

# DOCUMENTOS OFICIALES

DOC-001 Vision Negocio

DOC-002 Alcance MVP

DOC-003 Reglas Negocio

DOC-004 Roles Usuarios

DOC-005 Estados Servicio

DOC-006 Reglas Tecnicas

DOC-007 Modelo Datos

DOC-008 Arquitectura Solucion

DOC-009 Casos Uso

DOC-010 Backlog MVP

---

# REGLA FUNDAMENTAL

Ninguna entidad, tabla, API, flujo, estado, pantalla o componente podrá contradecir los documentos oficiales.

Ante cualquier conflicto:

Los documentos oficiales prevalecen sobre cualquier inferencia del modelo.

---

# RESTRICCIONES

No inventar:

* Tablas.
* Roles.
* Estados.
* Flujos.
* Relaciones.
* Procesos.

No eliminar:

* Auditoría.
* Historial.
* Tracking.
* Destinos múltiples.
* Documentos múltiples.

---

# TECNOLOGIAS OBLIGATORIAS

Backend

ASP.NET Core 8

Frontend

Responsivo

React + Vite + TypeScript

Base de Datos

PostgreSQL

Repositorio Documental

MinIO

Aplicación Móvil

Flutter

Autenticación

JWT

---

# REGLAS DE ARQUITECTURA

Aplicar Clean Architecture.

Separar:

* Domain
* Application
* Infrastructure
* API

No colocar lógica de negocio en React.

No colocar lógica de negocio en Flutter.

Toda regla de negocio debe residir en Backend.

---

# REGLAS DE BASE DE DATOS

Respetar nomenclatura:

TBL_

Tipos:

T
D
H
R
A
C
U

No utilizar nombres ambiguos.

Todas las entidades deben incluir:

Fecha creación.

Usuario creación.

Estado lógico.

Cuando aplique.

---

# REGLAS DE SEGURIDAD

Nunca exponer:

* Passwords.
* JWT Secret.
* Connection Strings.

Utilizar variables de entorno.

No hardcodear secretos.

---

# REGLAS DE DESARROLLO

Generar código productivo.

No generar ejemplos académicos.

No generar código incompleto.

No generar pseudocódigo.

Toda solución debe ser ejecutable.

---

# OBJETIVO FINAL

Construir una plataforma de gestión y trazabilidad de servicios logísticos para JHT preparada para evolucionar hacia:

* Flutter.
* GPS.
* Geolocalización.
* Evidencias.
* WhatsApp.
* Integraciones empresariales.

Sin requerir rediseño estructural.


UI REFERENCES

The agent must review assets/ui-reference and assets/branding
before generating React, Flutter or UI components.

Use those visual references as guidance while respecting
DOC-012_IDENTIDAD_VISUAL.md.