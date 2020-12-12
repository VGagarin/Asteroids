# Asteroids

Билд - https://drive.google.com/file/d/1dSo2wpkSxhdf4hNtcfBZSSOWfn7CYmHl/view?usp=sharing

В рамках тестового не стал выносить параметры в конфиги, а сделал всё в константах внутри соответствующих классов (что не круто, конечно)

Также не стал делать пулы объектов, хотя в этой игре они бы не помешали. Пока у меня просто все удаляются и создаются заново.

Класс Game имеет многовато ответственности, но в целом небольшой и понятный получился. А вообще лучше бы его разделить на того, кто все префабы создаст и закончит на этом, а второй будет медиатором для GameModel, HUDView, и спавнеров. А ещё лучше, конечно, чтоб спавнеры через один медиатор общались с GameModel, а HUDView с GameModel через второй.

Т.к. не уточнили в каком виде и формате нужна док-ция, то просто кратко пройдусь по всем классам:
### Game
Эта наша точка входа. Там инстациируюются из префабов:
HUDView, отвечающий за интерфейс, 
PlayerSpawner и EnemySpawner, инстациирующие игрока и врагов
Teleport, отвечающий за замыкание игрового поля

Также создается класс GameModel, хранящий состояние сессии. И происходят подписки на события, чтобы GameModel мог узнавать об игровых событиях смерти игрока/убийства врага, а HUDView обновлял интерфейс в соответствии с изменениями в GameModel.

### HUDView
Содержит SerializeField поля, отвечающие за интерфейс (Счётчики очков, индикатор жизней, диалог рестарта). По событиям класса GameModel, Game вызывает в нём соответствующие методы для отображения изменений в интерфейсе.

### PlayerSpawner
Создает игрока в рандомном месте и с рандомным вращением. Если игрок умирает, вызывает событие PlayerDied, затем игрок создается заново с некоторой задержкой.

### EnemySpawner
Создает врагов за радиусом круга, задаваемого 2-мя трансформами, размещенными так, чтоб враги появлялись за экраном.
Положение врага задаётся случайно. Модуль вектора скорости случайный, направление тоже случайно, а задаётся случайным отклонением от направления к центру, таким образом, чтоб враг летел в область экрана.

С небольшой вероятностью вместо астероида спавнится НЛО.

### Teleport
Т.к. экран замыкается сам на себе, то при выходе объекта за границы, отмеченными 4-мя трансформами, он телепортируется на противоположный край. 
Актуально для всех GameObject с компонентом Teleportable.

### Teleportable
На старте регистрируется в статическом методе класса Teleport, а в OnDestroy удаляется оттуда.

### GameModel
Хранит актуальную информацию по очкам, рекорду, жизням. Вызывает события при изменении этих показателей. Также вызывает событие GameEnded, если истрачена последняя жизнь.

### Enemy
Абстрактный класс, базовый для врагов. Содержит поле _destroyAction с делегатом, который вызывается в OnDestroy. Нужно для учёта убитых врагов.

### Asteroid
Содержит публичный метод Split, разбивающий астероид на осколки, если поле _generationIndex не достигло максимального значения. 
Для осколков прежний вектор скорости умножается на случайную величину и поворачивается на случайный угол от первоначального направления.

### Alien
Стреляет под себя в случайном направлении в рамках указанного разброса

### Movable
Компонент для движения (астероидов, пришельцев, пуль).
Через свойство Speed можно задать вектор скорости.
Метод RotateSpeedVector(float deflectionAngle) поворачивает нынешний вектор скорости на deflectionAngle.
Метод IncreaseSpeedVector(float modifier) умножает нынешний вектор скорости на modifier.
В FixedUpdate добавляет к позиции актуальный вектор скорости.

### Bullet
Имеет время жизни, спустя которое удаляется. Через методы инициализации для выставления вектора скорости обращается к компоненту Movable, наличие которого  задекларировано атрибутом RequireComponent.

### Blink
При вызове публичного StartBlinking(float duration), будет менять альфу на спрайте, заданном через SerializeField.
Альфа меняется по формуле:
alpha = (Mathf.Abs(Mathf.Sin(Time.time * Frequency)) + MinAlpha) / (MinAlpha + MaxAlpha);

- Mathf.Sin(Time.time * Frequency) даёт синусоиду, сужающуюся, если Frequency > 1 и расширяющуюся, если Frequency < 1.
- Взятия этой величины по модулю Mathf.Abs(Mathf.Sin(Time.time * Frequency)) гарантирует нам значения от 0 до 1 
- Потом поднимем график вверх, чтоб альфа не падала до нуля: Mathf.Abs(Mathf.Sin(Time.time * Frequency)) + MinAlpha
- Но теперь у нас появятся значения > 1. Чтоб максимальное значение стало снова 1, поделим полученное на (MinAlpha + MaxAlpha)

### Destructible
Компонент для разрушаемых объектов. 
Если свойство IsActive равно true, то вызовется виртуальный метод Distraction, вызывающий на данном GameObject Destroy.

### DestructibleWithParticleEffect
Наследуется от Destructible. Переопределяет Distraction, где спавнит систему частиц, затем вызывает базовую реализацию метода.

### AsteroidDestructible
Наследуется от DestructibleWithParticleEffect. Требует компонент Asteroid, на котором вызывает метод Split() при разрушении.

### Player
Требует компоненты Blink и Destructible.
Для обеспечения неуязвимости:
На старте меняет поле IsActive компонента Destructible в false и вызывает метод StartBlinking компонента Blink в соответствии с временем неуязвимости.
По истечении времени неуязвимости возвращает IsActive компонента Destructible в true.

Также имеет событие Died, вызываемое в OnDestroy.

### PlayerMove
Требует компонент Rigidbody2D.
Добавляет к Rigidbody2D силу в направлении “прямо” относительно игрока по нажатию на клавиши.

### PlayerRotate
Требует компонент Rigidbody2D.
Вращает Rigidbody2D при нажатию на клавиши.

### PlayerFiring
Требует компонент Rigidbody2D.
По нажатию на клавиши стрельбы, спавнит пулю в точке, задаваемой трансформом, соответствующим носовой части корабля игрока.
Вектор скорости направлен в направлении “прямо” относительно игрока.

Гарантирует соблюдение минимальной задержки между выстрелами, непрерывно спавнит пули по этой задержке при зажатии кнопке стрельбы.
