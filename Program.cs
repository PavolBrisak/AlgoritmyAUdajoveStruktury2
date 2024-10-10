using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdajovkySem1.StructureTester;
using UdajovkySem1.Tester;

namespace UdajovkySem1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// create a new KDTree
            //KDTree<PlotOfLand> tree = new KDTree<PlotOfLand>();
            //// create a new PlotOfLand
            //PlotOfLand plot = new PlotOfLand(1, "A", new GPSPosition('S', 1, 'W', 1), new GPSPosition('N', 3, 'E', 3));
            //PlotOfLand plot2 = new PlotOfLand(1, "B", new GPSPosition('S', 2, 'W', 2), new GPSPosition('N', 4, 'E', 4));
            //PlotOfLand plot3 = new PlotOfLand(1, "C", new GPSPosition('S', 0, 'W', 0), new GPSPosition('N', 1, 'E', 1));
            //PlotOfLand plot4 = new PlotOfLand(1, "D", new GPSPosition('S', 3, 'W', 2), new GPSPosition('N', 4, 'E', 3));
            //PlotOfLand plot5 = new PlotOfLand(1, "E", new GPSPosition('S', 3, 'W', 0), new GPSPosition('N', 5, 'E', 3));
            //// add the PlotOfLand to the KDTree
            //tree.Insert(plot);
            //tree.Insert(plot2);
            //tree.Insert(plot3);
            //tree.Insert(plot4);
            //tree.Insert(plot5);
            //// print the tree
            //Console.WriteLine("Tree:");
            //tree.Print();

            Random _random = new Random();

            KDTree<Numbers> tree = new KDTree<Numbers>();

            StructureTester<KDTree<Numbers>, Numbers> tester = new StructureTester<KDTree<Numbers>, Numbers>(
                tree,
                4,
                () => new Numbers(
                    _random.Next(0, 101),
                    _random.Next(0, 101),
                    _random.Next(0, 101)
                )
            );

            tester.TestInsert();

            tree.Print();

            Console.WriteLine("Enter three integers:");
            int first = int.Parse(Console.ReadLine());
            int second = int.Parse(Console.ReadLine());
            int third = int.Parse(Console.ReadLine());
            Numbers numbers = new Numbers(first, second, third);
            tree.Insert(numbers);
            tree.Print();

            Console.WriteLine("Enter three integers:");
            int first1 = int.Parse(Console.ReadLine());
            int second1 = int.Parse(Console.ReadLine());
            int third1 = int.Parse(Console.ReadLine());
            numbers = new Numbers(first1, second1, third1);
            List<Numbers> found = tree.Find(numbers);
            if (found.Count != 0)
            {
                foreach (Numbers n in found)
                {
                    Console.WriteLine(n);
                }
            }
            else
            {
                Console.WriteLine("Not found");
            }
            //KDTree<Numbers> tree = new KDTree<Numbers>();
            //int i = 0;
            //tree.Insert(new Numbers(10, 10, ++i));
            //tree.Insert(new Numbers(15, 10, ++i));
            //tree.Insert(new Numbers(5, 10, ++i));
            //tree.Insert(new Numbers(15, 15, ++i));
            //tree.Insert(new Numbers(14, 5, ++i));
            //tree.Insert(new Numbers(10, 15, ++i));
            //tree.Insert(new Numbers(10, 5, ++i));
            //tree.Print();
        }
    }
}
