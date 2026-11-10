using System.Globalization;
using System.Windows;

namespace ClipNest;

public static class LanguageManager
{
    public static readonly (string Key, string Name)[] Languages =
    [
        ("system", "System default"), ("en", "English"), ("tr", "Türkçe"),
        ("fr", "Français"), ("es", "Español"), ("ru", "Русский"),
        ("zh-CN", "简体中文"), ("de", "Deutsch")
    ];

    private static readonly Dictionary<string, Dictionary<string, string>> Texts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["en"] = new()
        {
            ["Subtitle"] = "Your copied text, organized and ready when you need it.", ["SensitiveHint"] = "Pause monitoring before copying sensitive information.",
            ["MonitoringOn"] = "Monitoring", ["MonitoringPaused"] = "Paused", ["Search"] = "Search clipboard history",
            ["Pause"] = "Pause", ["Resume"] = "Resume", ["History"] = "HISTORY", ["Items"] = "items", ["PinnedCount"] = "pinned",
            ["ClearUnpinned"] = "Clear unpinned", ["Copy"] = "Copy", ["Pin"] = "Pin", ["Unpin"] = "Unpin", ["Delete"] = "Delete",
            ["Ready"] = "Ready", ["NewEntry"] = "New text captured.", ["Copied"] = "Text copied to clipboard.", ["ClipboardError"] = "Clipboard is temporarily unavailable.",
            ["Settings"] = "Settings", ["SettingsTitle"] = "ClipNest settings", ["TabGeneral"] = "General", ["TabLanguage"] = "Language",
            ["TabAppearance"] = "Appearance", ["TabAdvanced"] = "Advanced", ["GeneralDescription"] = "Choose how ClipNest behaves and stores your history.",
            ["SaveHistory"] = "Save clipboard history on this device", ["SaveHistoryDescription"] = "History stays in your Windows user profile; it is never synced.",
            ["StorageLocation"] = "Local data folder", ["LanguageTitle"] = "App language", ["LanguageDescription"] = "Choose the language used throughout ClipNest.",
            ["ThemeTitle"] = "Theme", ["ThemeSystem"] = "System", ["ThemeLight"] = "Light", ["ThemeDark"] = "Dark", ["AccentTitle"] = "Accent color",
            ["AccentDescription"] = "Choose a highlight color for buttons and controls.", ["StartupTitle"] = "Start with Windows", ["StartupDescription"] = "Open ClipNest automatically when you sign in.",
            ["MinimizedTitle"] = "Start minimized", ["MinimizedDescription"] = "When launched at sign-in, start minimized to the taskbar.",
            ["MonitorTitle"] = "Monitor clipboard when the app opens", ["MonitorDescription"] = "Collect text copied while ClipNest is running.",
            ["MaxHistory"] = "Maximum history items", ["PollInterval"] = "Clipboard check interval", ["Milliseconds"] = "milliseconds",
            ["ConfirmClear"] = "Ask before clearing unpinned items", ["Save"] = "Save settings", ["Cancel"] = "Cancel", ["Saved"] = "Settings saved.",
            ["EmptyHistory"] = "No clipboard items yet. Copy some text to get started.", ["ConfirmClearTitle"] = "Clear history?", ["ConfirmClearMessage"] = "Remove all unpinned items? Pinned items will be kept.",
            ["PinnedMarker"] = "PINNED", ["SettingsSaved"] = "Your settings have been saved.", ["OpenFolder"] = "Open data folder",
            ["LocalOnly"] = "Stored locally on this device", ["SaveError"] = "Could not save. Check folder permissions and try again.",
            ["StartupError"] = "Could not update Windows startup settings.", ["HistoryReadError"] = "Previous history could not be read; a new history will be started."
        },
        ["tr"] = new()
        {
            ["Subtitle"] = "Kopyaladığın metinler düzenli ve ihtiyaç duyduğunda hazır.", ["SensitiveHint"] = "Hassas içerik kopyalamadan önce izlemeyi duraklat.",
            ["MonitoringOn"] = "Pano izleniyor", ["MonitoringPaused"] = "Duraklatıldı", ["Search"] = "Pano geçmişinde ara",
            ["Pause"] = "Duraklat", ["Resume"] = "Başlat", ["History"] = "GEÇMİŞ", ["Items"] = "öğe", ["PinnedCount"] = "sabitli",
            ["ClearUnpinned"] = "Sabitsizleri temizle", ["Copy"] = "Kopyala", ["Pin"] = "Sabitle", ["Unpin"] = "Sabitlemeyi kaldır", ["Delete"] = "Sil",
            ["Ready"] = "Hazır", ["NewEntry"] = "Panodan yeni metin eklendi.", ["Copied"] = "Metin panoya kopyalandı.", ["ClipboardError"] = "Panoya geçici olarak erişilemiyor.",
            ["Settings"] = "Ayarlar", ["SettingsTitle"] = "ClipNest ayarları", ["TabGeneral"] = "Genel", ["TabLanguage"] = "Dil",
            ["TabAppearance"] = "Görünüm", ["TabAdvanced"] = "Gelişmiş", ["GeneralDescription"] = "ClipNest’in çalışma ve geçmişi saklama şeklini seç.",
            ["SaveHistory"] = "Pano geçmişini bu cihazda sakla", ["SaveHistoryDescription"] = "Geçmiş Windows kullanıcı profilinde kalır; eşitlenmez.",
            ["StorageLocation"] = "Yerel veri klasörü", ["LanguageTitle"] = "Uygulama dili", ["LanguageDescription"] = "ClipNest genelinde kullanılacak dili seç.",
            ["ThemeTitle"] = "Tema", ["ThemeSystem"] = "Sistem", ["ThemeLight"] = "Açık", ["ThemeDark"] = "Koyu", ["AccentTitle"] = "Vurgu rengi",
            ["AccentDescription"] = "Düğmeler ve denetimler için vurgu rengini seç.", ["StartupTitle"] = "Windows ile başlat", ["StartupDescription"] = "Oturum açtığında ClipNest otomatik açılsın.",
            ["MinimizedTitle"] = "Simge durumunda başlat", ["MinimizedDescription"] = "Oturum açılışında görev çubuğunda simge durumunda başlat.",
            ["MonitorTitle"] = "Uygulama açıldığında panoyu izle", ["MonitorDescription"] = "ClipNest çalışırken kopyalanan metinleri kaydet.",
            ["MaxHistory"] = "Maksimum geçmiş öğesi", ["PollInterval"] = "Pano kontrol aralığı", ["Milliseconds"] = "milisaniye",
            ["ConfirmClear"] = "Sabitsiz öğeleri temizlemeden önce sor", ["Save"] = "Ayarları kaydet", ["Cancel"] = "İptal", ["Saved"] = "Ayarlar kaydedildi.",
            ["EmptyHistory"] = "Henüz pano öğesi yok. Başlamak için bir metin kopyala.", ["ConfirmClearTitle"] = "Geçmiş temizlensin mi?", ["ConfirmClearMessage"] = "Sabitsiz tüm öğeler silinsin mi? Sabitlenenler korunur.",
            ["PinnedMarker"] = "SABİTLİ", ["SettingsSaved"] = "Ayarların kaydedildi.", ["OpenFolder"] = "Veri klasörünü aç"
        },
        ["fr"] = new()
        {
            ["Subtitle"] = "Vos textes copiés, organisés et prêts à être réutilisés.", ["SensitiveHint"] = "Mettez la surveillance en pause avant de copier des données sensibles.",
            ["MonitoringOn"] = "Surveillance active", ["MonitoringPaused"] = "En pause", ["Search"] = "Rechercher dans l’historique",
            ["Pause"] = "Mettre en pause", ["Resume"] = "Reprendre", ["History"] = "HISTORIQUE", ["Items"] = "éléments", ["PinnedCount"] = "épinglés",
            ["ClearUnpinned"] = "Effacer les éléments non épinglés", ["Copy"] = "Copier", ["Pin"] = "Épingler", ["Unpin"] = "Désépingler", ["Delete"] = "Supprimer",
            ["Ready"] = "Prêt", ["NewEntry"] = "Nouveau texte enregistré.", ["Copied"] = "Texte copié dans le presse-papiers.", ["ClipboardError"] = "Presse-papiers temporairement indisponible.",
            ["Settings"] = "Paramètres", ["SettingsTitle"] = "Paramètres de ClipNest", ["TabGeneral"] = "Général", ["TabLanguage"] = "Langue",
            ["TabAppearance"] = "Apparence", ["TabAdvanced"] = "Avancé", ["GeneralDescription"] = "Choisissez le fonctionnement de ClipNest et la conservation de l’historique.",
            ["SaveHistory"] = "Enregistrer l’historique sur cet appareil", ["SaveHistoryDescription"] = "L’historique reste dans votre profil Windows et n’est jamais synchronisé.",
            ["StorageLocation"] = "Dossier de données local", ["LanguageTitle"] = "Langue de l’application", ["LanguageDescription"] = "Choisissez la langue utilisée dans ClipNest.",
            ["ThemeTitle"] = "Thème", ["ThemeSystem"] = "Système", ["ThemeLight"] = "Clair", ["ThemeDark"] = "Sombre", ["AccentTitle"] = "Couleur d’accentuation",
            ["AccentDescription"] = "Choisissez la couleur des boutons et des contrôles.", ["StartupTitle"] = "Démarrer avec Windows", ["StartupDescription"] = "Ouvrir ClipNest automatiquement à la connexion.",
            ["MinimizedTitle"] = "Démarrer réduit", ["MinimizedDescription"] = "Au démarrage de la session, réduire ClipNest dans la barre des tâches.",
            ["MonitorTitle"] = "Surveiller le presse-papiers à l’ouverture", ["MonitorDescription"] = "Enregistrer les textes copiés lorsque ClipNest est actif.",
            ["MaxHistory"] = "Nombre maximal d’éléments", ["PollInterval"] = "Fréquence de vérification", ["Milliseconds"] = "millisecondes",
            ["ConfirmClear"] = "Demander confirmation avant l’effacement", ["Save"] = "Enregistrer", ["Cancel"] = "Annuler", ["Saved"] = "Paramètres enregistrés.",
            ["EmptyHistory"] = "Aucun élément. Copiez du texte pour commencer.", ["ConfirmClearTitle"] = "Effacer l’historique ?", ["ConfirmClearMessage"] = "Supprimer tous les éléments non épinglés ? Les éléments épinglés seront conservés.",
            ["PinnedMarker"] = "ÉPINGLÉ", ["SettingsSaved"] = "Vos paramètres ont été enregistrés.", ["OpenFolder"] = "Ouvrir le dossier de données"
        },
        ["es"] = new()
        {
            ["Subtitle"] = "Tus textos copiados, organizados y listos cuando los necesites.", ["SensitiveHint"] = "Pausa la supervisión antes de copiar información sensible.",
            ["MonitoringOn"] = "Supervisando", ["MonitoringPaused"] = "En pausa", ["Search"] = "Buscar en el historial",
            ["Pause"] = "Pausar", ["Resume"] = "Reanudar", ["History"] = "HISTORIAL", ["Items"] = "elementos", ["PinnedCount"] = "fijados",
            ["ClearUnpinned"] = "Borrar elementos no fijados", ["Copy"] = "Copiar", ["Pin"] = "Fijar", ["Unpin"] = "Desfijar", ["Delete"] = "Eliminar",
            ["Ready"] = "Listo", ["NewEntry"] = "Nuevo texto guardado.", ["Copied"] = "Texto copiado al portapapeles.", ["ClipboardError"] = "El portapapeles no está disponible temporalmente.",
            ["Settings"] = "Configuración", ["SettingsTitle"] = "Configuración de ClipNest", ["TabGeneral"] = "General", ["TabLanguage"] = "Idioma",
            ["TabAppearance"] = "Apariencia", ["TabAdvanced"] = "Avanzado", ["GeneralDescription"] = "Elige cómo funciona ClipNest y conserva tu historial.",
            ["SaveHistory"] = "Guardar el historial en este dispositivo", ["SaveHistoryDescription"] = "El historial permanece en tu perfil de Windows; nunca se sincroniza.",
            ["StorageLocation"] = "Carpeta de datos local", ["LanguageTitle"] = "Idioma de la aplicación", ["LanguageDescription"] = "Elige el idioma usado en ClipNest.",
            ["ThemeTitle"] = "Tema", ["ThemeSystem"] = "Sistema", ["ThemeLight"] = "Claro", ["ThemeDark"] = "Oscuro", ["AccentTitle"] = "Color de acento",
            ["AccentDescription"] = "Elige el color destacado de botones y controles.", ["StartupTitle"] = "Iniciar con Windows", ["StartupDescription"] = "Abrir ClipNest automáticamente al iniciar sesión.",
            ["MinimizedTitle"] = "Iniciar minimizado", ["MinimizedDescription"] = "Al iniciar sesión, abrir ClipNest minimizado en la barra de tareas.",
            ["MonitorTitle"] = "Supervisar el portapapeles al abrir la aplicación", ["MonitorDescription"] = "Guardar los textos copiados mientras ClipNest está abierto.",
            ["MaxHistory"] = "Máximo de elementos del historial", ["PollInterval"] = "Intervalo de comprobación", ["Milliseconds"] = "milisegundos",
            ["ConfirmClear"] = "Confirmar antes de borrar elementos no fijados", ["Save"] = "Guardar configuración", ["Cancel"] = "Cancelar", ["Saved"] = "Configuración guardada.",
            ["EmptyHistory"] = "Aún no hay elementos. Copia texto para empezar.", ["ConfirmClearTitle"] = "¿Borrar el historial?", ["ConfirmClearMessage"] = "¿Eliminar los elementos no fijados? Los fijados se conservarán.",
            ["PinnedMarker"] = "FIJADO", ["SettingsSaved"] = "Se guardó la configuración.", ["OpenFolder"] = "Abrir carpeta de datos"
        },
        ["ru"] = new()
        {
            ["Subtitle"] = "Скопированный текст всегда под рукой и упорядочен.", ["SensitiveHint"] = "Приостановите сбор перед копированием конфиденциальных данных.",
            ["MonitoringOn"] = "Мониторинг включён", ["MonitoringPaused"] = "Пауза", ["Search"] = "Поиск в истории буфера обмена",
            ["Pause"] = "Пауза", ["Resume"] = "Продолжить", ["History"] = "ИСТОРИЯ", ["Items"] = "элементов", ["PinnedCount"] = "закреплено",
            ["ClearUnpinned"] = "Удалить незакреплённые", ["Copy"] = "Копировать", ["Pin"] = "Закрепить", ["Unpin"] = "Открепить", ["Delete"] = "Удалить",
            ["Ready"] = "Готово", ["NewEntry"] = "Новый текст сохранён.", ["Copied"] = "Текст скопирован в буфер обмена.", ["ClipboardError"] = "Буфер обмена временно недоступен.",
            ["Settings"] = "Настройки", ["SettingsTitle"] = "Настройки ClipNest", ["TabGeneral"] = "Общие", ["TabLanguage"] = "Язык",
            ["TabAppearance"] = "Внешний вид", ["TabAdvanced"] = "Дополнительно", ["GeneralDescription"] = "Настройте работу ClipNest и хранение истории.",
            ["SaveHistory"] = "Хранить историю на этом устройстве", ["SaveHistoryDescription"] = "История хранится в профиле Windows и не синхронизируется.",
            ["StorageLocation"] = "Локальная папка данных", ["LanguageTitle"] = "Язык приложения", ["LanguageDescription"] = "Выберите язык интерфейса ClipNest.",
            ["ThemeTitle"] = "Тема", ["ThemeSystem"] = "Системная", ["ThemeLight"] = "Светлая", ["ThemeDark"] = "Тёмная", ["AccentTitle"] = "Цвет акцента",
            ["AccentDescription"] = "Выберите цвет выделения для кнопок и элементов управления.", ["StartupTitle"] = "Запускать вместе с Windows", ["StartupDescription"] = "Автоматически открывать ClipNest при входе.",
            ["MinimizedTitle"] = "Запускать свёрнутым", ["MinimizedDescription"] = "При входе запускать ClipNest свёрнутым на панели задач.",
            ["MonitorTitle"] = "Следить за буфером при запуске", ["MonitorDescription"] = "Сохранять скопированный текст, пока ClipNest открыт.",
            ["MaxHistory"] = "Максимум элементов истории", ["PollInterval"] = "Интервал проверки буфера", ["Milliseconds"] = "мс",
            ["ConfirmClear"] = "Спрашивать перед очисткой незакреплённых записей", ["Save"] = "Сохранить настройки", ["Cancel"] = "Отмена", ["Saved"] = "Настройки сохранены.",
            ["EmptyHistory"] = "История пуста. Скопируйте текст, чтобы начать.", ["ConfirmClearTitle"] = "Очистить историю?", ["ConfirmClearMessage"] = "Удалить все незакреплённые записи? Закреплённые сохранятся.",
            ["PinnedMarker"] = "ЗАКРЕПЛЕНО", ["SettingsSaved"] = "Настройки сохранены.", ["OpenFolder"] = "Открыть папку данных"
        },
        ["zh-CN"] = new()
        {
            ["Subtitle"] = "复制的文字井然有序，需要时随时可用。", ["SensitiveHint"] = "复制敏感信息前，请先暂停监控。",
            ["MonitoringOn"] = "正在监控", ["MonitoringPaused"] = "已暂停", ["Search"] = "搜索剪贴板历史",
            ["Pause"] = "暂停", ["Resume"] = "继续", ["History"] = "历史记录", ["Items"] = "项", ["PinnedCount"] = "已固定",
            ["ClearUnpinned"] = "清除未固定项", ["Copy"] = "复制", ["Pin"] = "固定", ["Unpin"] = "取消固定", ["Delete"] = "删除",
            ["Ready"] = "就绪", ["NewEntry"] = "已收录新的文本。", ["Copied"] = "文本已复制到剪贴板。", ["ClipboardError"] = "剪贴板暂时不可用。",
            ["Settings"] = "设置", ["SettingsTitle"] = "ClipNest 设置", ["TabGeneral"] = "常规", ["TabLanguage"] = "语言",
            ["TabAppearance"] = "外观", ["TabAdvanced"] = "高级", ["GeneralDescription"] = "选择 ClipNest 的工作方式和历史记录保存方式。",
            ["SaveHistory"] = "在此设备上保存剪贴板历史", ["SaveHistoryDescription"] = "历史记录保存在 Windows 用户配置中，不会同步。",
            ["StorageLocation"] = "本地数据文件夹", ["LanguageTitle"] = "应用语言", ["LanguageDescription"] = "选择 ClipNest 使用的界面语言。",
            ["ThemeTitle"] = "主题", ["ThemeSystem"] = "跟随系统", ["ThemeLight"] = "浅色", ["ThemeDark"] = "深色", ["AccentTitle"] = "强调色",
            ["AccentDescription"] = "选择按钮和控件的强调颜色。", ["StartupTitle"] = "随 Windows 启动", ["StartupDescription"] = "登录 Windows 时自动打开 ClipNest。",
            ["MinimizedTitle"] = "启动时最小化", ["MinimizedDescription"] = "登录时将 ClipNest 最小化到任务栏。",
            ["MonitorTitle"] = "应用打开时监控剪贴板", ["MonitorDescription"] = "ClipNest 运行期间收集复制的文本。",
            ["MaxHistory"] = "历史记录最大数量", ["PollInterval"] = "剪贴板检查间隔", ["Milliseconds"] = "毫秒",
            ["ConfirmClear"] = "清除未固定项前询问", ["Save"] = "保存设置", ["Cancel"] = "取消", ["Saved"] = "设置已保存。",
            ["EmptyHistory"] = "暂无剪贴板记录。复制一些文字即可开始。", ["ConfirmClearTitle"] = "清除历史记录？", ["ConfirmClearMessage"] = "删除所有未固定项吗？已固定项将保留。",
            ["PinnedMarker"] = "已固定", ["SettingsSaved"] = "设置已保存。", ["OpenFolder"] = "打开数据文件夹"
        },
        ["de"] = new()
        {
            ["Subtitle"] = "Kopierte Texte, übersichtlich gespeichert und sofort verfügbar.", ["SensitiveHint"] = "Pausiere die Überwachung, bevor du vertrauliche Inhalte kopierst.",
            ["MonitoringOn"] = "Überwachung aktiv", ["MonitoringPaused"] = "Pausiert", ["Search"] = "Zwischenablageverlauf durchsuchen",
            ["Pause"] = "Pausieren", ["Resume"] = "Fortsetzen", ["History"] = "VERLAUF", ["Items"] = "Einträge", ["PinnedCount"] = "angeheftet",
            ["ClearUnpinned"] = "Nicht angeheftete löschen", ["Copy"] = "Kopieren", ["Pin"] = "Anheften", ["Unpin"] = "Lösen", ["Delete"] = "Löschen",
            ["Ready"] = "Bereit", ["NewEntry"] = "Neuer Text gespeichert.", ["Copied"] = "Text in die Zwischenablage kopiert.", ["ClipboardError"] = "Zwischenablage vorübergehend nicht verfügbar.",
            ["Settings"] = "Einstellungen", ["SettingsTitle"] = "ClipNest-Einstellungen", ["TabGeneral"] = "Allgemein", ["TabLanguage"] = "Sprache",
            ["TabAppearance"] = "Darstellung", ["TabAdvanced"] = "Erweitert", ["GeneralDescription"] = "Lege fest, wie ClipNest arbeitet und den Verlauf speichert.",
            ["SaveHistory"] = "Verlauf auf diesem Gerät speichern", ["SaveHistoryDescription"] = "Der Verlauf bleibt in deinem Windows-Profil und wird nie synchronisiert.",
            ["StorageLocation"] = "Lokaler Datenordner", ["LanguageTitle"] = "App-Sprache", ["LanguageDescription"] = "Wähle die in ClipNest verwendete Sprache.",
            ["ThemeTitle"] = "Design", ["ThemeSystem"] = "System", ["ThemeLight"] = "Hell", ["ThemeDark"] = "Dunkel", ["AccentTitle"] = "Akzentfarbe",
            ["AccentDescription"] = "Wähle eine Hervorhebungsfarbe für Schaltflächen und Steuerelemente.", ["StartupTitle"] = "Mit Windows starten", ["StartupDescription"] = "ClipNest bei der Anmeldung automatisch öffnen.",
            ["MinimizedTitle"] = "Minimiert starten", ["MinimizedDescription"] = "ClipNest bei der Anmeldung minimiert in der Taskleiste starten.",
            ["MonitorTitle"] = "Zwischenablage beim Öffnen überwachen", ["MonitorDescription"] = "Kopierte Texte speichern, solange ClipNest geöffnet ist.",
            ["MaxHistory"] = "Maximale Verlaufseinträge", ["PollInterval"] = "Prüfintervall", ["Milliseconds"] = "Millisekunden",
            ["ConfirmClear"] = "Vor dem Löschen nicht angehefteter Einträge fragen", ["Save"] = "Einstellungen speichern", ["Cancel"] = "Abbrechen", ["Saved"] = "Einstellungen gespeichert.",
            ["EmptyHistory"] = "Noch keine Einträge. Kopiere Text, um loszulegen.", ["ConfirmClearTitle"] = "Verlauf löschen?", ["ConfirmClearMessage"] = "Alle nicht angehefteten Einträge entfernen? Angeheftete bleiben erhalten.",
            ["PinnedMarker"] = "ANGEHEFTET", ["SettingsSaved"] = "Deine Einstellungen wurden gespeichert.", ["OpenFolder"] = "Datenordner öffnen"
        }
    };

    static LanguageManager()
    {
        Add("en", ("SystemDefault", "System default"));
        Add("tr", ("SystemDefault", "Sistem varsayılanı"), ("LocalOnly", "Yalnızca bu cihazda saklanır"), ("SaveError", "Kaydedilemedi. Klasör izinlerini kontrol edip tekrar deneyin."), ("StartupError", "Windows başlangıç ayarı güncellenemedi."), ("HistoryReadError", "Önceki geçmiş okunamadı; yeni bir geçmiş başlatılacak."));
        Add("fr", ("SystemDefault", "Valeur système"), ("LocalOnly", "Stocké uniquement sur cet appareil"), ("SaveError", "Enregistrement impossible. Vérifiez les autorisations du dossier."), ("StartupError", "Impossible de modifier le démarrage de Windows."), ("HistoryReadError", "Historique précédent illisible ; un nouvel historique sera créé."));
        Add("es", ("SystemDefault", "Predeterminado del sistema"), ("LocalOnly", "Guardado solo en este dispositivo"), ("SaveError", "No se pudo guardar. Comprueba los permisos de la carpeta."), ("StartupError", "No se pudo actualizar el inicio de Windows."), ("HistoryReadError", "No se pudo leer el historial anterior; se creará uno nuevo."));
        Add("ru", ("SystemDefault", "По умолчанию системы"), ("LocalOnly", "Хранится только на этом устройстве"), ("SaveError", "Не удалось сохранить. Проверьте права доступа к папке."), ("StartupError", "Не удалось изменить параметры запуска Windows."), ("HistoryReadError", "Не удалось прочитать прежнюю историю; будет создана новая."));
        Add("zh-CN", ("SystemDefault", "系统默认"), ("LocalOnly", "仅保存在此设备上"), ("SaveError", "无法保存，请检查文件夹权限后重试。"), ("StartupError", "无法更新 Windows 启动设置。"), ("HistoryReadError", "无法读取旧历史记录，将创建新的记录。"));
        Add("de", ("SystemDefault", "Systemstandard"), ("LocalOnly", "Nur auf diesem Gerät gespeichert"), ("SaveError", "Speichern nicht möglich. Überprüfe die Ordnerberechtigungen."), ("StartupError", "Windows-Autostart konnte nicht aktualisiert werden."), ("HistoryReadError", "Der vorherige Verlauf konnte nicht gelesen werden; ein neuer wird erstellt."));
    }

    private static void Add(string language, params (string Key, string Value)[] values)
    {
        foreach (var (key, value) in values) Texts[language][key] = value;
    }

    public static string Resolve(string? language)
    {
        if (string.IsNullOrWhiteSpace(language) || language.Equals("system", StringComparison.OrdinalIgnoreCase))
        {
            var system = CultureInfo.CurrentUICulture.Name;
            if (system.StartsWith("zh", StringComparison.OrdinalIgnoreCase)) return "zh-CN";
            var two = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return Texts.ContainsKey(two) ? two : "en";
        }
        return Texts.ContainsKey(language) ? language : "en";
    }

    public static string Get(string key)
    {
        var language = Resolve(App.Settings.Language);
        if (Texts.TryGetValue(language, out var dictionary) && dictionary.TryGetValue(key, out var value)) return value;
        return Texts["en"].TryGetValue(key, out var english) ? english : key;
    }

    public static void Apply(string? language)
    {
        var resolved = Resolve(language);
        if (Application.Current is null) return;
        foreach (var pair in Texts["en"]) Application.Current.Resources[pair.Key] = pair.Value;
        foreach (var pair in Texts[resolved]) Application.Current.Resources[pair.Key] = pair.Value;
    }
}
