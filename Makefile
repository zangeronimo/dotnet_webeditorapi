# ===== CONFIG =====

CONTEXT=PlatformDbContext
INFRA=WEBEditorAPI.Infrastructure
API=WEBEditorAPI.Api
MIGRATION?=NewMigration

DOCKER_COMPOSE=docker compose

# ===== MIGRATIONS =====

## Criar nova migration
migrate-add:
	dotnet ef migrations add $(MIGRATION) \
	--context $(CONTEXT) \
	--project $(INFRA) \
	--startup-project $(API)

## Aplicar migrations pendentes (up)
migrate-up:
	dotnet ef database update \
	--context $(CONTEXT) \
	--project $(INFRA) \
	--startup-project $(API)

## Rollback (volta para migration específica)
migrate-down:
	@echo "⚠️ Informe a migration anterior:"
	@echo "Ex: make migrate-down MIGRATION=NomeAnterior"
	dotnet ef database update $(MIGRATION) \
	--context $(CONTEXT) \
	--project $(INFRA) \
	--startup-project $(API)

## Remover última migration (código)
migrate-remove:
	dotnet ef migrations remove \
	--context $(CONTEXT) \
	--project $(INFRA) \
	--startup-project $(API)

# ===== DOCKER =====

## Subir containers
docker-up:
	$(DOCKER_COMPOSE) up -d

## Subir com rebuild
docker-build:
	$(DOCKER_COMPOSE) up -d --build

## Derrubar containers
docker-down:
	$(DOCKER_COMPOSE) down

## Ver logs
docker-logs:
	$(DOCKER_COMPOSE) logs -f

## Reiniciar containers
docker-restart:
	$(DOCKER_COMPOSE) restart

## Ver status
docker-ps:
	$(DOCKER_COMPOSE) ps

# ===== DEVELOPMENT =====

## Rodar API local
run-api:
	dotnet run --project $(API)

## Rodar testes
test:
	dotnet test

## Restore
restore:
	dotnet restore

## Build
build:
	dotnet build

# ===== HELP =====

help:
	@echo ""
	@echo "===== MIGRATIONS ====="
	@echo "make migrate-add MIGRATION=Nome -> Cria migration"
	@echo "make migrate-up -> Aplica migrations"
	@echo "make migrate-down MIGRATION=Nome -> Rollback"
	@echo "make migrate-remove -> Remove última migration"
	@echo ""
	@echo "===== DOCKER ====="
	@echo "make docker-up -> Sobe containers"
	@echo "make docker-build -> Rebuilda containers"
	@echo "make docker-down -> Derruba containers"
	@echo "make docker-logs -> Logs containers"
	@echo "make docker-restart -> Reinicia containers"
	@echo "make docker-ps -> Lista containers"
	@echo ""
	@echo "===== DEVELOPMENT ====="
	@echo "make run-api -> Roda API local"
	@echo "make test -> Executa testes"
	@echo "make restore -> Restore pacotes"
	@echo "make build -> Build solução"