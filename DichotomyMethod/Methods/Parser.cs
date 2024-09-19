using org.mariuszgromada.math.mxparser;
using OxyPlot.Series;
using System.Windows;
using OxyPlot;

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

            medianLine.Points.Add(new DataPoint(Convert.ToInt32(window.tbXStart.Text), 0));
            medianLine.Points.Add(new DataPoint(Convert.ToInt32(window.tbXEnd.Text), 0));

            var absicc = new LineSeries
            {
                Title = "Y",
                Color = OxyColor.FromRgb(255, 0, 0), // Красный цвет
                StrokeThickness = 2,
            };

            absicc.Points.Add(new DataPoint(0, Convert.ToInt32(window.tbXEnd.Text)));
            absicc.Points.Add(new DataPoint(0, Convert.ToInt32(window.tbXStart.Text)));


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

            double fa = SolveFunc(func, window.tba.Text);
            double fb = SolveFunc(func, window.tbb.Text);

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
                double fc = SolveFunc(func, c.ToString().Replace(",", "."));

                if (Math.Abs(fc) == 0)
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

        public double SolveFunc(Function function, string x)
        {
            return new org.mariuszgromada.math.mxparser.Expression($"f({x})", function).calculate();
        }
    }
}
