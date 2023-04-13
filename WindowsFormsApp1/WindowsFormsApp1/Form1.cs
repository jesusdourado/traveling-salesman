using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> cidadesGrafico = new List<string>();
        List<Cromossomo> populacao = new List<Cromossomo>();
        List<Cidade> cidades = new List<Cidade>();

        int flag = 0;
        int parada = 10;
        int quantCidades = 0;
        int quantPopulacao = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            botaoCriarCidades();
            button3.Enabled = true;
            button6.Enabled = false;
        }

        private void botaoCriarCidades ()
        {
            listBox4.Items.Clear();
            cidades.Clear();
            criarCidades();
            desenharBase();
            desenharGrafico();
        }

        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        //Criando cidades aleatórias e armazemando em uma lista
        private void criarCidades ()
        {
            Random x = new Random();
            quantCidades = Convert.ToInt32(textBox1.Text);

            listBox1.Items.Clear();
            for(int i = 0; i < quantCidades; i ++)
            {
                Cidade city = new Cidade();
                city.setX(x.Next(1000));
                city.setY(x.Next(1000));
                city.setGrafico(city.getX(), city.getY());
                city.setPosicao(i + 1);
                cidades.Add(city);
                listBox1.Items.Add("Cidade " + Convert.ToString((i + 1)) + "- X:" + Convert.ToString(city.getX()) + ", Y:" + Convert.ToString(city.getY()));
            }
        }

        private void desenharGrafico()
        { 
            //Declarando as ferramentas para o gráfico
            System.Drawing.Pen myPen;
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush myBrushBackGround = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            for (int i = 0; i < quantCidades; i ++)
            {
                formGraphics.FillRectangle(myBrush, (cidades[i].getXG() - 5), (cidades[i].getYG() - 5), 10, 10);
            }

            myPen.Dispose();
            formGraphics.Dispose();
            
        }

        private void desenharBase ()
        {
            System.Drawing.Pen myPen;
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush myBrushBackGround = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();

            //Desenhando o BackGround do gráfico
            //formGraphics.Clear(Color.DimGray);
            formGraphics.FillRectangle(myBrushBackGround, 30, 30, 440, 440);
            formGraphics.DrawLine(myPen, 50, 470, 50, 30);
            formGraphics.DrawLine(myPen, 30, 450, 470, 450);
        }

        private void desenhandoRota(List<int> cromo)
        {
            System.Drawing.Pen myPen = myPen = new System.Drawing.Pen(System.Drawing.Color.BlueViolet);
            System.Drawing.Graphics formGraphics = this.CreateGraphics();


            int x0 = cidades[cromo[0] - 1].getXG();
            int y0 = cidades[cromo[0] - 1].getYG();
            int x2, y2;

            int x1 = x0, y1 = y0;
            for(int i = 1; i < quantCidades; i ++)
            {
                x2 = cidades[cromo[i] - 1].getXG();
                y2 = cidades[cromo[i] - 1].getYG();
                formGraphics.DrawLine(myPen, x1, y1, x2, y2);
                x1 = x2;
                y1 = y2;

            }
            formGraphics.DrawLine(myPen, x0, y0, x1, y1);

            desenharGrafico();

            myPen.Dispose();
            formGraphics.Dispose();
        }
        
        //Função que gera uma população de cromossomos aleatórios
        private void gerarPopulacao ()
        {
            quantPopulacao = Convert.ToInt32(textBox2.Text);
            quantCidades = Convert.ToInt32(textBox1.Text);
            Random x = new Random();
            populacao.Clear();

            string result = "";
            int numAtual;
            listBox3.Items.Clear();
            for (int i = 0; i < quantPopulacao; i++)
            {
                Cromossomo atual = new Cromossomo();
                numAtual = x.Next(1, quantCidades + 1);
                result = Convert.ToString(numAtual) + "-";
                atual.addAlelo(numAtual);
                for (int i2 = 1; i2 < quantCidades; i2++)
                {
                    numAtual = x.Next(1, quantCidades + 1);
                    while (numNaLista(numAtual, atual.getAlelos()) != false)
                    {
                        numAtual = x.Next(1, quantCidades + 1);
                    }
                    result = result + Convert.ToString(numAtual) + "-";
                    atual.addAlelo(numAtual);
                }
                listBox3.Items.Add(result);
                populacao.Add(atual);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            gerarPopulacao();
            button6.Enabled = true;
        }

        private bool numNaLista(int num, List<int> lista)
        {
            for(int i = 0; i < lista.Count(); i ++)
            {
                if (lista[i] == num)
                    return true;
            }
            return false;
        }

        private List<int> crossoverPonto (List<int> pai, List<int> mae, int opcao)
        {
            Random x = new Random();
            List<int> filho = new List<int>();
            int pontoUnico = x.Next(1, (quantCidades - 2));
            bool flag = false;
            
            if(opcao == 1)
            {
                for(int i = 0; i <= pontoUnico; i ++)
                {
                    filho.Add(pai[i]);
                }
                
                for(int i2 = 0; i2 < quantCidades; i2 ++) //Percorrendo toda a mãe
                {
                    if (elementoNaLista(filho, mae[i2]) != true)
                        filho.Add(mae[i2]);
                }
            }
            else if (opcao == 2)
            {
                for (int i = 0; i <= pontoUnico; i++)
                {
                    filho.Add(mae[i]);
                }
                for (int i = (pontoUnico + 1); i < quantCidades; i++)
                {
                    filho.Add(pai[i]);
                }
            }
            listBox2.Items.Clear();
            listBox2.Items.Add(pontoUnico + 1);
            listBox2.Items.Add(lista2String(pai));
            listBox2.Items.Add(lista2String(mae));
            listBox2.Items.Add(lista2String(filho));
            return filho;
        }

        private string lista2String (List<int> lista)
        {
            string result = "";
            for(int i = 0; i < lista.Count(); i ++)
            {
                result = result + lista[i] + "-";
            }
            return result;
        }

        private bool elementoNaLista(List<int> lista, int elemento)
        {
            bool result = false;
            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i] == elemento)
                {
                    result = true;
                }
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            crossoverPonto(populacao[0].getAlelos(), populacao[1].getAlelos(), 1);
            listBox2.Items.Add("pai1 - " +lista2String(populacao[0].getAlelos()));
            listBox2.Items.Add("pai2 - " + lista2String(populacao[1].getAlelos()));
        }

        //Função de aptidão (Distância total da rota)

        private double aptidao (Cromossomo cromo)
        {
            int x0 = cidades[cromo.getAlelos()[0] - 1].getX();
            int y0 = cidades[cromo.getAlelos()[0] - 1].getY();
            int x1, x2, y1, y2;
            double distancia = 0;

            x1 = x0;
            y1 = y0;
            for(int i = 1; i < quantCidades; i ++)
            {
                x2 = cidades[cromo.getAlelos()[i] - 1].getX();
                y2 = cidades[cromo.getAlelos()[i]- 1].getY();

                distancia = distancia + dist2Pontos(x1, y1, x2, y2);
                x1 = x2;
                y1 = y2;
            }
            distancia = distancia + dist2Pontos(x1, y1, x0, y0);
            return distancia;
        }

        //Função distância euclidiana entre 2 pontos
        private double dist2Pontos (int x1, int y1, int x2, int y2)
        {
            int x = modulo(x1 - x2);
            int y = modulo(y1 - y2);
            return Math.Pow(Math.Pow(x, 2) + Math.Pow(y, 2), (0.5));
        }

        //Função módulo de um inteiro
        private int modulo (int x)
        {
            if (x < 0)
                return (x * (-1));
            else
                return x;
        }

        //Função que atualiza o parâmetro aptidão de todos os cromossomos da população
        private void atualizaAptidaoPop()
        {
            listBox3.Items.Clear();
            for(int i = 0; i < populacao.Count; i++)
            {
                populacao[i].setAptidao(aptidao(populacao[i]));
                listBox3.Items.Add(populacao[i].getAptidao());
            }
        }

        //Função bolha para organizar aptidões da população
        private void bolhaPopulacao()
        {
            Cromossomo aux = new Cromossomo();
            for (int i = 0; i < populacao.Count; i++) { 
                for (int i1 = 0; i1 < populacao.Count; i1++)
                {
                    for (int i2 = i1; i2 < (populacao.Count - 1); i2++)
                    {
                        aux = populacao[i2];

                        if (aux.getAptidao() > populacao[i2 + 1].getAptidao())
                        {
                            populacao[i2] = populacao[i2 + 1];
                            populacao[i2 + 1] = aux;
                        }
                    }
                }
            }
        }

        private void NovaGeracao ()
        {
            Cromossomo filho1 = new Cromossomo();
            Cromossomo filho2 = new Cromossomo();

            filho1.setAlelos(crossoverPonto(populacao[0].getAlelos(), populacao[1].getAlelos(), 1));
            filho2.setAlelos(crossoverPonto(populacao[1].getAlelos(), populacao[1].getAlelos(), 2));
            //|
            if ((compararParente(filho1, populacao[0])) | (compararParente(filho1, populacao[1])))
                filho1 = mutacaoSimples(filho1);
            if ((compararParente(filho2, populacao[0])) | (compararParente(filho2, populacao[1])))
                filho2 = mutacaoSimples(filho2);

            listBox2.Items.Clear();
            listBox2.Items.Add("Pai: " + lista2String(populacao[0].getAlelos()));
            listBox2.Items.Add("Mãe: " + lista2String(populacao[1].getAlelos()));
            listBox2.Items.Add("Filho1: " + lista2String(filho1.getAlelos()));
            listBox2.Items.Add("Filho2: " + lista2String(filho2.getAlelos()));

            populacao.Remove(populacao[populacao.Count - 1]);
            populacao.Remove(populacao[populacao.Count - 1]);
            populacao.Add(filho1);
            populacao.Add(filho2);
            atualizaAptidaoPop();
            bolhaPopulacao();
        }

        private Cromossomo mutacaoSimples (Cromossomo x)
        {
            Random a = new Random();
            int val1, val2, aux;

            val1 = a.Next(0, x.getAlelos().Count);
            val2 = a.Next(0, x.getAlelos().Count);

            while(val2 == val1)
            {
                val2 = a.Next(0, x.getAlelos().Count);
            }

            aux = x.getAlelos()[val1];
            x.getAlelos()[val1] = x.getAlelos()[val2];
            x.getAlelos()[val2] = aux;
            return x;
        }

        private bool compararParente(Cromossomo pai, Cromossomo filho)
        {
            bool result = true;
            for(int i = 0; i < pai.getAlelos().Count; i ++)
            {
                if(filho.getAlelos()[i] != pai.getAlelos()[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void imprimirPopulacao()
        {
            listBox4.Items.Add("Melhor: " + populacao[0].getAptidao());
            listBox4.SelectedItem = listBox4.Items.Count - 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            atualizaAptidaoPop();
            bolhaPopulacao();
            imprimirPopulacao();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Geracao();
        }

        private void Geracao ()
        {
            NovaGeracao();
            desenharBase();
            desenhandoRota(populacao[0].getAlelos());
            desenharGrafico();
            imprimirPopulacao();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Geracao();
            if(flag > parada)
            {
                button1.Enabled = true;
                button2.Enabled = true;
                timer1.Enabled = false;
            }

            flag++;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            flag = 1;
            parada = Convert.ToInt32(textBox3.Text);
            botaoCriarCidades();
            gerarPopulacao();
            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
        }
    }
}
