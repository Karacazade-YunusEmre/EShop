# EShop

.NET 10 üzerine kurulu, **Clean Architecture** prensiplerini izleyen, production-ready bir WebAPI **template** projesi. Yeni projelere temiz bir başlangıç iskeleti sağlamak için tasarlanmıştır.

> 🚧 Bu belge proje geliştikçe büyür. Aşağıda *(iskelet ilerledikçe doldurulacak)* olarak işaretli bölümler, ilgili kod ortaya çıktıkça tamamlanacaktır.

---

## İçindekiler

- [Bu Proje Nedir?](#bu-proje-nedir)
- [Mimari Genel Bakış](#mimari-genel-bakış)
- [Gereksinimler](#gereksinimler)
- [Hızlı Başlangıç](#hızlı-başlangıç)
- [Proje Yapısı](#proje-yapısı)
- [Yeni Feature Nasıl Eklenir?](#yeni-feature-nasıl-eklenir)
- [Konfigürasyon](#konfigürasyon)
- [Testleri Çalıştırma](#testleri-çalıştırma)
- [Template Olarak Kullanım](#template-olarak-kullanım)

---

## Bu Proje Nedir?

EShop, kurumsal bir .NET WebAPI'sinin ihtiyaç duyduğu tüm teknik altyapıyı (katmanlı mimari, kimlik doğrulama/yetkilendirme, validation, hata yönetimi, API versiyonlama, OpenAPI dokümantasyonu, test altyapısı) hazır sunan bir başlangıç şablonudur. Amaç: iş mantığına odaklanabilmen için tekrar eden altyapı kodunu en aza indirmek.

**Öne çıkanlar:**
- Clean Architecture — net katman ayrımı, içe doğru bağımlılık
- Modern, lisans-temiz paket seçimi (ticari lisans gerektiren kütüphanelerden kaçınılmıştır)
- Source-generator tabanlı mediator ve mapping (sıfır reflection)
- Çoklu veritabanı sağlayıcı desteği (PostgreSQL / SQL Server / SQLite)

---

## Mimari Genel Bakış

```
Api ──────► Application ──────► Domain
 │                                ▲
 └────────► Infrastructure ───────┘
```

| Katman | Sorumluluk |
|---|---|
| **Domain** | İş kuralları, entity'ler, value object'ler. Hiçbir dış bağımlılığı yoktur. |
| **Application** | Use-case'ler (CQRS), soyutlamalar, validation. Yalnızca Domain'e bağımlıdır. |
| **Infrastructure** | Veritabanı erişimi (EF Core), dış servisler — Application arayüzlerinin implementasyonu. |
| **Api** | HTTP endpoint'leri, middleware, composition root. |

Altın kural: **Bağımlılıklar içe doğru akar, Domain hiçbir şeye bağımlı değildir.**

---

## Gereksinimler

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (PostgreSQL ve Faz 2 servisleri için)
- Bir IDE / editör (Visual Studio, Rider veya VS Code)

---

## Hızlı Başlangıç

*(iskelet ilerledikçe doldurulacak — clone → veritabanını Docker ile ayağa kaldır → migration → çalıştır → ilk istek adımları buraya gelecek)*

```bash
# Örnek (henüz tamamlanmadı):
git clone <repo-url>
cd EShop
# docker compose up -d
# dotnet run --project src/EShop.Api
```

---

## Proje Yapısı

```
EShop/
├─ src/
│  ├─ EShop.Domain/          # İş kalbi: entity, value object, domain event, iş kuralları
│  ├─ EShop.Application/     # Use-case'ler, CQRS, soyutlamalar, validation
│  ├─ EShop.Infrastructure/  # EF Core, dış servisler, arayüz implementasyonları
│  └─ EShop.Api/             # Endpoint'ler, middleware, composition root
└─ tests/
   ├─ EShop.Domain.UnitTests/
   ├─ EShop.Application.UnitTests/
   └─ EShop.Api.IntegrationTests/
```

*(her klasörün iç yapısı — BuildingBlocks, Features vb. — iskelet ilerledikçe detaylandırılacak)*

---

## Yeni Feature Nasıl Eklenir?

*(iskelet ilerledikçe doldurulacak)*

Hedeflenen akış: yeni bir özellik eklemek için yalnızca **command/query + handler + validator + endpoint** yazman yeterli olacak; geri kalan altyapı (mapping, validation, transaction, hata yönetimi) otomatik devreye girecek.

---

## Konfigürasyon

*(iskelet ilerledikçe doldurulacak)*

- **Veritabanı seçimi:** PostgreSQL / SQL Server / SQLite (connection string + sağlayıcı ayarı)
- **JWT ayarları:** issuer, audience, signing key
- **Diğer:** Faz 2 servis ayarları (Redis, RabbitMQ, vb.)

---

## Testleri Çalıştırma

```bash
dotnet test
```

Test yığını: **xUnit** (framework) · **Shouldly** (assertion) · **NSubstitute** (mock) · **Testcontainers** (entegrasyon testlerinde gerçek veritabanı).

---

## Template Olarak Kullanım

*(iskelet ilerledikçe doldurulacak — `dotnet new` template'i, proje adı/namespace parametrizasyonu)*