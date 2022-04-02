/*
 * Made by Andrea Mate.
 * For practice.
 * This is the way!
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;


/**
 *
 * @author Máté Andrea
 */

namespace repterek
{
    class Program
    {
        static List<Repter> ReadFile(string fileName) /* itt szeretnek optimalizalashoz segitseget*/
        {
            List<Repter> repterek = new List<Repter>();
            StreamReader sr = new StreamReader(fileName, Encoding.UTF8);
            sr.ReadLine();
            string[] data;
            while (!sr.EndOfStream)
            {
                data = sr.ReadLine().Split(',');
                if (!repterek.Any<Repter>(repter => repter.Nev == data[0]))
                { repterek.Add(new Repter(data[0])); }
            }
            sr.Close();

            foreach (Repter item in repterek)
            {
                sr = new StreamReader(fileName, Encoding.UTF8);
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split(',');
                    if (data[0].Equals(item.Nev))
                    {
                        item.AddPalya(
                            int.Parse(data[1]),
                            int.Parse(data[2]),
                            (Anyag)Enum.Parse(typeof(Anyag), data[3]));
                    }
                } sr.Close();
            }
            return repterek;
        }
        static void osszesRepter(List<Repter> repters)
        {
            foreach (Repter item in repters)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }
        static void legtobbPalya(List<Repter> repters){
            Repter kivalasztott = repters[0];
            int c = kivalasztott.palyakSzama;
            foreach (Repter item in repters)
            {
                kivalasztott = item.tobbPalya(kivalasztott,out c);
            }
            Console.WriteLine($"A legtobb palyaja a(z) {kivalasztott.Nev} repternek van {c} palyaval.\n");
            Console.WriteLine();
        }
        static void repterekTerulete(List<Repter> repters)
        {
            foreach (Repter item in repters)
            {
                int terulet = 0;
                foreach (Palya itemP in item.Palyak)
                {
                    terulet += itemP.Terulet;
                }
                Console.WriteLine($"A(z) {item.Nev} repter terulete \t{Math.Round(terulet/1000.0,2)} negyzetkilometer.");
            }
            Console.WriteLine();
        }
        static void legnagyobbRepter(List<Repter> repters)
        {
            int maxTer = int.MinValue;
            Repter kivalasztott=repters[0];
            foreach (Repter item in repters)
            {
                int terulet = 0;
                foreach (Palya itemP in item.Palyak)
                {
                    terulet += itemP.Terulet;
                }
                if (terulet > maxTer)
                {
                    maxTer = terulet;
                    kivalasztott = item;
                }
            }
            Console.WriteLine($"A legnagyobb teruletu repter: {kivalasztott.Nev}, {Math.Round(maxTer / 1000.0,2)} negyzetkilometeres terulettel.\n");
            Console.WriteLine();
        }
        static void leghosszabbPalya_repter(List<Repter> repters,bool hosszu)
        {
            foreach (Repter item in repters)
            {
                

                if (hosszu)
                {
                    Palya kivalasztott=new Palya(100,16,Anyag.beton);
                    foreach (Palya itemP in item.Palyak)
                    {
                        kivalasztott = itemP.hosszabb(kivalasztott);
                    }
                    Console.WriteLine($"A(z) {item.Nev} repter leghosszabb palyaja: \n{kivalasztott}.");

                }
                else
                {
                    Palya kivalasztott = new Palya(int.MaxValue, 16, Anyag.beton);
                    foreach (Palya itemP in item.Palyak)
                    {
                        kivalasztott = itemP.rovidebb(kivalasztott);
                    }
                    Console.WriteLine($"A(z) {item.Nev} repter legrovidebb palyaja: \n\t{kivalasztott}.");
                }
                
            }

        }
        static void leghosszabb(List<Repter> repters, Anyag anyag, bool hosszu)
        {

            if (hosszu)
            {
                Repter kival_r = new Repter("teszt");
                kival_r.AddPalya(10, 16, Anyag.beton);
                Palya kival_p = kival_r.Palyak[0];

                foreach (Repter item in repters)
                {
                    foreach (Palya itemp in item.Palyak)
                    {
                        if (itemp.Anyag == anyag)
                        {
                            kival_p = itemp.hosszabb(kival_p);
                        }
                    }
                    if (!kival_r.Palyak.Contains(kival_p))
                    {
                        kival_r = item;
                    }
                }
                Console.WriteLine($"A leghosszabb {anyag} palya: \n{kival_r.Nev}\t{kival_p}");
            }

            else
            {
                Repter kival_r = new Repter("teszt");
                kival_r.AddPalya(int.MaxValue, 16, Anyag.beton);
                Palya kival_p = kival_r.Palyak[0];

                foreach (Repter item in repters)
                {
                    foreach (Palya itemp in item.Palyak)
                    {
                        if (itemp.Anyag == anyag)
                        {
                            kival_p = itemp.rovidebb(kival_p);
                        }
                    }
                    if (!kival_r.Palyak.Contains(kival_p))
                    {
                        kival_r = item;
                    }
                }
                Console.WriteLine($"A legrovidebbs {anyag} palya: \n{kival_r.Nev}\t{kival_p}");
            }
            Console.WriteLine();
        }
        static void osszegzes(List<Repter> repters,string avg)
        {
            int s = 0;
            double c = 0;
            int ter = 0;
            foreach (Repter item in repters)
            {
                foreach (Palya itemp in item.Palyak)
                {
                    s += itemp.Hossz;
                    c++;
                    ter += itemp.Terulet;
                }
            }
            switch (avg)
            {
                case "osszh":
                    Console.WriteLine($"Az ossz palyahossz: {s}");
                    break;
                case "osszt":
                    Console.WriteLine($"Az ossz palyaterulet: {ter}");
                    break;
                case "avgt":
                    Console.WriteLine($"Az palyak atlagos terulete: {Math.Round((double)ter / c, 2)}");
                    break;
                default:
                    Console.WriteLine($"Az atlagos palyahossz: {Math.Round((double)s / c, 2)}");
                    break;
            }
        }
        static void Main(string[] args)
        {
            List<Repter> repterek = ReadFile("palyak.csv");
            bool run = true;
            while (run)
            {
                int valasz;
                do
                {
                    Console.WriteLine("Kerem valasszon az alabbi menupontokbol: \n\t1 - osszes repter kilistazasa\n\t2 - repter a legtobb palyaval\n\t3 - repterek terulete\n\t4 - a legnagyobb teruletu repter\n\t5 - repterenkent a legrovidebb palya\n\t6 - repterenkent a leghosszabb palya\n\t7 - leghosszabb fuves palya\n\t8 - leghosszabb betonos palya\n\t9 - legrovidebb fuves palya\n\t10 - legrovidebb betonos palya\n\t11 - atlagos palya hossz\n\t12 - ossz palyahossz\n\t13 - ossz repter terrulet\n\t14 - palyak atlagos terulete\n\t0 - kilepes");
                    
                } while (!int.TryParse(Console.ReadLine(), out valasz));
                switch (valasz)
                {
                    case 1:
                        osszesRepter(repterek);
                        break;
                    case 2:
                        legtobbPalya(repterek);
                        break;
                    case 3:
                        repterekTerulete(repterek);
                        break;
                    case 4:
                        legnagyobbRepter(repterek);
                        break;
                    case 5:
                        leghosszabbPalya_repter(repterek, false); 
                        break;
                    case 6:
                        leghosszabbPalya_repter(repterek, true);
                        break;
                    case 7:
                        leghosszabb(repterek, Anyag.fű,true);
                        break;
                    case 8:
                        leghosszabb(repterek, Anyag.beton,true);
                        break;
                    case 9:
                        leghosszabb(repterek, Anyag.fű,false);
                        break;
                    case 10:
                        leghosszabb(repterek, Anyag.beton, false);
                        break;
                    case 11:
                        osszegzes(repterek,"");
                        break;
                    case 12:
                        osszegzes(repterek, "osszh"); break;
                    case 13:
                        osszegzes(repterek, "osszt"); break;
                    case 14:
                        osszegzes(repterek, "avgt"); break;
                    default:
                        run = false;
                        break;
                }
            }
        }
    }
}
