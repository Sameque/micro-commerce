# SPRINT_PLAN.md

# MicroCommerce Sprint Plan

## Objetivo

Este documento descreve o planejamento de execução do projeto MicroCommerce.

Estratégia adotada:

* Entrega incremental
* MVP funcional o mais cedo possível
* Evolução gradual da arquitetura
* Priorização dos componentes de negócio
* Demonstração progressiva de competências técnicas

---

# Visão Geral

| Sprint    | Objetivo                     |
| --------- | ---------------------------- |
| Sprint 01 | Foundation                   |
| Sprint 02 | Auth Service                 |
| Sprint 03 | Customer Service             |
| Sprint 04 | Catalog Service              |
| Sprint 05 | Cart Service                 |
| Sprint 06 | Order + Inventory            |
| Sprint 07 | RabbitMQ + Payment           |
| Sprint 08 | Saga + Outbox                |
| Sprint 09 | Gateway + BFF + Frontend     |
| Sprint 10 | Observabilidade + Kubernetes |

---

# Sprint 01 - Foundation

## Objetivo

Criar toda a estrutura base do projeto.

## Entregas

### Repositório

* Estrutura de diretórios
* Solution .NET

### Building Blocks

* Shared Kernel
* Contracts
* Event Bus
* Observability
* Outbox

### Infraestrutura

Docker Compose contendo:

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ

## Critério de Aceite

```text
docker compose up -d

todos os containers funcionando
```

---

# Sprint 02 - Auth Service

## Objetivo

Implementar autenticação.

## Entregas

### Auth Service

Endpoints:

```http
POST /register

POST /login

POST /refresh-token
```

### Funcionalidades

* Cadastro
* Login
* JWT
* Refresh Token

### Banco

PostgreSQL

## Critério de Aceite

```text
Usuário consegue:

Cadastrar
Logar
Receber JWT
Renovar sessão
```

---

# Sprint 03 - Customer Service

## Objetivo

Gerenciar clientes.

## Entregas

### Endpoints

```http
GET /customers/{id}

PUT /customers/{id}

POST /customers/address
```

### Funcionalidades

* Dados cadastrais
* Endereços

### Evento

```text
CustomerCreated
```

## Critério de Aceite

```text
Cliente consegue atualizar seus dados
```

---

# Sprint 04 - Catalog Service

## Objetivo

Disponibilizar catálogo.

## Entregas

### Categorias

CRUD completo

### Produtos

CRUD completo

### Consulta

```http
GET /products

GET /products/{id}
```

### Eventos

```text
ProductCreated

ProductUpdated
```

## Critério de Aceite

```text
Produtos podem ser cadastrados e consultados
```

---

# Sprint 05 - Cart Service

## Objetivo

Implementar carrinho.

## Entregas

### Redis

Persistência do carrinho

### Endpoints

```http
POST /cart/items

PUT /cart/items

DELETE /cart/items

GET /cart
```

## Critério de Aceite

```text
Usuário consegue:

Adicionar item
Atualizar quantidade
Remover item
Visualizar carrinho
```

---

# Sprint 06 - Order + Inventory

## Objetivo

Criar fluxo inicial de pedidos.

## Entregas

### Order Service

```http
POST /orders

GET /orders

GET /orders/{id}
```

### Inventory Service

```http
POST /inventory/reserve

POST /inventory/release
```

### Estados

```text
Pending

Confirmed

Cancelled
```

## Critério de Aceite

```text
Pedido criado com sucesso
```

---

# Sprint 07 - RabbitMQ + Payment

## Objetivo

Implementar comunicação assíncrona.

## Entregas

### RabbitMQ

* Exchanges
* Queues
* DLQ

### Payment Service

```http
POST /payments
```

### Eventos

```text
OrderCreated

InventoryReserved

PaymentApproved

PaymentRejected
```

## Critério de Aceite

```text
Eventos trafegando via RabbitMQ
```

---

# Sprint 08 - Saga + Outbox

## Objetivo

Implementar consistência distribuída.

## Entregas

### Saga

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

### Compensação

```text
PaymentRejected
    |
InventoryReleased
    |
OrderCancelled
```

### Outbox

* Persistência
* Dispatcher
* Retry

## Critério de Aceite

```text
Fluxos felizes e compensatórios funcionando
```

---

# Sprint 09 - Gateway + BFF + Frontend

## Objetivo

Disponibilizar interface completa.

## Entregas

### API Gateway

YARP

Funcionalidades:

* Roteamento
* JWT
* Rate Limiting

### BFF

Endpoints:

```http
GET /catalog

GET /cart

POST /checkout

GET /orders
```

### Frontend

Telas:

* Login
* Cadastro
* Home
* Catálogo
* Produto
* Carrinho
* Checkout
* Pedidos

## Critério de Aceite

```text
Fluxo completo:

Login
Catálogo
Carrinho
Checkout
Pedido
```

---

# Sprint 10 - Observabilidade + Kubernetes

## Objetivo

Finalizar arquitetura cloud-native.

## Entregas

### OpenTelemetry

Instrumentação:

* HTTP
* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

### Prometheus

Coleta de métricas

### Grafana

Dashboards:

* APIs
* RabbitMQ
* Banco de Dados
* Saga

### Kubernetes

Manifests:

```text
Deployment

Service

Ingress

ConfigMap

Secret
```

## Critério de Aceite

```text
Sistema totalmente monitorado
```

---

# Definição de Pronto (Definition of Done)

Uma tarefa é considerada concluída quando:

### Código

* Compila
* Sem warnings críticos

### Testes

* Unitários implementados
* Todos passando

### Observabilidade

* Logs estruturados
* Health Check disponível

### Documentação

* Swagger atualizado
* Documentação revisada

### Qualidade

* Code Review realizado
* Padrões arquiteturais respeitados

---

# MVP

Ao final da Sprint 09 o sistema deve permitir:

```text
Cadastro

Login

Consulta de Produtos

Carrinho

Checkout

Pedido

Pagamento

Confirmação
```

---

# Métricas de Sucesso

## Backend

* Cobertura mínima de testes: 80%
* Health Checks em todos os serviços
* Tracing distribuído funcionando

## Frontend

* Lighthouse > 90
* Responsivo

## Infraestrutura

* Docker Compose funcional
* Kubernetes funcional

---

# Resultado Esperado

Ao final do projeto será possível demonstrar experiência prática com:

* Microservices
* DDD
* Clean Architecture
* CQRS
* RabbitMQ
* Saga Pattern
* Outbox Pattern
* PostgreSQL
* MongoDB
* Redis
* OpenTelemetry
* Prometheus
* Grafana
* Docker
* Kubernetes
* Next.js
* BFF Pattern
* API Gateway

em um cenário realista de e-commerce distribuído de nível corporativo.
