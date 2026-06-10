# Fluxo Principal do Sistema

## Cadastro de Cliente

```text
Cliente
   |
   v
Customer Service
   |
   v
CustomerCreated
```

---

## Consulta de Produtos

```text
Cliente
   |
Gateway
   |
Catalog Service
   |
PostgreSQL
```

---

## Adicionar ao Carrinho

```text
Cliente
   |
Cart Service
   |
Redis
```

---

## Criar Pedido

```text
Cliente
   |
Order Service
   |
PostgreSQL
   |
OrderCreated Event
```

---

## Reserva de Estoque

```text
OrderCreated
      |
RabbitMQ
      |
Inventory Service
      |
InventoryReserved
```

---

## Processamento de Pagamento

```text
InventoryReserved
        |
RabbitMQ
        |
Payment Service
        |
PaymentApproved
```

---

## Confirmação de Pedido

```text
PaymentApproved
       |
Order Service
       |
OrderConfirmed
```

---

# Fluxo de Compensação

## Falha no Pagamento

```text
PaymentRejected
        |
RabbitMQ
        |
Inventory Service
        |
InventoryReleased
        |
Order Service
        |
OrderCancelled
```

---

# Estados do Pedido

```text
Pending
Reserved
Paid
Confirmed
Cancelled
```

---

# Estados do Estoque

```text
Available
Reserved
Released
```

---

# Estados do Pagamento

```text
Pending
Approved
Rejected
Refunded
```

---

# Escalabilidade

Serviços que devem ser escalados horizontalmente:

* Catalog
* Order
* Payment
* Notification

---

# Cache

Redis deve ser utilizado para:

* Carrinho
* Produtos mais acessados
* Dados de sessão

---

# API Gateway

Responsabilidades:

* Autenticação
* Roteamento
* Rate Limiting
* Logging

---

# Health Checks

Todos os serviços:

```http
GET /health
```

Retorno:

```json
{
  "status": "healthy"
}
```
