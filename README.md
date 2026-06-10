# MicroCommerce

> Plataforma de E-commerce baseada em Microservices desenvolvida com .NET 8, RabbitMQ, PostgreSQL, MongoDB, Redis, Docker, Kubernetes e OpenTelemetry.

---

# Sobre o Projeto

MicroCommerce é um projeto de portfólio criado para demonstrar a implementação de uma arquitetura moderna de Microservices utilizando práticas adotadas em sistemas distribuídos de alta escala.

O objetivo não é apenas construir um e-commerce funcional, mas demonstrar conhecimentos em:

* Microservices
* Domain Driven Design (DDD)
* Clean Architecture
* CQRS
* Event Driven Architecture
* RabbitMQ
* Saga Pattern
* Outbox Pattern
* Redis
* API Gateway
* Observabilidade
* Kubernetes
* Docker
* Testes Automatizados

---

# Arquitetura

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

# Tecnologias

## Backend

* .NET 8
* ASP.NET Core Minimal APIs
* MediatR
* FluentValidation

## Bancos de Dados

* PostgreSQL
* MongoDB
* Redis

## Mensageria

* RabbitMQ

## Observabilidade

* OpenTelemetry
* Prometheus
* Grafana

## Infraestrutura

* Docker
* Docker Compose
* Kubernetes

## API Gateway

* YARP Reverse Proxy

---

# Microservices

## Auth Service

Responsável por:

* Registro
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

Utiliza Redis.

---

## Order Service

Responsável por:

* Pedidos
* Estados do pedido
* Publicação de eventos

---

## Inventory Service

Responsável por:

* Estoque
* Reserva de estoque
* Liberação de estoque

---

## Payment Service

Responsável por:

* Processamento de pagamentos

---

## Notification Service

Responsável por:

* E-mails
* Notificações

---

## Audit Service

Responsável por:

* Auditoria
* Rastreamento de eventos

---

# Event Driven Architecture

Os serviços se comunicam através de eventos publicados no RabbitMQ.

Exemplo:

```text
OrderCreated
      |
      v
RabbitMQ
      |
      +------------------+
      |                  |
      v                  v
Inventory          Audit
```

---

# Saga Pattern

Fluxo principal:

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

Objetivo:

Garantir consistência eventual entre múltiplos serviços.

---

# Outbox Pattern

Todos os eventos são persistidos localmente antes da publicação.

Benefícios:

* Evita perda de mensagens
* Garante consistência
* Suporta reprocessamento

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
├── docker/
│
├── k8s/
│
└── docs/
```

---

# Banco por Serviço

Cada microservice possui seu próprio banco.

| Serviço   | Banco      |
| --------- | ---------- |
| Auth      | PostgreSQL |
| Customer  | PostgreSQL |
| Catalog   | PostgreSQL |
| Order     | PostgreSQL |
| Inventory | PostgreSQL |
| Payment   | MongoDB    |
| Audit     | MongoDB    |
| Cart      | Redis      |

Nenhum serviço acessa diretamente o banco de outro serviço.

---

# CQRS

Todos os serviços seguem CQRS.

Exemplo:

## Commands

```csharp
CreateOrderCommand
UpdateProductCommand
CreateCustomerCommand
```

## Queries

```csharp
GetOrderByIdQuery
GetProductsQuery
GetCustomerQuery
```

---

# Observabilidade

Todos os serviços expõem:

```http
GET /health

GET /metrics
```

Monitoramento realizado através de:

* OpenTelemetry
* Prometheus
* Grafana

---

# Métricas Monitoradas

## APIs

* Requests por segundo
* Tempo médio de resposta
* Erros por endpoint

## RabbitMQ

* Tamanho das filas
* Taxa de consumo
* Taxa de publicação

## Banco de Dados

* Conexões ativas
* Tempo médio de consultas

---

# Executando Localmente

## Pré-requisitos

* Docker
* Docker Compose
* .NET 8 SDK

---

## Clonar Repositório

```bash
git clone https://github.com/seuusuario/microcommerce.git

cd microcommerce
```

---

## Subir Infraestrutura

```bash
docker compose up -d
```

---

## Verificar Containers

```bash
docker ps
```

---

## Executar Serviços

```bash
dotnet build

dotnet run
```

---

# Ambientes

## Desenvolvimento

Docker Compose

## Homologação

Kubernetes

## Produção

Kubernetes

---

# Testes

Executar todos os testes:

```bash
dotnet test
```

Cobertura mínima esperada:

```text
80%
```

---

# Roadmap

* [x] Arquitetura definida
* [ ] Shared Kernel
* [ ] Contracts
* [ ] RabbitMQ
* [ ] Auth Service
* [ ] Customer Service
* [ ] Catalog Service
* [ ] Cart Service
* [ ] Order Service
* [ ] Inventory Service
* [ ] Payment Service
* [ ] Notification Service
* [ ] Audit Service
* [ ] Saga Pattern
* [ ] Outbox Pattern
* [ ] OpenTelemetry
* [ ] Grafana
* [ ] Prometheus
* [ ] API Gateway
* [ ] Kubernetes

---

# Conceitos Demonstrados

* Microservices
* DDD
* Clean Architecture
* CQRS
* Event Driven Architecture
* Saga Pattern
* Outbox Pattern
* RabbitMQ
* Redis
* PostgreSQL
* MongoDB
* Docker
* Kubernetes
* OpenTelemetry
* Prometheus
* Grafana
* API Gateway
* Resiliência Distribuída
* Observabilidade

---

# Autor

Projeto desenvolvido para fins de estudo, portfólio e demonstração de conhecimentos avançados em arquitetura de software e sistemas distribuídos.
