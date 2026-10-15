bash
#!/bin/bash
# entrypoint.sh

echo "Aguardando o PostgreSQL iniciar..."
until pg_isready -h postgres_db -p 5432 -U postgres; do
  sleep 2
done

echo "PostgreSQL está pronto! Executando as migrações do Entity Framework..."
dotnet ef database update --project Orders.Infra/Orders.Infra.csproj --startup-project Orders.API/Orders.API.csproj

echo "Migrações concluídas. Iniciando a aplicação..."
dotnet Orders.API.dll