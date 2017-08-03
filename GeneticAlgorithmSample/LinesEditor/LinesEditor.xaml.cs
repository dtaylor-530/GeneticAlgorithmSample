using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace WPFSamples.Samples.LinesEditor
{
    [Category("Highlights")]
    [Description("Add lines to a Canvas, and set their location, color, opacity, and thickness.\r\nThis example showcases the use of DataBinding, DataTemplating and ViewModels in WPF to keep the UI decoupled from the underlying data and logic, while still allowing a highly customizable UI, and the ability to represent the same data object in completely different ways.\r\nIt also shows how the WPF Lookless Control Mode allows complete customization of elements such as the ListBox in order to convert them in something that looks totally different while retaining their basic functionality such as Item selection.")]
  
    public partial class LinesEditor : Window
    {
       /// <summary>
        /// An ObservableCollection that will serve as ItemsSource for the ListBox
        /// </summary>
        public ObservableCollection<LineViewModel> Lines { get; set; }

        public LinesEditor()
        {
            InitializeComponent();

            Lines = new ObservableCollection<LineViewModel>(LinesDataSource.GetRandomLines());

            DataContext = Lines;
        }

        //private void AddLine_Click(object sender, RoutedEventArgs e)
        //{
        //    var newline = LinesDataSource.GetRandomLine(Lines.Count);
        //    Lines.Add(newline);
            
        //    lst.SelectedItem = newline;
        //}
        
        //private void RemoveLine_Click(object sender, RoutedEventArgs e)
        //{
        //    var item = lst.SelectedItem as LineViewModel;
        //    if (item != null)
        //    {
        //        item.StopTimer();
        //        Lines.Remove(item);
        //        lst.SelectedItem = null;
        //    }
        //}

        private void TextBox_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var txt = sender as TextBox;

            if (txt == null)
                return;

            int txtvalue;
            if (int.TryParse(txt.Text,out txtvalue))
            {
                txt.Text = (txtvalue + e.Delta / 10).ToString();
            }
        }

        private int ZIndex = 99;

        ////private void BringToTop_Click(object sender, RoutedEventArgs e)
        ////{
        ////    var line = lst.SelectedItem as LineViewModel;
        ////    if (line != null)
        ////    {
        ////        var listboxitem = lst.ItemContainerGenerator.ContainerFromItem(line) as ListBoxItem;
        ////        if (listboxitem != null)
        ////            Panel.SetZIndex(listboxitem,ZIndex++);
        ////    }
        ////}

        private void HyperLink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var hyperlink = sender as Hyperlink;
            if (hyperlink != null)
            {
                Process.Start(hyperlink.NavigateUri.AbsoluteUri);
            }
        }
    }
}
