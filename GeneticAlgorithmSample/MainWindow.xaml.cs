using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GeneticAlgorithmSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public ObservableCollection<WPFSamples.Samples.LinesEditor.LineViewModel> Lines { get; set; }

        public ViewModels.MainViewModel MainViewModel { get; set; }
        public GeneticAlgorithmWrapper GeneticAlgorithm { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainViewModel = new ViewModels.MainViewModel();

            this.DataContext = MainViewModel;


            MainViewModel.Lines.Add(new WPFSamples.Samples.LinesEditor.LineViewModel()
            {

            });

  

            MainViewModel.SelectedItem = MainViewModel.Lines[0];
        }

      


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GeneticAlgorithm = new GeneticAlgorithmWrapper(ViewBox1.lst.ActualHeight, ViewBox1.lst.ActualWidth);
            GeneticAlgorithm.NotifyOfImprovement += GeneticAlgorithm_NotifyOfImprovement;
            GeneticAlgorithm.Run();

        }

        private void GeneticAlgorithm_NotifyOfImprovement(Point arg1, Point arg2)
        {
    

            MainViewModel.Lines.Add(new WPFSamples.Samples.LinesEditor.LineViewModel()
            {
                X1 = arg1.X,
                X2 = arg2.X,
                Y1 = arg1.Y,
                Y2 = arg2.Y,
                Thickness = 6,
                Opacity = FitnessToOpacity(GeneticAlgorithm.latestFitness),
                //Color1 = Color.FromRgb(50, 100, 250),
                AnimationSpeed = 5,
                Animate = false,
                Name = GeneticAlgorithm.GenerationNumber.ToString()
             
        });
            
        }

        private double FitnessToOpacity(double fitness)
        {
            return 0.1 + 0.9*(fitness/10000);

        }
    }
}
