## Projeto

**MicroCommerce**

Plataforma de e-commerce desenvolvida utilizando arquitetura de Microservices com .NET 8, Event-Driven Architecture, RabbitMQ, Docker, Kubernetes e observabilidade completa.

O objetivo principal deste projeto é demonstrar conhecimentos avançados em:

* Microservices
* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* Event-Driven Architecture
* Saga Pattern
* Outbox Pattern
* RabbitMQ
* Redis
* PostgreSQL
* MongoDB
* OpenTelemetry
* Prometheus
* Grafana
* Docker
* Kubernetes
* API Gateway

Este projeto será utilizado como portfólio profissional e deverá seguir boas práticas de arquitetura e engenharia de software.

---

# Objetivos Arquiteturais

O sistema deve demonstrar:

* Independência entre serviços
* Banco de dados por serviço
* Comunicação síncrona via HTTP/gRPC
* Comunicação assíncrona via RabbitMQ
* Consistência eventual
* Escalabilidade horizontal
* Observabilidade distribuída
* Resiliência
* Separação clara de responsabilidades

---

# Stack Tecnológica

## Backend

* .NET 8
* ASP.NET Core
* Minimal APIs

## Arquitetura

* Clean Architecture
* DDD
* CQRS
* MediatR
* FluentValidation

## Mensageria

* RabbitMQ

## Banco de Dados

### PostgreSQL

Utilizar para:

* Auth Service
* Customer Service
* Catalog Service
* Order Service
* Inventory Service

### MongoDB

Utilizar para:

* Payment Service
* Audit Service

### Redis

Utilizar para:

* Cart Service
* Cache distribuído

## Gateway

YARP Reverse Proxy

## Observabilidade

* OpenTelemetry
* Prometheus
* Grafana

## Containerização

Docker

## Orquestração

Kubernetes

---

# Estrutura do Repositório

```text
microcommerce/

src/

├── gateway/
│
├── services/
│   ├── auth-service/
│   ├── customer-service/
│   ├── catalog-service/
│   ├── cart-service/
│   ├── order-service/
│   ├── payment-service/
│   ├── inventory-service/
│   ├── notification-service/
│   └── audit-service/
│
├── building-blocks/
│   ├── shared-kernel/
│   ├── contracts/
│   ├── event-bus/
│   ├── observability/
│   └── outbox/
│
├── docker/
│
├── k8s/
│
└── docs/
```

---

# Padrão Interno dos Serviços

Todos os microservices devem utilizar a seguinte estrutura:

```text
ServiceName

src/

├── Api
├── Application
├── Domain
├── Infrastructure
└── Tests
```

---

# Regras Obrigatórias

## Banco por Serviço

Cada microservice deve possuir seu próprio banco.

Nunca compartilhar banco de dados entre serviços.

Comunicação entre serviços deve ocorrer exclusivamente via:

* API
* Eventos

---

## Acoplamento

Evitar dependências diretas entre serviços.

Preferir:

* Eventos de domínio
* Contratos compartilhados

---

## Contratos

Todos os contratos de eventos devem ficar em:

```text
building-blocks/contracts
```

Exemplo:

```csharp
public record OrderCreatedEvent(
    Guid OrderId,
    Guid CustomerId,
    decimal TotalAmount
);
```

---

# Microservices

## Auth Service

Responsabilidades:

* Registro de usuário
* Login
* Refresh Token
* JWT

Banco:

PostgreSQL

Endpoints:

```http
POST /register
POST /login
POST /refresh-token
```

---

## Customer Service

Responsabilidades:

* Clientes
* Endereços

Banco:

PostgreSQL

---

## Catalog Service

Responsabilidades:

* Produtos
* Categorias
* Consulta de catálogo

Banco:

PostgreSQL

---

## Cart Service

Responsabilidades:

* Carrinho de compras

Banco:

Redis

---

## Order Service

Responsabilidades:

* Criação de pedidos
* Consulta de pedidos

Banco:

PostgreSQL

Eventos:

```text
OrderCreated
OrderCancelled
OrderPaid
```

---

## Inventory Service

Responsabilidades:

* Reserva de estoque
* Liberação de estoque

Banco:

PostgreSQL

Eventos:

```text
InventoryReserved
InventoryReleased
InventoryFailed
```

---

## Payment Service

