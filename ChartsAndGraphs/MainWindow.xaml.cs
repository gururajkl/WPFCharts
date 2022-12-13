using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization;
using dataVis = System.Windows.Forms.DataVisualization;
using LiveCharts;
using Excel;
using System.IO;
using CsvHelper;

namespace ChartsAndGraphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int c = 1;
            List<string> BatchColumn = new List<string>();
            List<string> TCBColumn = new List<string>();
            
            using (var fileReader = File.OpenText(@"C:\Users\ee212551\Desktop\TCBCSV.csv"))
            using (var csvResult = new CsvHelper.CsvReader(fileReader))
            {
                while (csvResult.Read())
                {
                    string BatchField = csvResult.GetField<string>("Batches");
                    string TcbField = csvResult.GetField<string>("TCB Status");
                    BatchColumn.Add(TcbField);
                    TCBColumn.Add(BatchField);
                }
            }

            Chart.Series.Clear();
            Chart.ChartAreas.Clear();
            Chart.Series.Add("S");
            Chart.ChartAreas.Add("A").AxisY.Title = "Progress";

            foreach (var i in TCBColumn)
            {
                Chart.Series["S"].Points.AddXY("Batches " + c++, i.ToString());
            }
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            switch (((RadioButton)sender).Name)
            {
                case "ColRadio":
                    Chart.Series[0].ChartType = dataVis.Charting.SeriesChartType.Column;
                    break;
                case "BarRadio":
                    Chart.Series[0].ChartType = dataVis.Charting.SeriesChartType.Bar;
                    break;
                case "PieRadio":
                    Chart.Series[0].ChartType = dataVis.Charting.SeriesChartType.Pie;
                    break;
                case "LineRadio":
                    Chart.Series[0].ChartType = dataVis.Charting.SeriesChartType.Line;
                    break;
                default: break;
            }
        }
    }
}
