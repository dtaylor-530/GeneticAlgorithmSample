using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace GeneticAlgorithmSample.ViewModels
{

    public class MainViewModel : System.ComponentModel.INotifyPropertyChanged
    {


        public ObservableCollection<WPFSamples.Samples.LinesEditor.LineViewModel> Lines { get; set; }


        private WPFSamples.Samples.LinesEditor.LineViewModel selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public WPFSamples.Samples.LinesEditor.LineViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }


        public MainViewModel()
        {

            // Lines = new ObservableCollection<WPFSamples.Samples.LinesEditor.LineViewModel>(WPFSamples.Samples.LinesEditor.LinesDataSource.GetRandomLines());

            Lines = new ObservableCollection<WPFSamples.Samples.LinesEditor.LineViewModel>();
            Lines.CollectionChanged += RegisterPropertyChanges;

            foreach (WPFSamples.Samples.LinesEditor.LineViewModel line in Lines)
            {
                RegisterPropertyChanges(line, null);

            }

            // Lines.(x => { x.PropertyChanged("IsSelected") += dffsd);
        }





       public void RegisterPropertyChanges(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender is WPFSamples.Samples.LinesEditor.LineViewModel)
            {
                (sender as WPFSamples.Samples.LinesEditor.LineViewModel).PropertyChanged += Line_PropertyChanged;
            }
            else
            {
                foreach (WPFSamples.Samples.LinesEditor.LineViewModel line in e.NewItems)
                    line.PropertyChanged += Line_PropertyChanged;
            }
        }

        private void Line_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                SelectedItem = sender as WPFSamples.Samples.LinesEditor.LineViewModel;
            }

        }

        private void NotifyPropertyChanged(string propertyName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }));
        }
    }


}