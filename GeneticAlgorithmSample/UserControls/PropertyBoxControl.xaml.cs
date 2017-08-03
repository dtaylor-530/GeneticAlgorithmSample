using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneticAlgorithmSample.Views
{
    /// <summary>
    /// Interaction logic for PropertyBoxControl.xaml
    /// </summary>
    public partial class PropertyBoxControl : Grid
    {
        public PropertyBoxControl()
        {
            InitializeComponent();
        }



        private void TextBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var txt = sender as TextBox;

            if (txt == null)
                return;

            int txtvalue;
            if (int.TryParse(txt.Text, out txtvalue))
            {
                txt.Text = (txtvalue + e.Delta / 10).ToString();
            }
        }

        private int ZIndex = 99;


    
    }
}
