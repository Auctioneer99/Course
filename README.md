# Терминология
Для внесения ясности в предметную область, введены термины, расположенные ниже. В последующих частях эти термины будут выделяться курсивом.
•	Клиент - приложение, работающее в режиме "Игрока"
•	Сервер - приложение, работающее в режиме "Обработки логики"
•	Коллекция - набор карт, зарегистрированный для определенного игрока.
•	Карта - Сущность, имеющая набор показателей. Она выступает основной боевой единицей на поле. 
•	Колода - набор из 30 карт, собранный по определенным правилам.
•	Кладбище - набор карт, пополняемый погибшими картами.
•	Лидер - Особый случай карты. При гибели карты, игрок, ассоциированный с этой картой, проигрывает матч.
•	Рука - Набор карт, которой может оперировать игрок в данный момент.
•	Муллиган - стадия игры в которой вы можете заменить ненужные карты из руки.
•	Поле - игровое пространство, состоящее из гексагональных плиток. 
•	Область влияния - область игрового пространства, принадлежащая игроку.
•	Кулдаун - является видом таймера, который не позволяет что-либо если его значение не истекло. 
•	Способность - Действие, которое может выполняться картой, инициируемое игроком.


 
# Введение
Основная цель курсовой работы разработка Клиентского и Серверного приложения в Web.
## Описание сущностей
### Игрок
Каждый игрок имеет свою коллекцию карт, из которой он создаёт колоды. Игроки используют колоды карт, чтобы участвовать в матче. Во время матча к каждому игроку прикрепляется колода карт, которую он выбрал. Изначально другие игроки не знают из каких карт состоит колода противника. Игрок имеет запас маны. Изначально у игрока 1 мана, но по мере продвижения игры её запас увеличивается вплоть до определенного значения для каждого игрока равное. Количество маны игрока при этом не может опуститься ниже нуля.  По мере продвижения игры игрок получает в руку карты из колоды. Эти карты он может выставлять на поле и использовать.
### Колода
Колода состоит из 30 карт и 1 лидера. Колода не может содержать свыше или менее карт. В колоде не может встречать более 2 копий одинаковых карт. В остальном же выбор карт для составления колоды остаётся за игроком. Колода используется во время игры для добора карт в руку. Если в колоде заканчиваются карты она не пополняется сама собой и остаётся пустой.
### Рука
В руке игрока находятся карты в порядке взятия из их колоды. Рука держится в секрете от других игроков. Рука имеет максимальный размер - 10 карт. При попытке взятии карты, свыше этого количества, карты остаются в колоде.
### Кладбище
Также является вместилищем для карт любая карта, погибшая во время матча, попадает сюда. Размер кладбища не ограничен. Карты в ней отсортированы в порядки попадания их на кладбище. Кладбище доступно для просмотра любому игроку.
### Карта
Основное взаимодействие игроков происходит через карты. Каждая карта имеет уникальный набор атрибутов. Основными атрибутами карты является атака, здоровье, инициатива, очки действий, стоимость.
Атака. Этот атрибут представлен числовым значением и не может быть ниже нуля. Верхний порог не ограничен. Он используются способностями карты чтобы определить значение нанесенного урона другой карте. Урон может наноситься только той карте, которая находится поле.
Здоровье. Представляет собой совокупность двух числовых атрибутов: верхний порог здоровья и текущее здоровье карты. Текущее здоровье карты может варьироваться между нижним и верхним порогом здоровья. Нижний порог - всегда 0. При пересечении верхнего порога, значения текущего здоровья становятся равными верхнему порогу. При пересечении нижнего порога - карта погибает. Гибель карты означает удаление её с поля боя и перенос на кладбище.
Инициатива. Также представляет собой числовой атрибут. Используется в игре для определения очередности возможности действовать для карт. Нижний порог - 0. Верхний порог - неограничен.
Очки действия. Является совокупностью двух числовых значений. Также, как и со здоровьем. Текущее количество действий и верхний порог действий. Текущее количество действий может варьироваться в интервале между нижним и верхним порогом. Нижний порог - 0. Текущее количество очков действий не может быть выше верхнего порога. При достижении нижнего порога, карта заканчивает действовать, и ход передаётся следующей. Очки действий тратятся при использовании Способностей.
Стоимость. Выражается в числовом атрибуте. Обозначает стоимость в количестве маны, которую игрок должен потратить чтобы выставить эту карту на поле.
Также у карты могут существовать Теги - строковое обозначение принадлежности к чему-либо. Пример: Механизм, Человек, Демон.
Помимо этого, у карты есть набор Способностей: начальные, врожденные, приобретенные:
Начальные. Способности, которые есть у всех карт без исключений:
•	Основная атака,
•	Основное перемещение,
•	Ожидание,
•	Защита.
Врожденные. Способности, определенные самой картой, они могут повторятся от карты к карте, но однозначно не все карты имеют эти способности.
Приобретенные. Способности, которые появились у карты во время матча в следствии каких-либо причин.
Во время игры карта может находится только в одном месте: в колоде, в руке, на поле и на кладбище. Если она размещена на поле, то она ассоциируется с определенной клеткой на поле.
### Лидер
Особый случай обычной карты. Зачастую имеет повышенное количество здоровья.
### Способность
Способностями может обладать только карта. Способность имеет основные атрибуты: стоимость в очках действий, стоимость в количестве маны и кулдаун.
Стоимость в очках действий. Представляет собой целое число. Минимальное значение - 0. Обозначает сколько очков действий потратит карта, за которой закреплена способность, после использования. Если у карты недостаточно текущих очков действий, то способность не может быть применена.
Стоимость в количестве маны. Целое число. Минимальный значение - 0. Показывает сколько маны потребуется чтобы использовать данную способность. Мана будет использоваться у игрока, с которым ассоциирована карта. Если у игрока недостаточное количество маны, то способность не может быть применена.
Кулдаун. Пара целых чисел. Одно из них представляет собой текущее значение таймера, а второе верхнее значение таймера. Текущее значение таймера не может опуститься ниже нуля. Изначально текущее значение таймера равно 0. После использования способности его значение становится равно верхнему значению. Каждый начало хода смещает текущее значение таймера на единицу вниз. Если текущее значение не равно нулю, то способность не может быть применена.
Каждая способность имеет свой уникальный эффект после применения. К примеру: "Нанесите 1 единицу урона выбранному врагу" или "Восстановите себе 2 единицы здоровья".
Способ применения способности определяется самим эффектом: он может быть направленным на карту, на поле боя, и не направленным. Способность может иметь условия применения, к примеру: "Нанесите 3 единицы урона выбранному демону". Здесь используется условие на принадлежность карты к тегу "Демон". Существует несколько условий отбора:
•	По тегу - пример выше,
•	По расстоянию - "Нанесите 2 единицы урона всем картам в радиусе 1 клетки",
•	По отношению - "Восстановите 4 единицы здоровья союзному отряду".
Эффект от способности может влиять на разные показатели карты, не только на её здоровье. Список возможных изменений:
•	Изменение занятой клетки на поле (относительно карты, глобально),
•	Изменение текущего здоровья,
•	Изменение очков действий.
Описание начальных способностей. ОД - стоимость в очках действий, КМ - стоимость в количестве маны, П - кулдаун.
Атака. "Наносит урон равный атаке карты выбранному врагу. ОД - 1, КМ - 0, П - 1".
Перемещение. "Передвигает карту на 1 клетку в выбранном направлении, если это возможно. ОД -1, КМ - 0, П - 0".
Ожидание. "Если карта еще не потратила не одного очка действия, то передаёт право ходить следующему игроку. Карта перекладывает начало своего хода на другой промежуток этого раунда. ОД - 0, КМ - 0, П - 1".
Защита. "Карта немедленно заканчивает ход, и передает право на ход следующему. ОД - 0, КМ - 0, П - 1".
### Поле
Поле боя представляет собой гексагональные клетки, расположенные так, чтобы они составляли плотную сетку без дыр. Для каждой клетки в соседях может находиться от 1 до 6 клеток. По клеткам можно перемещаться если они находиться в соседях. В одно и то же время на клетке может находиться только одна карта. Клетка может быть занята картой или не разрушаемым объектом. Этот объект не может быть уничтожен или перемещен.
На поле существуют определенные участки территорий, называемые областью влияния. Каждый игрок имеет свою область влияния. Для игрока не может существовать 2 различные области. Они располагаются на максимальном удалении друг от друга. Внутри этой области карты могут спокойно перемещаться, но если карта выйдет за границу этой области, то она не сможет вернуться обратно в эту область.
В предполагаемом центе поля устанавливается точка захвата. На ней создаётся карта захвата.
# Правила игры
В игре могут участвовать от 2 до 3 игроков. Игрокам неизвестно из каких карт состоит колоды противников и что у него в руке.
## Стадии игры
Изначально игра находится в режиме ожидания игроков. Игроки могут выбрать колоду карт, которую они будут использовать в течении матча. Выбор колоды осуществляется до начала игры. После набора достаточного количества игроков начинается игра, после этого игра не может вернуться в это состояние, но может перейти в следующие:
•	Инициация игры,
•	Муллиган,
•	Начало раунда,
•	Выставление карт,
•	Определение очередности,
•	Ход игрока,
•	Окончание игры.
Инициация игры. В это состояние игра входит один раз только в начале матча. Игроки набирают определенное количество карт из коллекций, которые они выбрали до начала игры. Создаётся поле боя для игры. Расставляются лидеры каждого игрока, определяется их область влияния. Создаётся карта для захвата. После всех инициализаций игра переходит в состояние Муллиган.
Муллиган. В этом состоянии игроку или игрокам предоставляется возможность избавиться от ненужных карт в руке. После выбора ненужных карт, на их замену приходят другие карты из колоды, а они уходят в колоду. За игроком остаётся выбор использовать данную возможность или нет. После того как все игроки сделали свой выбор игра переходит в следующее состояние - начало раунда.
Начало раунда. В этом состоянии каждый игрок берет одну карту из своей колоды. А также восполняет всю израсходованную ману и получает 1 неиспользованную ману. Следом идёт выставление карт. 
Выставление карт. Каждый игрок может выставить карту из своей руки на свою область влияния. Каждая карта имеет свою стоимость выставления. Если её разыгрывает игрок количество его ману уменьшается на стоимость этой карты. Игрок может выставлять карты или не выставлять, если хочет сохранить ману. Выставление карт является скрытым действием. То есть противники не знают какие карты вы сыграть вплоть до момента, когда все игроки подтвердили свой выбор. После того как все игроки подтвердили свой выбор игра переходит в состояние определения очередности.
Определение очередности. В этом состоянии определяется последовательность, в которой владельцам даётся возможность управления картами. В игре присутствует циклический список игроков. Один из них является главным. Главный игрок передает права следующему за ним игроку в начале каждого определения. Правила составления очередности: все карты, выставленные на поле, сортируются в группы по инициативе, затем в группе они сортируются в новые группы по относительному главенству игрока, и наконец, если остались спорные ситуации, то группы сортируются в порядке выставления их на поле. Итоговый порядок будет определять кто будет ходить и кому передавать права на ход.
Ход игрока. В этом состоянии он управляет только той картой, которая определена очередностью. В начале хода карта восполняет все использованные очки действия. Кулдауны способностей снижаются на 1 единицу. Далее игроку предстоит сделать выбор из способностей карты. 
Варианты победы
Для победы в матче есть 3 основные варианта:
•	Захват,
•	Доминирование,
•	Выжидание.
Захват. Победа захватом осуществляется, когда вы уничтожаете карту захвата. Она расположена в центре поля боя. Игрок, уничтоживший эту карту, становится победителем матча.
Доминирование. Доминирование предполагает, что ваша карта лидер осталась единственной картой лидером на поле боя. Это означает что все остальные карты лидеры были уничтожены вами или другим врагом. Когда осталась одна карта лидер, победа присуждается игроку, ассоциированному с этой картой.
Выжидание. После определенного количество раундов игра не может быть продолжена, поэтому победитель определяется преждевременно. Победителем становится тот игрок, чьё здоровье лидера больше всех. Если среди карт лидеров наибольшее здоровье имеет несколько карт, то победа никому не присуждается, все игроки проигрывают.
Визуальный стиль
Действия игры происходит вымышленной вселенной, с развитием примерно равным развитию нашего человечества в 13 - 16 веках нашей эры. Игра должна ассоциироваться с чем-то темным и ужасным. Поэтому элементы игры должны поддерживать холодную палитру цветов. И использовать острые углы при дизайне.
# Аналоги
Основными аналогами, а также источниками вдохновения были игры:
•	Серия пошаговых стратегических игр King's Bounty,
•	Коллекционная карточная игра Gwent,
•	Коллекционная карточная игра Hearthstone,
•	Серия 4Х стратегических игр Sid Meier's Civilization.
King's Bounty. Игра подразделяется на две взаимосвязанные игры. Первая - это ролевая игра с исследованием мира и сюжетом. Вторая - пошаговая стратегия. Ключевые моменты, которые похожи - поле боя, способности и атрибуты. На игровом поле изначально расставлены все существа. Вы управляете своим отрядом из максим 5 начальных существ. Порядок хода определяется инициативой отрядов. Поражение присуждается игроку, потерявшему все свои отряды. Другому игроку присуждается победа.
Gwent. Игра, выпущенная CD Project Red. Карточная игра. В ней вы создаете колоду карт по определенным правилам. Вы используете их чтобы выставлять их на поле боя. Поле боя разделяется на 2 части: ближний ряд и дальний ряд. Чтобы одержать победу в матче вы должны выиграть 2 раунда. Победа в раунде присуждается игроку, чья сумма мощи всех его разыгранных карт на поле боя больше противника. Ходы передаются по очереди. В начале хода вы может сыграть только 1 карту с руки. Если вы не хотите сыграть карту вы обязаны спасовать, то есть вы не сможете управлять своими существами и играть карты до конца раунда.
Hearthstone. Также является карточной игрой и всеми её вытекающими. Поле боя состоит из 2 частей: поле противника и ваше поле. Ходы передаются по очереди. В свой ход вы можете управлять всеми существами под вашим контролем и разыгрывать карты с руки. Побеждает тот игрок, который первым уничтожит Героя противника.
Sid Meier's Civilization. В этой игре вам предстоит управлять своей империей, развивать её. Действия происходят на гексагональном поле. Ваша цивилизация образуется с помощью ваших городов. В городах вы можете производить дома для улучшения города и существ для постройки новых городов, или захвата вражеских. Ход игрока устанавливается правилами игры. Ходы могут происходить по очереди или одновременно. Как только все игроки закончат свой ход, игра переходит в следующий раунд. Существа перемещаются по гексагональному полю. На поле могу встречаться преграды и пересеченные места, в которых тратится больше очков действий чтобы их пересечь. Побед в этой игре множество. Рассматриваемая победа была победа через завоевание - использование разных типов существ для захвата вражеских городов. Как только у игрока не осталось городов он проигрывает.
