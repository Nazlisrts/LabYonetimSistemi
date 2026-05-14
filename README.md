# 🧪 LRP - Laboratuvar Rezervasyon Portalı

Üniversite laboratuvarlarındaki bilgisayar envanterini yönetmek, öğrencilere zimmet atamak ve öğrenci portalı üzerinden zimmetli cihaz bilgilerini görüntülemek için geliştirilmiş bir web uygulamasıdır.

---

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Teknolojiler](#-teknolojiler)
- [Proje Yapısı](#-proje-yapısı)
- [Kurulum](#-kurulum)
- [Çalıştırma](#-çalıştırma)
- [Kullanım](#-kullanım)
- [API Endpointleri](#-api-endpointleri)
- [Veritabanı Modelleri](#-veritabanı-modelleri)
- [Ekran Görüntüleri](#-ekran-görüntüleri)

---

## 📖 Proje Hakkında

**LRP (Laboratuvar Rezervasyon Portalı)**, üniversite laboratuvar ortamlarında bilgisayar donanımlarının takibini ve öğrencilere zimmet atama sürecini kolaylaştırmak amacıyla geliştirilmiştir.

### Temel Özellikler

| Özellik | Açıklama |
|---------|----------|
| 🔐 **Kimlik Doğrulama** | Kullanıcı girişi ve kayıt sistemi (Admin / Öğrenci rolleri) |
| 🏫 **Laboratuvar Yönetimi** | Laboratuvar ekleme ve düzenleme |
| 💻 **Bilgisayar Yönetimi** | PC ekleme, düzenleme ve donanım özelliklerini takip etme |
| 👨‍🎓 **Zimmet Atama** | Öğrencilere bilgisayar zimmetleme ve otomatik hesap oluşturma |
| 📊 **Öğrenci Portalı** | Öğrencilerin zimmetli bilgisayar bilgilerini görüntülemesi |
| 📝 **Otomatik Demirbaş Kodu** | Her bilgisayara otomatik `LABx-PC-xx` formatında kod atanması |

---

## 🛠 Teknolojiler

### Backend
| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| ASP.NET Core | 8.0 | Minimal API mimarisi |
| Entity Framework Core | 8.0 | ORM (Veritabanı yönetimi) |
| SQLite | - | Gömülü veritabanı |
| Swagger / OpenAPI | - | API dokümantasyonu (Geliştirme ortamı) |

### Frontend
| Teknoloji | Versiyon | Açıklama |
|-----------|----------|----------|
| Bootstrap | 5.3.0 | CSS framework (Responsive tasarım) |
| FontAwesome | 6.0.0 | İkon kütüphanesi |
| Vanilla JavaScript | ES6+ | Fetch API ile AJAX istekleri |
| Google Fonts (Inter) | - | Modern tipografi |

> ⚠️ **Not:** Framework (React, Vue, Angular vb.) kullanılmamıştır. Tüm frontend işlemleri saf JavaScript ile gerçekleştirilmiştir.

---

## 📁 Proje Yapısı

```
LabYonetimSistemi/
├── Data/
│   └── AppDbContext.cs          # EF Core veritabanı bağlamı
├── Models/
│   ├── Computer.cs              # Bilgisayar modeli
│   ├── Lab.cs                   # Laboratuvar modeli
│   ├── Student.cs               # Öğrenci modeli
│   ├── User.cs                  # Kullanıcı modeli
│   ├── Software.cs              # Yazılım modeli
│   └── Issue.cs                 # Arıza/Sorun modeli
├── Migrations/                  # EF Core migration dosyaları
├── Properties/
│   └── launchSettings.json      # Uygulama başlatma ayarları
├── wwwroot/
│   ├── css/
│   │   └── style.css            # Özel CSS tasarım dosyası
│   ├── JS/
│   │   ├── admin.js             # Admin paneli JavaScript işlemleri
│   │   └── auth.js              # Kimlik doğrulama yardımcı fonksiyonları
│   ├── index.html               # Giriş / Kayıt sayfası
│   ├── admin.html               # Admin paneli
│   └── student.html             # Öğrenci portalı
├── Program.cs                   # Uygulama giriş noktası ve API tanımları
├── appsettings.json             # Uygulama yapılandırması
├── LabyonetimSistemi.csproj     # .NET proje dosyası
├── LabyonetimSistemi.sln        # Solution dosyası
├── laboratuvar.db               # SQLite veritabanı dosyası
└── README.md                    # Bu dosya
```

---

## ⚙️ Kurulum

### Ön Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) veya üzeri
- Herhangi bir modern web tarayıcısı (Chrome, Firefox, Edge vb.)

### Adımlar

1. **Projeyi klonlayın:**
   ```bash
   git clone https://github.com/kullanici-adi/LabYonetimSistemi.git
   cd LabYonetimSistemi
   ```

2. **Bağımlılıkları yükleyin:**
   ```bash
   dotnet restore
   ```

3. **Veritabanını oluşturun:**
   
   Uygulama ilk çalıştırıldığında SQLite veritabanı (`laboratuvar.db`) otomatik olarak oluşturulur. Ayrıca varsayılan admin kullanıcısı da otomatik olarak eklenir.

---

## 🚀 Çalıştırma

```bash
dotnet run
```

Uygulama varsayılan olarak aşağıdaki adreste çalışır:

```
http://localhost:5141
```

> 💡 Geliştirme ortamında Swagger UI'a `http://localhost:5141/swagger` adresinden erişebilirsiniz.

---

## 📖 Kullanım

### 🔑 Varsayılan Admin Girişi

| Alan | Değer |
|------|-------|
| Kullanıcı Adı | `admin` |
| Şifre | `admin123` |

### Admin Paneli İşlemleri

1. **Laboratuvar Yönetimi**
   - Sol menüden "Laboratuvarlar" sekmesine tıklayın
   - "＋ Yeni Lab" butonuyla yeni laboratuvar ekleyin
   - Mevcut laboratuvarları "Düzenle" butonuyla güncelleyin

2. **Bilgisayar Yönetimi**
   - "Bilgisayarlar" sekmesinden PC ekleyin veya düzenleyin
   - Her PC için marka, işlemci, RAM, HDMI, internet ve Veyon bilgilerini girin
   - Demirbaş kodu (`LABx-PC-xx`) otomatik olarak atanır

3. **Zimmet Atama**
   - "Zimmet Ata" sekmesinden öğrenciye bilgisayar atayın
   - Öğrenci adı, numarası, sınıfı ve atanacak PC'yi seçin
   - Zimmet ataması yapıldığında öğrenci hesabı **otomatik** olarak oluşturulur
   - İlk giriş bilgileri: `Kullanıcı Adı = Öğrenci No`, `Şifre = Öğrenci No`

### Öğrenci Portalı

- Öğrenci numarası ve şifresiyle giriş yapın
- Zimmetli bilgisayarın tüm donanım detaylarını görüntüleyin

---

## 🌐 API Endpointleri

### Kimlik Doğrulama

| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| `POST` | `/api/login` | Kullanıcı girişi |
| `POST` | `/api/register` | Yeni kullanıcı kaydı |

### Laboratuvarlar

| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/api/labs` | Tüm laboratuvarları listele |
| `POST` | `/api/admin/labs` | Yeni laboratuvar ekle |
| `PUT` | `/api/admin/labs/{id}` | Laboratuvar güncelle |

### Bilgisayarlar

| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/api/computers` | Tüm bilgisayarları listele |
| `POST` | `/api/admin/computers` | Yeni bilgisayar ekle |
| `PUT` | `/api/admin/computers/{id}` | Bilgisayar güncelle |

### Öğrenciler / Zimmet

| Metod | Endpoint | Açıklama |
|-------|----------|----------|
| `GET` | `/api/admin/students` | Tüm öğrencileri listele |
| `POST` | `/api/admin/assign` | Öğrenciye zimmet ata ve hesap oluştur |
| `GET` | `/api/student/my-pc/{username}` | Öğrencinin zimmetli PC bilgisi |

---

## 🗃 Veritabanı Modelleri

### User (Kullanıcı)
| Alan | Tip | Açıklama |
|------|-----|----------|
| Id | int | Birincil anahtar |
| Username | string | Kullanıcı adı |
| Password | string | Şifre |
| Role | string | Rol (`Admin` veya `Student`) |

### Lab (Laboratuvar)
| Alan | Tip | Açıklama |
|------|-----|----------|
| Id | int | Birincil anahtar |
| Name | string | Laboratuvar adı (Örn: `Lab-A`) |
| Computers | List\<Computer\> | İlişkili bilgisayarlar |

### Computer (Bilgisayar)
| Alan | Tip | Açıklama |
|------|-----|----------|
| Id | int | Birincil anahtar |
| AssetCode | string | Otomatik demirbaş kodu (`LABx-PC-xx`) |
| Brand | string | Marka |
| Processor | string | İşlemci |
| Ram | int | RAM miktarı (GB) |
| HasHdmi | bool | HDMI çıkışı var mı |
| HasInternet | bool | İnternet bağlantısı var mı |
| HasVeyon | bool | Veyon yazılımı yüklü mü |
| LabId | int | Bağlı olduğu laboratuvar (FK) |

### Student (Öğrenci)
| Alan | Tip | Açıklama |
|------|-----|----------|
| Id | int | Birincil anahtar |
| FullName | string | Ad Soyad |
| StudentNumber | string | Öğrenci numarası |
| Grade | int | Sınıf |
| ComputerId | int | Zimmetli bilgisayar (FK) |
| UserId | int? | Otomatik oluşturulan kullanıcı hesabı (FK) |

---

## 🖼 Ekran Görüntüleri

### Giriş Sayfası
- Modern gradient arka plan
- İkonlu input alanları
- Giriş ve Kayıt formları arası geçiş

### Admin Paneli
- Gradient sidebar navigasyonu
- Laboratuvar, Bilgisayar ve Zimmet yönetim bölümleri
- Renkli durum badge'leri (Var/Yok)
- İkonlu aksiyon butonları

### Öğrenci Portalı
- Zimmetli bilgisayar detay kartı
- Donanım özelliklerinin ikonlu gösterimi

---

## 👨‍💻 Geliştirici Notları

- Uygulama **Minimal API** mimarisi kullanmaktadır (Controller tabanlı değil).
- Veritabanı olarak **SQLite** tercih edilmiştir; ek kurulum gerektirmez.
- İlk çalıştırmada admin hesabı (`admin / admin123`) otomatik olarak seed edilir.
- Frontend tarafında herhangi bir JavaScript framework'ü kullanılmamıştır.
- Tüm API istekleri **Fetch API** ile gerçekleştirilmektedir.

---

## 📄 Lisans

Bu proje eğitim amaçlıdır.
