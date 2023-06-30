
using Decompiler.Patches;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsgBox;
using AvaloniaEdit;
using ReactiveUI;

namespace Decompiler
{
	public partial class Disassembly : Window
	{
		public static string[][] OpcodeArgs
= {
			new[]{"NOP", ""},
			new[]{"IADD", ""},
			new[]{"ISUB", ""},
			new[]{"IMUL", ""},
			new[]{"IDIV", ""},
			new[]{"IMOD", ""},
			new[]{"INOT", ""},
			new[]{"INEG", ""},
			new[]{"IEQ", ""},
			new[]{"INE", ""},
			new[]{"IGT", ""},
			new[]{"IGE", ""},
			new[]{"ILT", ""},
			new[]{"ILE", ""},
			new[]{"FADD", ""},
			new[]{"FSUB", ""},
			new[]{"FMUL", ""},
			new[]{"FDIV", ""},
			new[]{"FMOD", ""},
			new[]{"FNEG", ""},
			new[]{"FEQ", ""},
			new[]{"FNE", ""},
			new[]{"FGT", ""},
			new[]{"FGE", ""},
			new[]{"FLT", ""},
			new[]{"FLE", ""},
			new[]{"VADD", ""},
			new[]{"VSUB", ""},
			new[]{"VMUL", ""},
			new[]{"VDIV", ""},
			new[]{"VNEG", ""},
			new[]{"IAND", ""},
			new[]{"IOR", ""},
			new[]{"IXOR", ""},
			new[]{"I2F", ""},
			new[]{"F2I", ""},
			new[]{"F2V", ""},
			new[]{"PUSH_CONST_U8", "b"},
			new[]{"PUSH_CONST_U8_U8", "bb"},
			new[]{"PUSH_CONST_U8_U8_U8", "bbb"},
			new[]{"PUSH_CONST_U32", "d"},
			new[]{"PUSH_CONST_F", "f"},
			new[]{"DUP", ""},
			new[]{"DROP", ""},
			new[]{"NATIVE", "bbb"},
			new[]{"ENTER", "bs"},
			new[]{"LEAVE", "bb"},
			new[]{"LOAD", ""},
			new[]{"STORE", ""},
			new[]{"STORE_REV", ""},
			new[]{"LOAD_N", ""},
			new[]{"STORE_N", ""},
			new[]{"ARRAY_U8", "b"},
			new[]{"ARRAY_U8_LOAD", "b"},
			new[]{"ARRAY_U8_STORE", "b"},
			new[]{"LOCAL_U8", "b"},
			new[]{"LOCAL_U8_LOAD", "b"},
			new[]{"LOCAL_U8_STORE", "b"},
			new[]{"STATIC_U8", "b"},
			new[]{"STATIC_U8_LOAD", "b"},
			new[]{"STATIC_U8_STORE", "b"},
			new[]{"IADD_U8", "b"},
			new[]{"IMUL_U8", "b"},
			new[]{"IOFFSET", ""},
			new[]{"IOFFSET_U8", "b"},
			new[]{"IOFFSET_U8_LOAD", "b"},
			new[]{"IOFFSET_U8_STORE", "b"},
			new[]{"PUSH_CONST_S16", "s"},
			new[]{"IADD_S16", "s"},
			new[]{"IMUL_S16", "s"},
			new[]{"IOFFSET_S16", "s"},
			new[]{"IOFFSET_S16_LOAD", "s"},
			new[]{"IOFFSET_S16_STORE", "s"},
			new[]{"ARRAY_U16", "h"},
			new[]{"ARRAY_U16_LOAD", "h"},
			new[]{"ARRAY_U16_STORE", "h"},
			new[]{"LOCAL_U16", "h"},
			new[]{"LOCAL_U16_LOAD", "h"},
			new[]{"LOCAL_U16_STORE", "h"},
			new[]{"STATIC_U16", "h"},
			new[]{"STATIC_U16_LOAD", "h"},
			new[]{"STATIC_U16_STORE", "h"},
			new[]{"GLOBAL_U16", "h"},
			new[]{"GLOBAL_U16_LOAD", "h"},
			new[]{"GLOBAL_U16_STORE", "h"},
			new[]{"J", "R"},
			new[]{"JZ", "R"},
			new[]{"IEQ_JZ", "R"},
			new[]{"INE_JZ", "R"},
			new[]{"IGT_JZ", "R"},
			new[]{"IGE_JZ", "R"},
			new[]{"ILT_JZ", "R"},
			new[]{"ILE_JZ", "R"},
			new[]{"CALL", "a"},
			new[]{"LOCAL_U24", "a"},
			new[]{"LOCAL_U24_LOAD", "a"},
			new[]{"LOCAL_U24_STORE", "a"},
			new[]{"GLOBAL_U24", "a"},
			new[]{"GLOBAL_U24_LOAD", "a"},
			new[]{"GLOBAL_U24_STORE", "a"},
			new[]{"PUSH_CONST_U24", "a"},
			new[]{"SWITCH", ""},
			new[]{"STRING", ""},
			new[]{"STRINGHASH", ""},
			new[]{"TEXT_LABEL_ASSIGN_STRING", "b"},
			new[]{"TEXT_LABEL_ASSIGN_INT", "b"},
			new[]{"TEXT_LABEL_APPEND_STRING", "b"},
			new[]{"TEXT_LABEL_APPEND_INT", "b"},
			new[]{"TEXT_LABEL_COPY", ""},
			new[]{"CATCH", ""},
			new[]{"THROW", ""},
			new[]{"CALLINDIRECT", ""},
			new[]{"PUSH_CONST_M1", ""},
			new[]{"PUSH_CONST_0", ""},
			new[]{"PUSH_CONST_1", ""},
			new[]{"PUSH_CONST_2", ""},
			new[]{"PUSH_CONST_3", ""},
			new[]{"PUSH_CONST_4", ""},
			new[]{"PUSH_CONST_5", ""},
			new[]{"PUSH_CONST_6", ""},
			new[]{"PUSH_CONST_7", ""},
			new[]{"PUSH_CONST_FM1", ""},
			new[]{"PUSH_CONST_F0", ""},
			new[]{"PUSH_CONST_F1", ""},
			new[]{"PUSH_CONST_F2", ""},
			new[]{"PUSH_CONST_F3", ""},
			new[]{"PUSH_CONST_F4", ""},
			new[]{"PUSH_CONST_F5", ""},
			new[]{"PUSH_CONST_F6", ""},
			new[]{"PUSH_CONST_F7", ""},
			new[]{"IS_BIT_SET", ""}
		};
		private Function Function;
		private Patch[] patches;
		private readonly TextEditor _textEditor;
		private readonly ContextMenu _contextMenu;

