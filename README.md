# MicroCommerce

Uma plataforma completa de e-commerce desenvolvida utilizando arquitetura moderna baseada em Microservices.

O objetivo deste projeto é demonstrar a implementação de conceitos avançados de engenharia de software utilizados em sistemas distribuídos de larga escala.

---

# Objetivos do Projeto

Este projeto foi criado para demonstrar experiência prática com:

* Microservices
* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* Event Driven Architecture
* Saga Pattern
* Outbox Pattern
* API Gateway
* Backend For Frontend (BFF)
* RabbitMQ
* Redis
* PostgreSQL
* MongoDB
* Docker
* Kubernetes
* OpenTelemetry
* Prometheus
* Grafana
* Testes Automatizados

---

# Arquitetura

```text
Browser
   |
Next.js Frontend
   |
Web BFF
   |
API Gateway
   |
-----------------------------------------------------
|         |          |          |         |          |
Auth   Customer   Catalog     Cart      Order   Inventory
                                          |
                                       RabbitMQ
                                          |
                         ----------------------------
                         |            |             |
                      Payment   Notification     Audit
```

---

# Tecnologias

## Frontend

* Next.js 15
* React 19
* TypeScript
* Tailwind CSS
* Zustand
* TanStack Query
* Axios

---

## Backend

* .NET 8
* ASP.NET Core
* Minimal APIs
* MediatR
* FluentValidation
* Serilog
* Polly

---

## Banco de Dados

* PostgreSQL
* MongoDB
* Redis

---

## Mensageria

* RabbitMQ

---

## Observabilidade

* OpenTelemetry
* Prometheus
* Grafana

---

## Infraestrutura

* Docker
* Docker Compose
* Kubernetes

---

# Principais Conceitos Implementados

## Domain Driven Design (DDD)

Separação dos domínios de negócio:

* Auth
* Customer
* Catalog
* Cart
* Order
* Inventory
* Payment

---

## Clean Architecture

Todos os serviços seguem a estrutura:

```text
Service

├── Api
├── Application
├── Domain
├── Infrastructure
└── Tests
```

---

## CQRS

Separação entre:

* Commands
* Queries

Exemplo:

```text
CreateOrderCommand
GetOrderByIdQuery
```

---

## Event Driven Architecture

Integração entre microservices realizada através de eventos.

Exemplos:

```text
OrderCreated
InventoryReserved
PaymentApproved
OrderConfirmed
```

---

## Saga Pattern

Fluxo principal:

```text
OrderCreated
      |
InventoryReserved
      |
PaymentApproved
      |
OrderConfirmed
```

Fluxo de compensação:

```text
PaymentRejected
      |
InventoryReleased
      |
OrderCancelled
```

---

## Outbox Pattern

Garantia de consistência entre banco de dados e RabbitMQ.

```text
Save Aggregate
      |
Save Outbox Event
      |
Commit Transaction
      |
Dispatcher
      |
RabbitMQ
```

---

# Microservices

## Auth Service

Responsável por:

* Cadastro
* Login
* JWT
* Refresh Token

---

## Customer Service

Responsável por:

* Clientes
* Endereços

---

## Catalog Service

Responsável por:

* Produtos
* Categorias

---

## Cart Service

Responsável por:

* Carrinho de compras

Persistência:

* Redis

---

## Order Service

Responsável por:

* Pedidos
* Orquestração da Saga

---

## Inventory Service

Responsável por:

* Estoque
* Reservas

---

## Payment Service

Responsável por:

* Processamento de pagamentos

---

## Notification Service

Responsável por:

* Notificações
* E-mails

---

## Audit Service

Responsável por:

* Auditoria
* Histórico de eventos

---

# Frontend

Funcionalidades disponíveis:

* Cadastro de usuário
* Login
* Catálogo de produtos
* Busca de produtos
* Carrinho de compras
* Checkout
* Histórico de pedidos

---

# BFF

O Frontend nunca acessa diretamente os microservices.

Fluxo:

```text
Frontend
    |
BFF
    |
Gateway
    |
Microservices
```

Responsabilidades:

* Agregação de chamadas
* Cache
* View Models
* Autenticação

---

# Banco de Dados por Serviço

| Serviço   | Banco      |
| --------- | ---------- |
| Auth      | PostgreSQL |
| Customer  | PostgreSQL |
| Catalog   | PostgreSQL |
| Order     | PostgreSQL |
| Inventory | PostgreSQL |
| Cart      | Redis      |
| Payment   | MongoDB    |
| Audit     | MongoDB    |

---

# Estrutura do Projeto

```text
microcommerce/

src/

├── frontend/
│   └── web/
│
├── bff/
│   └── web-bff/
│
├── gateway/
│   └── api-gateway/
│
├── services/
│   ├── auth-service/
│   ├── customer-service/
│   ├── catalog-service/
│   ├── cart-service/
│   ├── order-service/
│   ├── inventory-service/
│   ├── payment-service/
│   ├── notification-service/
│   └── audit-service/
│
├── building-blocks/
│
├── tests/
│
├── docs/
│
├── docker/
│
└── k8s/
```

---

# Como Executar

## Pré-requisitos

* Docker
* Docker Compose
* .NET 8 SDK
* Node.js 22+

---

## Subir infraestrutura

```bash
docker compose up -d
```

---

## Executar Backend

```bash
dotnet build

dotnet run
```

---

## Executar Frontend

```bash
cd src/frontend/web

npm install

npm run dev
```

---

# Observabilidade

Todos os serviços expõem:

```http
GET /health

GET /metrics
```

Ferramentas utilizadas:

* OpenTelemetry
* Prometheus
* Grafana

---

# Testes

Executar todos os testes:

```bash
dotnet test
```

Cobertura mínima:

```text
80%
```

Tipos de teste:

* Unit Tests
* Integration Tests
* End-to-End Tests

---

# Documentação

Documentos disponíveis:

* AGENTS.md
* ARCHITECTURE.md
* SYSTEM_DESIGN.md
* EVENT_CATALOG.md
* ADR.md
* SPRINT_PLAN.md

---

# Roadmap

## Fase 1

* Foundation
* Auth
* Customer
* Catalog

## Fase 2

* Cart
* Order
* Inventory

## Fase 3

* RabbitMQ
* Saga
* Payment

## Fase 4

* Notification
* Audit

## Fase 5

* Gateway
* BFF
* Frontend

## Fase 6

* OpenTelemetry
* Prometheus
* Grafana

## Fase 7

* Kubernetes
* CI/CD

---

# Diferenciais Técnicos

Este projeto demonstra na prática:

✅ Microservices

✅ DDD

✅ Clean Architecture

✅ CQRS

✅ RabbitMQ

✅ Saga Pattern

✅ Outbox Pattern

✅ API Gateway

✅ BFF Pattern

✅ Redis

✅ PostgreSQL

✅ MongoDB

✅ Docker

✅ Kubernetes

✅ OpenTelemetry

✅ Prometheus

✅ Grafana

✅ Testes Automatizados

---

# Autor

Desenvolvido como projeto de estudo e portfólio profissional para demonstração de arquitetura distribuída moderna utilizando .NET, Next.js e tecnologias cloud-native.
