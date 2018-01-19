using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStudentovPodlaPala
{
    class Otazka
    {
        public string Text;

              

        private Moznost[] moznosti = new Moznost[3];

        public Moznost[] Moznosti
        {
            get
            {
                return moznosti;
            }
            set
            {
                moznosti = value;
            }
        }

        public Moznost[] Odpovede
        {
            get;
            set;
        }

        public virtual void VypisOtazku()
        {
            Console.WriteLine(Text);
            Console.WriteLine("-------------------------");
            foreach (Moznost m in moznosti)
            {
                Console.WriteLine(m.Text);
            }
        }

        public virtual int vyhodnotOtazku()
        {
            return 0;
        }
       
    }

    class SingleOtazka: Otazka
    {

        public override int vyhodnotOtazku()
        {
            int cnt = this.Odpovede.Length;

            for (int i = 0; i < cnt; i++)
            {
                if (this.Odpovede[i].Spravnost) return 1;
            }

            return 0;
        }

        public override void VypisOtazku()
        {
            Console.WriteLine("Single");
            base.VypisOtazku();
        }

    }
    class MultiOtazka: Otazka
    {
        public override void VypisOtazku()
        {
            Console.WriteLine("Multi");
            base.VypisOtazku();
        }


        public override int vyhodnotOtazku()
        {

            int body = 0;
            int cnt = this.Odpovede.Length;

            for (int i = 0; i < cnt; i++)
            {
                
                if (this.Odpovede[i].Spravnost)
                {
                    body++;
                }
                else
                {
                    body--;
                }
            }

            return body;

        }


    }
}
