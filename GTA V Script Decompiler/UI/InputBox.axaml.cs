using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Decompiler
{
    public partial class InputBox : Window
    {
        private InputBoxResult result;
        public string Value;

        public enum InputBoxButtons
        {
            Ok,
            OkCancel,
            YesNo,
            YesNoCancel
        }

        public enum InputBoxResult
        {
            Ok,
            Cancel,
            Yes,
            No
        }

        public InputBox()
        {
            AvaloniaXamlLoader.Load(this);
        }


        public Task<InputBoxResult> Show(Window parent, string text, string title, InputBoxButtons buttons)
        {
            var msgbox = new InputBox()
            {
                Title = title
            };
            msgbox.FindControl<TextBlock>("Text").Text = text;
            var textBox = msgbox.FindControl<TextBox>("returnInput");
            var buttonPanel = msgbox.FindControl<StackPanel>("Buttons");

            result = InputBoxResult.Ok;

            void AddButton(string caption, InputBoxResult r, bool def = false)
            {
                var btn = new Button {Content = caption};
                btn.Click += (_, __) => { 
                    result = r;
                    Value = textBox.Text;
                    msgbox.Close();
                };
                buttonPanel.Children.Add(btn);
                if (def)
                    result = r;
            }

            if (buttons == InputBoxButtons.Ok || buttons == InputBoxButtons.OkCancel)
                AddButton("Ok", InputBoxResult.Ok, true);
            if (buttons == InputBoxButtons.YesNo || buttons == InputBoxButtons.YesNoCancel)
            {
                AddButton("Yes", InputBoxResult.Yes);
                AddButton("No", InputBoxResult.No, true);
            }

            if (buttons == InputBoxButtons.OkCancel || buttons == InputBoxButtons.YesNoCancel)
                AddButton("Cancel", InputBoxResult.Cancel, true);


            var tcs = new TaskCompletionSource<InputBoxResult>();
            msgbox.Closed += delegate { tcs.TrySetResult(result); };
            if (parent != null)
                msgbox.ShowDialog(parent);
            else msgbox.Show();
            return tcs.Task;
        }


    }

}