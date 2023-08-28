# BiblioTech

Este projeto é uma aplicação de back end desenvolvida em C# que oferece uma API RESTful para gerenciar as operações de uma biblioteca. Ele incorpora várias tecnologias e práticas de desenvolvimento modernas, incluindo Injeção de Dependência, JWT, Unit of Work, AutoMapper, Dapper, REST e Swagger.

## Rotas da API

### Auth

- **POST /api/Auth/Login**: Rota para autenticação de usuários.

### Book

- **POST /api/Books/CreateBook**: Cria um novo livro.
- **PUT /api/Books/UpdateBook**: Atualiza informações de um livro existente.
- **DELETE /api/Books/DeleteBook**: Remove um livro da biblioteca.
- **GET /api/Books/GetBookByFilter**: Obtém informações de livros com base em filtros.

### Loan

- **POST /api/Loan/CreateLoan**: Registra um novo empréstimo de livro.
- **PUT /api/Loan/RegisterReturnBook**: Registra a devolução de um livro emprestado.
- **GET /api/Loan/ListLoansByFilter**: Lista empréstimos de acordo com filtros.

### Reserve

- **POST /api/Reserve/CreateReserve**: Cria uma reserva para um livro.
- **PUT /api/Reserve/CloseReserveBook**: Fecha uma reserva após o livro ser disponibilizado.
- **GET /api/Reserve/ListReservesByFilter**: Lista reservas de acordo com filtros.

### UserClient

- **GET /api/UserClient/GetUserByFilter**: Obtém informações de usuários clientes com base em filtros.
- **POST /api/UserClient/RegisterUserClient**: Registra um novo usuário cliente.
- **PUT /api/UserClient/UpdateUserClient**: Atualiza informações de um usuário cliente.
- **DELETE /api/UserClient/DeleteUserClient**: Remove um usuário cliente.
- **GET /api/UserClient/GetBooksLoanUserClient**: Obtém os livros emprestados por um usuário cliente.
- **GET /api/UserClient/GetReserveUserClient**: Obtém as reservas de um usuário cliente.

### UserEmployee

- **GET /api/UserEmployee/ListEmployeeByFilter**: Lista informações de funcionários com base em filtros.
- **POST /api/UserEmployee/RegisterUserEmployee**: Registra um novo funcionário.
- **PUT /api/UserEmployee/UpdateUserEmployee**: Atualiza informações de um funcionário.
- **DELETE /api/UserEmployee/DeleteUserEmployee**: Remove um funcionário.
- **GET /api/UserEmployee/ListLoanEmployee**: Lista os empréstimos gerenciados por um funcionário.
- **GET /api/UserEmployee/ListReserveEmployee**: Lista as reservas gerenciadas por um funcionário.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **JWT (JSON Web Tokens)**: Autenticação segura para a API.
- **Unit of Work**: Padrão para gerenciamento de transações e operações no banco de dados.
- **AutoMapper**: Mapeamento entre objetos de diferentes tipos.
- **Dapper**: Mapeamento eficiente entre objetos e banco de dados.
- **Swagger**: Documentação interativa da API.

## Configuração e Execução

1. Clone este repositório.
2. Configure as conexões com o banco de dados e outras configurações relevantes.
3. Execute o projeto.
