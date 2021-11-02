using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatPuzzlevaniaDuel
{
    class Program
    {
        static void Main(string[] args)
        {
            Duelist[] duelees = new Duelist[3];
            int a_dubs = 0;
            int b_dubs = 0;
            int c_dubs = 0;

            int trials = 10000;
            int s = 0;

            while(trials > 0)
            {
                duelees[0] = new Duelist("Aaron", 33.33);
                duelees[1] = new Duelist("Bob", 50);
                duelees[2] = new Duelist("Charlie", 99.5);

                while (true)
                {
                    if (duelees[s].IsAlive())
                    {
                        int threat = -1;
                        for (int t = 0; t < 3; t++)
                        {
                            if (t != s && duelees[t].IsAlive() && (threat < 0 || duelees[t].Accuracy > duelees[threat].Accuracy))
                            {
                                threat = t;
                            }
                        }
                        if (threat > -1)
                        {
                            Duel(duelees[s], duelees[threat]);
                        }
                        else
                        {
                            if (s == 0)
                                a_dubs++;
                            else if (s == 1)
                                b_dubs++;
                            else
                                c_dubs++;

                            trials--;
                            break;
                        }
                    }
                    s++;
                    if (s > 2)
                        s = 0;
                }
            }

            Console.WriteLine("Aaron won " + a_dubs + "/10000, or %" + a_dubs / 100f + " of the time!");
            Console.WriteLine("Bob won " + b_dubs + "/10000, or %" + b_dubs / 100f + " of the time!");
            Console.WriteLine("Charlie won " + c_dubs + "/10000, or %" + c_dubs / 100f + " of the time!");

            Console.Read();
        }

        public static string Duel(Duelist shooter, Duelist target)
        {
            shooter.ShootAtDuelist(target);
            if (!target.IsAlive())
                return shooter.Name;
            else
                return "Miss";
        }
    }

    class Duelist
    {
        string name;
        public string Name { get { return name; } set { name = value; } }


        double accuracy;
        public double Accuracy { get { return accuracy; } set { accuracy = value; } }


        bool alive;
        public bool IsAlive() { return alive; }
        public void KillDuelist() { alive = false; }

        public Duelist()
        {
            name = "";
            accuracy = 0;
            alive = true;
        }

        public Duelist(string _name, double _accuracy)
        {
            name = _name;
            accuracy = _accuracy;
            alive = true;
        }

        public void ShootAtDuelist(Duelist target)
        {
            double hit_chance = new Random().NextDouble() * 99;
            if (hit_chance <= accuracy)
                target.KillDuelist();
        }
    }
}
