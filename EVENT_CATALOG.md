# Catálogo de Eventos

Todos os eventos devem ser publicados via RabbitMQ.

---

# CustomerCreated

Publicado por:

Customer Service

Consumidores:

* Audit Service

Payload:

```json
{
  "customerId": "guid",
  "name": "string",
  "email": "string"
}
```

---

# ProductCreated

Publicado por:

Catalog Service

Consumidores:

* Audit Service

Payload:

```json
{
  "productId": "guid",
  "name": "string",
  "price": 100
}
```

---

# OrderCreated

Publicado por:

Order Service

Consumidores:

* Inventory Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "customerId": "guid",
  "totalAmount": 100.00,
  "items": []
}
```

---

# InventoryReserved

Publicado por:

Inventory Service

Consumidores:

* Payment Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "reservedAt": "datetime"
}
```

---

# InventoryFailed

Publicado por:

Inventory Service

Consumidores:

* Order Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "reason": "string"
}
```

---

# PaymentApproved

Publicado por:

Payment Service

Consumidores:

* Order Service
* Notification Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "transactionId": "guid",
  "amount": 100.00
}
```

---

# PaymentRejected

Publicado por:

Payment Service

Consumidores:

* Inventory Service
* Order Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "reason": "string"
}
```

---

# InventoryReleased

Publicado por:

Inventory Service

Consumidores:

* Order Service
* Audit Service

Payload:

```json
{
  "orderId": "guid"
}
```

---

# OrderConfirmed

Publicado por:

Order Service

Consumidores:

* Notification Service
* Audit Service

Payload:

```json
{
  "orderId": "guid"
}
```

---

# OrderCancelled

Publicado por:

Order Service

Consumidores:

* Notification Service
* Audit Service

Payload:

```json
{
  "orderId": "guid",
  "reason": "string"
}
```

---

# Convenções

Todos os eventos devem:

* Ser imutáveis.
* Utilizar record.
* Possuir EventId.
* Possuir OccurredAt.
* Possuir CorrelationId.
* Possuir Version.

Exemplo:

```csharp
public record OrderCreatedEvent(
    Guid EventId,
    Guid CorrelationId,
    DateTime OccurredAt,
    int Version,
    Guid OrderId,
    Guid CustomerId,
    decimal TotalAmount
);
```
