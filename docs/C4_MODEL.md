# C4_MODEL.md

# MicroCommerce - C4 Model

Este documento descreve a arquitetura do sistema utilizando o modelo C4.

Níveis:

* Level 1 - Context Diagram
* Level 2 - Container Diagram
* Level 3 - Component Diagram
* Level 4 - Deployment Diagram

---

# Level 1 - System Context Diagram

Visão geral dos usuários e sistemas externos.

```mermaid
flowchart LR

    Customer[Cliente]

    MicroCommerce[MicroCommerce Platform]

    PaymentGateway[Fake Payment Gateway]

    EmailProvider[Email Provider]

    Customer --> MicroCommerce

    MicroCommerce --> PaymentGateway

    MicroCommerce --> EmailProvider
```

---

## Descrição

### Cliente

Usuário final que realiza compras.

### MicroCommerce

Plataforma de e-commerce baseada em microservices.

### Payment Gateway

Sistema externo utilizado para simular pagamentos.

### Email Provider

Sistema externo responsável pelo envio de notificações.

---

# Level 2 - Container Diagram

Visão dos containers principais.

```mermaid
flowchart TB

    Client[Client]

    Gateway[API Gateway]

    Auth[Auth Service]

    CustomerSvc[Customer Service]

    CatalogSvc[Catalog Service]

    CartSvc[Cart Service]

    OrderSvc[Order Service]

    InventorySvc[Inventory Service]

    PaymentSvc[Payment Service]

    NotificationSvc[Notification Service]

    AuditSvc[Audit Service]

    RabbitMQ[RabbitMQ]

    Redis[(Redis)]

    PgOrder[(PostgreSQL)]

    Mongo[(MongoDB)]

    Client --> Gateway

    Gateway --> Auth

    Gateway --> CustomerSvc

    Gateway --> CatalogSvc

    Gateway --> CartSvc

    Gateway --> OrderSvc

    Gateway --> InventorySvc

    Gateway --> PaymentSvc

    Gateway --> NotificationSvc

    CartSvc --> Redis

    OrderSvc --> PgOrder

    InventorySvc --> PgOrder

    CustomerSvc --> PgOrder

    CatalogSvc --> PgOrder

    Auth --> PgOrder

    PaymentSvc --> Mongo

    AuditSvc --> Mongo

    OrderSvc --> RabbitMQ

    RabbitMQ --> InventorySvc

    RabbitMQ --> PaymentSvc

    RabbitMQ --> NotificationSvc

    RabbitMQ --> AuditSvc
```

---

## Containers

### API Gateway

Responsável por:

* Autenticação
* Rate Limiting
* Roteamento

---

### Auth Service

Responsável por:

* Login
* JWT
* Refresh Token

---

### Customer Service

Responsável por:

* Clientes
* Endereços

---

### Catalog Service

Responsável por:

* Produtos
* Categorias

---

### Cart Service

Responsável por:

* Carrinho

---

### Order Service

Responsável por:

* Pedidos

---

### Inventory Service

Responsável por:

* Estoque

---

### Payment Service

Responsável por:

* Pagamentos

---

### Notification Service

Responsável por:

* Notificações

---

### Audit Service

Responsável por:

* Auditoria

---

# Level 3 - Component Diagram

Order Service.

```mermaid
flowchart TB

    Api[API Layer]

    Commands[Commands]

    Queries[Queries]

    Handlers[Handlers]

    Domain[Domain]

    Repository[Repository]

    Outbox[Outbox]

    Db[(PostgreSQL)]

    Api --> Commands

    Api --> Queries

    Commands --> Handlers

    Queries --> Handlers

    Handlers --> Domain

    Handlers --> Repository

    Repository --> Db

    Domain --> Outbox

    Outbox --> Db
```

---

## Componentes

### API Layer

Endpoints HTTP.

---

### Commands

Operações de escrita.

---

### Queries

Operações de leitura.

---

### Handlers

Executam regras de negócio.

---

### Domain

Entidades e regras.

---

### Repository

Persistência.

---

### Outbox

Persistência dos eventos.

