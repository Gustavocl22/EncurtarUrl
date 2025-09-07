# 🔗 Encurtador de URLs

🔗 Acesse o encurtador online: [https://encurtador-front-eta.vercel.app](https://encurtador-front-eta.vercel.app)

Projeto de encurtador de URLs desenvolvido em ASP.NET Core, com Entity Framework Core e banco de dados PostgreSQL hospedado no Render.

## 🚀 Tecnologias Utilizadas
- ASP.NET Core
- Entity Framework Core
- PostgreSQL (Render)
- Docker (opcional)

## 📦 Como rodar localmente

### Pré-requisitos
- .NET 8 SDK
- PostgreSQL (ou use o banco do Render)

### Passos
1. Clone o repositório:
   ```sh
   git clone https://github.com/seu-usuario/EncurtarUrl.git
   cd EncurtarUrl
   ```
2. Configure o arquivo `.env` com as credenciais do banco:
   ```env
   DB_NAME=seu_banco
   DB_USER=seu_usuario
   DB_PASSWORD=sua_senha
   DB_SERVER=host.render.com
   DB_PORT=1234
   ```
3. Restaure os pacotes:
   ```sh
   dotnet restore
   ```
4. Rode as migrations:
   ```sh
   dotnet ef database update
   ```
5. Inicie a aplicação:
   ```sh
   dotnet run
   ```

## 🌐 Deploy no Render
- Suba o código para o GitHub.
- Crie um novo serviço Web no Render e conecte ao repositório.
- Configure as variáveis de ambiente no painel do Render (iguais ao `.env`).
- O Render irá gerar uma URL pública para sua API.


## 🔒 Segurança
- As credenciais do banco ficam no `.env` (não suba para o GitHub).
- O arquivo `.gitignore` já está configurado para proteger `.env`, `appsettings.json` e outros arquivos sensíveis, evitando que informações privadas sejam enviadas para o GitHub.
- O CORS está configurado para aceitar apenas o domínio do frontend.

## 📚 Principais Rotas da API
- `POST /api/urlshortener` — Encurta uma URL
- `GET /api/urlshortener/{id}` — Recupera a URL original

## 📝 Observações
- O projeto está pronto para deploy em produção.
- Para dúvidas ou sugestões, abra uma issue ou entre em contato.

---

Desenvolvido por [Gustavo Catucci](https://github.com/Gustavocl22)
