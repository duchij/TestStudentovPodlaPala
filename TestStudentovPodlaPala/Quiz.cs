using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStudentovPodlaPala
{
    class Quiz
    {
        private Otazka[] otazky;

        public Quiz()
        {
            otazky = new Otazka[2];

            Random r = new Random();
            DBOtazok db = new DBOtazok();
            ArrayList vybraneCisla = new ArrayList();

            for (int i=0; i<2 ; i++)
            {
                int index;
                do
                {
                    index = r.Next(3);
                }
                while (vybraneCisla.Contains(index));

                otazky[i] = (Otazka)db.Otazky[index];
                vybraneCisla.Add(index);

            }
        }

        public void SpustiQuiz()
        {

           // bool st = true;
            int defBody = 0;
            foreach(Otazka o in otazky)
            {
                string uzivOdpoved;
                int[] poleUzivIndexov;
                o.VypisOtazku();

                do
                {

                    uzivOdpoved = Console.ReadLine();
                    Console.WriteLine("vasa odppoved {0}",uzivOdpoved);

                   

                } while (!skontrolujVstup(uzivOdpoved, o, out poleUzivIndexov));

                int cnt = poleUzivIndexov.Length;

                o.Odpovede = new Moznost[cnt];

                for (int index=0; index<cnt; index++)
                {
                    o.Odpovede[index] = o.Moznosti[poleUzivIndexov[index] - 1];
                }

                defBody += o.vyhodnotOtazku();


            }

            Console.WriteLine("Pocet bodov je {0}", defBody);
            Console.ReadLine();
        }

        private bool skontrolujVstup(string uzivVstup, Otazka otazka, out int[] poleIndexov)
        {
            int index;

            if (uzivVstup == String.Empty)
            {
                poleIndexov = new int[0];
                return false;
            }

            if(otazka is SingleOtazka)
            {
                bool res =  jeCisloAJeVIndexe(uzivVstup, otazka, out index);
                poleIndexov = new int[] { index };
                if (!res) Console.Write("Zle zadane S");
                return res;
            }
            else
            {
                string[] poleOdpovediUzivatela = uzivVstup.Split(' ');

                poleIndexov = new int[poleOdpovediUzivatela.Length];

                for(int i = 0; i < poleOdpovediUzivatela.Length;i++)
                {
                    if (!jeCisloAJeVIndexe(poleOdpovediUzivatela[i], otazka, out index))
                    {

                        Console.WriteLine("Zly vstup M");
                        return false;
                    }

                        
                    poleIndexov[i] = index;
                }

                return true;
            }
        }

        private bool jeCisloAJeVIndexe(string uzivVstup, Otazka otazka, out int index)
        {

            bool jeCislo = int.TryParse(uzivVstup, out index);

            if (!jeCislo)
            {
                return false;
            }
            else
            {
                return index + 1 > 0 && index + 1 < otazka.Moznosti.Length+1;
            }
        }
    }
}
