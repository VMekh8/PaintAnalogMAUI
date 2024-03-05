using SkiaSharp;
using System.Diagnostics;

namespace PaintAnalogMAUI
{
    public partial class MainPage : ContentPage
    {
        DrawProperty drawProperty;

        public MainPage()
        {
            InitializeComponent();
            drawProperty = new DrawProperty();

            StrokePicker.SelectedIndexChanged += (o, e) =>
            {
                drawProperty._strokeWidth = Convert.ToInt32(StrokePicker.SelectedItem);
                Debug.WriteLine(drawProperty._strokeWidth);
            };
        }

        
       
    }

}
