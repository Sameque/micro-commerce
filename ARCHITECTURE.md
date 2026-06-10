# Visão Geral

MicroCommerce é uma plataforma de e-commerce baseada em Microservices.

A arquitetura segue os princípios de:

* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* Event Driven Architecture
* Saga Pattern
* Outbox Pattern

---

# Arquitetura de Alto Nível

```text
                           Client
                              |
                              v
                       API Gateway
                              |
    -----------------------------------------------------
    |           |           |          |                |
    v           v           v          v                v

 Auth     Customer    Catalog    Cart       Order
 Service   Service    Service    Service    Service

                                          |
                                          v
                                       RabbitMQ
                                          |
                     ------------------------------------
                     |                |                |
                     v                v                v

                Inventory      Payment       Notification
                 Service       Service          Service

                                          |
                                          v

                                     Audit Service
```

---

# Banco de Dados

Cada serviço possui seu próprio banco.

Nenhum serviço pode acessar o banco de outro.

## PostgreSQL

* Auth
* Customer
* Catalog
* Inventory
* Order

## MongoDB

* Payment
* Audit

## Redis

* Cart
* Cache Distribuído

---

# Comunicação

## Síncrona

Utilizar HTTP REST.

Permitido apenas para:

* Consultas
* Dados em tempo real

Exemplo:

```text
Gateway -> Catalog Service
Gateway -> Customer Service
```

---

## Assíncrona

Utilizar RabbitMQ.

Obrigatório para:

* Integrações entre domínios
* Eventos de negócio

Exemplo:

```text
OrderCreated
PaymentApproved
InventoryReserved
```

---

# Shared Kernel

Localização:

```text
building-blocks/shared-kernel
```

Contém:

* BaseEntity
* AggregateRoot
* DomainEvent
* Result Pattern
* Value Objects

---

# Contracts

Localização:

```text
building-blocks/contracts
```

Contém:

* Integration Events
* Event Contracts

Nunca compartilhar entidades entre serviços.

---

# Segurança

Autenticação:

* JWT

Autorização:

* Roles
* Policies

Gateway deve validar JWT.

---

# Resiliência

Utilizar:

* Retry
* Circuit Breaker
* Timeout

Ferramenta:

Polly

---

# Observabilidade

Todos os serviços devem possuir:

* Logs Estruturados
* Traces Distribuídos
* Métricas

Ferramentas:

* OpenTelemetry
* Prometheus
* Grafana
