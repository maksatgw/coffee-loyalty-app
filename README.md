# Coffee Loyalty App ☕🎉

**Coffee Loyalty App**, küçük kahve dükkanları için dijital sadakat programı sunan bir RESTful API projesidir.  
Müşteriler QR kodlarıyla takip edilir, “6 kahve alana 1 bedava” kampanyası otomatik işlenir.

---

## 🔢 Sürüm

> Current Version: **v0.9.0**

Temel API iş mantığı tamamlandı. Oturum yönetimi, kullanıcı girişi ve Flutter uygulaması gibi geliştirmeler için v1.0.0’a doğru ilerlenmektedir.


## 🚀 Özellikler

- **Kullanıcı Yönetimi**  
  - Müşteri CRUD (ekleme, güncelleme, silme, listeleme)  
  - Her müşteriye otomatik üretilen benzersiz QR kod  
- **Ürün Menüsü**  
  - Kahve çeşitlerini listeleme, ekleme, güncelleme, silme  
  - Fiyat ve açıklama bilgileri  
- **Satın Alma & Sadakat**  
  - QR ile hızlı satış (barista cihazından okut–kaydet akışı)  
  - “6 kahve → 7. bedava” mantığı  
  - Satış geçmişini görüntüleme  
- **Teknik**  
  - ASP.NET Core Web API + EF Core (Code First)  
  - SQL Server (optionally In‑Memory/SQLite for tests)  
  - Repository Pattern & DTO katmanı  
  - Swagger ile otomatik API dokümantasyonu  
  - Seed data ile demo müşteri ve ürünler  

---

## 🛠️ Teknoloji & Mimari

- **Backend:** .NET 6+ (ASP.NET Core Web API)  
- **ORM:** Entity Framework Core (Code First migrations)  
- **Veritabanı:** SQL Server (localdb)  
- **Yapı:** Repository Pattern, DTO’lar, Dependency Injection  
- **Test:** In‑Memory/SQLite DB ile kolay birim test desteği  
- **Dokümantasyon:** Swagger (Swashbuckle)  

---

## 📥 Kurulum & Çalıştırma

1. Repository’yi klonla  
   ```bash
   git clone https://github.com/<kullaniciAdin>/coffee-loyalty-app.git
   cd coffee-loyalty-app
2. Gerekli NuGet paketlerini yükle
   ```bash
   dotnet restore
3. Veritabanını oluştur ve seed et
   ```bash
   dotnet ef database update

