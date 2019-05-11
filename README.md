# Sonic C# level generator
Генератор уровней для соника
Уровень представляет собой строку level в program.cs  
  
| Символ объекта| Имя                           | Текстура      | Размеры в блоках  |  
|:-------------:|:-----------------             |:--------      |:-----------------:|  
| 0             | Air                           |![alt][logo1]  |1x1                |  
| 1             | Tile                          |![alt][logo2]  |1x1                |  
| 2             | Sonic                         |![alt][logo3]  |1x2                |  
| 3             | Spikes                        |![alt][logo4]  |1x1                |  
| r             | Spikes right                  |![alt][logo5]  |1x1                |  
| d             | Spikes down                   |![alt][logo6]  |1x1                |  
| 4             | Conveyor right                |![alt][logo7]  |7x1                |  
| 5             | Conveyor left                 |![alt][logo8]  |7x1                |  
| 6             | Smoke                         |![alt][logo9]  |1x1                |  
| 7             | Spike ball small up down      |![alt][logo10] |1x4                |  
| 8             | Spike ball small left right   |![alt][logo11] |4x1                |  
| 9             | Spike ball big up down        |![alt][logo12] |2x4                |  
| a             | Spike ball big left right     |![alt][logo13] |4x2                |  
| l             | Top lava block                |![alt][logo14] |1x1                |  
| i             | Lava                          |![alt][logo15] |1x1                |  
| s             | Spring                        |![alt][logo16] |1x1                |  
| p             | Platform                      |![alt][logo17] |2x1                |  
| t             | Troll square(invisible)       |![alt][logo18] |1x1                |  
| b             | Badnik                        |![alt][logo19] |-ix1               |  
| f             | Badnik fish                   |![alt][logo20] |1x1(1x~9)          |  
| m             | Badnik motobug                |![alt][logo21] |1x1                |  
| z             | Shutter                       |--             |1x1(?)             |  
| n             | 4 rings                       |![alt][logo22] |1x1                |  
  
[logo1]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/tileRes/tile0.png "Air"  
[logo2]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/tileRes/tile2.png "Tile"  
[logo3]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/sonicStanding.png "Sonic"  
[logo4]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesUp.png "Spikes"  
[logo5]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesRight.png "Spikes"  
[logo6]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikesDown.png "Spikes"  
[logo7]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/conveyorBelt1.png "Conveyor"  
[logo8]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/conveyorBelt1.png "Conveyor"  
[logo9]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/bgSmoke4.png "Smoke"  
[logo10]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallSmall.png "Spike ball"  
[logo11]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallSmall.png "Spike ball"  
[logo12]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallBig.png "Spike ball"  
[logo13]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/spikeBallBig.png "Spike ball"  
[logo14]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/lavaTop2.png "Lava"  
[logo15]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/lava2.png "Lava"  
[logo16]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/yellowSpring1.png "Spring"  
[logo17]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/platform.png "Platform"  
[logo18]: https://ugc-gaming.net/styles/default/xenforo/custom.smiles/nicesoftlock.png "Kek"  
[logo19]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikSimple.png "Badnik"  
[logo20]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikFish1.png "Badnik fish"  
[logo21]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/badnikMotobug2.png "Badnik motobug"  
[logo22]: https://github.com/Plugway/Sonic-C-game/blob/master/sonic-c-sharp/graphics/ringRotating1.png "Ring"  
  
Каждый символ представляет собой квадрат 32х32.  
Некоторые объекты требуют больше места и не помещаются в 1 квадрат. Например, platform - имеет размеры 2х1 и прописывается как p0. Если указать что то кроме 0, то консоль известит вас, где именно происходит пересечение.  
Проверку коллизий объектов можно выключить, закомментировав строчки условий в switch/case в файле Generator.  
Объект tile имеет несколько видов текстур. Они подбираются автоматически и находятся в папке graphics/tileRes, туда же можно добавить еще текстур более чем 1х1, их же надо добавить в массив tileTypes в методе Run файла Generator. new TileType(размер по Х в блоках, размер по Y в блоках, имя в виде int), это самое число будет подставлено в название текстуры по шаблону tile{number}.png.  
Сгенерированный уровень по умолчанию появляется в bin/debug/  
Поместить его нужно в bin/debug/ соника и в файле Program соника поменять название.  
Если будут вопросы то можете связаться со мной в [Telegram](https://t.me/Plugway "Plugway")  
