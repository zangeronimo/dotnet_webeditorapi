# ===== CONFIG =====
CONTEXT=PlatformDbContext
INFRA=WEBEditorAPI.Infrastructure
API=WEBEditorAPI.Api
MIGRATION?=NewMigration

# ===== COMMANDS =====

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

## Rollback (volta 1 migration)
migrate-down:
	@echo "⚠️  Informe a migration anterior:"
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

## Help
help:
	@echo "===== MIGRATIONS ====="
	@echo "make migrate-add MIGRATION=Nome -> Cria nova migration"
	@echo "make migrate-up -> Aplica migrations pendentes no banco"
	@echo "make migrate-down MIGRATION=NomeAnterior -> Rollback para uma migration anterior"
	@echo "make migrate-remove -> Remove última migration do código"