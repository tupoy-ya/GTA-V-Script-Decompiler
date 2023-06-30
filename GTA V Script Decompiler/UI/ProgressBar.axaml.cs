using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Decompiler
{
    public partial class ProgressBar : Window
    {
        public int Minimum;
        public int Maximum;
        private double Value = 0;
        private double ValueInternal = 0;

        public ProgressBar()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SetMax(int max)
        {
            Maximum = 100;
        }

        public void SetValue(double value)
        {
            ValueInternal = value;
            Value = (ValueInternal / Maximum) * 100;
        }

        public void IncrementValue()
        {
            SetValue(ValueInternal + 1);
        }
    }
}
