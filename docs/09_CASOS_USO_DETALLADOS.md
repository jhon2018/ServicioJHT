# 09_CASOS_USO_DETALLADOS.md

# ACTORES DEL SISTEMA

1. Administrador
2. Operador
3. Conductor
4. Cliente Externo

---

# CU-001

Registrar Cliente

Actor:

Administrador

Flujo:

1. Ingresa módulo clientes.
2. Registra información.
3. Guarda registro.

Resultado:

Cliente disponible para servicios.

---

# CU-002

Registrar Conductor

Actor:

Administrador

Flujo:

1. Ingresa módulo conductores.
2. Selecciona tipo.

Interno
Externo

3. Completa datos.
4. Guarda.

Resultado:

Conductor disponible.

---

# CU-003

Registrar Vehículo

Actor:

Administrador

Flujo:

1. Ingresa módulo vehículos.
2. Registra placa.
3. Guarda.

Resultado:

Vehículo disponible.

---

# CU-004

Crear Servicio

Actor:

Operador

Flujo:

1. Selecciona cliente.
2. Selecciona tipo servicio.
3. Registra información general.
4. Adjunta documentos.
5. Guarda.

Resultado:

Sistema genera código JHT.

Ejemplo:

JHT-2026-000001

---

# CU-005

Registrar Destinos

Actor:

Operador

Flujo:

1. Ingresa servicio.
2. Agrega destinos.
3. Define secuencia.

Resultado:

Ruta registrada.

---

# CU-006

Asignar Conductor

Actor:

Operador

Flujo:

1. Selecciona servicio.
2. Selecciona conductor.
3. Selecciona vehículo.
4. Guarda.

Resultado:

Servicio asignado.

---

# CU-007

Notificar Conductor

Actor:

Sistema

Flujo:

1. Detecta asignación.
2. Genera notificación.

Resultado:

Conductor visualiza nuevo servicio.

---

# CU-008

Consultar Servicios Asignados

Actor:

Conductor

Flujo:

1. Inicia sesión.
2. Consulta servicios.

Resultado:

Visualiza servicios pendientes.

---

# CU-009

Actualizar Estado

Actor:

Conductor

Flujo:

1. Abre servicio.
2. Selecciona estado.

Estados:

En Ruta
Muy Cerca
Entregado

3. Guarda.

Resultado:

Estado actualizado.

Historial generado.

---

# CU-010

Consultar Tracking Público

Actor:

Cliente

Flujo:

1. Ingresa código.
2. Sistema busca servicio.
3. Muestra información.

Resultado:

Visualización del estado.

---

# CU-011

Visualizar Historial

Actor:

Cliente

Flujo:

1. Consulta servicio.
2. Visualiza línea de tiempo.

Resultado:

Seguimiento completo.

---

# CU-012

Descargar Documento

Actor:

Cliente

Flujo:

1. Consulta servicio.
2. Selecciona documento.
3. Descarga archivo.

Resultado:

Documento descargado.

---

# CU-013

Consultar Auditoría

Actor:

Administrador

Flujo:

1. Ingresa auditoría.
2. Filtra registros.

Resultado:

Visualiza cambios históricos.

---

# CU-014

Reasignar Servicio

Actor:

Operador

Flujo:

1. Selecciona servicio.
2. Selecciona nuevo conductor.
3. Guarda.

Resultado:

Nueva asignación registrada.

Historial conservado.

---

# CU-015

Cancelar Servicio

Actor:

Administrador

Flujo:

1. Selecciona servicio.
2. Registra motivo.
3. Confirma.

Resultado:

Estado Cancelado.

No se elimina información.

---

# CASOS DE USO FUTUROS

CU-F01

Captura Fotográfica

CU-F02

Firma Digital

CU-F03

Geolocalización

CU-F04

GPS

CU-F05

WhatsApp

CU-F06

Dashboard Gerencial

CU-F07

Integración ERP

CU-F08

Integración Facturación Electrónica
