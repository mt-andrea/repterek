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
    class Palya
    {
        static int ID = 1;
        int id;
        int hossz, szelesseg;
        Anyag anyag;

        public Palya( int hossz, int szelesseg, Anyag anyag):this()
        {
            Hossz = hossz;
            Szelesseg = szelesseg;
            Anyag = anyag;
        }

        public Palya()
        {
            id = ID++;
        }

        public int getId()=> id; 
        public int Hossz { get => hossz;
            set {
                if (value<10)
                {throw new Exception("Adjon meg ervenyes hosszt!");
                }
                hossz = value; } }
        public int Szelesseg { get => szelesseg;
            set
            {
                if (value<16)
                {
                    throw new Exception("Tul keskeny palya!");
                }
                szelesseg = value;
            } }
        internal Anyag Anyag { get => anyag; set => anyag = value; }

        public Palya hosszabb(Palya p)
        {
            if (hossz>p.hossz)
            {
                return this;
            }return p;
        }
        public Palya rovidebb(Palya p)
        {
            if (hossz < p.hossz)
            {
                return this;
            }
            return p;
        }
        public int Terulet
        {
            get => hossz * szelesseg;
        }
        public Palya nagyobbTerulet(Palya p)
        {
            if (Terulet>p.Terulet)
            {
                return this;
            }return p;
        }
        public override string ToString()
        {
            return "" + hossz + "\t" + szelesseg + "\t\t" + anyag;
        }
    }
}
