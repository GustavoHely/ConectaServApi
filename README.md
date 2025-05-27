# ConectaServ API

API RESTful desenvolvida em ASP.NET Core para a plataforma **ConectaServ**, que conecta prestadores de serviço com CNPJ a clientes. A API é responsável por autenticação de usuários, cadastro de prestadores e empresas, gerenciamento de endereços, contatos, clientes, entre outros recursos.

## 🚀 Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- MySQL (Railway)
- Swagger (documentação e testes)

---

## 📦 Funcionalidades Implementadas

### ✅ Autenticação (JWT)
- Registro (`/api/Auth/registrar`)
- Login (`/api/Auth/login`)

### ✅ Usuários
- Cadastro de usuário com senha criptografada (BCrypt)
- Login com geração de token JWT

### ✅ Endereços
- Cadastro de endereço
- Listagem de endereços

### ✅ Clientes
- Cadastro de cliente associado a um usuário e endereço

### ✅ Prestadores
- Cadastro de prestador associado a um usuário

### ✅ Empresas
- Cadastro de empresa com nome e razão social

### ✅ Contato da Empresa
- Cadastro de telefones (fixo e WhatsApp) e e-mail por empresa

---

## 🔐 Proteção por Token JWT

Rotas como `/Cliente/cadastrar`, `/Prestador/cadastrar` e `/Empresa/cadastrar` exigem **token JWT** no cabeçalho:

```
Authorization: Bearer <seu_token_aqui>
```

---

## 📁 Estrutura de Pastas

```
ConectaServApi/
│
├── Controllers/       # Rotas da API
├── DTOs/              # Objetos de transferência de dados
├── Models/            # Entidades do banco de dados
├── Data/              # DbContext e configurações
├── Services/          # Lógica de token JWT
├── Program.cs         # Configuração da API
└── appsettings.json   # String de conexão e chave JWT
```

---

## 🧪 Testando com Swagger

1. Execute a aplicação (F5 ou `dotnet run`)
2. Acesse: `http://localhost:5255/swagger`
3. Use o endpoint `/Auth/login` para gerar um token
4. Clique em “Authorize” (ícone de cadeado) e cole o token com `Bearer`

---

## ⚙️ Como rodar o projeto

1. Clone o repositório:

```
git clone https://github.com/GustavoHely/ConectaServApi.git
cd ConectaServApi/ConectaServApi
```

2. Configure a string de conexão no `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Port=...;Database=railway;User=root;Password=..."
}
```

3. Execute os comandos:

```
dotnet restore
dotnet build
dotnet ef database update
dotnet run
```

---

## 🛠️ Desenvolvedor

- Gustavo Hely
- API desenvolvida com suporte do ChatGPT
- Última atualização: 27/05/2025
