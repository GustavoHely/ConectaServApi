# ConectaServ API

API RESTful desenvolvida em ASP.NET Core para a plataforma ConectaServ.

## Funcionalidades atuais

- Cadastro e listagem de usuários
- Banco de dados SQLite com Entity Framework Core
- Preparado para migração para PostgreSQL
- Swagger configurado para testes locais

## Como rodar localmente

1. Instale os pacotes NuGet necessários:
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.Sqlite
   - Microsoft.EntityFrameworkCore.Tools

2. Execute as migrations no terminal do Visual Studio:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```

3. Execute o projeto (`F5`) e acesse o Swagger:
   ```
   https://localhost:5001/swagger
   ```

## Futuro

- Autenticação JWT
- Deploy na Railway
- Integração com app mobile e frontend responsivo