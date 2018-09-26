using System;

namespace Graphs
{
    public class Program
    {
        static void Main(string[] args)
        {
            GridGraph graph = new GridGraph(15, 15);

            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());

            GridNode[] blah = graph.GetNodeFromLocation(new Vector2(x, y)).Neighbours;

            for (int i = 0; i < 4; i++)
            {
                if (blah[i] != null)
                    Console.WriteLine("{0},{1}", blah[i].Location.X, blah[i].Location.Y);
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

    }
}
