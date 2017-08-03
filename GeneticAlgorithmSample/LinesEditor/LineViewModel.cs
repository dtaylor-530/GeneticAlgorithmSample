using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Collections.Generic;

namespace WPFSamples.Samples.LinesEditor
{
    /// <summary>
    /// This is the ViewModel for the lines.
    /// If you look closely, you will realize its 
    /// "Just Simple, Simple Properties and INotifyPropertyChanged"
    /// That's how you program in WPF. no need to manipulate complex classes such as UI elements or anything else.
    /// No need for complicated calculations of size, no Graphics.From() anything, 
    /// or doing any hacks such as owner draw or anything like that
    /// 
    /// Another important thing to note is that this class has no knowledge or dependency to any specific UI element or anything like that.
    /// Once again, it's "Just Simple, Simple Properties and INotifyPropertyChanged"
    /// </summary>
    public class LineViewModel : INotifyPropertyChanged
    {
        #region Timer-based Animation

        private System.Threading.Timer Timer;
        private static Random Rnd = new Random();

        private bool _animate;
        public bool Animate
        {
            get { return _animate; }
            set
            {
                _animate = value;
                NotifyPropertyChanged("Animate");
                if (value)
                    StartTimer();

                else
                    StopTimer();
            }
        }

        private int _animationSpeed = 1;
        public int AnimationSpeed
        {
            get { return _animationSpeed; }
            set
            {
                _animationSpeed = value;
                NotifyPropertyChanged("AnimationSpeed");
                if (Timer != null)
                    Timer.Change(0, 100/value);
            }
        }

        private static readonly List<int> _animationSpeeds = new List<int>{1,2,3,4,5};
        public List<int> AnimationSpeeds
        {
            get { return _animationSpeeds; }
        }

        private int counter;

        public void StartTimer()
        {
            StopTimer();
            Timer = new Timer(x =>
            {
                Timer_Tick(); counter++;
                if (counter == 30)  //or whatever your limit is
                    StopTimer();
            }, 
            
            null, 0, 100/AnimationSpeed);
        }

        public void StopTimer()
        {
            if (Timer != null)
            {
                Timer.Dispose();
                Timer = null;
            }

            counter = 0;
        }

        private void Timer_Tick()
        {
            X1 = X1 + Rnd.Next(-2, 3);
            Y1 = Y1 + Rnd.Next(-2, 3);
            X2 = X2 + Rnd.Next(-2, 3);
            Y2 = Y2 + Rnd.Next(-2, 3);
        }

        #endregion

        #region Coordinates

        private double _x1;
        public double X1
        {
            get { return _x1; }
            set
            {
                _x1 = value;
                NotifyPropertyChanged("X1");
            }
        }

        private double _y1;
        public double Y1
        {
            get { return _y1; }
            set
            {
                _y1 = value;
                NotifyPropertyChanged("Y1");
            }
        }

        private double _x2;
        public double X2
        {
            get { return _x2; }
            set
            {
                _x2 = value;
                NotifyPropertyChanged("X2");
            }
        }

        private double _y2;
        public double Y2
        {
            get { return _y2; }
            set
            {
                _y2 = value;
                NotifyPropertyChanged("Y2");
            }
        }

        #endregion

        #region Other Properties

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private double _thickness;
        public double Thickness
        {
            get { return _thickness; }
            set
            {
                _thickness = value;
                NotifyPropertyChanged("Thickness");
            }
        }

        public Color Color1 { get; set; }
        public Color Color2 { get; set; }

        private double _opacity = 1;

        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                NotifyPropertyChanged("Opacity");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
                Animate = value;

            }
}


        #endregion

            #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }));
        }

        #endregion
    }
}