using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace mapGenerator
{
    class Generator
    {
        private static string DefineType(char[,] tileMatrix, int column, int row, TileType[] tileTypes)
        {
            if (tileMatrix[column, row] == '0')
                return GenerateString(0);
            if (CompareTile(true, true, true, true, tileMatrix, column, row))
                return GenerateString(10);
            if (CompareTile(true, false, true, true, tileMatrix, column, row))
                return GenerateString(9);
            if (CompareTile(false, true, true, true, tileMatrix, column, row))
                return GenerateString(8);
            if (CompareTile(true, true, true, false, tileMatrix, column, row))
                return GenerateString(13);
            if (CompareTile(true, true, false, true, tileMatrix, column, row))
                return GenerateString(14);
            if (CompareTile(false, true, false, true, tileMatrix, column, row))
                return GenerateString(7);
            if (CompareTile(true, false, false, true, tileMatrix, column, row))
                return GenerateString(6);
            if (CompareTile(true, false, true, false, tileMatrix, column, row))
                return GenerateString(5);
            if (CompareTile(false, true, true, false, tileMatrix, column, row))
                return GenerateString(4);
            if (CompareTile(true, true, false, false, tileMatrix, column, row))
                return GenerateString(12);
            if (CompareTile(false, false, true, true, tileMatrix, column, row))
                return GenerateString(11);
            if (CompareTile(false, false, false, true, tileMatrix, column, row))
                return GenerateString(3);
            if (CompareTile(false, false, true, false, tileMatrix, column, row))
                return GenerateString(1);
            if (CompareTile(false, true, false, false, tileMatrix, column, row))
                return GenerateString(15);
            if (CompareTile(true, false, false, false, tileMatrix, column, row))
                return GenerateString(16);
            foreach (var tileType in tileTypes)
            {
                if (CheckCollision(tileType.X, tileType.Y, tileMatrix, column, row, tileType.Name.ToString(), '1', false))
                {
                    FillSubmatrix(tileType.X, tileType.Y, tileMatrix, column, row);
                    return GenerateString(tileType.Name);
                }
            }
            return GenerateString(2);
        }
        private static bool CompareTile(bool right, bool left, bool up, bool down, char[,] tileMatrix, int column, int row)
        {
            var r = false;
            var l = false;
            var u = false;
            var d = false;
            if ((row + 1 < tileMatrix.GetLength(1) && tileMatrix[column, row + 1] == '~') || !down) 
                d = true;
            if ((row - 1 >= 0 && tileMatrix[column, row - 1] == '~') || !up)
                u = true;
            if ((column - 1 >= 0 && tileMatrix[column - 1, row] == '~') || !left)
                l = true;
            if ((column + 1 < tileMatrix.GetLength(0) && tileMatrix[column + 1, row] == '~') || !right)
                r = true;
            return r && l && u && d;
        }
        private static string GenerateString(int name)
        {
            return $"graphics/tileRes/tile{name}.png";
        }

        private static void FillSubmatrix(int shiftX, int shiftY, char[,] tileMatrix, int column, int row)
        {
            for (var i = 0; i < shiftX; i++)
            {
                for (var j = 0; j < shiftY; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                    tileMatrix[column + i, row + j] = '0';
                }
            }
        }
        private static char[,] CreateMatrix(string level)
        {
            level = Regex.Replace(level, "\r", string.Empty);
            var levelArr = level.Split('\n').Where((item, state) => item != string.Empty).ToArray();
            var width = levelArr[0].Length;
            var height = levelArr.Count();
            var matrix = new char[width, height];
            for (var j = 0; j < height; j++)
                for (var i = 0; i < width; i++)
                    matrix[i, j] = levelArr[j][i];
            return matrix;
        }

        private static char[,] CreateTileMatrix(char[,] matrix)
        {
            var tileMatrix = new char[matrix.GetLength(0), matrix.GetLength(1)];
            for(var i = 0; i < tileMatrix.GetLength(0); i++)
                for(var j = 0; j < tileMatrix.GetLength(1); j++)
                    tileMatrix[i, j] = '~';

            for (var i = 0; i < matrix.GetLength(0); i++)
                for (var j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] == '1')
                        tileMatrix[i, j] = '1';
            return tileMatrix;
        }

        private static bool CheckCollision(int shiftX, int shiftY, char[,] matrix, int column, int row,
            string objName, char type, bool logging)
        {
            for (var k = 0; k < Math.Abs(shiftY); k++)
            {
                var rowShift = k;
                if (shiftY < 0)
                    rowShift *= -1;
                for (var l = 0; l < Math.Abs(shiftX); l++)
                {
                    var columnShift = l;
                    if (shiftX < 0)
                        columnShift *= -1;
                    if (k == 0 && l == 0)
                        continue;
                    var colPos = column + columnShift;
                    var rowPos = row + rowShift;
                    if (colPos >= matrix.GetLength(0) || rowPos >= matrix.GetLength(1) || colPos < 0 || rowPos < 0)
                    {
                        if(logging)
                            Console.WriteLine($"Whoops! It seems the {objName} goes beyond the level");
                        return false;
                    }
                    if (matrix[colPos, rowPos] != type)
                    {
                        if (logging)
                            Console.WriteLine($"Whoops! It seems the {objName} in column {column + 1} and row {row + 1} intersects with object {matrix[colPos, rowPos]} C{colPos + 1}R{rowPos + 1}");
                        return false;
                    }
                }
            }
            return true;
        }

        public static void Run(string level, string[] resources, string fileName)
        {
            FileStream aFile = new FileStream(fileName, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(aFile);
            aFile.Seek(0, SeekOrigin.End);
            var tileTypes = new TileType[]
            {
                new TileType(8, 3, 19),
                new TileType(8, 8, 20),
                new TileType(3, 8, 21),
                new TileType(8, 8, 22),
                new TileType(4, 8, 23),
                new TileType(4, 3, 24),
                new TileType(2, 2, 25),
                new TileType(2, 2, 17),
                new TileType(2, 2, 18)
            };
            TileType.Sort(tileTypes);
            var matrix = CreateMatrix(level);
            var tileMatrix = CreateTileMatrix(matrix);

            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    var item = matrix[i, j];
                    var x = i * 32;
                    var y = j * 32;
                    switch (item)
                    {
                        case '1':
                            var type = DefineType(tileMatrix, i, j, tileTypes);
                            CreateTile(type, sw, x, y);
                            break;
                        case '2':
                            CreateSonic(resources[2], sw, x, y);
                            break;
                        case '3':
                            CreateSpikes(resources[3], sw, x, y);
                            break;
                        case '4':
                            if (CheckCollision(7, 1, matrix, i, j, "conveyor right", '0', true))
                                CreateConveyorRight(resources[4], sw, x, y);
                            break;
                        case '5':
                            if (CheckCollision(7, 1, matrix, i, j, "conveyor left", '0', true))
                                CreateConveyorLeft(resources[5], sw, x, y);
                            break;
                        case '6':
                            CreateSmoke(resources[6], sw, x, y);
                            break;
                        case '7':
                            if(CheckCollision(1, -4, matrix, i, j, "spike ball small up down", '0', true))
                                CreateSpikeBallSmallUpDown(resources[7], sw, x, y);
                            break;
                        case '8':
                            if(CheckCollision(4, 1, matrix, i, j, "spike ball small left right", '0', true))
                                CreateSpikeBallSmallLeftRight(resources[8], sw, x, y);
                            break;
                        case '9':
                            if (CheckCollision(2, -4, matrix, i, j, "spike ball big up down", '0', true))
                                CreateSpikeBallBigUpDown(resources[9], sw, x, y);
                            break;
                        case 'a':
                            if(CheckCollision(4, 2, matrix, i, j, "spike ball big left right", '0', true))
                                CreateSpikeBallBigLeftRight(resources[10], sw, x, y);
                            break;
                        case 'l':
                            CreateLavaTop(resources[11], sw, x, y);
                            break;
                        case 'i':
                            CreateLava(resources[11], sw, x, y);
                            break;
                        case 's':
                            CreateSpring(resources[12], sw, x, y);
                            break;
                        case 'p':
                            if(CheckCollision(2, 1, matrix, i, j, "platform", '0', true))
                                CreatePlatform(resources[13], sw, x, y);
                            break;
                        case 'r':
                            CreateSpikesRight(resources[14], sw, x, y);
                            break;
                        case 'd':
                            CreateSpikesDown(resources[15], sw, x, y);
                            break;
                        case 't':
                            CreateTroll(resources[16], sw, x, y);
                            break;
                        case 'b':
                            if(CheckCollision(-i, 1, matrix, i, j, "badnik", '0',true))
                                CreateBadnik(resources[17], sw, x, y);
                            break;
                        case 'z':
                            CreateShutter(resources[18], sw, x, y);
                            break;
                        case 'f':
                            //if(CheckCollision(1, -9, matrix, i, j, "badnik fish", '0', true))
                                CreateBadnikFish(resources[19], sw, x, y);
                            break;
                        case 'm':
                            CreateBadnikMotobug(resources[20], sw, x, y);
                            break;
                        case 'n':
                            CreateRings(resources[21], sw, x, y);
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine($"{Math.Round((double)j/matrix.GetLength(1)*100, 2)}%");
            }
            sw.Close();
            Console.WriteLine("Done!");
        }

        private static void CreateRings(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Ring;{x + 11};{y + 11};\n}}\n");
            sw.Write($"Ring;{x + 22};{y + 11};\n}}\n");
            sw.Write($"Ring;{x + 11};{y + 22};\n}}\n");
            sw.Write($"Ring;{x + 22};{y + 22};\n}}\n");
        }

        private static void CreateBadnikMotobug(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"BadnikMotobug;{x};{y + 3};\n:0;0;36;30;\n}}\n");
        }

        private static void CreateBadnikFish(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"BadnikFish;{x};{y};\n}}\n");
        }

        private static void CreateShutter(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Shutters;{x};{y};true;\n}}\n");
        }

        private static void CreateBadnik(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"BadnikSimple;{x};{y};\n:0;8;64;24;\n}}\n");
        }

        private static void CreateTroll(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"InvisibleDamaging;{x};{y};\n:0;0;32;32;\n}}\n");
        }

        private static void CreateSpikesDown(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\nInvisibleDamaging;{x + 1};{y + 31};\n:0;0;30;1;\n}}\n");
        }

        private static void CreateSpikesRight(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\nInvisibleDamaging;{x + 31};{y + 1};\n:0;0;1;30;\n}}\n");
        }

        private static void CreateLava(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\nLava;{x};{y};\n}}\n");
        }

        private static void CreatePlatform(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;64;16;\n}}\n");
        }

        private static void CreateSpring(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"YellowSpring;{x + 2};{y};\n}}\n");
        }

        private static void CreateLavaTop(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\nLava;{x};{y};\n}}\nLavaTop;{x};{y-32};\n}}\n");
        }

        private static void CreateSpikeBallBigLeftRight(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"SpikeBallBig;{x};{y};true;\n}}\n");
        }

        private static void CreateSpikeBallBigUpDown(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"SpikeBallBig;{x};{y};false;\n}}\n");
        }

        private static void CreateSpikeBallSmallLeftRight(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"SpikeBallSmall;{x};{y};false;\n}}\n");
        }

        private static void CreateSpikeBallSmallUpDown(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"SpikeBallSmall;{x};{y};true;\n}}\n");
        }

        private static void CreateSmoke(string v, StreamWriter sw, int x, int y)
        {
            sw.Write($"Smoke;{x};{y};\n}}\n");
        }

        private static void CreateConveyorRight(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;199;29;\n}}\nConveyorBelt;{x};{y};true;\n}}\n");
        }

        private static void CreateConveyorLeft(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;199;29;\n}}\nConveyorBelt;{x};{y};false;\n}}\n");
        }

        private static void CreateSpikes(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\nInvisibleDamaging;{x};{y};\n:0;0;30;30;\n}}\n");
        }

        private static void CreateSonic(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Sonic;{x};{y};\n}}\n");
        }

        private static void CreateTile(string type, StreamWriter sw, int x, int y)
        {
            sw.Write($"Tile;{x};{y};true;{type};\n:0;0;32;32;\n}}\n");
        }
    }
}
