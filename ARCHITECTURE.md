# ARCHITECTURE.md

# MicroCommerce Architecture

## Visão Geral

MicroCommerce é uma plataforma de e-commerce baseada em arquitetura de Microservices.

O projeto foi concebido para demonstrar conceitos modernos de arquitetura distribuída e servir como portfólio profissional.

Princípios arquiteturais adotados:

* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* Event Driven Architecture
* Saga Pattern
* Outbox Pattern
* Backend For Frontend (BFF)
* Database Per Service
* Observabilidade Distribuída

---

# Objetivos Arquiteturais

O sistema deve atender aos seguintes objetivos:

* Baixo acoplamento entre domínios
* Escalabilidade horizontal
* Independência de deploy
* Resiliência
* Consistência eventual
* Observabilidade completa
* Facilidade de manutenção
* Testabilidade

---

# Arquitetura de Alto Nível

```text id="9nzybr"
Browser
   |
Next.js Frontend
   |
Web BFF
   |
API Gateway
   |
----------------------------------------------------------
|         |          |          |         |               |
Auth   Customer   Catalog     Cart      Order      Inventory
                                            |
                                         RabbitMQ
                                            |
                          ------------------------------------
                          |                |                 |
                       Payment      Notification         Audit
```

---

# Camadas da Solução

A solução é composta por cinco camadas principais.

## Camada de Apresentação

Responsável pela interação do usuário.

Componentes:

* Frontend Web

Tecnologias:

* Next.js
* React
* TypeScript
* Tailwind CSS

Responsabilidades:

* Interface do usuário
* Renderização
* Gerenciamento de sessão
* Navegação

---

## Camada BFF

Backend For Frontend.

Responsável por adaptar os microservices às necessidades do frontend.

Tecnologia:

* .NET 8
* ASP.NET Core Minimal APIs

Responsabilidades:

* Agregação de chamadas
* View Models
* Redução de round trips
* Cache
* Autenticação

O frontend nunca deve consumir diretamente os microservices.

Toda comunicação deve passar pelo BFF.

---

## Camada Gateway

Responsável pela entrada única da plataforma.

Tecnologia:

* YARP

Responsabilidades:

* Roteamento
* JWT Validation
* Rate Limiting
* Logging
* Telemetria

---

## Camada de Domínio

Contém os microservices.

Cada domínio possui:

* Banco próprio
* Deploy próprio
* Regras próprias

Serviços:

* Auth Service
* Customer Service
* Catalog Service
* Cart Service
* Order Service
* Inventory Service
* Payment Service
* Notification Service
* Audit Service

---

## Camada de Infraestrutura

Componentes compartilhados.

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ
* Prometheus
* Grafana

---

# Estrutura do Repositório

```text id="r6ybz4"
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
│   ├── shared-kernel/
│   ├── contracts/
│   ├── event-bus/
│   ├── observability/
│   └── outbox/
│
├── tests/
├── docs/
├── docker/
└── k8s/
```

---

# Domínios da Aplicação

## Auth Domain

Responsável por:

* Cadastro
* Login
* JWT
* Refresh Token

Banco:

PostgreSQL

---

## Customer Domain

Responsável por:

* Clientes
* Endereços

Banco:

PostgreSQL

---

## Catalog Domain

Responsável por:

* Produtos
* Categorias

Banco:

PostgreSQL

---

## Cart Domain

Responsável por:

* Carrinho

Banco:

Redis

---

## Order Domain

Responsável por:

* Pedidos
* Estados do pedido

Banco:

PostgreSQL

---

## Inventory Domain

Responsável por:

* Estoque
* Reservas

Banco:

PostgreSQL

---

## Payment Domain

Responsável por:

* Pagamentos

Banco:

MongoDB

---

## Notification Domain

Responsável por:

* E-mails
* Notificações

---

## Audit Domain

Responsável por:

* Auditoria
* Rastreamento de eventos

Banco:

MongoDB

---

# Comunicação Entre Componentes

## Frontend → BFF

Comunicação:

HTTP REST

Objetivo:

Consumir APIs otimizadas para interface.

---

## BFF → Gateway

Comunicação:

HTTP REST

Objetivo:

Centralizar acesso aos microservices.

---

## Gateway → Microservices

Comunicação:

HTTP REST

Objetivo:

Encaminhamento de requisições.

