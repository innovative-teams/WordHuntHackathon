# Word Hunt Hackathon Project
<b>bu proje Word Hunt Hackathon için oluşturulmuştur.Backend ve Frontend olarak ikiye ayrılmaktadır</b>
 #### Default Kullanıcı Bilgileri

> email : user@user.com <br/>
password : User@123 

#### Projenin Demo Linki
> Demo Link : https://wordhunthackathon.netlify.app/
## Backend
<b>c# Web API ile katmanli mimari olarak tasarlandı</b>
- #### Sürümler
  - Angular CLI 12.0.2
  - .Net 5.0
  - .Net Standart 2.1
<b>
   Sürümlerle ilgili ayrıntıyı bilgiye projelerin içinde bulunan `.csproj` dosyalarından ve <link href="" blank="email">umit9551@gmail.com</link>


<br/>

## BackEnd için Yapılması Gerekenler
- ### Veritabanı Etkinleştirme

  Tek Yapmanız gereken Nuget Developer Console'u açıp default project'i DataAccess Seçtikten Sonra `Update-Database` yazmanız yeterli olacaktır(SQLServer).
  Veritabanı ile ilgili bağlantı özellikleri(servername,databasename)
  `WebAPI/appsettings.json` içindeki 21.satırdan düzeltebilirsiniz.

  <br>

- ### Projeyi Çalıştırma

  Klasör içinde bulunan `.sln` uzantılı dosya açılmalıdır daha sonra `WebAPI` başlangıç projesi
  olarak işaretlenmeli ve proje build edildikten sonra çalıştırılmalıdır. Eğer önünüzde kullanıcılar varsa tebrikler
  artık API çalışıyor. Açılan sekmedeki port adresini backend'in çalıştığı porttur.
<br/>

## Frontend
- ### Angular Projesi için Gerekli Modülleri Kurma

  Angular dosya dizini içinde yeni bir terminal oluşturup `npm install` komutunu yazmanız yeterlidir Angular proje için gerekli paketleri kuracaktır.

  <br>

  - ### Angular Projesini Çalıştırma

  Kurulum işlemi bittikten sonra yapmanız gereken terminale `ng serve --open --port 4200` komutunu yazmak olacaktır. Eğer bu port meşgulse veya çalışmıyorsa port adresini değiştirmelisiniz. Port adresini değiştirdiğinizde `WebAPI/Startup.cs` içindeki

  `app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());`

  kısmını düzenlemeniz gerekir sadece url değiştirmeniz yeterli olacaktır bu işlemden sonra WebAPI'yi durdurup tekrar çalıştırmanız gerekecektir.

---

<br>

## Emeği Geçenler

Ümit Doğan:<link href="umit9551@gmail.com" blank="email">umit9551@gmail.com</link> <br/>
İsmail Kaygısız:<link href="kaygisizismail24@gmail.com" blank="email">kaygisizismail24@gmail.com</link>
