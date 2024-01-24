using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Decompiler
{
    public partial class ProgressBar : Window
    {
        public int Min;
        public int Max;
        private int Value = 0;
        private Avalonia.Controls.ProgressBar _progressbar;

        public ProgressBar(string name, int min, int max)
        {
            AvaloniaXamlLoader.Load(this);
            _progressbar = this.FindControl<Avalonia.Controls.ProgressBar>("progressbar");
            _progressbar.Value = min;
            _progressbar.Maximum = max;
            _progressbar.Minimum = min;
            Title = name;
            Min = min;
            Max = max;
            Value = min;
        }

        public void SetMax(int max)
        {
            Max = max;
            _progressbar.Maximum = max;
        }

        public void SetValue(int value)
        {
            Value = value;
            Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => _progressbar.Value = value);
        }

        public void IncrementValue()
        {
            Value = Value + 1;
            Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() => _progressbar.Value = Value);
        }
    }
}
