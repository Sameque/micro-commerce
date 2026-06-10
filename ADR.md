# Architecture Decision Records

Este documento registra as principais decisões arquiteturais do projeto MicroCommerce.

---

# ADR-001 - Arquitetura de Microservices

## Status

Accepted

## Contexto

O objetivo do projeto é demonstrar competências em arquitetura distribuída moderna e servir como portfólio profissional.

O sistema possui múltiplos domínios de negócio:

* Clientes
* Produtos
* Carrinho
* Pedidos
* Estoque
* Pagamentos
* Notificações

Em uma arquitetura monolítica, esses domínios tenderiam a crescer de forma acoplada ao longo do tempo.

## Decisão

Utilizar arquitetura baseada em Microservices.

## Consequências

### Positivas

* Independência entre domínios
* Escalabilidade individual
* Deploy independente
* Demonstração de conhecimentos em sistemas distribuídos

### Negativas

* Maior complexidade operacional
* Comunicação distribuída
* Necessidade de observabilidade

---

# ADR-002 - Banco de Dados por Serviço

## Status

Accepted

## Contexto

Compartilhamento de banco de dados entre microservices gera forte acoplamento.

Mudanças em um domínio podem impactar outros serviços.

## Decisão

Cada microservice possuirá seu próprio banco de dados.

## Consequências

### Positivas

* Baixo acoplamento
* Independência tecnológica
* Escalabilidade

### Negativas

* Consistência eventual
* Duplicação de alguns dados

---

# ADR-003 - PostgreSQL como Banco Principal

## Status

Accepted

## Contexto

Os domínios principais possuem natureza relacional.

Exemplos:

* Clientes
* Produtos
* Pedidos
* Estoque

## Decisão

Utilizar PostgreSQL como banco principal.

## Consequências

### Positivas

* Open Source
* Excelente performance
* Ampla adoção
* Suporte a JSON

### Negativas

* Necessidade de administração adicional em produção

---

# ADR-004 - MongoDB para Eventos e Pagamentos

## Status

Accepted

## Contexto

Auditoria e histórico de pagamentos possuem estrutura mais flexível.

## Decisão

Utilizar MongoDB para:

* Audit Service
* Payment Service

## Consequências

### Positivas

* Flexibilidade de schema
* Facilidade de armazenar eventos

### Negativas

* Ausência de relacionamentos nativos

---

# ADR-005 - Redis para Carrinho

## Status

Accepted

## Contexto

Carrinho é altamente volátil.

Necessita:

* Baixa latência
* Atualizações frequentes

## Decisão

Utilizar Redis.

## Consequências

### Positivas

* Performance extremamente alta
* Simplicidade

### Negativas

* Persistência opcional

---

# ADR-006 - RabbitMQ como Broker

## Status

Accepted

## Contexto

O projeto necessita de comunicação assíncrona entre serviços.

Alternativas avaliadas:

* RabbitMQ
* Kafka
* Azure Service Bus

## Decisão

Utilizar RabbitMQ.

## Motivos

* Fácil configuração local
* Excelente documentação
* Curva de aprendizado menor
* Amplamente utilizado com .NET

## Consequências

### Positivas

* Simplicidade
* Boa integração com .NET

### Negativas

* Menor throughput que Kafka

---

# ADR-007 - Event Driven Architecture

## Status

Accepted

## Contexto

Comunicação síncrona excessiva gera dependência entre serviços.

## Decisão

Eventos serão o principal mecanismo de integração.

## Exemplo

```text
OrderCreated

InventoryReserved

PaymentApproved

OrderConfirmed
```

## Consequências

### Positivas

* Baixo acoplamento
* Escalabilidade

### Negativas

* Consistência eventual

---

# ADR-008 - Saga Pattern

## Status

Accepted

## Contexto

Pedidos envolvem múltiplos serviços.

Exemplo:

* Pedido
* Estoque
* Pagamento

Não existe transação distribuída.

## Decisão

Utilizar Saga Pattern.

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

## Fluxo de Compensação

```text
PaymentRejected
      |
InventoryReleased
      |
OrderCancelled
```

## Consequências

### Positivas

* Consistência eventual
* Escalabilidade

### Negativas

* Complexidade adicional

---

# ADR-009 - Saga Orquestrada

