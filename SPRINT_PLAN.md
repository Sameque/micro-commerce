# MicroCommerce - Plano de Implementação

## Objetivo

Implementar o projeto MicroCommerce de forma incremental, seguindo práticas reais de desenvolvimento de software.

Cada sprint deve gerar uma entrega funcional, executável e validável.

Duração sugerida:

* 1 semana por sprint
* Total: 12 sprints

---

# Sprint 01 - Foundation

## Objetivo

Criar toda a infraestrutura base do projeto.

## Entregas

### Estrutura da Solution

```text
microcommerce/

src/
services/
gateway/
building-blocks/
docs/
docker/
k8s/
```

### Shared Kernel

Implementar:

* Entity
* AggregateRoot
* ValueObject
* DomainEvent
* Result Pattern

### Contracts

Implementar:

* BaseIntegrationEvent
* EventMetadata
* CorrelationId
* EventVersion

### Docker Compose

Subir:

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ

### Critérios de Aceitação

* Solution compila.
* Infraestrutura sobe com docker compose.
* RabbitMQ acessível.
* Bancos acessíveis.

---

# Sprint 02 - Auth Service

## Objetivo

Implementar autenticação.

## Entregas

### Endpoints

```http
POST /register
POST /login
POST /refresh-token
```

### Funcionalidades

* JWT
* Refresh Token
* Hash de senha
* Validação

### Critérios

* Login funcional.
* Token válido.
* Refresh Token funcional.

---

# Sprint 03 - Customer Service

## Objetivo

Implementar domínio de clientes.

## Entregas

### Cliente

* Criar
* Atualizar
* Consultar

### Endereço

* Adicionar
* Atualizar
* Remover

### Evento

```text
CustomerCreated
```

### Critérios

* CRUD completo.
* Evento publicado.

---

# Sprint 04 - Catalog Service

## Objetivo

Implementar catálogo.

## Entregas

### Categoria

CRUD completo.

### Produto

CRUD completo.

### Busca

* Nome
* Categoria

### Eventos

```text
ProductCreated
ProductUpdated
```

### Critérios

* CRUD funcional.
* Busca funcional.
* Eventos publicados.

---

# Sprint 05 - Cart Service

## Objetivo

Implementar carrinho.

## Entregas

### Funcionalidades

* Adicionar item
* Remover item
* Atualizar quantidade
* Limpar carrinho

### Persistência

Redis

### Critérios

* Carrinho persistido.
* Operações funcionais.

---

# Sprint 06 - RabbitMQ Infrastructure

## Objetivo

Criar infraestrutura de mensageria.

## Entregas

### Event Bus

Implementar:

* Publisher
* Consumer

### Recursos

* Retry
* DLQ
* Logging

### Critérios

* Eventos trafegando.
* Retry funcionando.
* DLQ funcionando.

---

# Sprint 07 - Order Service

## Objetivo

Implementar pedidos.

## Entregas

### Pedido

* Criar pedido
* Consultar pedido

### Estados

```text
Pending
Reserved
Paid
Confirmed
Cancelled
```

### Evento

```text
OrderCreated
```

### Critérios

* Pedido criado.
* Evento publicado.

---

# Sprint 08 - Inventory Service

## Objetivo

Implementar estoque.

## Entregas

### Estoque

* Entrada
* Saída

### Reserva

* Reservar
* Liberar

### Eventos

```text
InventoryReserved
InventoryReleased
InventoryFailed
```

### Critérios

* Reserva funcionando.
* Eventos publicados.

---

# Sprint 09 - Payment Service

## Objetivo

Implementar pagamentos.

## Entregas

### Gateway Simulado

Criar mock de gateway.

### Eventos

```text
PaymentApproved
PaymentRejected
```

### Critérios

* Aprovação simulada.
* Rejeição simulada.
* Eventos publicados.

---

# Sprint 10 - Saga Pattern

## Objetivo

Implementar consistência distribuída.

## Fluxo

```text
OrderCreated
      |
InventoryReserved
      |
PaymentApproved
      |
OrderConfirmed
```

