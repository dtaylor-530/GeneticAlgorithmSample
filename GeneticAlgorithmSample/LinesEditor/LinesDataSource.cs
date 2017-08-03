using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Media;


namespace WPFSamples.Samples.LinesEditor
{
    /// <summary>
    /// This acts as a DataSource for the example. As you can see, its returning instances of the
    /// LineViewModel class, which actually contains regular int and double properties.
    /// Therefore, it could be perfectly replaced by a DB data source, 
    /// with no need to modify any part of the XAML or the rest of the applicaton.
    /// </summary>
    public class LinesDataSource
    {
        public static Random rnd = new Random();

        public static List<LineViewModel> GetRandomLines()
        {
            return Enumerable.Range(0, rnd.Next(10, 20)).Select(GetRandomLine).ToList();
        }

        public static LineViewModel GetRandomLine(int index)
        {
            var colors = typeof (Colors).GetProperties().Select(x => (Color) x.GetValue(null, null)).ToList();

            return new LineViewModel()
                {
                    Name = "Line" + index.ToString(),
                    X1 = rnd.Next(0, 500),
                    X2 = rnd.Next(0, 500),
                    Y1 = rnd.Next(0, 500),
                    Y2 = rnd.Next(0, 500),
                    Opacity = .7,
                    Color1 = colors[rnd.Next(0, colors.Count - 1)],
                    Color2 = colors[rnd.Next(0, colors.Count - 1)],
                    Thickness = rnd.Next(5, 10),
                    AnimationSpeed = 5,
                    Animate = false
                };
        }
    }
}