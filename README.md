# PurchaseOrder – Documentación técnica (Clean Architecture / CQRS)

## 🧭 Visión general

Sistema de **Órdenes de Compra** construido sobre **.NET** siguiendo **Clean Architecture** y el patrón **CQRS**. El objetivo es mantener una separación clara de responsabilidades, prueba‑bilidad y facilidad de evolución.

Estructura típica de proyectos:

```
PurchaseOrder.sln
├─ PurchaseOrder.Domain/          ← Núcleo del negocio (entidades, value objects, reglas)
├─ PurchaseOrder.Application/     ← Casos de uso (CQRS con MediatR), DTOs, validaciones, Endpoints/Controllers, DI (DependecyInjection)
├─ PurchaseOrder.Infrastructure/  ← Implementaciones técnicas (EF Core/Dapper, repos, UoW)
```

---

## 🏗️ Capas en detalle

### 1) Domain (Dominio)

**Propósito:** modelar el negocio sin dependencias de infraestructura.

**Contiene:**

* **Entities** (p. ej. `Customer`, `PurchaseOrder`/`PuchaseOrder`, `PurchaseOrderItem`, `Product`).
* **Value Objects** (p. ej. `Money`, `OrderNumber`, `Email`).
* **Enumerations** (estados del pedido, tipos, etc.).
* **Domain Events** (opcional) para propagar cambios significativos.
* **Reglas invariante** y **lógica de negocio pura** (métodos en entidades/servicios de dominio).

**No contiene:** acceso a datos, HTTP, frameworks externos. Sólo .NET base.

**Beneficios:** portabilidad y test unitarios muy rápidos.

---

### 2) Application (Aplicación)

**Propósito:** orquestar **casos de uso** (commands/queries), coordinando dominio e infraestructura a través de **abstracciones**.

**Contiene:**

* **CQRS con MediatR**:

  * *Commands* (crear/actualizar/eliminar)
  * *Queries* (lecturas y listados)
* **Handlers** (`IRequestHandler<,>`) que llaman repositorios/UoW.
* **DTOs / VMs** para entrada/salida.
* **Validaciones** (FluentValidation) antes de tocar el dominio.
* **Mappers** (AutoMapper) entre DTOs y entidades.
* **Interfaces** (p. ej. `IPurchaseOrderRepository`, `IPurchaseOrderUnitOfWork`) consumidas por handlers.
* **Controllers** (endpoints REST).
* **Wiring** de DI: registrar repos/UoW, MediatR, AutoMapper, validators, DbContext, etc.
* **Configuración** (appsettings, env vars).

**No contiene:** EF Core ni implementaciones concretas.

**Beneficios:** reemplazo fácil de infraestructura, casos de uso testeables con dobles (mocks/fakes).

---

### 3) Infrastructure (Infraestructura)

**Propósito:** implementaciones **técnicas** de las interfaces de Application.

**Contiene:**

* **Persistencia** (EF Core o Dapper): `DbContext`, *Configurations*, *Migrations*.
* **Repositorios** que implementan `IPurchaseOrderRepository` & cía.
* **Unit of Work** (`IPurchaseOrderUnitOfWork`) para transacciones/`SaveChanges`.
* **Servicios externos** (SMTP, colas, archivos, cache) si aplican.
* **Implementación de logging** (Serilog/Microsoft.Extensions.Logging).

**No contiene:** lógica de negocio.

**Beneficios:** todo lo “acoplado a tecnología” queda encapsulado.

---

## 🔁 Flujo típico (Create Purchase Order)

1. **HTTP POST** llega a `POST /api/purchase-orders` con `CreatePurchaseOrderCommand`.
2. **Model binding + validación** (FluentValidation): reglas de requeridos, rangos, negocio liviano.
3. **MediatR** envía el command a `CreatePurchaseOrderCommandHandler`.
4. El **handler** mapea DTO → Entidades (AutoMapper) y usa **repositorios** del **UoW**.
5. **Dominio** valida invariantes (p. ej., totales, estados) y agrega items.
6. **UoW.SaveChangesAsync()** confirma la transacción.
7. Retorna **DTO/VM** con el resultado.

