# EShop — Yol Haritası ve Karar Kaydı

> Bu belge **yaşayan bir dokümandır**. Projenin vizyonunu, mimari kararlarını (ve *neden* o kararı verdiğimizi), faz planlarını ve ilerleme günlüğünü tutar. Her adımı tamamladıkça **İlerleme Günlüğü** bölümüne tarihli kayıt eklenir.

---

## 1. Vizyon ve Amaç

EShop iki amaca aynı anda hizmet eder:

1. **Eğitim projesi** — Clean Architecture'ı, modern .NET tekniklerini ve kurumsal bir WebAPI'nin tüm teknik altyapısını *anlayarak* kurmak. Her karar gerekçesiyle açıklanır.
2. **Production-ready template** — Tüm fazlar bittiğinde, yeni projelere iskelet olabilecek, "kur ve hemen yazmaya başla" diyebileceğin kurumsal kalitede bir şablon.

Geliştirme stratejisi: **Önce boş iskelet → üzerine e-ticaret eti (referans uygulama) → en sonunda şablonun çıkarımı.** E-ticaret domeni, mimarideki tüm yapıları kullanmaya yetecek kadar (ne eksik ne fazla) örnek sağlar.

**Çalışma prensibi:** Tüm kodu öğrenci elle yazar; rehberlik, "neden" açıklamaları ve kod incelemesi yapılır. Production aşamasında son kullanıcıya mümkün olduğunca az boilerplate yazdırılır (source generator'lar, base sınıflar, convention'lar).

---

## 2. Mimari Özet

Clean Architecture — dört katman, bağımlılıklar **içe doğru** akar; Domain hiçbir şeye bağımlı değildir.

```
Api ──────► Application ──────► Domain
 │                                ▲
 └────────► Infrastructure ───────┘
```

| Katman | Sorumluluk | Bağımlılık |
|---|---|---|
| **Domain** | İş kalbi: entity'ler, value object'ler, domain event'ler, iş kuralları | Hiçbir şey (framework bile yok) |
| **Application** | Use-case'ler, CQRS, soyutlamalar (arayüzler), validation | Yalnızca Domain |
| **Infrastructure** | "Nasıl": EF Core, dış servisler, arayüzlerin implementasyonu | Application (→ Domain) |
| **Api** | Composition root, endpoint'ler, middleware, dış dünyaya açılan kapı | Application + Infrastructure (yalnızca DI kaydı için) |

