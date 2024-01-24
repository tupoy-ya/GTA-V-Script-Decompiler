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
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Decompiler.Views
{
	public partial class MainWindow : Window
	{
		private ScriptFile OpenFile;
		private Queue<string> CompileList;
		private string filename;
		private Dictionary<Function, int> Functions;

		private readonly TextEditor _textEditor;
		private readonly ContextMenu _contextMenu;
		private readonly CheckBox _showArraySizeCheckBox;
		private readonly CheckBox _showNativeNamespaceCheckBox;

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
			// This causes a crash if used twice.
			// EDIT: Ok now it just works for some reason.
			var num = Convert.ToInt32(func);
			_textEditor.TextArea.Caret.Line = num;
			_textEditor.TextArea.Caret.BringCaretToView();
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

		private void OnLineNumbersClick(object sender, RoutedEventArgs e)
		{
			_textEditor.ShowLineNumbers = Properties.Settings.Default.LineNumbers;
		}

		private async void OnSaveClick(object sender, RoutedEventArgs e)
		{
			if (!StorageProvider.CanSave)
				return;

			var file_extensions = new[] { "*.c", "*.c4", "*.sc", "*.sc" }; // TODO: KDE FilePicker only displays *.c, check if others are the same.
			var file = await StorageProvider.SaveFilePickerAsync( new FilePickerSaveOptions
			{
				FileTypeChoices = new FilePickerFileType[] { new("Decompiled Script Files") { Patterns = file_extensions } },
				DefaultExtension = ".c",
				SuggestedFileName = filename
			});

			if (string.IsNullOrEmpty(file.Name))
				return;

			_textEditor.Save(await file.OpenWriteAsync());
		}

		private async void OnOpenClick(object sender, RoutedEventArgs e)
		{
			if (!StorageProvider.CanOpen)
				return;

			var file_extensions = new[] { "*.ysc", "*.osc", "*.dsc", "*.psc", "*.ssc", "*.ysc.full", "*.osc.full", "*.dsc.full", "*.psc.full", "*.ssc.full" };
			var file = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
				Title = "Select file to decompile",
				AllowMultiple = false,
				FileTypeFilter = new FilePickerFileType[] { new("GTA V Script Files") { Patterns = file_extensions } },
				// SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Path.GetDirectoryName(filename))
			});

			if (file?.Count == 0)
				return;

			filename = file[0].Path.AbsolutePath;

			var Start = DateTime.Now;
			var progressBar = new ProgressBar("Decompile File", 1, 2);
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

		private async void OnExportFileClick(object sender, RoutedEventArgs e)
		{
			if (!StorageProvider.CanOpen)
				return;

			var file_extensions = new[] { "*.ysc", "*.osc", "*.dsc", "*.psc", "*.ssc", "*.ysc.full", "*.osc.full", "*.dsc.full", "*.psc.full", "*.ssc.full" };
			var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
				Title = "Select file to decompile",
				AllowMultiple = true,
				FileTypeFilter = new FilePickerFileType[] { new("GTA V Script Files") { Patterns = file_extensions } },
				// SuggestedStartLocation = await StorageProvider.TryGetFolderFromPathAsync(Path.GetDirectoryName(filename))
			});

			if (files?.Count == 0)
				return;

			var Start = DateTime.Now;
			foreach (var file in files)
			{
				filename = file.Path.AbsolutePath;

				var StartFile = DateTime.Now;
				var progressBar = new ProgressBar("Export File", 1, 2);
				progressBar.Show();

				OpenFile = new ScriptFile(File.OpenRead(filename));
				await OpenFile.Decompile(progressBar);
				progressBar.Close();

				Console.WriteLine("Decompiled script file. Time taken: " + (DateTime.Now - StartFile).ToString());
				
				string OutputFilePath = Path.ChangeExtension(file.Path.AbsolutePath, ".c");
				Stream OutputFileStream = File.OpenWrite(OutputFilePath);
				OpenFile.Save(OutputFileStream);
				OutputFileStream.Close();
				OpenFile.Close();
			}
			Console.WriteLine("Ready. Time taken: " + (DateTime.Now - Start).ToString());
		}

		private async void OnExportDirClick(object sender, RoutedEventArgs e)
		{
			if (!StorageProvider.CanOpen)
				return;

			var folders = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
				Title = "Select folder to decompile",
				AllowMultiple = true
			});

			if (folders?.Count == 0)
				return;

			var Start = DateTime.Now;
			foreach (var folder in folders)
			{
				await BatchDecompile(folder.Path.AbsolutePath);
			}
			Console.WriteLine("Ready. Time taken: " + (DateTime.Now - Start).ToString());
		}

		private async Task Decompile(string directory, ProgressBar progressBar)
        {
            while (CompileList.Count > 0)
            {
                string scriptToDecode;

                lock (CompileList)
                {
                    scriptToDecode = CompileList.Dequeue();
                }

                try
                {
                    await Task.Run(async () =>
                    {
                        ScriptFile scriptFile = new(File.OpenRead(scriptToDecode));
                        await scriptFile.Decompile();
                        scriptFile.Save(Path.Combine(directory, Path.GetFileNameWithoutExtension(scriptToDecode) + ".c"));
                        scriptFile.Close();
                    });
                }
                catch (Exception)
                {
                    // MessageBox.Show("Error decompiling script " + Path.GetFileNameWithoutExtension(scriptToDecode) + " - " + ex.Message);
                    throw;
                }

                progressBar.IncrementValue();
            }
        }

		private async Task BatchDecompile(string dirPath)
        {
            CompileList = new Queue<string>();
            var tasks = new List<Task>();

            var Start = DateTime.Now;
            var saveDirectory = Path.Combine(dirPath, "exported");
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);

            foreach (var file in Directory.GetFiles(dirPath, "*.ysc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.osc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.dsc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.psc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.ssc"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.ysc.full"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.osc.full"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.dsc.full"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.psc.full"))
            {
                CompileList.Enqueue(file);
            }

            foreach (var file in Directory.GetFiles(dirPath, "*.ssc.full"))
            {
                CompileList.Enqueue(file);
            }

            var progressBar = new ProgressBar("Export Directory", 1, CompileList.Count + 1);
            progressBar.Show();

            if (Properties.Settings.Default.UseMultithreading)
            {
                for (var i = 0; i < Environment.ProcessorCount; i++)
                {
                    tasks.Add(Decompile(saveDirectory, progressBar));
                }

                await Task.WhenAll(tasks);
            }
            else
            {
                await Decompile(saveDirectory, progressBar);
            }

			progressBar.Close();

            Focus();
            Console.WriteLine("Directory exported. Time taken: " + (DateTime.Now - Start).ToString());
        }

		private void OnCloseClick(object sender, RoutedEventArgs e)
		{
			// https://stackoverflow.com/questions/68684968/close-a-window-in-avalonia-gui#comment133193421_69150075
			if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
				lifetime.Shutdown();
		}

		private static bool Islegalchar(char c) => char.IsLetterOrDigit(c) || c == '_';

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
				if (Islegalchar(line[min - 1]))
					min--;
				else
					break;
			}

			var len = line.Length;
			while (max < len)
			{
				if (Islegalchar(line[max]))
					max++;
				else
					break;
			}

			return line[min..max];
		}

	}
}