---

# Component Diagram - Payment Service

```mermaid
flowchart TB

    Consumer[Event Consumer]

    PaymentHandler[Payment Handler]

    Gateway[Payment Gateway Adapter]

    Mongo[(MongoDB)]

    Publisher[Event Publisher]

    Consumer --> PaymentHandler

    PaymentHandler --> Gateway

    PaymentHandler --> Mongo

    PaymentHandler --> Publisher
```

---

# Component Diagram - Inventory Service

```mermaid
flowchart TB

    Consumer[OrderCreated Consumer]

    InventoryHandler[Inventory Handler]

    InventoryRepository[Inventory Repository]

    Db[(PostgreSQL)]

    Publisher[Event Publisher]

    Consumer --> InventoryHandler

    InventoryHandler --> InventoryRepository

    InventoryRepository --> Db

    InventoryHandler --> Publisher
```

---

# Level 4 - Deployment Diagram

Ambiente Kubernetes.

```mermaid
flowchart TB

    User[Client]

    Ingress[Ingress]

    GatewayPod[Gateway Pod]

    AuthPod[Auth Pod]

    CustomerPod[Customer Pod]

    CatalogPod[Catalog Pod]

    CartPod[Cart Pod]

    OrderPod[Order Pod]

    InventoryPod[Inventory Pod]

    PaymentPod[Payment Pod]

    NotificationPod[Notification Pod]

    AuditPod[Audit Pod]

    Rabbit[(RabbitMQ)]

    Redis[(Redis)]

    Postgres[(PostgreSQL)]

    Mongo[(MongoDB)]

    Grafana[(Grafana)]

    Prometheus[(Prometheus)]

    User --> Ingress

    Ingress --> GatewayPod

    GatewayPod --> AuthPod

    GatewayPod --> CustomerPod

    GatewayPod --> CatalogPod

    GatewayPod --> CartPod

    GatewayPod --> OrderPod

    GatewayPod --> InventoryPod

    GatewayPod --> PaymentPod

    GatewayPod --> NotificationPod

    GatewayPod --> AuditPod

    OrderPod --> Rabbit

    InventoryPod --> Rabbit

    PaymentPod --> Rabbit

    NotificationPod --> Rabbit

    AuditPod --> Rabbit

    CartPod --> Redis

    AuthPod --> Postgres

    CustomerPod --> Postgres

    CatalogPod --> Postgres

    OrderPod --> Postgres

    InventoryPod --> Postgres

    PaymentPod --> Mongo

    AuditPod --> Mongo

    Prometheus --> GatewayPod
    Prometheus --> AuthPod
    Prometheus --> CustomerPod
    Prometheus --> CatalogPod
    Prometheus --> OrderPod
    Prometheus --> InventoryPod
    Prometheus --> PaymentPod

    Grafana --> Prometheus
```

---

# Fluxo de Negócio Principal

```mermaid
sequenceDiagram

    participant Client

    participant Order

    participant RabbitMQ

    participant Inventory

    participant Payment

    participant Notification

    Client->>Order: Create Order

    Order->>RabbitMQ: OrderCreated

    RabbitMQ->>Inventory: Reserve Stock

    Inventory->>RabbitMQ: InventoryReserved

    RabbitMQ->>Payment: Process Payment

    Payment->>RabbitMQ: PaymentApproved

    RabbitMQ->>Order: PaymentApproved

    Order->>RabbitMQ: OrderConfirmed

    RabbitMQ->>Notification: Send Email
```

---

# Fluxo de Compensação

```mermaid
sequenceDiagram

    participant Order

    participant RabbitMQ

    participant Inventory

    participant Payment

    Order->>RabbitMQ: OrderCreated

    RabbitMQ->>Inventory: Reserve Stock

    Inventory->>RabbitMQ: InventoryReserved

    RabbitMQ->>Payment: Process Payment

    Payment->>RabbitMQ: PaymentRejected

    RabbitMQ->>Inventory: Release Stock

    Inventory->>RabbitMQ: InventoryReleased

    RabbitMQ->>Order: Cancel Order
```