		public ReactiveCommand<Unit, Unit> CreatePatternToolCommand { get; }
		public ReactiveCommand<Patch, Unit> OnPatchClickCommand { get; }

		public Disassembly()
		{
			InitializeComponent();

			_contextMenu = this.FindControl<ContextMenu>("ctxMenu");
			_textEditor = this.FindControl<TextEditor>("disassemblyCodeBox");
			_textEditor.IsReadOnly = true;
			_textEditor.TextArea.RightClickMovesCaret = true;
			_textEditor.ShowLineNumbers = true;
			_contextMenu.Opening += new System.ComponentModel.CancelEventHandler(OnContextMenuOpening);

			CreatePatternToolCommand = ReactiveCommand.CreateFromTask(CreatePatternTool);
			OnPatchClickCommand = ReactiveCommand.CreateFromTask<Patch>(OnPatchClick);
		}

		public void SetFunction(Function func)
		{
			Function = func;
			patches = Patch.GetPatches(Function);
			_textEditor.Text = DisassembleFunction();
		}

		// TODO: Rewrite this
		private string DisassembleInstruction(Instruction instruction)
		{
			var bytes = "";

			bytes += ((uint)instruction.OriginalOpcode).ToString("X").PadLeft(2, '0');

			var i = 0;
			foreach (var op in instruction.Operands)
			{
				bytes += " " + ((sbyte)op).ToString("X").PadLeft(2, '0');

				i++;

				if (i > 4)
				{
					bytes += "...";
					break;
				}
			}

			bytes = bytes.PadRight(16);

			bytes += " " + instruction.OriginalOpcode.ToString();

			var args = OpcodeArgs[(int)instruction.OriginalOpcode];

			switch (args[1])
			{
				case "b":
					bytes += " " + (int)instruction.GetOperand(0);
					break;
				case "bb":
					bytes += " " + (int)instruction.GetOperand(0) + ", " + (int)instruction.GetOperand(1);
					break;
				case "bbb":
					bytes += " " + (int)instruction.GetOperand(0) + ", " + (int)instruction.GetOperand(1) + ", " + (int)instruction.GetOperand(2);
					break;
				case "bs":
					bytes += " " + (int)instruction.GetOperand(0) + ", " + BitConverter.ToInt16(new byte[] { instruction.GetOperand(1), instruction.GetOperand(2) });
					break;
				case "d":
				case "h":
				case "s":
				case "R":
				case "a":
					bytes += " " + instruction.GetOperandsAsInt;
					break;
				case "f":
					bytes += " " + instruction.GetFloat;
					break;
			}

			if (instruction.OriginalOpcode == Opcode.NATIVE)
			{
				bytes += " // " + Function.ScriptFile.X64NativeTable.GetNativeFromIndex(instruction.GetNativeIndex) + ", " + instruction.GetNativeParams + ", " + instruction.GetNativeReturns;
			}
			else if (instruction.OriginalOpcode == Opcode.CALL)
			{
				if (Function.ScriptFile.FunctionAtLocation.TryGetValue(instruction.GetOperandsAsInt, out var func))
					bytes += " // " + func.Name + ", " + func.NumParams + ", " + func.NumReturns;
			}

			return bytes;
		}

