using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Exp03_Probability
{
    public partial class Form1 : Form
    {
        static double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }

        public double PDF1D(double x, double mu = 0, double sigma = 1)
         {
              return Math.Exp(-1 * Math.Pow((x - mu), 2)/(2 * Math.Pow(sigma, 2)) / (Math.Sqrt(2 * Math.PI) * sigma));;
         }

        public double CDF1D(double x, double mu = 0, double sigma = 1)
         {
              return (1 + Erf((x - mu) / Math.Sqrt(2) / sigma)) / 2;
         }

        public Form1()
        {
            InitializeComponent();
                                                      
            Form1_Load();
        }

        public void Form1_Load()
        {
            chart1.Series.Clear();
            Series Series1 = chart1.Series.Add("Type 1"); //새로운 series 생성
            Series1.ChartType = SeriesChartType.Column; //그래프 모양을 '선'으로 지정
            Series Series2 = chart1.Series.Add("Type 2"); //새로운 series 생성
            Series2.ChartType = SeriesChartType.Column; //그래프 모양을 '선'으로 지정
            /*
            Series Series3 = chart1.Series.Add("Type 3"); //새로운 series 생성
            Series3.ChartType = SeriesChartType.Column; //그래프 모양을 '선'으로 지정
            Series Series4 = chart1.Series.Add("Type 4"); //새로운 series 생성
            Series4.ChartType = SeriesChartType.Column; //그래프 모양을 '선'으로 지정
            */

            Series[] Ss = { Series1, Series2, /*Series3, Series4*/ };
            
            for (double i = 1; i <= 100; i+= 10)
            {
                for (int j = 0; j <= 1; j++)
                {
                    double mu = 8.0, sigma = (2 + (j - 2) * 0.5) * 5;
                    //Ss[j].Points.AddXY(i, (int)(PDF1D(i, mu, sigma) * 100));
                    Ss[j].Points.AddXY(i, (int)(CDF1D(i, mu, sigma) * 10));
                }
            }
        }
    }
}
