using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaEdit;
using ReactiveUI;

namespace Decompiler.Views
{
	public partial class MainWindow : Window
	{
		private ScriptFile OpenFile;
		private readonly TextEditor _textEditor;
		private readonly ContextMenu _contextMenu;
		private string filename;
		private Dictionary<Function, int> Functions;

		public ReactiveCommand<int, Unit> GotoDeclarationCommand { get; }
		public ReactiveCommand<Function, Unit> DisassembleCommand { get; }

		public MainWindow()
		{
			InitializeComponent();
			_textEditor = this.FindControl<TextEditor>("scriptCodeBox");
			_contextMenu = this.FindControl<ContextMenu>("ctxMenu");
			_textEditor.ShowLineNumbers = Properties.Settings.Default.LineNumbers;
			_textEditor.TextArea.RightClickMovesCaret = true;
			_textEditor.IsReadOnly = true;
			_contextMenu.Opening += new System.ComponentModel.CancelEventHandler(OnContextMenuOpening);

			GotoDeclarationCommand = ReactiveCommand.CreateFromTask<int>(GotoDeclaration);
			DisassembleCommand = ReactiveCommand.CreateFromTask<Function>(Disassemble);
		}

		private async Task Disassemble(Function func)
		{
			var dis = new Disassembly();
			dis.SetFunction(func);
			dis.Show();
		}

		private async Task GotoDeclaration(int func)
		{
			// This coases a crash if used twice.
			// var num = Convert.ToInt32(func);
			// _textEditor.TextArea.Caret.Line = num;
			// _textEditor.TextArea.Caret.BringCaretToView();
		}

		private void OnContextMenuOpening(object sender, CancelEventArgs e)
		{
			if(Functions == null || Functions.Count == 0)
			{
				e.Cancel = true;
				return;
			}

			string word = GetWordAtCursor();
			_contextMenu.ItemsSource = null;

			foreach (var lvi in Functions)
			{
				if (lvi.Key.Name == word)
				{
					_contextMenu.ItemsSource = new[]
					{
						new Avalonia.Controls.MenuItem { Header="Goto Declaration (" + lvi.Key.Name + ")", Command =  GotoDeclarationCommand, CommandParameter=lvi.Value },
						new Avalonia.Controls.MenuItem { Header="Disassemble (" + lvi.Key.Name + ")", Command =  DisassembleCommand, CommandParameter=lvi.Key }
					};
				}
			}

			if (_contextMenu.ItemCount == 0)
				e.Cancel = true;
		}

		private async void OnOpenClick(object sender, RoutedEventArgs e)
		{
			if (!StorageProvider.CanOpen)
			{
				return;
			}

			var file = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
			{
				Title = "Select file to decompile",
				AllowMultiple = false,
				FileTypeFilter = new FilePickerFileType[] { new("GTA V Script Files") { Patterns = new[] { "*.ysc", "*.ysc.full" } } },
				// SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Path.GetDirectoryName(filename))
			});

			if (file?.Count == 0)
			{
				return;
			}
			filename = file[0].Path.AbsolutePath;

			var Start = DateTime.Now;
			var progressBar = new ProgressBar();
			progressBar.Minimum = 0;
			progressBar.SetMax(100);
			progressBar.Show();

			OpenFile = new ScriptFile(File.OpenRead(filename));
			await OpenFile.Decompile(progressBar);
			progressBar.Close();

			Console.WriteLine("Decompiled script file. Time taken: " + (DateTime.Now - Start).ToString());
			MemoryStream ms = new();

			OpenFile.Save(ms, false);

			Functions = OpenFile.FunctionLines;

			OpenFile.Close();
			StreamReader sr = new(ms);
			ms.Position = 0;
			Console.WriteLine("Loading text in viewer...");
			_textEditor.Document.Text = sr.ReadToEnd();

			sr.Close();
			Console.WriteLine("Ready. Time taken: " + (DateTime.Now - Start).ToString());
		}

		private bool islegalchar(char c) => char.IsLetterOrDigit(c) || c == '_';

		private string GetWordAtCursor()
		{
			var _line = _textEditor.Document.Lines[_textEditor.TextArea.Caret.Line - 1];
			if (_line.Length == 0 || _line.Length == _textEditor.TextArea.Caret.Column - 1)
				return "";
			string line = _textEditor.Document.GetText(_line.Offset, _line.Length);
			var pos = _textEditor.TextArea.Caret.Column - 1;
			int min = pos, max = pos;
			while (min > 0)
			{
				if (islegalchar(line[min - 1]))
					min--;
				else
					break;
			}

			var len = line.Length;
			while (max < len)
			{
				if (islegalchar(line[max]))
					max++;
				else
					break;
			}

			return line[min..max];
		}

	}
}
