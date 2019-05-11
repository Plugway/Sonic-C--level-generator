using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mapGenerator
{
    class Class1
    {
        public static void Go()
        {

            var tileTypes = new TileType[]
                {
                new TileType(8, 3, 10),
                new TileType(8, 8, 11),
                new TileType(3, 8, 12),
                new TileType(8, 8, 13),
                new TileType(4, 8, 14),
                new TileType(4, 3, 15),
                new TileType(2, 2, 16),
                new TileType(2, 2, 17),
                new TileType(2, 2, 18)
                };
            TileType.Sort(tileTypes);
            Console.WriteLine('1');
        }
    }
}
