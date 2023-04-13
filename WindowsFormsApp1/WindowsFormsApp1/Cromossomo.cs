using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Cromossomo
    {
        private double aptidao;
        private List<int> alelos = new List<int>();

        public Cromossomo() { }

        public double getAptidao()
        {
            return this.aptidao;
        }
        public void setAptidao(double aptidao)
        {
            this.aptidao = aptidao;
        }

        public List<int> getAlelos ()
        {
            return this.alelos;
        }
        
       public void setAlelos (List<int> alelos)
        {
            this.alelos = alelos;
        }

        public void addAlelo(int alelo) {
            this.alelos.Add(alelo);
        }

    }
}
