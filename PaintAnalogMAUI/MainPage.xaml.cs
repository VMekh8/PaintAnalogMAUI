using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System.Diagnostics;

namespace PaintAnalogMAUI
{
    public partial class MainPage : ContentPage
    {
        List<SKPaint> paints = new List<SKPaint>();
        SKPaint currentPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Gray,
            StrokeWidth = 10
        };
        private List<List<SKPoint>> lines = new List<List<SKPoint>>();
        private List<SKPoint> currentLine;

        public MainPage()
        {
            InitializeComponent();
            canvasView.EnableTouchEvents = true;
            canvasView.Touch += OnScreenTouch;

            StrokePicker.SelectedIndexChanged += (o, e) =>
            {
                currentPaint.StrokeWidth = Convert.ToInt32(StrokePicker.SelectedItem);
                Debug.WriteLine(currentPaint.StrokeWidth);
            };

            StylePicker.SelectedIndexChanged += (o, e) =>
            {
                currentPaint.Style = (SKPaintStyle)Enum.Parse(typeof(SKPaintStyle), (string)StylePicker.SelectedItem);
                Debug.WriteLine(currentPaint.Style.ToString());
            };

            ChangeColorBtn.Clicked += (o, e) =>
            {
                currentPaint.Color = ColorPicker.PickedColor.ToSKColor();
                Debug.WriteLine(currentPaint.StrokeWidth);
            };

            ClearBtn.Clicked += (o, e) =>
            {
                lines.Clear();
                paints.Clear();
                canvasView.InvalidateSurface();
            };

            ChangeBackBtn.Clicked += (o, e) =>
            {
                canvasView.BackgroundColor = ColorPicker.PickedColor;
            };


        }

        private void OnScreenTouch(object sender, SKTouchEventArgs args)
        {
            if (args.InContact)
            {
                switch (args.ActionType)
                {
                    case SKTouchAction.Pressed:
                        {
                            currentLine = new List<SKPoint>();
                            lines.Add(currentLine);
                            paints.Add(currentPaint.Clone());
                            currentLine.Add(args.Location);
                            ((SKCanvasView)sender).InvalidateSurface();
                            break;
                        }
                    case SKTouchAction.Moved:
                        {
                            currentLine.Add(args.Location);
                            ((SKCanvasView)sender).InvalidateSurface();
                            break;
                        }
                }
            }

            args.Handled = true;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var paint = paints[i];
                for (int j = 0; j < line.Count - 1; j++)
                {
                    canvas.DrawLine(line[j], line[j + 1], paint);
                }
            }
        }
    }
}
