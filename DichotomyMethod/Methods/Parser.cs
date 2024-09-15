using org.mariuszgromada.math.mxparser;
using OxyPlot.Series;
using OxyPlot;
using System.Windows;

namespace DichotomyMethod
{
    public class Graph
    {
        private List<DataPoint> Graphic = new List<DataPoint>();

        public void GetFunction(MainWindow window, string function)
        {
            Graphic.Clear();

            Function func = new Function("f(x) = " +  function);

            for (int counterI = Convert.ToInt32(window.tbXStart.Text);  counterI <= Convert.ToInt32(window.tbXEnd.Text); ++counterI)
            {
                org.mariuszgromada.math.mxparser.Expression e1 = new org.mariuszgromada.math.mxparser.Expression($"f({counterI})", func);
                Graphic.Add(new DataPoint(counterI, e1.calculate()));
            }

            var plotModel = new PlotModel { Title = "График функции f(x)" };

            var medianLine = new LineSeries
            {
                Title = "X",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2
            };

            medianLine.Points.Add(new DataPoint(-10, 0));
            medianLine.Points.Add(new DataPoint(10, 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, 10));
            absicc.Points.Add(new DataPoint(0, -10));


            // Создаем серию точек графика
            var lineSeries = new LineSeries
            {
                Title = "f(x)",
                Color = OxyColor.FromRgb(0, 0, 255), // Синий цвет линии
                LineStyle = LineStyle.Dot
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(Graphic);

            // Добавляем серию точек к модели графика
            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(medianLine);
            plotModel.Series.Add(absicc);

            // Отображаем график
            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;
        }      

        public void DichotomyMethod(MainWindow window)
        {
            Function func = new Function("f(x) = " + window.tbFunction.Text);
            org.mariuszgromada.math.mxparser.Expression eA = new org.mariuszgromada.math.mxparser.Expression($"f({window.tba.Text})", func);
            org.mariuszgromada.math.mxparser.Expression eB = new org.mariuszgromada.math.mxparser.Expression($"f({window.tbb.Text})", func);

            double fa = eA.calculate();
            double fb = eB.calculate();

            double a = Convert.ToDouble(window.tba.Text);
            double b = Convert.ToDouble(window.tbb.Text);
            double eps = Convert.ToDouble(window.tbe.Text);

            if (fa * fb > 0)
            {
                throw new ArgumentException("The function must have opposite signs at the endpoints of the interval.");
            }

            while (b - a > eps)
            {
                double c = (a + b) / 2;
                double fc = new org.mariuszgromada.math.mxparser.Expression($"f({c.ToString().Replace(",", ".")})", func).calculate();

                if (Math.Abs(fc) < eps)
                {
                    MessageBox.Show($"Ответ: {c}");
                    return;
                }
                else if (fa * fc < 0)
                {
                    b = c;
                }
                else
                {
                    a = c;
                    fa = fc;
                }
            }

            MessageBox.Show($"Ответ: {(a + b) / 2}");
            return;
        }
    }
}
