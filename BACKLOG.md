# BACKLOG.md

# MicroCommerce Product Backlog

## Objetivo

Este documento contém o backlog completo do projeto MicroCommerce.

Os itens estão organizados em:

* Épicos
* Features
* User Stories
* Dependências

Prioridade:

* P0 = Crítico
* P1 = Alta
* P2 = Média
* P3 = Baixa

---

# EPIC 01 - Foundation

## Objetivo

Preparar toda a estrutura inicial do projeto.

Prioridade:

P0

---

### FEATURE 01.01

Criar estrutura do repositório.

#### User Stories

* Como desenvolvedor quero uma estrutura organizada para suportar múltiplos serviços.

---

### FEATURE 01.02

Configurar solução .NET.

#### User Stories

* Como desenvolvedor quero uma solution única para facilitar o desenvolvimento.

---

### FEATURE 01.03

Criar Shared Kernel.

#### User Stories

* Como desenvolvedor quero reutilizar componentes básicos entre serviços.

Itens:

* Entity
* AggregateRoot
* ValueObject
* Result
* DomainEvent

---

### FEATURE 01.04

Criar Contracts.

Itens:

* IntegrationEvent
* CorrelationId
* EventVersion

---

### FEATURE 01.05

Criar ambiente Docker.

Itens:

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ

---

# EPIC 02 - Auth Service

Prioridade:

P0

---

### FEATURE 02.01

Cadastro de usuário.

#### User Stories

* Como usuário quero criar uma conta.

---

### FEATURE 02.02

Login.

#### User Stories

* Como usuário quero autenticar no sistema.

---

### FEATURE 02.03

Refresh Token.

#### User Stories

* Como usuário quero renovar minha sessão sem novo login.

---

### FEATURE 02.04

JWT Authentication.

---

# EPIC 03 - Customer Service

Prioridade:

P0

---

### FEATURE 03.01

Cadastro de cliente.

---

### FEATURE 03.02

Atualização de cliente.

---

### FEATURE 03.03

CRUD de endereço.

---

### FEATURE 03.04

Publicação de evento CustomerCreated.

---

# EPIC 04 - Catalog Service

Prioridade:

P0

---

### FEATURE 04.01

Cadastro de categorias.

---

### FEATURE 04.02

Cadastro de produtos.

---

### FEATURE 04.03

Atualização de produtos.

---

### FEATURE 04.04

Busca de produtos.

---

### FEATURE 04.05

Filtro por categoria.

---

### FEATURE 04.06

Eventos ProductCreated.

---

### FEATURE 04.07

Eventos ProductUpdated.

---

# EPIC 05 - Cart Service

Prioridade:

P0

---

### FEATURE 05.01

Criar carrinho.

---

### FEATURE 05.02

Adicionar item.

---

### FEATURE 05.03

Remover item.

---

### FEATURE 05.04

Atualizar quantidade.

---

### FEATURE 05.05

Persistência Redis.

---

# EPIC 06 - RabbitMQ Infrastructure

Prioridade:

P0

---

### FEATURE 06.01

Configurar exchanges.

---

### FEATURE 06.02

Configurar filas.

---

### FEATURE 06.03

Implementar consumers.

---

### FEATURE 06.04

Implementar publisher.

---

### FEATURE 06.05

Retry Policy.

---

### FEATURE 06.06

Dead Letter Queue.

---

# EPIC 07 - Order Service

Prioridade:

P0

---

### FEATURE 07.01

Criar pedido.

---

### FEATURE 07.02

Consultar pedido.

---

### FEATURE 07.03

Histórico de pedidos.

---

### FEATURE 07.04

Estados do pedido.

Estados:

```text
Pending
Confirmed
Cancelled
```

---

### FEATURE 07.05

Evento OrderCreated.

---

# EPIC 08 - Inventory Service

Prioridade:

P0

---

### FEATURE 08.01

Controle de estoque.

---

### FEATURE 08.02

Reserva de estoque.

---

### FEATURE 08.03

Liberação de estoque.

---

### FEATURE 08.04

Evento InventoryReserved.

---

### FEATURE 08.05

Evento InventoryReleased.

---

### FEATURE 08.06

Evento InventoryFailed.

---

# EPIC 09 - Payment Service

Prioridade:

P0

---

### FEATURE 09.01

Processamento de pagamento.

---

### FEATURE 09.02

Aprovação de pagamento.

---

### FEATURE 09.03

Rejeição de pagamento.

---

### FEATURE 09.04

Evento PaymentApproved.

---

### FEATURE 09.05

Evento PaymentRejected.

---

# EPIC 10 - Saga Pattern

Prioridade:

P0

---

### FEATURE 10.01

Saga de Checkout.

---

### FEATURE 10.02

Fluxo principal.

```text
OrderCreated
InventoryReserved
PaymentApproved
OrderConfirmed
```

---

### FEATURE 10.03

Fluxo de compensação.

```text
PaymentRejected
InventoryReleased
OrderCancelled
```

---

# EPIC 11 - Notification Service

Prioridade:

P1

---

### FEATURE 11.01

Consumir OrderConfirmed.

---

### FEATURE 11.02

Consumir OrderCancelled.

---

### FEATURE 11.03

Enviar e-mail.

---

# EPIC 12 - Audit Service

Prioridade:

P1

---

### FEATURE 12.01

