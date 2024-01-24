using Avalonia;

namespace Decompiler.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public bool showArraySize
        {
            get => Properties.Settings.Default.ShowArraySize;
            set { Properties.Settings.Default.ShowArraySize = value; Properties.Settings.Default.Save(); }
        }
        public bool showNativeNamespace
        {
            get => Properties.Settings.Default.ShowNativeNamespace;
            set { Properties.Settings.Default.ShowNativeNamespace = value; Properties.Settings.Default.Save(); }
        }
        public bool reverseHashes
        {
            get => Properties.Settings.Default.ReverseHashes;
            set { Properties.Settings.Default.ReverseHashes = value; Properties.Settings.Default.Save(); }
        }
        public bool declareVariables
        {
            get => Properties.Settings.Default.DeclareVariables;
            set { Properties.Settings.Default.DeclareVariables = value; Properties.Settings.Default.Save(); }
        }
        public bool shiftVariables
        {
            get => Properties.Settings.Default.ShiftVariables;
            set { Properties.Settings.Default.ShiftVariables = value; Properties.Settings.Default.Save(); }
        }
        public bool hexIndex
        {
            get => Properties.Settings.Default.HexIndex;
            set { Properties.Settings.Default.HexIndex = value; Properties.Settings.Default.Save(); }
        }
        public bool showFunctionPointers
        {
            get => Properties.Settings.Default.ShowFunctionPointers;
            set { Properties.Settings.Default.ShowFunctionPointers = value; Properties.Settings.Default.Save(); }
        }
        public bool showFunctionHash
        {
            get => Properties.Settings.Default.IncludeFunctionHash;
            set { Properties.Settings.Default.IncludeFunctionHash = value; Properties.Settings.Default.Save(); }
        }
        public bool uppercaseNatives
        {
            get => Properties.Settings.Default.UppercaseNatives;
            set { Properties.Settings.Default.UppercaseNatives = value; Properties.Settings.Default.Save(); }
        }
        public bool isRDR2
        {
            get => Properties.Settings.Default.IsRDR2;
            set { Properties.Settings.Default.IsRDR2 = value; Properties.Settings.Default.Save(); }
        }
        public bool lineNumbers
        {
            get => Properties.Settings.Default.LineNumbers;
            set { Properties.Settings.Default.LineNumbers = value; Properties.Settings.Default.Save(); }
        }
    }
}