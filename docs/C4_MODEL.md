# C4_MODEL.md

# MicroCommerce - C4 Model

## Objetivo

Este documento descreve a arquitetura da plataforma utilizando o modelo C4.

Níveis documentados:

* Level 1 — System Context
* Level 2 — Container
* Level 3 — Component

---

# Level 1 — System Context

## Visão Geral

MicroCommerce é uma plataforma de e-commerce distribuída baseada em Microservices.

O sistema permite:

* Cadastro de usuários
* Login
* Consulta de produtos
* Carrinho de compras
* Checkout
* Processamento de pedidos
* Pagamentos
* Notificações

---

## Context Diagram

```text
+------------------------------------------------+
|                 Cliente                         |
+------------------------------------------------+
                    |
                    v
+------------------------------------------------+
|               MicroCommerce                     |
|            E-commerce Platform                  |
+------------------------------------------------+
                    |
     ---------------------------------
     |               |               |
     v               v               v
 Payment Gateway   Email Provider   Monitoring Stack
```

---

## Sistemas Externos

### Payment Gateway

Responsável por:

* Processamento de pagamentos

Exemplos futuros:

* Stripe
* Mercado Pago
* Pagar.me

---

### Email Provider

Responsável por:

* Envio de notificações

Exemplos futuros:

* SendGrid
* Amazon SES

---

### Monitoring Stack

Responsável por:

* Observabilidade
* Dashboards
* Métricas

Componentes:

* Grafana
* Prometheus

---

# Level 2 — Container Diagram

## Visão Geral

O sistema é composto por múltiplos containers independentes.

---

## Container Diagram

```text
┌─────────────────────┐
│      Cliente        │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Frontend Web      │
│      Next.js        │
└──────────┬──────────┘
           │ HTTP
           ▼
┌─────────────────────┐
│      Web BFF        │
│      .NET 8         │
└──────────┬──────────┘
           │ HTTP
           ▼
┌─────────────────────┐
│    API Gateway      │
│       YARP          │
└──────────┬──────────┘
           │
           ▼
─────────────────────────────────────────────
│ Auth Service                              │
│ Customer Service                          │
│ Catalog Service                           │
│ Cart Service                              │
│ Order Service                             │
│ Inventory Service                         │
│ Payment Service                           │
│ Notification Service                      │
│ Audit Service                             │
─────────────────────────────────────────────
           │
           ▼
┌─────────────────────┐
│     RabbitMQ        │
└─────────────────────┘
```

---

## Frontend Web

Tecnologia:

* Next.js
* React
* TypeScript

Responsabilidades:

* Interface do usuário
* Navegação
* Autenticação
* Checkout

---

## Web BFF

Tecnologia:

* .NET 8

Responsabilidades:

* Agregação de chamadas
* View Models
* Cache
* Orquestração para frontend

---

## API Gateway

Tecnologia:

* YARP

Responsabilidades:

* Roteamento
* JWT Validation
* Rate Limiting

---

## Auth Service

Responsabilidades:

* Cadastro
* Login
* JWT
* Refresh Token

Banco:

PostgreSQL

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

Banco:

PostgreSQL

---

## Cart Service

Responsabilidades:

* Carrinho

Banco:

Redis

---

## Order Service

Responsabilidades:

* Pedidos
* Saga

Banco:

PostgreSQL

---

## Inventory Service

Responsabilidades:

* Estoque
* Reservas

Banco:

PostgreSQL

---

## Payment Service

Responsabilidades:

* Pagamentos

Banco:

MongoDB

---

## Notification Service

Responsabilidades:

* Notificações

---

## Audit Service

Responsabilidades:

* Auditoria

Banco:

MongoDB

---

# Banco de Dados por Serviço

```text
Auth Service ---------- PostgreSQL

Customer Service ------ PostgreSQL

Catalog Service ------- PostgreSQL

Order Service --------- PostgreSQL

Inventory Service ----- PostgreSQL

Cart Service ---------- Redis

Payment Service ------- MongoDB

Audit Service --------- MongoDB
```

