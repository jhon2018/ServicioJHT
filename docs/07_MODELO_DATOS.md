# 07_MODELO_DATOS.md

# MODELO DE DATOS OFICIAL

## OBJETIVO

Definir las entidades principales del Portal de Gestión y Trazabilidad de Servicios Logísticos JHT.

El modelo debe soportar:

* Servicios logísticos.
* Múltiples destinos.
* Múltiples documentos.
* Conductores internos.
* Conductores externos.
* Vehículos.
* Tracking público.
* Historial de estados.
* Auditoría.
* Aplicación Flutter.
* Integraciones futuras.

---

# CONVENCIONES

## Tablas

Formato:

TBL_ + Tipo + Nombre

Ejemplos:

TBL_TSERVICIO
TBL_DSERVICIO_DESTINO
TBL_HSERVICIO_ESTADO

## Tipos

T = Transaccional Principal

D = Detalle

H = Histórico

C = Catálogo

R = Relación

A = Auditoría

U = Seguridad

---

# SEGURIDAD

## TBL_UUSUARIO

Usuarios del sistema.

Campos principales:

USU_ID
USU_LOGIN
USU_NOMBRE
USU_EMAIL
USU_PASSWORD_HASH
USU_ESTADO
USU_FECHA_CREACION

---

## TBL_CROL

Catálogo de roles.

ROL_ID
ROL_NOMBRE
ROL_DESCRIPCION

Valores iniciales:

ADMINISTRADOR
OPERADOR
CONDUCTOR

---

## TBL_RUSUARIO_ROL

Relación usuario-rol.

USR_ID
ROL_ID

---

# CLIENTES

## TBL_TCLIENTE

CLI_ID
CLI_RUC
CLI_RAZON_SOCIAL
CLI_DIRECCION
CLI_CONTACTO
CLI_TELEFONO
CLI_EMAIL
CLI_ESTADO

---

# CONDUCTORES

## TBL_TCONDUCTOR

CON_ID

CON_CODIGO_EXTERNO

CON_TIPO

Valores:

I = Interno
E = Externo

CON_DNI

CON_NOMBRE_COMPLETO

CON_TELEFONO

CON_EMAIL

CON_ESTADO

Observación:

La entidad debe soportar conductores propios y terceros.

---

# VEHICULOS

## TBL_TVEHICULO

VEH_ID

VEH_PLACA

VEH_MARCA

VEH_MODELO

VEH_TIPO

VEH_CAPACIDAD

VEH_ESTADO

---

# SERVICIOS

## TBL_TSERVICIO

Entidad principal del negocio.

SER_ID

SER_CODIGO

Ejemplo:

JHT-2026-000001

SER_FECHA_REGISTRO

SER_FECHA_SERVICIO

CLI_ID

SER_TIPO_SERVICIO

Ejemplos:

DISTRIBUCION
COURIER
MUDANZA
RECOJO
ESPECIAL

SER_DESCRIPCION

SER_OBSERVACION

SER_ESTADO_GENERAL

SER_CANTIDAD_DESTINOS

SER_ESTADO

---

# ASIGNACIONES

## TBL_RSERVICIO_CONDUCTOR

SER_ID

CON_ID

FECHA_ASIGNACION

USUARIO_ASIGNADOR

ESTADO

Permite futuras reasignaciones.

---

## TBL_RSERVICIO_VEHICULO

SER_ID

VEH_ID

FECHA_ASIGNACION

ESTADO

Permite cambios de unidad.

---

# DESTINOS

## TBL_DSERVICIO_DESTINO

SERDES_ID

SER_ID

SERDES_SECUENCIA

Ejemplo:

1
2
3

SERDES_DESTINATARIO

SERDES_RUC

SERDES_DIRECCION

SERDES_REFERENCIA

SERDES_CONTACTO

SERDES_TELEFONO

SERDES_ESTADO

Estados:

PENDIENTE
EN_RUTA
MUY_CERCA
ENTREGADO
CANCELADO

Observación:

Un servicio puede tener múltiples destinos.

---

# DOCUMENTOS

## TBL_CTIPO_DOCUMENTO

TIPDOC_ID

TIPDOC_CODIGO

TIPDOC_NOMBRE

Valores Iniciales:

GRE
FACTURA
BOLETA
ORDEN_SERVICIO
ORDEN_COMPRA
CARGO
OTRO

---

## TBL_DSERVICIO_DOCUMENTO

SERDOC_ID

SER_ID

TIPDOC_ID

SERDOC_NUMERO

SERDOC_ARCHIVO_URL

SERDOC_NOMBRE_ORIGINAL

SERDOC_FECHA_CARGA

USUARIO_CARGA

Observación:

Un servicio puede tener múltiples documentos.

---

# ESTADOS

## TBL_CESTADO_SERVICIO

EST_ID

EST_CODIGO

EST_NOMBRE

Valores Iniciales:

RECEPCIONADO

PROGRAMADO

UNIDAD_ASIGNADA

EN_RUTA

MUY_CERCA

ENTREGADO

CANCELADO

---

# HISTORIAL

## TBL_HSERVICIO_ESTADO

HSE_ID

SER_ID

EST_ID

HSE_FECHA_HORA

HSE_OBSERVACION

USUARIO_REGISTRO

Observación:

Nunca eliminar registros.

Representa la línea de tiempo del servicio.

---

# TRACKING PUBLICO

## TBL_TTRACKING_PUBLICO

TRK_ID

SER_ID

TRK_TOKEN

TRK_FECHA_CREACION

TRK_ESTADO

Observación:

Permite futuras URLs seguras.

Ejemplo:

https://rastreo.jhttransporte.pe/t/ABCD123456

---

# AUDITORIA

## TBL_AAUDITORIA

AUD_ID

AUD_TABLA

AUD_REGISTRO

AUD_ACCION

INSERT

UPDATE

DELETE_LOGICO

AUD_USUARIO

AUD_FECHA

AUD_VALOR_ANTERIOR

AUD_VALOR_NUEVO

Observación:

Aplicable a todas las entidades críticas.

---

# PREPARADO PARA FLUTTER

La aplicación Flutter consumirá:

* Servicios asignados.
* Destinos.
* Estados.
* Historial.
* Observaciones.

No tendrá acceso directo a PostgreSQL.

Toda comunicación será mediante API REST.

---

# PREPARADO PARA FUTURAS FASES

## GPS

TBL_TGPS_POSICION

(Fase 2)

## Evidencias

TBL_DEVIDENCIA_ENTREGA

(Fase 2)

## Firma Digital

TBL_DFIRMA_ENTREGA

(Fase 2)

## WhatsApp

TBL_HWHATSAPP_EVENTO

(Fase 3)

## Dashboard

DataMart Analítico

(Fase 3)
