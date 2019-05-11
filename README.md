# Sonic C# level generator
Генератор уровней для соника
Уровень представляет собой строку level в program.cs
Все возможные символы:
    0 - air
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/tileRes/tile0.png "Air")
    1 - tile
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/tileRes/tile2.png "Tile")
    2 - sonic
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/sonicStanding.png "Sonic")
    3 - spikes
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesUp.png "Spikes")
    r - spikes toRight
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesRight.png "Spikes")
    d - spikes down
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesDown.png "Spikes")
    4 - conveyor r
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/conveyorBelt1.png "Conveyor")
    5 - conveyor l
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/conveyorBelt1.png "Conveyor")
    6 - smoke
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/bgSmoke4.png "Smoke")
    7 - SpikeBallSmallUpDown
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallSmall.png "Spike ball")
    8 - SpikeBallSmallLeftRight
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallSmall.png "Spike ball")
    9 - SpikeBallBig u d
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallBig.png "Spike ball")
    a - SpikeBallBig l r
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallBig.png "Spike ball")
    l - LavaTop
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/lavaTop2.png "Lava")
    i - Lava
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/lava2.png "Lava")
    s - spring
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/yellowSpring1.png "Spring")
    p - platform
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/platform.png "Platform")
    t - troll square
    ![alt](https://ugc-gaming.net/styles/default/xenforo/custom.smiles/nicesoftlock.png "Kek")
    b - badnik
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikSimple.png "Badnik")
    f - badnik fish
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikFish1.png "Badnik fish")
    m - badnik motobug
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikMotobug2.png "Badnik motobug")
    z - shutter 7x5 --
    n - rings
    ![alt](https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/ringRotating1.png "Ring")
Каждый символ представляет собой квадрат 32х32.
Некоторые объекты требуют больше места и не помещаются в 1 квадрат. Например, platform - имеет размеры 2х1 и прописывается как p0. Если указать что то кроме 0, то консоль известит вас, где именно происходит пересечение.
Проверку коллизий объектов можно выключить, закомментировав строчки условий в switch/case в файле Generator.
Объект tile имеет несколько видов текстур. Они подбираются автоматически и находятся в папке graphics/tileRes, туда же можно добавить еще текстур более чем 1х1, их же надо добавить в массив tileTypes в методе Run файла Generator. new TileType(размер по Х в блоках, размер по Y в блоках, имя в виде int), это самое число будет подставлено в название текстуры по шаблону tile{number}.png.
Сгенерированный уровень по умолчанию появляется в bin/debug/
Поместить его нужно в bin/debug/ соника и в файле Program соника поменять название.
Если будут вопросы то можете связаться со мной в [Telegramm](https://t.me/Plugway "Plugway")
