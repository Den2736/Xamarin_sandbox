Phoneword
=========

Первый проект сделаю по этому
[руководству](https://docs.microsoft.com/ru-ru/xamarin/xamarin-forms/get-started/)

----- Первый запуск -----
-------------------------

После создания проекта сразу попробовал запустить приложение на своём
устройстве. Для этого при выборе отладки выбрал Live player, по
подсказке скачал на телефон приложение *Xamarin Live Player*, запустил,
телефон со студией быстро познакомились, приложение запустилось. Всё
просто.

Попробовал запустить на Windows Phone, но там требовалось активировать
режим разработчика, я так и не нашёл, как это сделать. Позже, если будет
желание, заморочусь еще раз.

----- PhoneTranslator -----
---------------------------

В этой части я смысла не вижу - это просто произвольное преобразование
текстовой части введённой строки в номер.

----- Dialer -----
------------------

Реализация звонка зависит от конкретной платформы, поэтому будет
реализована в каждом соответствующем проекте. Однако в общем проекте
стоит создать интерфейс

    public interface IDialer
    {
        bool Dial(string number);
    }

Вот только из проекта Android я не вижу этот интерфейс. В процессе
поиска решения, а может и сразу, возникли ошибки в MainActivity (не
удалось найти App()). Откатил изменения.

Так, повторив все действия, такое ощущение, что файл Dialer исключен из
проекта - он не парсится и не анализируется. Лечение - исключение и
повторное включение файла в проект. Теперь вроде нормально.

----- Test -----
----------------

Звонок не произошёл, поскольку нет контекста в *MainActivity.Instance*.
В отладке стало ясно, что метод MainActivity.OnCreate даже не
вызывается.

А вот на эмуляторе работает. Так не интересно.

**09.08**

На эмуляторе OnCreate вызывается при создании формы (да, логично).
Теперь нужно выяснить, дело в отладке на телефоне, или же в самом
телефоне. Для этого нужно сгенерить .apk и установить на устройстве.

----- Archive -----
-------------------

Для архивации в свойствах Android-проекта нужно отключить *Shared Mono
runtime*. Затем во время архивации появляется сообщение

> Please ensure that you are using a release configuration and that "Use
> Shared Mono Runtime" option unchecked

На [форуме](https://github.com/xamarin/xamarin-android/issues/1760)
одним из советов было перезагрузить студию - помогло.

Архив был создан, но установить его не удалось. Подозреваю, что дело
версии Android - пакет нацелен на версию 8.1, а телефон 8.0. Скачал sdk
для 8.0, в свойствах проекта поменял версию, встретился с множеством
ошибок аналогичных следующей.

### Error: Xamarin.Android.Support.v7.AppCompat

На
[форуме](https://stackoverflow.com/questions/47988647/xamarin-error-after-updating-visual-studio-2017-to-version-15-5-2)
одним из советов было удалить AppCompatActivity - помогло.

Всё равно не удаётся установить приложение.

Сформировал на телефоне bugreport в 255 000 строк, где отыскал
следующее:

> \[line 202509\] Historical install sessions: Session 1733822157:
> userId=0 installerPackageName=com.google.android.packageinstaller
> installerUid=10024 createdMillis=1533799659695 s
> tageDir=/data/app/vmdl1733822157.tmp stageCid=null mode=1
> installFlags=0x12 installLocation=0 sizeBytes=17946700
> appPackageName=com.companyname.Xamarin\_quick\_start a ppIcon=false
> appLabel=null originatingUri=null originatingUid=10119
> referrerUri=null abiOverride=null volumeUuid=null
> grantedRuntimePermissions=null mClientProgress=1.0 mProgress=0.8
> mSealed=true mPermissionsAccepted=true mRelinquished=false
> mDestroyed=true mFds= 0 mBridges=1 mFinalStatus=-103
> mFinalMessage=Failed to collect certificates from
> /data/app/vmdl1733822157.tmp/PackageInstaller: Attempt to get length
> of null array

Значит, не обнаружен сертификат. По данному
[руководству](https://docs.microsoft.com/ru-ru/xamarin/android/deploy-test/signing/index?tabs=vswin)
сгенерировал сертификат и сформировал подписанный .ask файл. Этот файл
нормально установился, приложение заработало, причем наблюдаемой при
отладке ошибки не возникло.
