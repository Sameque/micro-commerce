# SYSTEM_DESIGN.md

# MicroCommerce - System Design

## Objetivo

Este documento descreve o funcionamento interno da plataforma MicroCommerce.

O foco deste documento é apresentar:

* Fluxos de negócio
* Comunicação entre componentes
* Eventos
* Integrações
* Processos distribuídos
* Estratégias de consistência

---

# Visão Geral

A plataforma é composta por:

* Frontend Web
* BFF
* API Gateway
* Microservices
* RabbitMQ
* Bancos de Dados
* Ferramentas de Observabilidade

---

# Fluxo Geral da Aplicação

```text
Usuário
   |
Frontend (Next.js)
   |
BFF
   |
API Gateway
   |
Microservices
   |
RabbitMQ
```

---

# Fluxo de Autenticação

## Login

### Passo 1

Usuário informa:

* E-mail
* Senha

---

### Passo 2

Frontend envia requisição para:

```http
POST /login
```

---

### Passo 3

BFF encaminha para:

```http
POST /auth/login
```

---

### Passo 4

Auth Service:

* Valida usuário
* Valida senha
* Gera JWT
* Gera Refresh Token

---

### Passo 5

Resposta:

```json
{
  "accessToken": "jwt",
  "refreshToken": "token"
}
```

---

## Diagrama

```text
Frontend
    |
    v
BFF
    |
    v
Gateway
    |
    v
Auth Service
```

---

# Fluxo de Cadastro

## Objetivo

Criar nova conta.

---

### Fluxo

```text
Frontend
    |
BFF
    |
Gateway
    |
Auth Service
```

---

### Endpoint

```http
POST /register
```

---

### Resultado

Usuário criado.

Evento publicado:

```text
CustomerCreated
```

---

# Fluxo de Catálogo

## Consulta de Produtos

### Fluxo

```text
Frontend
    |
BFF
    |
Gateway
    |
Catalog Service
```

---

### Endpoint BFF

```http
GET /catalog
```

---

### Endpoint Interno

```http
GET /products
```

---

### Resposta

```json
{
  "items": []
}
```

---

# Fluxo de Detalhes do Produto

### Endpoint

```http
GET /catalog/{id}
```

---

### Processo

```text
Frontend
    |
BFF
    |
Catalog Service
```

---

### Retorno

* Produto
* Categoria
* Estoque disponível

---

# Fluxo do Carrinho

## Adicionar Item

### Endpoint

```http
POST /cart
```

---

### Processo

```text
Frontend
    |
BFF
    |
Gateway
    |
Cart Service
    |
Redis
```

---

### Persistência

Redis.

---

## Remover Item

```http
DELETE /cart/items/{id}
```

---

## Atualizar Quantidade

```http
PUT /cart/items/{id}
```

---

# Fluxo de Checkout

## Objetivo

Transformar carrinho em pedido.

---

## Sequência

### 1

Cliente clica:

```text
Finalizar Compra
```

---

### 2

Frontend chama:

```http
POST /checkout
```

---

### 3

BFF recupera:

* Usuário
* Carrinho

---

### 4

BFF envia:

```http
POST /orders
```

---

### 5

Order Service cria pedido.

Estado inicial:

```text
Pending
```

---

### 6

Evento publicado:

```text
OrderCreated
```

---

# Fluxo Saga

Após OrderCreated.

```text
OrderCreated
      |
RabbitMQ
      |
Inventory Service
```

---

# Reserva de Estoque

## Inventory Service

Recebe:

```text
OrderCreated
```

---

Valida:

* Produto existe
* Quantidade disponível

---

### Sucesso

Publica:

```text
InventoryReserved
```

---

### Falha

Publica:

```text
InventoryFailed
```

---

# Processamento de Pagamento

## Payment Service

Recebe:

```text
InventoryReserved
```

---

Executa:

```text
ProcessPayment
```

---

### Sucesso

Publica:

```text
PaymentApproved
```

---

### Falha

Publica:

```text
PaymentRejected
```

---

# Confirmação do Pedido

## Order Service

Recebe:

```text
PaymentApproved
```

---

Atualiza:

```text
Pending
   |
Confirmed
```

---

Publica:

```text
OrderConfirmed
```

---

# Fluxo Completo da Saga

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

# Fluxo de Compensação

Quando o pagamento falhar.

---

## Evento

```text
PaymentRejected
```

---

### Inventory Service

Recebe:

```text
PaymentRejected
```

---

Libera estoque.

Publica:

```text
InventoryReleased
```

---

### Order Service

Recebe:

```text
InventoryReleased
```

Atualiza:

```text
Cancelled
```

---

Publica:

```text
OrderCancelled
```

---

# Fluxo de Notificações

## Notification Service

Consumidor dos eventos:

```text
OrderConfirmed

OrderCancelled
```

---

### Exemplo

```text
OrderConfirmed
      |
Notification Service
      |
Send Email
```

---

# Fluxo de Auditoria

## Audit Service

Escuta:

```text
Todos os eventos
```

---

Persistência:

MongoDB

---

### Estrutura

```json
{
  "eventId": "",
  "eventType": "",
  "occurredAt": "",
  "payload": {}
}
```

---

# Comunicação Síncrona

Utilizada para:

* Login
* Cadastro
* Consulta de produtos
* Consulta de pedidos
* Carrinho

Tecnologia:

HTTP REST

---

# Comunicação Assíncrona

Utilizada para:

* Criação de pedido
* Estoque
* Pagamento
* Notificações
* Auditoria

Tecnologia:

RabbitMQ

---

# Event Catalog

Eventos principais:

```text
CustomerCreated

ProductCreated
ProductUpdated

OrderCreated
OrderConfirmed
OrderCancelled

InventoryReserved
InventoryReleased
InventoryFailed

PaymentApproved
PaymentRejected
```

---

# Estratégia de Consistência

O sistema NÃO utiliza transações distribuídas.

Estratégia adotada:

```text
Consistência Eventual
```

Implementada através de:

* RabbitMQ
* Saga Pattern
* Outbox Pattern

---

# Outbox Pattern

Fluxo interno:

```text
Salvar Pedido
      |
Salvar Evento Outbox
      |
Commit
      |
Worker
      |
RabbitMQ
```

Objetivo:

Evitar perda de eventos.

---

# Cache

Utilizado em:

## Carrinho

Redis

---

## Catálogo

Opcionalmente:

Redis

---

# Segurança

Autenticação:

JWT

---

Autorização:

Roles

Policies

---

# Observabilidade

Todos os componentes devem fornecer:

## Logs

Serilog

---

## Traces

OpenTelemetry

---

## Métricas

Prometheus

---

## Dashboards

Grafana

---

# Health Checks

Todos os serviços:

```http
GET /health
```

---

# Métricas

Todos os serviços:

```http
GET /metrics
```

---

# Objetivo Final

Demonstrar uma implementação completa de:

* Microservices
* DDD
* Clean Architecture
* CQRS
* RabbitMQ
* Saga Pattern
* Outbox Pattern
* BFF Pattern
* API Gateway
* Redis
* PostgreSQL
* MongoDB
* OpenTelemetry
* Prometheus
* Grafana
* Docker
* Kubernetes
* Testes Automatizados

em um cenário realista de e-commerce distribuído.