## Status

Accepted

## Contexto

Existem duas abordagens:

### Choreography

Eventos coordenam todo o fluxo.

### Orchestration

Um serviço controla a saga.

## Decisão

Utilizar Saga Orquestrada.

## Motivos

* Mais fácil de entender
* Melhor para portfólio
* Melhor rastreabilidade

## Consequências

### Positivas

* Fluxos centralizados
* Facilidade de debug

### Negativas

* Dependência do orquestrador

---

# ADR-010 - Outbox Pattern

## Status

Accepted

## Contexto

Publicar eventos após SaveChanges pode causar inconsistência.

Exemplo:

* Pedido salvo
* RabbitMQ indisponível

Resultado:

Pedido existe, evento não.

## Decisão

Implementar Outbox Pattern.

## Consequências

### Positivas

* Garantia de entrega
* Maior confiabilidade

### Negativas

* Complexidade adicional

---

# ADR-011 - CQRS

## Status

Accepted

## Contexto

Leitura e escrita possuem características diferentes.

## Decisão

Separar Commands e Queries.

## Consequências

### Positivas

* Código organizado
* Escalabilidade futura

### Negativas

* Mais classes

---

# ADR-012 - Clean Architecture

## Status

Accepted

## Contexto

Necessidade de desacoplar regras de negócio da infraestrutura.

## Decisão

Todos os serviços utilizarão Clean Architecture.

## Camadas

```text
API

Application

Domain

Infrastructure
```

## Consequências

### Positivas

* Testabilidade
* Manutenção

### Negativas

* Mais estrutura inicial

---

# ADR-013 - MediatR

## Status

Accepted

## Contexto

Necessidade de implementar CQRS de forma consistente.

## Decisão

Utilizar MediatR.

## Consequências

### Positivas

* Commands e Queries organizados
* Baixo acoplamento

### Negativas

* Curva inicial para iniciantes

---

# ADR-014 - YARP como API Gateway

## Status

Accepted

## Contexto

Necessidade de um API Gateway integrado ao ecossistema .NET.

Alternativas avaliadas:

* Ocelot
* Kong
* Nginx
* YARP

## Decisão

Utilizar YARP.

## Motivos

* Mantido pela Microsoft
* Excelente performance
* Integração nativa com ASP.NET Core

---

# ADR-015 - OpenTelemetry

## Status

Accepted

## Contexto

Microservices exigem rastreamento distribuído.

## Decisão

Utilizar OpenTelemetry.

## Consequências

### Positivas

* Standard de mercado
* Integração com Grafana
* Integração com Prometheus

---

# ADR-016 - Docker

## Status

Accepted

## Contexto

Necessidade de ambiente reproduzível.

## Decisão

Todos os serviços serão containerizados.

---

# ADR-017 - Kubernetes

## Status

Accepted

## Contexto

Necessidade de demonstrar orquestração moderna.

## Decisão

Utilizar Kubernetes.

## Objetivo

Demonstrar:

* Deployments
* Services
* Ingress
* ConfigMaps
* Secrets
* Escalabilidade horizontal

---

# ADR-018 - Testes Automatizados

## Status

Accepted

## Contexto

Garantir qualidade do projeto.

## Decisão

Implementar:

### Unit Tests

Cobertura mínima:

```text
80%
```

### Integration Tests

Cobrir:

* PostgreSQL
* MongoDB
* Redis
* RabbitMQ

---

# ADR-019 - Observabilidade Obrigatória

## Status

Accepted

## Contexto

Sistemas distribuídos exigem monitoramento.

## Decisão

Todos os serviços devem expor:

```http
GET /health

GET /metrics
```

Monitoramento via:

* OpenTelemetry
* Prometheus
* Grafana

---

# ADR-020 - Objetivo do Projeto

## Status

Accepted

## Contexto

Projeto destinado ao GitHub e demonstração profissional.

## Decisão

Priorizar:

* Clareza arquitetural
* Boas práticas
* Documentação
* Demonstração de conceitos modernos

Mesmo quando isso resultar em mais código ou mais componentes do que seriam necessários em um sistema simples.

## Consequência

O projeto servirá como referência de arquitetura moderna para entrevistas técnicas, apresentações e portfólio profissional.
