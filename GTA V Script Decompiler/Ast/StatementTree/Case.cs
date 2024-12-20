using System.Collections.Generic;
using System.Text;

namespace Decompiler.Ast.StatementTree
{
    internal class Case : Tree
    {
        public readonly int BreakOffset; 
        public readonly int StartOffset;
        public int EndOffset; // offset to the beginning of the next case stmt (or break if it's the last case stmt)
        private readonly List<AstToken> Cases;

        public Case(Function function, Tree parent, int offset, int end_offset, List<AstToken> cases, int breakOffset) : base(function, parent, offset)
        {
            StartOffset = offset;
            EndOffset = end_offset;
            BreakOffset = breakOffset;
            Cases = cases;
        }

        public override bool IsTreeEnd()
        {
            if (Offset < Function.Instructions.Count && Function.Instructions[Offset].Offset >= BreakOffset)
                return true;

            if (Statements.Count > 0 && (Statements[^1] is Break || Statements[^1] is Return || Statements[^1] is Jump))
            {
                return true;
            }

            if (Offset == EndOffset)
            {
                Statements.Add(new Attribute(Function, "fallthrough"));
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (var @case in Cases)
            {
                if (@case is not Default)
                    sb.Append("case ");
                sb.AppendLine(@case.ToString() + ":");
            }

            sb.Append(ToString(false));
            return sb.ToString();
        }
    }
}
