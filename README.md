# Emlak Yönetim Sistemi

## Proje Açıklaması
Bu proje, emlak portföylerinin yönetimi için geliştirilmiş web tabanlı bir .NET uygulamasıdır. Sistem, emlak ilanlarının admin tarafından yönetimi, kullanıcıların emlak ilanlarını görüntülemesi, favori listesi oluşturması ve emlak değişikliklerini takip etmesi gibi özellikleri içermektedir.

## Özellikler

### Kimlik Doğrulama (Authentication)
- İki farklı kullanıcı tipi (Admin/Kullanıcı)
- Güvenli giriş sistemi
- Oturum yönetimi
- Şifre sıfırlama özelliği

### Admin Paneli
- Emlak portföyü oluşturma
- Emlak bilgilerini güncelleme
- Emlak ilanı silme
- Kullanıcı yorumlarını yönetme
- Sistem ayarları
- Raporlama araçları

### Kullanıcı Özellikleri
- Emlak ilanlarını görüntüleme
- Favori listesi oluşturma
- İlanlara yorum yapma
- Emlak değişiklik bildirimleri alma
- Favori ilanları kaydetme

### Bildirim Sistemi
- Email üzerinden değişiklik bildirimleri
- Favori emlaklar için otomatik bilgilendirme
- Yorum onay bildirimleri

## Teknik Detaylar

### Kullanılan Teknolojiler
- .NET Core 8.0
- Entity Framework Core
- Mongo DB
- HTML5/CSS3
- JavaScript/React
- Bootstrap
- SMTP Email Servisi

### Veritabanı Şeması
- Users (Kullanıcılar)
- Properties (Emlaklar)
- Categories (Kategoriler)
- Images (Görseller)
- Favorites (Favoriler)
- Comments (Yorumlar)
- Notifications (Bildirimler)

## Kurulum
1. Projeyi klonlayın
2. Veritabanını oluşturun
3. Connection string'i güncelleyin
4. Email servis ayarlarını yapılandırın
5. Migrations'ları çalıştırın
6. Projeyi başlatın

## Proje Durumu
- [x] Proje başlangıcı
- [x] README oluşturulması
- [] Veritabanı tasarımı
- [] Authentication sistemi
- [] Admin paneli
- [] Kullanıcı paneli
- [] Emlak portföy yönetimi
- [] Favori listesi sistemi
- [] Yorum sistemi
- [] Email bildirim sistemi
- [] Arama ve filtreleme
- [] Raporlama sistemi
- [] Uygulama içi mesajlaşma sistemi

## Katkıda Bulunma
1. Fork yapın
2. Feature branch oluşturun
3. Değişikliklerinizi commit edin
4. Branch'inizi push edin
5. Pull request oluşturun