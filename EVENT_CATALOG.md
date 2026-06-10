# EVENT_CATALOG.md

# MicroCommerce - Event Catalog

## Objetivo

Este documento descreve todos os eventos utilizados na comunicação entre microservices.

Cada evento representa um fato de negócio já ocorrido.

Os eventos são publicados através do RabbitMQ.

---

# Convenções

Todos os eventos devem:

* Ser imutáveis
* Utilizar records
* Possuir versão
* Possuir CorrelationId
* Possuir EventId

---

# Estrutura Base

```csharp
public abstract record IntegrationEvent
{
    public Guid EventId { get; init; }
    public Guid CorrelationId { get; init; }
    public DateTime OccurredAt { get; init; }
    public int Version { get; init; }
}
```

---

# Classificação dos Eventos

## Customer Events

* CustomerCreated

## Catalog Events

* ProductCreated
* ProductUpdated
* ProductDeleted

## Order Events

* OrderCreated
* OrderConfirmed
* OrderCancelled

## Inventory Events

* InventoryReserved
* InventoryReleased
* InventoryFailed

## Payment Events

* PaymentApproved
* PaymentRejected

---

# Customer Events

---

## CustomerCreated

### Tipo

Integration Event

### Publicador

Customer Service

### Consumidores

* Audit Service

### Disparado Quando

Um novo cliente é criado.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "customerId": "",
  "name": "",
  "email": ""
}
```

---

# Catalog Events

---

## ProductCreated

### Tipo

Integration Event

### Publicador

Catalog Service

### Consumidores

* Audit Service

### Disparado Quando

Novo produto criado.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "productId": "",
  "name": "",
  "price": 0
}
```

---

## ProductUpdated

### Tipo

Integration Event

### Publicador

Catalog Service

### Consumidores

* Audit Service

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "productId": "",
  "name": "",
  "price": 0
}
```

---

## ProductDeleted

### Tipo

Integration Event

### Publicador

Catalog Service

### Consumidores

* Audit Service

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "productId": ""
}
```

---

# Order Events

---

## OrderCreated

### Tipo

Integration Event

### Publicador

Order Service

### Consumidores

* Inventory Service
* Audit Service

### Participa da Saga

Sim

### Disparado Quando

Pedido criado.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": "",
  "customerId": "",
  "totalAmount": 0,
  "items": []
}
```

---

## OrderConfirmed

### Tipo

Integration Event

### Publicador

Order Service

### Consumidores

* Notification Service
* Audit Service

### Participa da Saga

Sim

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": "",
  "customerId": ""
}
```

---

## OrderCancelled

### Tipo

Integration Event

### Publicador

Order Service

### Consumidores

* Notification Service
* Audit Service

### Participa da Saga

Sim

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": "",
  "reason": ""
}
```

---

# Inventory Events

---

## InventoryReserved

### Tipo

Integration Event

### Publicador

Inventory Service

### Consumidores

* Payment Service
* Audit Service

### Participa da Saga

Sim

### Disparado Quando

Estoque reservado com sucesso.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": "",
  "reservationId": ""
}
```

---

## InventoryReleased

### Tipo

Integration Event

### Publicador

Inventory Service

### Consumidores

* Order Service
* Audit Service

### Participa da Saga

Sim

### Disparado Quando

Reserva de estoque liberada.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": ""
}
```

---

## InventoryFailed

### Tipo

Integration Event

### Publicador

Inventory Service

### Consumidores

* Order Service
* Audit Service

### Participa da Saga

Sim

### Disparado Quando

Não foi possível reservar estoque.

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "orderId": "",
  "reason": ""
}
```

---

# Payment Events

---

## PaymentApproved

### Tipo

Integration Event

### Publicador

Payment Service

### Consumidores

* Order Service
* Audit Service

### Participa da Saga

Sim

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "paymentId": "",
  "orderId": "",
  "amount": 0
}
```

---

## PaymentRejected

### Tipo

Integration Event

### Publicador

Payment Service

### Consumidores

* Inventory Service
* Audit Service

### Participa da Saga

Sim

### Payload

```json
{
  "eventId": "",
  "correlationId": "",
  "occurredAt": "",
  "version": 1,
  "paymentId": "",
  "orderId": "",
  "reason": ""
}
```

---

# Fluxo da Saga

## Caminho Feliz

```text
OrderCreated
      |
InventoryReserved
      |
PaymentApproved
      |
OrderConfirmed
```

---

## Caminho de Compensação

```text
OrderCreated
      |
InventoryReserved
      |
PaymentRejected
      |
InventoryReleased
      |
OrderCancelled
```

---

# Exchanges RabbitMQ

---

## order.events

Eventos:

```text
OrderCreated
OrderConfirmed
OrderCancelled
```

---

## inventory.events

Eventos:

```text
InventoryReserved
InventoryReleased
InventoryFailed
```

---

## payment.events

Eventos:

```text
PaymentApproved
PaymentRejected
```

---

## customer.events

Eventos:

```text
CustomerCreated
```

---

## catalog.events

Eventos:

```text
ProductCreated
ProductUpdated
ProductDeleted
```

---

# Convenção de Versionamento

Formato:

```text
v1
v2
v3
```

Exemplo:

```text
OrderCreated v1
OrderCreated v2
```

Mudanças compatíveis:

* Novos campos opcionais

Mudanças incompatíveis:

* Remoção de campos
* Alteração de tipo

Exigem nova versão.

---

# CorrelationId

Todos os eventos da mesma Saga devem compartilhar o mesmo CorrelationId.

Exemplo:

```text
CorrelationId

OrderCreated
InventoryReserved
PaymentApproved
OrderConfirmed
```

Benefícios:

* Rastreamento distribuído
* Observabilidade
* Debug

---

# Idempotência

Todos os consumidores devem ser idempotentes.

Regra:

Um evento processado duas vezes não pode gerar efeitos colaterais duplicados.

Exemplo:

```text
PaymentApproved

Recebido 2 vezes

Resultado:
Pedido continua Confirmado
```

---

# Retry Policy

Falhas temporárias:

* Retry 1
* Retry 2
* Retry 3

Após falhas:

```text
Dead Letter Queue
```

---

# Dead Letter Queue

Fila para mensagens não processadas.

Exemplo:

```text
order.events.dlq
inventory.events.dlq
payment.events.dlq
```

Objetivo:

* Investigação
* Reprocessamento
* Auditoria

---

# Observabilidade

Todos os eventos devem registrar:

* EventId
* CorrelationId
* EventType
* Producer
* Consumer
* Timestamp

Todos os eventos devem gerar:

* Logs
* Traces
* Métricas

através de OpenTelemetry.

---

# Resumo

Este catálogo define todos os contratos de integração da plataforma MicroCommerce.

Qualquer alteração em eventos deve obrigatoriamente atualizar este documento e respeitar as regras de versionamento e compatibilidade estabelecidas.
