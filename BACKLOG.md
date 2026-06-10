# MicroCommerce

## Visão Geral

Objetivo:

Construir uma plataforma de e-commerce baseada em microservices para demonstrar conhecimentos avançados em:

* .NET 8
* Microservices
* DDD
* Clean Architecture
* CQRS
* RabbitMQ
* Redis
* PostgreSQL
* MongoDB
* Docker
* Kubernetes
* OpenTelemetry
* Prometheus
* Grafana
* Saga Pattern
* Outbox Pattern

---

# EPIC 01 - Foundation

## Feature 1.1 - Estrutura Inicial

### Task

Criar estrutura do monorepo.

### Critérios

* Criar solution principal.
* Criar pasta services.
* Criar pasta gateway.
* Criar pasta building-blocks.
* Criar pasta docs.
* Criar pasta docker.
* Criar pasta k8s.

---

## Feature 1.2 - Shared Kernel

### Task

Criar biblioteca compartilhada.

### Critérios

Implementar:

* Entity
* AggregateRoot
* ValueObject
* DomainEvent
* Result Pattern

---

## Feature 1.3 - Contracts

### Task

Criar biblioteca de contratos.

### Critérios

Implementar:

* BaseIntegrationEvent
* Event Metadata
* CorrelationId
* EventVersion

---

## Feature 1.4 - Docker Compose

### Task

Criar ambiente local.

### Critérios

Subir:

* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

---

# EPIC 02 - Auth Service

## Feature 2.1 - Estrutura

### Task

Criar Auth Service.

### Critérios

* Clean Architecture
* Minimal API
* PostgreSQL

---

## Feature 2.2 - Registro

### Task

Implementar cadastro.

### Critérios

Endpoint:

POST /register

---

## Feature 2.3 - Login

### Task

Implementar autenticação.

### Critérios

Endpoint:

POST /login

Retornar JWT.

---

## Feature 2.4 - Refresh Token

### Task

Implementar renovação de token.

---

# EPIC 03 - Customer Service

## Feature 3.1 - Cliente

### Tasks

* Criar Cliente
* Atualizar Cliente
* Consultar Cliente

---

## Feature 3.2 - Endereços

### Tasks

* Adicionar endereço
* Atualizar endereço
* Remover endereço

---

## Feature 3.3 - Eventos

### Tasks

Publicar:

* CustomerCreated

---

# EPIC 04 - Catalog Service

## Feature 4.1 - Categorias

### Tasks

CRUD completo.

---

## Feature 4.2 - Produtos

### Tasks

CRUD completo.

---

## Feature 4.3 - Busca

### Tasks

* Busca por nome
* Busca por categoria

---

## Feature 4.4 - Eventos

### Tasks

Publicar:

* ProductCreated
* ProductUpdated

---

# EPIC 05 - Cart Service

## Feature 5.1 - Carrinho

### Tasks

* Adicionar item
* Remover item
* Limpar carrinho

---

## Feature 5.2 - Redis

### Tasks

Persistir carrinho em Redis.

---

# EPIC 06 - Inventory Service

## Feature 6.1 - Estoque

### Tasks

* Entrada de estoque
* Saída de estoque

---

## Feature 6.2 - Reserva

### Tasks

* Reservar estoque
* Liberar estoque

---

## Feature 6.3 - Eventos

Publicar:

* InventoryReserved
* InventoryReleased
* InventoryFailed

---

# EPIC 07 - Order Service

## Feature 7.1 - Pedido

### Tasks

* Criar pedido
* Consultar pedido

---

## Feature 7.2 - Estados

Implementar:

* Pending
* Reserved
* Paid
* Confirmed
* Cancelled

---

## Feature 7.3 - Eventos

Publicar:

* OrderCreated
* OrderConfirmed
* OrderCancelled

---

# EPIC 08 - Payment Service

## Feature 8.1 - Pagamento

### Tasks

Criar mock de gateway.

---

## Feature 8.2 - Aprovação

### Tasks

Simular aprovação.

---

## Feature 8.3 - Rejeição

### Tasks

Simular rejeição.

---

## Feature 8.4 - Eventos

Publicar:

* PaymentApproved
* PaymentRejected

---

# EPIC 09 - Notification Service

## Feature 9.1 - Consumidor

Consumir:

* OrderConfirmed
* OrderCancelled

---

## Feature 9.2 - Email

Implementar provedor fake.

---

# EPIC 10 - Audit Service

## Feature 10.1 - Auditoria

Persistir todos os eventos.

MongoDB.

---

# EPIC 11 - RabbitMQ

## Feature 11.1 - Publisher

Implementar EventBus.

---

## Feature 11.2 - Consumer

Implementar consumidor genérico.

---

## Feature 11.3 - Retry

Implementar retry.

---

## Feature 11.4 - Dead Letter

Implementar DLQ.

---

# EPIC 12 - CQRS

## Feature 12.1 - Commands

Implementar MediatR.

---

## Feature 12.2 - Queries

Separar leitura e escrita.

---

# EPIC 13 - Outbox Pattern

## Feature 13.1 - Outbox

Criar tabela Outbox.

---

## Feature 13.2 - Dispatcher

Criar worker para publicação.

---

# EPIC 14 - Saga Pattern

## Feature 14.1 - Pedido

Orquestrar:

Pedido → Estoque → Pagamento

---

## Feature 14.2 - Compensação

Pagamento falhou:

* Liberar estoque
* Cancelar pedido

---

# EPIC 15 - API Gateway

## Feature 15.1 - YARP

Configurar roteamento.

---

## Feature 15.2 - JWT

Validar token.

---

## Feature 15.3 - Rate Limiting

Implementar limite de requisições.

---

# EPIC 16 - Observabilidade

## Feature 16.1 - OpenTelemetry

Instrumentar todos os serviços.

---

## Feature 16.2 - Prometheus

Exportar métricas.

---

## Feature 16.3 - Grafana

Criar dashboards.

---

# EPIC 17 - Testes

## Feature 17.1 - Unitários

Cobertura mínima:

80%

---

## Feature 17.2 - Integração

Testar:

* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

---

# EPIC 18 - Docker

## Feature 18.1

Dockerfile para todos os serviços.

---

## Feature 18.2

Docker Compose completo.

---

# EPIC 19 - Kubernetes

## Feature 19.1

Deployments

## Feature 19.2

Services

## Feature 19.3

ConfigMaps

## Feature 19.4

Secrets

## Feature 19.5

Ingress

---

# EPIC 20 - Documentação

## Feature 20.1

Swagger

## Feature 20.2

Architecture Diagrams

## Feature 20.3

Runbook

## Feature 20.4

README Principal

## Feature 20.5

Guia de Deploy
