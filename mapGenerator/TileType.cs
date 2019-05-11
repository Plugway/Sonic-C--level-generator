using System;

namespace mapGenerator
{
    class TileType : IComparable
    {
        public TileType(int x,int y,int name)
        {
            X = x;
            Y = y;
            Name = name;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Name { get; set; }

        public int CompareTo(object obj)
        {
            var tile = (TileType)obj;
            var thisSquare = X * Y;
            var thatSquare = tile.X * tile.Y;
            return thisSquare.CompareTo(thatSquare);
        }
        public static void Sort(TileType[] array, int start, int end)
        {
            if (end == start) return;
            var pivot = array[end];
            var storeIndex = start;
            for (int i = start; i <= end - 1; i++)
                if (array[i].CompareTo(pivot) == 1)
                {
                    var t = array[i];
                    array[i] = array[storeIndex];
                    array[storeIndex] = t;
                    storeIndex++;
                }

            var n = array[storeIndex];
            array[storeIndex] = array[end];
            array[end] = n;
            if (storeIndex > start) Sort(array, start, storeIndex - 1);
            if (storeIndex < end) Sort(array, storeIndex + 1, end);
        }
        public static void Sort(TileType[] array)
        {
            Sort(array, 0, array.Length - 1);
        }
    }
}