**Not:** Api → Infrastructure bağımlılığı yalnızca *composition root* içindir (DI'a implementasyon kaydetmek). Api'nin iş kodu daima Application'daki arayüzler üzerinden konuşur.

---

## 3. Teknoloji Kararları ve Gerekçeleri

Bu projedeki paket seçimleri **lisans** ve **modernlik** ölçütleriyle yapılmıştır. 2024–2025 döneminde birçok popüler .NET kütüphanesi ticari lisansa geçtiği için, bir *template* projesinde lisans temizliği kritiktir.

| Konu | Karar | Gerekçe |
|---|---|---|
| **Mediator** | Önce elle yaz → sonra `martinothamar/Mediator` (MIT) | MediatR 2 Temmuz 2025'te ticarileşti. Önce deseni elle yazmak öğretici; sonra MIT, source-generator tabanlı, MediatR-uyumlu API'ye geçiş. |
| **Object Mapping** | `Mapperly` (Apache 2.0) | AutoMapper da 2 Temmuz 2025'te ticarileşti. Mapperly source-generator: sıfır reflection, derleme-zamanı güvenliği, üretilen kod okunabilir. |
| **Validation** | `FluentValidation` (Apache 2.0) | Ticarileşme dalgasına kapılmadı, ücretsiz kaldı. |
| **Test — Assertion** | `Shouldly` (MIT) | FluentAssertions v8 ticari lisansa geçti. Shouldly ücretsiz, aktif, temiz hata mesajları. |
| **Test — Mock** | `NSubstitute` (MIT) | Moq'un geçmişteki SponsorLink tartışması nedeniyle tercih edilmedi. |
| **Test — Framework** | `xUnit` | En yaygın, en çok kaynaklı, öğrenmeye uygun. |
| **Test — Integration** | `Testcontainers` | Gerçek PostgreSQL'i Docker'da ayağa kaldırarak test. Docker + cross-OS temasına ve Faz 2'ye köprü. |
| **OpenAPI** | Yerleşik `Microsoft.AspNetCore.OpenApi` + `Scalar` (UI) | Swashbuckle eski usul. .NET 10 yerleşik doküman üretimi + Scalar modern yol. |
| **API Versiyonlama** | `Asp.Versioning.*` (MIT) | Standart, ücretsiz. |
| **Auth** | ASP.NET Core Identity + JwtBearer | Microsoft'un kendi çözümü, ücretsiz, production-standard. |
| **ORM** | EF Core 10 | — |
| **Hata Yönetimi** | Yerleşik `IExceptionHandler` + `ProblemDetails` | Ek paket yok, framework veriyor. |
| **Result tipi** | Elle yazılacak | Eğitim için; ek bağımlılık yok. |
| **Paket sürüm yönetimi** | Central Package Management (CPM) | Çok-projeli template'te sürüm tutarlılığı. |

---

## 4. Veritabanı Stratejisi

- **Geliştirme:** PostgreSQL (Docker üzerinde). Cross-OS (Windows/Ubuntu) sorunsuz, Faz 2'nin Docker temasına uyumlu, production'a yakın davranış.
- **Template varsayılanı:** SQL Server. Kurumsal .NET dünyasında en yaygın varsayılan.
- **Opsiyonel sağlayıcılar:** PostgreSQL / SQL Server / SQLite — EF Core provider modeliyle config üzerinden seçilebilir.

> **Açık karar (sonra işlenecek):** EF Core migration'ları sağlayıcıya özeldir. "DB opsiyonel" demek provider-swap demektir. Sağlayıcıya-özel kodu (ör. `jsonb`, sequence davranışları) izole tutacağız; template çıkarımında SQL Server migration'ları yeniden üretilecek. Strateji Infrastructure katmanında EF kurulurken netleştirilecek (muhtemelen `dotnet new` parametresiyle sağlayıcı seçimi).

---

## 5. Faz 1 — WebAPI İskeleti (Checklist)

- [ ] Solution iskeleti + proje referans grafiği
- [ ] `Directory.Build.props`, `Directory.Packages.props` (CPM), `.editorconfig`, `.gitignore`
- [ ] `ROADMAP.md` + `README.md` (ilk hâl)
- [ ] Domain building block'ları: `Entity`, `AggregateRoot`, `ValueObject`, `IDomainEvent`, `Result`/`Error`
- [ ] Application soyutlamaları: mediator (elle), pipeline behavior altyapısı, `IUnitOfWork`, repository arayüzleri
- [ ] Validation: FluentValidation + validation pipeline behavior
- [ ] Mapping: Mapperly kurulumu
- [ ] Infrastructure: `DbContext`, EF interceptor'ları (audit, domain event dispatch), repository implementasyonları
- [ ] Authentication / Authorization: Identity + JWT
- [ ] API: global exception handler + ProblemDetails, OpenAPI + Scalar, versiyonlama, health checks
- [ ] Composition root: `AddApplication()` / `AddInfrastructure()`
- [ ] Mediator paketine geçiş (elle → martinothamar/Mediator)
- [ ] Test altyapısı: xUnit + Shouldly + NSubstitute + Testcontainers

---

## 6. Faz 2 — Altyapı Entegrasyonları (Checklist)

Her teknoloji önce *tanınacak* (ne işe yarar, nasıl çalışır), sonra entegre edilip test edilecek. Tümü Docker ile.

- [ ] **Redis** — distributed cache
- [ ] **Serilog** — structured logging
- [ ] **RabbitMQ** — message broker *(MassTransit v9 ticari → muhtemelen ham `RabbitMQ.Client`)*
- [ ] **Elasticsearch** — arama / log aggregation
- [ ] **MinIO** — object storage (S3 uyumlu)
- [ ] **Hangfire** — background jobs / scheduling

---

## 7. Açık Kararlar / Sonraya Bırakılanlar

- Provider-swap stratejisinin kesin biçimi (migration organizasyonu).
- Faz 2 RabbitMQ: MassTransit v9 lisansı teyit edilip ham client kararı kesinleştirilecek.
- Template çıkarım mekanizması: `dotnet new` template'i (`template.json`), namespace parametrizasyonu, e-ticaret örneğinin opsiyonel parametre olması.

---

## 8. İlerleme Günlüğü

> Append-only. En yeni kayıt en üstte.

### (tarih) — Adım 1: Solution iskeleti
- Solution + 4 ana proje (Domain/Application/Infrastructure/Api) + 3 test projesi oluşturuldu.
- Bağımlılık grafiği bağlandı; Domain bilinçli olarak referanssız bırakıldı.
- `Directory.Build.props` (ortak MSBuild ayarları), `Directory.Packages.props` (CPM), `.editorconfig`, `.gitignore` eklendi.
- `ROADMAP.md` ve `README.md` ilk hâlleriyle oluşturuldu.