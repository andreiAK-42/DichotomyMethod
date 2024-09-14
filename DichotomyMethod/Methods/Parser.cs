using org.mariuszgromada.math.mxparser;
using OxyPlot.Series;
using OxyPlot;

namespace DichotomyMethod
{
    public class Graph
    {
        public static void GetFunction(MainWindow window, string function)
        {
            List<DataPoint> dot = new List<DataPoint>();

            Function func = new Function("f(x) = " +  function);

            for (int counterI = -15;  counterI <= 15; ++counterI)
            {
                Expression e1 = new Expression($"f({counterI})", func);
                dot.Add(new DataPoint(counterI, e1.calculate()));
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
                Color = OxyColor.FromRgb(0, 0, 255) // Синий цвет линии
            };

            // Добавляем все точки в серию
            lineSeries.Points.AddRange(dot);

            // Добавляем серию точек к модели графика
            plotModel.Series.Add(lineSeries);
            plotModel.Series.Add(medianLine);
            plotModel.Series.Add(absicc);

            // Отображаем график
            window.pvGraph.Model = plotModel;
            window.pvGraph.DataContext = plotModel;
        }      
    }
}
