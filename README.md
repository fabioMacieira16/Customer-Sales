# Sales API

API de vendas desenvolvida com .NET 8, seguindo os princípios do Domain-Driven Design (DDD) e Clean Architecture.

## 🚀 Tecnologias

- .NET 8
- MongoDB
- Docker
- Swagger/OpenAPI
- xUnit (Testes)
- FluentAssertions
- Moq

## 📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) ou [Docker](https://www.docker.com/products/docker-desktop)
- [MongoDB Compass](https://www.mongodb.com/try/download/compass) (opcional, para visualização dos dados)

## 🛠️ Instalação

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/ApiDDD.git
cd ApiDDD
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Configure o MongoDB:
   - Instale o MongoDB localmente ou use Docker:
   ```bash
   docker run -d -p 27017:27017 --name mongodb mongo:latest
   ```

4. Configure as variáveis de ambiente:
   - Crie um arquivo `appsettings.Development.json` na pasta `ApiDDD.Api` com:
   ```json
   {
     "MongoDB": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "SalesDB",
       "CollectionName": "Sales"
     }
   }
   ```

## 🚀 Executando a API

1. Execute a API:
```bash
cd ApiDDD.Api
dotnet run
```

2. Acesse a documentação Swagger:
```
https://localhost:7001/swagger
```

## 🧪 Executando os Testes

```bash
dotnet test
```

## 🐳 Executando com Docker

1. Construa a imagem:
```bash
docker build -t sales-api .
```

2. Execute o container:
```bash
docker run -d -p 7001:80 --name sales-api sales-api
```

## 📚 Estrutura do Projeto

```
ApiDDD/
├── ApiDDD.Api/              # API e Controllers
├── ApiDDD.Application/      # Serviços e DTOs
├── ApiDDD.Domain/           # Entidades e Regras de Negócio
├── ApiDDD.Data/             # Repositórios e Configurações
└── ApiDDD.Tests/            # Testes Unitários
```

## 📝 Regras de Negócio

### Descontos por Quantidade
- Menos de 4 itens: Sem desconto
- 4-9 itens: 10% de desconto
- 10-20 itens: 20% de desconto

### Limites de Quantidade
- Máximo de 20 unidades por produto
- Máximo de 20 unidades por venda

## 🔍 Endpoints

### Vendas
- `GET /api/sales` - Lista todas as vendas
- `GET /api/sales/{id}` - Obtém uma venda específica
- `POST /api/sales` - Cria uma nova venda
- `POST /api/sales/{id}/items` - Adiciona item à venda
- `DELETE /api/sales/{id}` - Cancela uma venda
- `DELETE /api/sales/{id}/items/{productId}` - Remove item da venda

## 📊 Visualizando os Dados

1. Abra o MongoDB Compass
2. Conecte usando a URL: `mongodb://localhost:27017`
3. Selecione o banco de dados `SalesDB`
4. Acesse a coleção `Sales`

## 🤝 Contribuindo

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes. 