### Fluxo de Compensação

```text
PaymentRejected
      |
InventoryReleased
      |
OrderCancelled
```

### Critérios

* Fluxo completo funcionando.
* Compensação funcionando.

---

# Sprint 11 - Notification + Audit

## Objetivo

Implementar consumidores finais.

## Notification Service

Consumir:

```text
OrderConfirmed
OrderCancelled
```

### Funcionalidades

* Simulação de envio de email

---

## Audit Service

Consumir:

Todos os eventos.

Persistir em MongoDB.

### Critérios

* Emails simulados.
* Auditoria armazenada.

---

# Sprint 12 - API Gateway

## Objetivo

Centralizar acesso.

## Entregas

### YARP

Implementar:

* Roteamento
* JWT Validation

### Rate Limiting

Implementar:

* Limite por IP

### Critérios

* Gateway roteando.
* JWT validado.
* Limite funcionando.

---

# Sprint 13 - Outbox Pattern

## Objetivo

Garantir consistência.

## Entregas

### Outbox Table

Criar persistência.

### Dispatcher

Criar Worker.

### Critérios

* Eventos persistidos.
* Publicação assíncrona funcionando.

---

# Sprint 14 - OpenTelemetry

## Objetivo

Observabilidade distribuída.

## Entregas

### Tracing

Instrumentar:

* APIs
* RabbitMQ
* PostgreSQL
* MongoDB
* Redis

### Critérios

* Traces visíveis.

---

# Sprint 15 - Prometheus

## Objetivo

Coletar métricas.

## Entregas

### Métricas

* Requests
* Errors
* Latência
* Filas

### Critérios

* Métricas disponíveis.

---

# Sprint 16 - Grafana

## Objetivo

Visualização.

## Dashboards

### APIs

* Requests
* Erros
* Latência

### RabbitMQ

* Filas
* Consumo

### Banco

* Conexões
* Tempo de consulta

### Critérios

* Dashboards prontos.

---

# Sprint 17 - Testes Automatizados

## Objetivo

Garantir qualidade.

## Unitários

Cobertura mínima:

```text
80%
```

## Integração

Cobrir:

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ

### Critérios

* Pipeline verde.

---

# Sprint 18 - Kubernetes

## Objetivo

Preparar deploy.

## Recursos

### Deployments

Todos os serviços.

### Services

Todos os serviços.

### ConfigMaps

Configurações.

### Secrets

Credenciais.

### Ingress

Entrada externa.

### Critérios

* Ambiente sobe no Kubernetes.

---

# Sprint 19 - CI/CD

## Objetivo

Automação.

## GitHub Actions

### Build

```bash
dotnet build
```

### Testes

```bash
dotnet test
```

### Docker

Build das imagens.

### Critérios

* Pipeline funcionando.

---

# Sprint 20 - Finalização do Portfólio

## Objetivo

Preparar apresentação profissional.

## Entregas

### README

Atualizado.

### Diagramas

* C4
* Fluxos
* Saga

### Screenshots

* Grafana
* RabbitMQ
* Swagger

### Documentação

* Architecture
* ADR
* Event Catalog
* Backlog

### Critérios

Projeto pronto para:

* GitHub
* Entrevistas
* Demonstrações técnicas

---

# Definition of Done

Uma sprint será considerada concluída quando:

* Código compilando.
* Testes passando.
* Docker funcional.
* Documentação atualizada.
* Code Review concluído.
* Nenhum erro crítico aberto.

---

# Resultado Esperado

Ao final da Sprint 20 o projeto deverá demonstrar:

* Arquitetura de Microservices
* DDD
* Clean Architecture
* CQRS
* RabbitMQ
* Saga Pattern
* Outbox Pattern
* Redis
* PostgreSQL
* MongoDB
* Docker
* Kubernetes
* OpenTelemetry
* Prometheus
* Grafana
* API Gateway
* CI/CD
* Testes Automatizados
* Observabilidade Distribuída

Tudo documentado e pronto para apresentação em entrevistas técnicas e avaliação por recrutadores.