Responsabilidades:

* Processamento de pagamento

Banco:

MongoDB

Eventos:

```text
PaymentApproved
PaymentRejected
```

---

## Notification Service

Responsabilidades:

* E-mail
* Notificações

Consumidor de eventos.

Inicialmente sem banco.

---

## Audit Service

Responsabilidades:

* Auditoria
* Rastreamento de eventos

Banco:

MongoDB

Consumir todos os eventos publicados.

---

# Event Driven Architecture

RabbitMQ deve ser utilizado para integração entre serviços.

Fluxo principal:

```text
Order Service
      |
      v
OrderCreated
      |
      v
RabbitMQ
      |
      +------------------+
      |                  |
      v                  v
Inventory          Payment
```

---

# Saga Pattern

Implementar Saga Orquestrada.

Fluxo:

```text
Pedido Criado
      |
Reserva Estoque
      |
Pagamento
      |
Pedido Confirmado
```

Fluxo de compensação:

```text
Pagamento Falhou
      |
Libera Estoque
      |
Cancela Pedido
```

Eventos de compensação devem ser implementados.

---

# Outbox Pattern

Todos os eventos publicados devem utilizar Outbox Pattern.

Objetivo:

Garantir consistência entre:

* Persistência local
* Publicação de eventos

Nenhum evento deve ser publicado diretamente após SaveChanges.

---

# CQRS

Todos os serviços devem separar:

Commands:

```text
CreateOrderCommand
CreateCustomerCommand
UpdateProductCommand
```

Queries:

```text
GetOrderByIdQuery
GetProductsQuery
GetCustomerQuery
```

---

# Observabilidade

Todos os serviços devem expor:

```text
/health
/metrics
```

Instrumentação obrigatória:

* Traces
* Metrics
* Logs

Utilizar OpenTelemetry.

---

# API Gateway

Implementar YARP.

Responsabilidades:

* Roteamento
* Rate Limiting
* Autenticação

Exemplo:

```text
/api/customers
/api/orders
/api/products
```

---

# Docker

Todos os serviços devem possuir:

```text
Dockerfile
```

Deve existir:

```text
docker-compose.yml
```

Subindo:

* APIs
* RabbitMQ
* PostgreSQL
* MongoDB
* Redis
* Grafana
* Prometheus

Comando esperado:

```bash
docker compose up -d
```

---

# Kubernetes

Criar manifests para:

* Deployments
* Services
* ConfigMaps
* Secrets
* Ingress

Estrutura:

```text
k8s/

├── auth-service
├── customer-service
├── catalog-service
├── order-service
├── payment-service
├── inventory-service
├── notification-service
└── gateway
```

---

# Qualidade de Código

Obrigatório:

* SOLID
* Clean Code
* Dependency Injection
* Unit Tests
* Integration Tests

Cobertura mínima:

```text
80%
```

---

# Convenções

Namespaces:

```csharp
MicroCommerce.ServiceName.*
```

Exemplo:

```csharp
MicroCommerce.OrderService.Domain
MicroCommerce.OrderService.Application
MicroCommerce.OrderService.Infrastructure
```

---

# Roadmap de Implementação

## Fase 1

Infraestrutura Base

* Solution
* Shared Kernel
* Contracts
* Docker Compose
* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

## Fase 2

Auth Service

## Fase 3

Customer Service

## Fase 4

Catalog Service

## Fase 5

Cart Service

## Fase 6

Order Service

## Fase 7

Inventory Service

## Fase 8

Payment Service

## Fase 9

Notification Service

## Fase 10

Audit Service

## Fase 11

Saga Pattern

## Fase 12

Outbox Pattern

## Fase 13

Observabilidade

## Fase 14

API Gateway

## Fase 15

Kubernetes

---

# Critério de Sucesso

O projeto será considerado concluído quando:

* Todos os microservices estiverem independentes.
* RabbitMQ estiver funcionando.
* Saga Pattern estiver implementada.
* Outbox Pattern estiver implementada.
* Observabilidade estiver funcionando.
* Docker Compose subir todo o ambiente.
* Kubernetes possuir manifests completos.
* Existirem testes automatizados.
* Toda arquitetura estiver documentada em `/docs`.
* O projeto puder ser apresentado como exemplo de arquitetura moderna de microservices em entrevistas técnicas.
