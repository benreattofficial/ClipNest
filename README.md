# ClipNest

**A lightweight, local-first clipboard history manager for Windows.** Search, pin, and reuse copied text from one clean desktop app.

## Features

- Automatically captures plain text while ClipNest is running; monitoring can be paused at any time.
- Search the local history, copy an item again, pin important entries, or remove items individually.
- Settings are organized into General, Language, Appearance, and Advanced tabs.
- Interface languages: English, Turkish, French, Spanish, Russian, Simplified Chinese, and German. System language can be selected as the default.
- Light, dark, and Windows system themes, plus six accent colors.
- Advanced controls: launch with Windows, start minimized at sign-in, enable monitoring on app launch, choose a history limit, choose the polling interval, and confirm before bulk clearing.
- A Windows `.ico` app icon is used by the app and installer.
- Per-user Windows installer: no administrator prompt required. The installer adds a Start menu shortcut and offers an optional desktop shortcut. Uninstalling preserves user settings and history.

## Privacy

ClipNest stores its settings and text history only on this Windows user profile under `%LOCALAPPDATA%\ClipNest`. It does not use a cloud account, network service, or synchronization. Passwords and other sensitive text may be captured like any other copied text; pause monitoring before copying sensitive content. Automatic secret detection is not included.

History is limited by the Advanced setting (default: 300 items). Individual text entries over 25,000 characters are ignored. Only plain text is captured; images and files are not.

## Install

Download and run `ClipNest-Setup-1.0.0-x64.exe` from the release package. The installer is per-user and does not require administrator rights.
This build is not code-signed. Depending on Windows SmartScreen or local Application Control policy, Windows may warn about or block the installer. A trusted code-signing certificate is required to establish publisher trust.

## Build from source

Requirements: Windows 10/11 and the .NET 8 SDK with Windows Desktop targeting support.

```powershell
dotnet build .\ClipNest.csproj -c Release
dotnet publish .\ClipNest.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o ..\ClipNest-win-x64
```

To compile the installer, install Inno Setup 6 and run:

```powershell
ISCC.exe .\ClipNest.iss
```

The installer script writes its output to `..\ClipNest-Setup`.

## Local development

```powershell
dotnet run --project .\ClipNest.csproj
```

## License

GPL-3.0. See [LICENSE](LICENSE).

---

## Türkçe

ClipNest, Windows için yerel çalışan bir pano geçmişi yöneticisidir. Kopyalanan metinlerde arama yapabilir, öğeleri sabitleyip tekrar kopyalayabilir veya silebilirsiniz. İzleme istediğiniz zaman duraklatılabilir.

Ayarlar; **Genel**, **Dil**, **Görünüm** ve **Gelişmiş** sekmelerinden oluşur. İngilizce, Türkçe, Fransızca, İspanyolca, Rusça, Basitleştirilmiş Çince ve Almanca dilleri; açık/koyu/sistem temaları ve altı vurgu rengi desteklenir. Gelişmiş seçeneklerde Windows ile başlatma, simge durumunda açılma, uygulama açılışında pano izleme, geçmiş sınırı, kontrol aralığı ve toplu silme onayı bulunur.

Kurulum paketi yönetici yetkisi istemeyen kullanıcı kapsamlı bir Windows yükleyicisidir; Başlat menüsüne kısayol ekler ve masaüstü kısayolunu isteğe bağlı sunar. Kaldırma işlemi `%LOCALAPPDATA%\ClipNest` içindeki kişisel ayar ve geçmişi silmez.
Lisans: GPL-3.0. Ayrıntılar için [LICENSE](LICENSE) dosyasına bakın.
Bu derleme kod imzası taşımıyor. Windows SmartScreen veya yerel Uygulama Denetimi politikasına bağlı olarak uyarı gösterilebilir ya da çalıştırma engellenebilir; yayımlayıcı güveni için güvenilir bir kod imzalama sertifikası gerekir.

Veriler yalnızca bu bilgisayarda saklanır; bulut eşitlemesi veya ağ servisi yoktur. Parola gibi hassas metinleri panoya kopyalamadan önce izlemeyi duraklatın. İlk sürüm yalnızca düz metni kaydeder; resim ve dosyaları almaz.
