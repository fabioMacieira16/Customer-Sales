# ApiDDD

Este é um projeto de API usando arquitetura DDD (Domain-Driven Design) com .NET 6.0 e SQLite.

## Pré-requisitos

- Docker Desktop
- Docker Compose

## Como executar o projeto

1. Clone o repositório
2. Abra o terminal na pasta do projeto
3. Execute o comando:
```bash
docker-compose up --build
```

A aplicação estará disponível em:
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger

O banco de dados SQLite será criado automaticamente na pasta `Data` e será populado com dados de exemplo.

## Estrutura do Projeto

- **ApiDDD.Api**: Camada de apresentação (Controllers, DTOs)
- **ApiDDD.Application**: Camada de aplicação (Casos de uso, Serviços)
- **ApiDDD.Domain**: Camada de domínio (Entidades, Regras de negócio)
- **ApiDDD.Data**: Camada de dados (Repositórios, Contexto do SQLite)

## Tecnologias Utilizadas

- .NET 6.0
- SQLite
- Entity Framework Core
- Docker
- Docker Compose
- Swagger 