		private string? CreatePatternAtLocation(int offset)
		{
			var pattern = "";

			for (var i = offset; i < Function.Instructions.Count; i++)
			{
				if (Function.Instructions[i].OriginalOpcode == Opcode.SWITCH)
					break;

				pattern += ((uint)Function.Instructions[i].OriginalOpcode).ToString("X").PadLeft(2, '0');

				foreach (var op in Function.Instructions[i].Operands)
				{
					if (Function.Instructions[i].OriginalOpcode is Opcode.LOCAL_U8 or Opcode.LOCAL_U8_LOAD or Opcode.LOCAL_U8_STORE or Opcode.ENTER)
					{
						pattern += " " + ((sbyte)op).ToString("X").PadLeft(2, '0');
					}
					else
					{
						pattern += " ?";
					}
				}

				pattern += " ";

				var results = GetNumPatternResults(pattern);

				if (results == 0)
					break;
				else if (results == 1)
				{
					return pattern;
				}
			}

			return null;
		}

		private async Task GeneratePatch(int offset, byte[] bytes)
		{
			var off = 0;

			if (offset == 1)
			{
				off = 5;
				offset = 0;
			}

			var pattern = CreatePatternAtLocation(offset);

			if (pattern == null)
			{
				await MessageBox.Show(null, "Could not create pattern at offset", "Error", MessageBox.MessageBoxButtons.Ok);
				return;
			}

			var length = 0;
			for (var i = offset; i < Function.Instructions.Count; i++)
			{
				if (length >= bytes.Length)
					break;

				length += Function.Instructions[i].InstructionLength;
			}

			if (length < bytes.Length)
			{
				await MessageBox.Show(null, "Generated patch is out of bounds", "Error", MessageBox.MessageBoxButtons.Ok);
				return;
			}

			var patch = "{ ";

			var first = true;
			foreach (var b in bytes)
			{
				if (!first)
					patch += ", ";

				patch += "0x" + b.ToString("X").PadLeft(2, '0');

				first = false;
			}

			patch += " }";

			var option = await MessageBox.Show(null, $"Pattern: {pattern}{Environment.NewLine}Patch: {patch}{Environment.NewLine}Offset: {off}{Environment.NewLine}Copy to clipboard?", "Patch Generated", MessageBox.MessageBoxButtons.YesNo);
			if (option == MessageBox.MessageBoxResult.Yes)
				Console.WriteLine($"\"{pattern}\", {off}, {patch}");
			//Clipboard.SetText($"\"{pattern}\", {off}, {patch}");
		}

		private string DisassembleFunction()
		{
			StringBuilder sb = new();

			foreach (var ins in Function.Instructions)
			{
				sb.AppendLine(DisassembleInstruction(ins));
			}

			return sb.ToString();
		}

