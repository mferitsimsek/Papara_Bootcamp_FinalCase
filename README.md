# Papara Bootcamp Final Projesi

Bu proje, Papara Bootcamp final projesi kapsamında geliştirilmiş, dijital ürün satışı yapan bir e-ticaret platformu örneğidir. Temiz mimari (Clean Architecture) prensiplerine göre tasarlanmıştır ve Microservices yaklaşımıyla iki ayrı API servisine bölünmüştür:

- **Papara.CaptainStore.API**: Ana API servisi, ürün yönetimi, kategori yönetimi, kullanıcı yönetimi, sipariş yönetimi ve diğer temel e-ticaret özelliklerini içerir.
- **Papara.CaptainStore.PaymentAPI**: Ödeme işlemlerini yöneten API servisi.

## Kullanılan Teknolojiler

### Backend
- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **MediatR (CQRS)**
- **FluentValidation**
- **AutoMapper**
- **Serilog**
- **RabbitMQ (Mesaj Kuyruğu)**
- **Redis (Caching)**
- **Elasticsearch (Arama)**
- **Kibana (Arama Görselleştirme)**
- **Seq (Loglama)**

### Frontend
- (Henüz geliştirilmedi. İsteğe bağlı olarak Angular, React veya Vue.js kullanılabilir.)

## Mimari

Proje, Temiz Mimari (Clean Architecture) prensiplerine göre tasarlanmıştır:
- **Domain**: İş mantığı ve kurallarını içerir.
- **Application**: Use case'leri ve iş mantığını uygular.
- **Infrastructure**: Veritabanı erişimi, dış servis entegrasyonları gibi altyapısal işlemleri gerçekleştirir.
- **Presentation**: API servislerini ve kullanıcı arayüzünü (henüz geliştirilmedi) içerir.

## Projeyi Çalıştırma

### Gereksinimler
- **.NET 8 SDK**
- **SQL Server**
- **Docker** (önerilir)

### Veritabanı Kurulumu
1. `Infrastructure/Papara.CaptainStore.Persistence` projesindeki `Migrations` klasöründeki migration'ları kullanarak veritabanı şemasını oluşturun.
2. Diğer bi seçenek olarak Db_Backup klasöründeki backup dosyasını Sql Server Management Studio üzerinden Restore edebilirsiniz.

### Docker ile Çalıştırma (önerilir)
Docker klasöründeki `docker-compose.yaml` dosyasını kullanarak Docker konteynerlarını başlatın:
```bash
docker-compose up -d
