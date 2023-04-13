using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Cidade
    {
        private int x;
        private int y;
        private int xG;
        private int yG;
        private int posicao;

        public int getPosicao()
        {
            return this.posicao;
        }
        public void setPosicao (int posicao)
        {
            this.posicao = posicao;
        }
        public int getX ()
        {
            return this.x;
        }
        public int getY()
        {
            return this.y;
        }
        public int getXG()
        {
            return this.xG;
        }
        public int getYG()
        {
            return this.yG;
        }
        public void setX (int x)
        {
            this.x = x;
        }
        public void setY(int y)
        {
            this.y = y;
        }
        public void setGrafico (int x, int y)
        {
            this.xG = (2 * x / 5) + 50;
            this.yG = 450 - (2 * y / 5);
        }
    }
}