Consumir todos os eventos.

---

### FEATURE 12.02

Persistir auditoria.

---

### FEATURE 12.03

Consulta de auditoria.

---

# EPIC 13 - API Gateway

Prioridade:

P1

---

### FEATURE 13.01

Configurar YARP.

---

### FEATURE 13.02

Roteamento.

---

### FEATURE 13.03

JWT Validation.

---

### FEATURE 13.04

Rate Limiting.

---

# EPIC 14 - Outbox Pattern

Prioridade:

P1

---

### FEATURE 14.01

Tabela Outbox.

---

### FEATURE 14.02

Dispatcher.

---

### FEATURE 14.03

Retry.

---

### FEATURE 14.04

Reprocessamento.

---

# EPIC 15 - BFF

Prioridade:

P1

---

### FEATURE 15.01

Criar Web BFF.

---

### FEATURE 15.02

Endpoint de catálogo.

```http
GET /catalog
```

---

### FEATURE 15.03

Endpoint de produto.

```http
GET /catalog/{id}
```

---

### FEATURE 15.04

Endpoint de carrinho.

```http
GET /cart
POST /cart
DELETE /cart
```

---

### FEATURE 15.05

Endpoint de checkout.

```http
POST /checkout
```

---

### FEATURE 15.06

Endpoint de pedidos.

```http
GET /orders
GET /orders/{id}
```

---

### FEATURE 15.07

Cache.

---

# EPIC 16 - Frontend

Prioridade:

P1

---

### FEATURE 16.01

Tela Home.

---

### FEATURE 16.02

Tela Login.

---

### FEATURE 16.03

Tela Cadastro.

---

### FEATURE 16.04

Tela Catálogo.

---

### FEATURE 16.05

Tela Produto.

---

### FEATURE 16.06

Tela Carrinho.

---

### FEATURE 16.07

Tela Checkout.

---

### FEATURE 16.08

Tela Pedidos.

---

### FEATURE 16.09

Autenticação.

---

# EPIC 17 - Observabilidade

Prioridade:

P1

---

### FEATURE 17.01

OpenTelemetry.

---

### FEATURE 17.02

Tracing distribuído.

---

### FEATURE 17.03

Instrumentação RabbitMQ.

---

### FEATURE 17.04

Instrumentação PostgreSQL.

---

### FEATURE 17.05

Instrumentação MongoDB.

---

### FEATURE 17.06

Instrumentação Redis.

---

# EPIC 18 - Prometheus

Prioridade:

P2

---

### FEATURE 18.01

Coleta de métricas.

---

### FEATURE 18.02

Métricas HTTP.

---

### FEATURE 18.03

Métricas RabbitMQ.

---

### FEATURE 18.04

Métricas Banco de Dados.

---

# EPIC 19 - Grafana

Prioridade:

P2

---

### FEATURE 19.01

Dashboard APIs.

---

### FEATURE 19.02

Dashboard RabbitMQ.

---

### FEATURE 19.03

Dashboard Banco de Dados.

---

### FEATURE 19.04

Dashboard Saga.

---

# EPIC 20 - Testes Automatizados

Prioridade:

P1

---

### FEATURE 20.01

Unit Tests.

---

### FEATURE 20.02

Integration Tests.

---

### FEATURE 20.03

End-to-End Tests.

---

### FEATURE 20.04

Cobertura mínima 80%.

---

# EPIC 21 - Kubernetes

Prioridade:

P2

---

### FEATURE 21.01

Deployments.

---

### FEATURE 21.02

Services.

---

### FEATURE 21.03

ConfigMaps.

---

### FEATURE 21.04

Secrets.

---

### FEATURE 21.05

Ingress.

---

# EPIC 22 - CI/CD

Prioridade:

P2

---

### FEATURE 22.01

GitHub Actions Build.

---

### FEATURE 22.02

GitHub Actions Tests.

---

### FEATURE 22.03

Docker Build.

---

### FEATURE 22.04

Docker Publish.

---

### FEATURE 22.05

Deploy Pipeline.

---

# EPIC 23 - Frontend Observability

Prioridade:

P3

---

### FEATURE 23.01

Frontend Logging.

---

### FEATURE 23.02

Frontend Metrics.

---

### FEATURE 23.03

Frontend Tracing.

---

# EPIC 24 - Portfólio

Prioridade:

P1

---

### FEATURE 24.01

Atualizar README.

---

### FEATURE 24.02

Criar diagramas finais.

---

### FEATURE 24.03

Screenshots.

---

### FEATURE 24.04

GIFs de demonstração.

---

### FEATURE 24.05

Vídeo demonstrativo.

---

# MVP

Para possuir um MVP funcional devem estar concluídos:

* EPIC 01
* EPIC 02
* EPIC 03
* EPIC 04
* EPIC 05
* EPIC 06
* EPIC 07
* EPIC 08
* EPIC 09
* EPIC 10
* EPIC 13
* EPIC 15
* EPIC 16

---

# Meta Final

Ao final do projeto será possível:

* Criar conta
* Realizar login
* Consultar catálogo
* Adicionar itens ao carrinho
* Finalizar compra
* Processar pagamento
* Confirmar pedido
* Visualizar histórico
* Monitorar a plataforma
* Demonstrar arquitetura distribuída moderna para entrevistas técnicas e portfólio profissional
