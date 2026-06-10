# REGLAS DE NEGOCIO

## RN-001

Todo servicio debe pertenecer a un cliente.

## RN-002

Todo servicio debe poseer un código único generado por el sistema.

Formato sugerido:

JHT-YYYY-NNNNN

Ejemplo:

JHT-2026-00001

## RN-003

Un servicio puede tener cero, uno o múltiples documentos asociados.

## RN-004

Un servicio puede tener uno o múltiples destinos.

## RN-005

Los estados operativos deben registrarse en historial.

Nunca deben sobrescribirse ni eliminarse.

## RN-006

Todo cambio relevante debe quedar auditado.

## RN-007

Un conductor puede ser:

* Interno.
* Externo.

## RN-008

Un conductor puede atender múltiples servicios.

## RN-009

Un servicio puede asignarse a un conductor y vehículo.

## RN-010

Los documentos asociados pueden ser:

* Guía Remisión.
* Factura.
* Orden Servicio.
* Orden Compra.
* Cargo.
* Otros.

El sistema no debe depender de un único tipo documental.

## RN-011

Los clientes externos solamente podrán visualizar información autorizada del servicio.

No podrán visualizar información administrativa interna.

## RN-012

Toda consulta pública deberá ser de solo lectura.

## RN-013

La eliminación física de registros estará prohibida.

Se utilizará estado activo/inactivo.
