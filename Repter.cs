/*
 * Made by Andrea Mate.
 * For practice.
 * This is the way!
 */
using System;
using System.Collections.Generic;
using System.Text;
/**
 *
 * @author Máté Andrea
 */
namespace repterek
{
    class Repter
    {
        string nev;
        List<Palya> palyak;

        public Repter(string nev)
        {
            this.Nev = nev;
            this.palyak = new List<Palya>();
        }

        
        public int palyakSzama
        {
            get
            {
                return Palyak.Count;
            }
        }
        public void AddPalya(int hossz, int szelesseg, Anyag anyag)
        {
            palyak.Add(new Palya(hossz, szelesseg, anyag));
        }
        internal List<Palya> Palyak
        {
            get
            {
                List<Palya> temp = new List<Palya>();
                if (palyak.Count == 0)
                {
                    throw new Exception("Nincs palya!");
                }
                foreach (Palya item in palyak)
                {
                    temp.Add(item);
                }
                return temp;
            }
        }

        public string Nev { get => nev;
            set { 
                if (value.Length < 4)
            {
                throw new Exception("Tul rovid nev!");
            }
            nev = value;
            }
        }

        public Repter tobbPalya(Repter repter, out int c)
        {
            if (palyakSzama>repter.palyakSzama)
            {
                c = palyakSzama;
                return this;
            }
            c = repter.palyakSzama;
            return repter;
        }
        public override string ToString()
        {
            string txt = "A(z) " + Nev + " repter palyai: szamuk=" + palyakSzama+"\nhossz\tszelesseg\tanyag\n";
            foreach (Palya item in Palyak)
            {
                txt +=item.ToString()+"\n";
            }
            return txt;
        }
    }
}