---

## Microservices → Microservices

Comunicação:

RabbitMQ

Objetivo:

Integração desacoplada baseada em eventos.

---

# Event Driven Architecture

RabbitMQ é o principal mecanismo de integração.

Fluxo padrão:

```text id="q44n3r"
Service A
   |
Business Event
   |
RabbitMQ
   |
Service B
```

Exemplos:

```text id="m9spwo"
OrderCreated

InventoryReserved

PaymentApproved

OrderConfirmed
```

---

# Saga Pattern

A criação de pedidos é implementada utilizando Saga Orquestrada.

Fluxo principal:

```text id="dxf72w"
OrderCreated
      |
InventoryReserved
      |
PaymentApproved
      |
OrderConfirmed
```

---

## Fluxo de Compensação

Quando o pagamento falhar:

```text id="7kwv58"
PaymentRejected
      |
InventoryReleased
      |
OrderCancelled
```

Objetivo:

Garantir consistência eventual.

---

# Outbox Pattern

Todos os eventos publicados devem utilizar Outbox Pattern.

Fluxo:

```text id="vrn1kx"
Persist Aggregate
        |
Persist Outbox Event
        |
Commit Transaction
        |
Background Worker
        |
RabbitMQ
```

Benefícios:

* Confiabilidade
* Reprocessamento
* Consistência

---

# Banco de Dados por Serviço

Regra obrigatória:

Cada microservice possui seu próprio banco.

Nenhum serviço pode acessar diretamente o banco de outro serviço.

---

## PostgreSQL

Utilizado por:

* Auth Service
* Customer Service
* Catalog Service
* Order Service
* Inventory Service

---

## MongoDB

Utilizado por:

* Payment Service
* Audit Service

---

## Redis

Utilizado por:

* Cart Service
* Cache Distribuído

---

# CQRS

Todos os serviços devem separar leitura e escrita.

## Commands

Exemplos:

```text id="35k79v"
CreateOrderCommand

CreateProductCommand

CreateCustomerCommand
```

---

## Queries

Exemplos:

```text id="e3hvkk"
GetOrderQuery

GetProductsQuery

GetCustomerQuery
```

---

# Clean Architecture

Todos os serviços devem seguir a seguinte estrutura:

```text id="uowjlwm"
Service

├── Api
├── Application
├── Domain
├── Infrastructure
└── Tests
```

---

## Domain

Contém:

* Entidades
* Value Objects
* Regras de Negócio

---

## Application

Contém:

* Commands
* Queries
* Handlers
* Validators

---

## Infrastructure

Contém:

* Banco de Dados
* RabbitMQ
* Cache
* APIs Externas

---

## API

Contém:

* Endpoints
* Middleware
* Configuração

---

# Observabilidade

Todos os componentes devem ser observáveis.

---

## Logs

Formato:

Structured Logging

Ferramenta:

Serilog

---

## Traces

Ferramenta:

OpenTelemetry

Instrumentar:

* HTTP
* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

---

## Métricas

Ferramenta:

Prometheus

Expor:

```http id="l4t4ry"
GET /metrics
```

---

## Health Checks

Todos os serviços:

```http id="yyohdn"
GET /health
```

---

# Segurança

Autenticação:

JWT

Autorização:

Policies
Roles

Validação realizada no Gateway e nos serviços protegidos.

---

# Resiliência

Todos os serviços devem utilizar:

* Retry
* Timeout
* Circuit Breaker

Ferramenta:

Polly

---

# Containerização

Todos os componentes devem possuir Dockerfile.

A plataforma deve ser executável via:

```bash id="wczv9c"
docker compose up -d
```

---

# Orquestração

Ambiente Kubernetes.

Recursos obrigatórios:

* Deployment
* Service
* ConfigMap
* Secret
* Ingress

---

# Princípios Arquiteturais Obrigatórios

1. Nenhum banco compartilhado.

2. Nenhuma entidade compartilhada.

3. Comunicação entre domínios via eventos.

4. Frontend acessa apenas o BFF.

5. BFF acessa apenas o Gateway.

6. Gateway acessa os microservices.

7. Eventos devem utilizar Outbox Pattern.

8. Fluxos distribuídos devem utilizar Saga Pattern.

9. Todos os serviços devem possuir observabilidade.

10. Todos os serviços devem possuir testes automatizados.
