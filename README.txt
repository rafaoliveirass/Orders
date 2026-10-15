Sobre o que utilizei para desenvolver a aplicação:
- ORM EF core
- FluentValidation
- Arquitetura modular DDD (Domain-Driven Design) com (API, application, Domain, Infra)


Passos para rodar o docker compose

- Precisa que o docker engine esteja instalado na máquina.
- Ir na raiz do projeto e abrir o terminal.
- executar o comando "docker-compose up"
- após isso irá subir o banco de dados e a aplicação.
- acesse com o caminho http://localhost:8080/swagger/index.html

Dados para testar 

- login do token (mocado sem tabela)
  - user: admin
  - password: admin

  json para o post do token
{
  "username": "admin",
  "password": "admin"
}

- Usuario banco de dados:
  - user: postgres
  - password: admin
  - database: orders

JSON para inclusão de um pedido, com os Ids que estão cadastrados no banco gerado pela migration
{
  "customerId": "557bc0cb-a88d-44cf-a072-1efdaed024c9",
  "currency": "R$",
  "itens": [
    {
      "productId": "d4f7fecb-aa31-4789-8f14-ee6f72f8b539",
      "quantity": 2
    },
    {
	"productId": "f4b3e8c1-9a72-41e5-82b3-6c19f0a2d48f",
	"quantity": 1
    }
  ]
}