---

# Comunicação Entre Containers

## Síncrona

Tecnologia:

HTTP REST

Fluxo:

```text
Frontend
    |
BFF
    |
Gateway
    |
Microservice
```

---

## Assíncrona

Tecnologia:

RabbitMQ

Fluxo:

```text
Microservice
      |
RabbitMQ
      |
Microservice
```

---

# Level 3 — Component Diagram

## Auth Service

### Componentes

```text
API
 |
Application
 |
Domain
 |
Infrastructure
```

---

### API

Responsável por:

* Endpoints
* Middleware
* Swagger

---

### Application

Responsável por:

* Commands
* Queries
* Validators
* Handlers

---

### Domain

Responsável por:

* Entidades
* Regras de negócio
* Eventos de domínio

---

### Infrastructure

Responsável por:

* PostgreSQL
* JWT
* Repositórios

---

# Catalog Service

## Componentes

```text
API
 |
Application
 |
Domain
 |
Infrastructure
```

---

### Principais Componentes

#### Product Aggregate

Responsável por:

* Nome
* Descrição
* Preço
* Categoria

---

#### Product Repository

Responsável por:

Persistência.

---

#### Product Queries

Responsável por:

Leitura de produtos.

---

#### Product Commands

Responsável por:

Escrita de produtos.

---

# Order Service

## Componentes

```text
API
 |
Application
 |
Domain
 |
Infrastructure
```

---

### Aggregate Principal

Order

---

### Commands

```text
CreateOrderCommand

ConfirmOrderCommand

CancelOrderCommand
```

---

### Queries

```text
GetOrderQuery

GetOrdersQuery
```

---

### Eventos

```text
OrderCreated

OrderConfirmed

OrderCancelled
```

---

### Saga Coordinator

Responsável por:

* Iniciar Saga
* Controlar estado
* Publicar eventos

---

# Inventory Service

## Componentes

### Inventory Aggregate

Responsável por:

* Quantidade disponível
* Reserva

---

### Commands

```text
ReserveInventoryCommand

ReleaseInventoryCommand
```

---

### Eventos

```text
InventoryReserved

InventoryReleased

InventoryFailed
```

---

# Payment Service

## Componentes

### Payment Aggregate

Responsável por:

* Transação
* Status

---

### Commands

```text
ProcessPaymentCommand
```

---

### Eventos

```text
PaymentApproved

PaymentRejected
```

---

# Notification Service

## Componentes

### Email Sender

Responsável por:

* Enviar e-mails

---

### Event Consumers

Consumidores de:

```text
OrderConfirmed

OrderCancelled
```

---

# Audit Service

## Componentes

### Event Consumer

Consome todos os eventos.

---

### Audit Repository

Responsável por:

Persistência dos eventos.

---

# Fluxo de Checkout

## Caminho Feliz

```text
Frontend
    |
BFF
    |
Order Service
    |
OrderCreated
    |
RabbitMQ
    |
Inventory Service
    |
InventoryReserved
    |
RabbitMQ
    |
Payment Service
    |
PaymentApproved
    |
RabbitMQ
    |
Order Service
    |
OrderConfirmed
```

---

# Fluxo de Compensação

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

# Observabilidade

Todos os containers devem possuir:

```text
Logs

Traces

Metrics

Health Checks
```

---

## Logs

Ferramenta:

Serilog

---

## Traces

Ferramenta:

OpenTelemetry

---

## Métricas

Ferramenta:

Prometheus

---

## Dashboards

Ferramenta:

Grafana

---

# Resumo

A arquitetura do MicroCommerce foi projetada para demonstrar:

* Microservices
* DDD
* Clean Architecture
* CQRS
* Event Driven Architecture
* Saga Pattern
* Outbox Pattern
* BFF Pattern
* API Gateway
* Observabilidade Distribuída
* Infraestrutura Cloud Native

utilizando tecnologias amplamente adotadas em ambientes corporativos modernos.