---

## 📦 Dependencias y por qué se usan

> Ajusta según tu `*.csproj`.

* **MediatR**: implementa **CQRS** con **mediación** desacoplando controladores de handlers. Facilita *pipeline behaviors* (logging, validación, transacciones).
* **FluentValidation**: validaciones declarativas y testeables en Application antes de tocar el dominio.
* **AutoMapper**: mapeos consistentes entre DTOs/VMs y entidades; reduce código repetitivo.
* **EF Core** *(si aplica)*: ORM para acceso a datos, migrations y LINQ.
* **Swashbuckle/Swagger** *(API)*: documentación y pruebas interactivas de endpoints.
* **Microsoft.Extensions.* (DI, Logging, Options)**: infraestructura base de .NET moderna.

---

## 🧪 Pruebas

* **Domain**: tests de invariantes y reglas puras.
* **Application**: tests de handlers con repositorios/UoW simulados (mocks).
* **Infrastructure**: tests de integración (con DB local o contenedores) para repos y mappings EF.
* **API**: tests de endpoint (WebApplicationFactory) y Contratos (Swagger / snapshots).

---

## ⚙️ Configuración

* `appsettings.json` / variables de entorno para cadenas de conexión y *feature flags*.
* Perfiles: `Development`, `Staging`, `Production`.
* Conexión DB (ejemplo): `ConnectionStrings:Default`.

---

## ▶️ Cómo ejecutar local

1. **Restaurar paquetes**

   ```bash
   dotnet restore
   ```
2. **Aplicar migraciones** (si usas EF Core)

   ```bash
   dotnet ef database update -s PurchaseOrder.API -p PurchaseOrder.Infrastructure
   ```
   (si usas terminal Nuget)
   ```Nuget
   Add-Migration FirstMigration -Context PurchaseOrderContext
   Update-Database -Context PurchaseOrderContext
   ```
4. **Levantar API**

   ```bash
   dotnet run --project PurchaseOrder.API
   ```
5. Navega a `https://localhost:{puerto}/swagger`.

---

## 🧩 Convenciones y buenas prácticas

* **Nombres claros**
* **Inmutabilidad** en Value Objects.
* **Respuestas estándar**: `GenericResponse` con `Success`, `Message`, `Errors` y `Data`.

---

## 🗂️ Estructura sugerida de carpetas (resumen)

**Domain**

```
Entities/
ValueObjects/
Enums/
Events/
Exceptions/
```

**Application**

```
Common/Behaviors/            # Validación, Logging, Performance
Contracts/Persistence/       # Interfaces (Repos, UoW)
Features/PurchaseOrder/
  Commands/Create/
  Commands/Update/
  Commands/Delete/
  Queries/GetById/
  Queries/List/
Mappings/                    # Profiles AutoMapper
Validators/                  # FluentValidation
```

**Infrastructure**

```
Persistence/
  DbContext/
  Configurations/
  Migrations/
Repositories/
Services/
```

---

## 🧷 Puntos de extensión

* **Caching** de queries (por ejemplo Memory/Redis) tras el handler de lectura.
* **Outbox Pattern** para integración eventual con otros sistemas.
* **Ids robustos** (GUID/Ulid) y *concurrency tokens* en EF Core.

---

## 📚 Glosario

* **CQRS**: separar escritura (commands) y lectura (queries).
* **UoW**: unidad de trabajo, coordina transacción y persistencia.
* **Repository**: patrón para encapsular acceso a datos por agregado.
* **DTO/VM**: contratos de datos para transporte y presentación.

---

## ✍️ Créditos

Autor: Jose Ayala (PurchaseOrder)

---

