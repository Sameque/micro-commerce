# ADR.md

# Architecture Decision Records

## Objetivo

Este documento registra as principais decisões arquiteturais adotadas no projeto MicroCommerce.

Todos os ADRs devem seguir o formato:

* Contexto
* Decisão
* Consequências

---

# ADR-001 - Arquitetura de Microservices

## Status

Accepted

## Contexto

A plataforma possui múltiplos domínios independentes:

* Autenticação
* Clientes
* Catálogo
* Carrinho
* Pedidos
* Estoque
* Pagamentos
* Notificações

Uma arquitetura monolítica aumentaria o acoplamento entre esses domínios.

## Decisão

Utilizar arquitetura baseada em Microservices.

## Consequências

### Positivas

* Deploy independente
* Escalabilidade por domínio
* Isolamento de responsabilidades

### Negativas

* Complexidade operacional
* Comunicação distribuída

---

# ADR-002 - Database Per Service

## Status

Accepted

## Contexto

Compartilhamento de banco gera forte acoplamento entre serviços.

## Decisão

Cada microservice possuirá seu próprio banco de dados.

## Consequências

### Positivas

* Independência
* Escalabilidade
* Evolução isolada

### Negativas

* Consistência eventual
* Duplicação controlada de dados

---

# ADR-003 - PostgreSQL como Banco Principal

## Status

Accepted

## Contexto

A maior parte dos domínios possui natureza relacional.

## Decisão

Utilizar PostgreSQL para:

* Auth Service
* Customer Service
* Catalog Service
* Order Service
* Inventory Service

## Consequências

### Positivas

* Open Source
* Confiável
* Excelente suporte no ecossistema .NET

---

# ADR-004 - MongoDB para Eventos e Pagamentos

## Status

Accepted

## Contexto

Pagamentos e auditoria possuem estruturas flexíveis e orientadas a documentos.

## Decisão

Utilizar MongoDB para:

* Payment Service
* Audit Service

## Consequências

### Positivas

* Schema flexível
* Boa aderência a eventos

---

# ADR-005 - Redis para Carrinho

## Status

Accepted

## Contexto

Carrinho exige baixa latência e alta frequência de atualização.

## Decisão

Utilizar Redis.

## Consequências

### Positivas

* Alta performance
* Simplicidade

### Negativas

* Dados temporários

---

# ADR-006 - RabbitMQ como Message Broker

## Status

Accepted

## Contexto

O sistema necessita comunicação assíncrona entre domínios.

Alternativas avaliadas:

* RabbitMQ
* Kafka
* Azure Service Bus

## Decisão

Utilizar RabbitMQ.

## Motivos

* Simplicidade
* Facilidade de configuração local
* Excelente integração com .NET

## Consequências

### Positivas

* Curva de aprendizado menor
* Bom suporte para filas e DLQ

### Negativas

* Menor throughput comparado ao Kafka

---

# ADR-007 - Event Driven Architecture

## Status

Accepted

## Contexto

Chamadas síncronas entre serviços aumentam o acoplamento.

## Decisão

Utilizar eventos como principal mecanismo de integração.

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

Não existe transação distribuída entre:

* Pedido
* Estoque
* Pagamento

## Decisão

Utilizar Saga Pattern.

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

* Choreography
* Orchestration

## Decisão

Utilizar Saga Orquestrada.

## Motivos

* Fluxos mais previsíveis
* Mais fácil para demonstração
* Melhor rastreabilidade

---

# ADR-010 - Outbox Pattern

## Status

Accepted

## Contexto

Falhas entre persistência e publicação de eventos podem gerar inconsistências.

## Decisão

Implementar Outbox Pattern.

## Consequências

### Positivas

* Garantia de entrega
* Maior confiabilidade

### Negativas

* Mais componentes

---

# ADR-011 - CQRS

## Status

Accepted

## Contexto

Leitura e escrita possuem responsabilidades diferentes.

## Decisão

Separar Commands e Queries.

## Consequências

### Positivas