		private int GetNumPatternResults(string pat)
		{
			pat = pat.Trim();

			var bytes = pat.Split(" ");
			var num = 0;

			var compiled = new List<int>();

			foreach (var @byte in bytes)
			{
				if (int.TryParse(@byte, System.Globalization.NumberStyles.HexNumber, null, out var res))
					compiled.Add(res);
				else
					compiled.Add(-1);
			}

			for (var i = 0; i < (Function.ScriptFile.CodeTable.Count - compiled.Count); i++)
			{
				for (var j = 0; j < bytes.Length; j++)
				{
					if (compiled[j] == -1)
						continue;

					if (compiled[j] != Function.ScriptFile.CodeTable[i + j])
						goto fail;
				}

				num++;
			fail:
				continue;
			}

			return num;
		}

		private async void checkPatternUniquenessToolStripMenuItem_Click(object sender, RoutedEventArgs e)
		{
			InputBox IB = new();

			var result = await IB.Show(null, "Pattern", "Enter a pattern", InputBox.InputBoxButtons.OkCancel);
			if (result == InputBox.InputBoxResult.Cancel)
				return;

            var results = GetNumPatternResults(IB.Value);

			if (results == 0)
				await MessageBox.Show(null, "Cannot find pattern", "Error", MessageBox.MessageBoxButtons.Ok);
			else if (results == 1)
				await MessageBox.Show(null, "Pattern is unique", "", MessageBox.MessageBoxButtons.Ok);
			else
				await MessageBox.Show(null, $"Pattern is not unique, {results} matches found!", "Error", MessageBox.MessageBoxButtons.Ok);
		}

		private async Task CreatePatternTool()
		{
			var pattern = CreatePatternAtLocation(_textEditor.TextArea.Caret.Line - 1);

			if (pattern != null)
			{
				var option = await MessageBox.Show(null, $"The pattern is {pattern}{Environment.NewLine}Copy to clipboard?", "Pattern Found", MessageBox.MessageBoxButtons.YesNo);
				if (option == MessageBox.MessageBoxResult.Yes)
					Console.WriteLine(pattern);
				//Clipboard.SetText(pattern);
			}
			else
			{
				await MessageBox.Show(null, "Cannot create pattern", "Error", MessageBox.MessageBoxButtons.Ok);
			}
		}

		private async Task OnPatchClick(Patch patch)
		{
			int start = _textEditor.TextArea.Caret.Line - 1;
			int end = _textEditor.TextArea.Caret.Line - 1;
			if(!_textEditor.TextArea.Selection.IsEmpty)
			{
				start = Math.Min(_textEditor.TextArea.Selection.StartPosition.Line, _textEditor.TextArea.Selection.EndPosition.Line) - 1;
				end = Math.Max(_textEditor.TextArea.Selection.StartPosition.Line, _textEditor.TextArea.Selection.EndPosition.Line) - 1;
				Console.WriteLine("Selection not empty " + start + " " + end);
			}


			if (await patch.GetData(start, end) == false)
				return;

			await GeneratePatch(start, patch.GetPatch(start, end));
		}

		private void OnContextMenuOpening(object sender, CancelEventArgs e)
		{
			var start = Math.Min(_textEditor.TextArea.Selection.StartPosition.Line, _textEditor.TextArea.Selection.EndPosition.Line);
			var end = Math.Max(_textEditor.TextArea.Selection.StartPosition.Line, _textEditor.TextArea.Selection.EndPosition.Line);

			List<Avalonia.Controls.MenuItem> Items = new List<Avalonia.Controls.MenuItem>();
			Items.Add(new Avalonia.Controls.MenuItem { Header = "Create Pattern", Command = CreatePatternToolCommand });

			foreach (var patch in patches)
			{
				if (!patch.ShouldShowPatch(start, end))
					continue;

				Items.Add(new Avalonia.Controls.MenuItem { Header = $"Patch: {patch.GetName(start, end)}", Command = OnPatchClickCommand, CommandParameter = patch });

				/*if (!patch.ShouldEnablePatch(start, end))
                    patchButton.Enabled = false;*/
			}

			_contextMenu.ItemsSource = Items;
		}

	}
}