* Organização
* Escalabilidade futura

---

# ADR-012 - Clean Architecture

## Status

Accepted

## Contexto

Necessidade de desacoplamento entre domínio e infraestrutura.

## Decisão

Todos os serviços seguirão Clean Architecture.

## Camadas

* Api
* Application
* Domain
* Infrastructure

---

# ADR-013 - MediatR

## Status

Accepted

## Contexto

Implementação de CQRS.

## Decisão

Utilizar MediatR.

## Consequências

### Positivas

* Commands e Queries organizados
* Baixo acoplamento

---

# ADR-014 - YARP como API Gateway

## Status

Accepted

## Contexto

Necessidade de Gateway integrado ao ecossistema .NET.

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

Necessidade de rastreamento distribuído.

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

Todos os componentes serão containerizados.

---

# ADR-017 - Kubernetes

## Status

Accepted

## Contexto

Demonstrar orquestração moderna.

## Decisão

Utilizar Kubernetes.

---

# ADR-018 - Testes Automatizados

## Status

Accepted

## Contexto

Garantir qualidade do sistema.

## Decisão

Implementar:

* Unit Tests
* Integration Tests
* End-to-End Tests

Cobertura mínima:

80%

---

# ADR-019 - Observabilidade Obrigatória

## Status

Accepted

## Contexto

Sistemas distribuídos exigem monitoramento completo.

## Decisão

Todos os serviços devem expor:

```http
GET /health
GET /metrics
```

Além de:

* Logs
* Traces
* Métricas

---

# ADR-020 - Frontend Separado

## Status

Accepted

## Contexto

Permitir evolução independente da interface.

## Decisão

Frontend desacoplado dos microservices.

Tecnologia:

* Next.js
* React
* TypeScript

## Consequências

### Positivas

* Escalabilidade
* Independência de deploy

---

# ADR-021 - Backend For Frontend (BFF)

## Status

Accepted

## Contexto

Frontend precisaria conhecer múltiplos microservices.

Isso aumentaria:

* Complexidade
* Acoplamento
* Número de requisições

## Decisão

Criar um BFF exclusivo para o frontend.

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

## Consequências

### Positivas

* Menos round trips
* Melhor experiência do usuário
* Menor acoplamento

### Negativas

* Componente adicional

---

# ADR-022 - Next.js

## Status

Accepted

## Contexto

Necessidade de framework moderno para frontend.

Alternativas:

* Angular
* React SPA
* Next.js

## Decisão

Utilizar Next.js.

## Motivos

* SSR
* Server Components
* Excelente DX
* Popularidade de mercado

---

# ADR-023 - TanStack Query

## Status

Accepted

## Contexto

Necessidade de gerenciamento eficiente de dados remotos.

## Decisão

Utilizar TanStack Query.

## Consequências

### Positivas

* Cache automático
* Revalidação
* Redução de chamadas

---

# ADR-024 - Zustand

## Status

Accepted

## Contexto

Necessidade de gerenciamento simples de estado local.

Alternativas:

* Redux
* MobX
* Zustand

## Decisão

Utilizar Zustand.

## Motivos

* Simplicidade
* Pouco boilerplate
* Fácil integração com React

---

# ADR-025 - API First

## Status

Accepted

## Contexto

Frontend e Backend evoluirão de forma independente.

## Decisão

Todos os contratos devem ser definidos antes da implementação.

Ferramenta:

* OpenAPI
* Swagger

---

# ADR-026 - Objetivo do Projeto

## Status

Accepted

## Contexto

Projeto destinado a portfólio e entrevistas técnicas.

## Decisão

Priorizar:

* Clareza arquitetural
* Boas práticas
* Documentação
* Demonstração de conceitos modernos

Mesmo que isso aumente a complexidade em relação a um e-commerce simples.

## Resultado Esperado

O projeto deverá demonstrar capacidade de atuação como:

* Desenvolvedor Backend Senior
* Desenvolvedor Full Stack Senior
* Software Engineer
* Tech Lead
* Software Architect
