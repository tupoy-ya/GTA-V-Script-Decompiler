using System;
using System.Collections.Generic;

namespace Decompiler.Ast.StatementTree
{
    internal class Switch : Tree
    {
        public readonly Dictionary<int, List<AstToken>> Cases;
        public readonly int BreakOffset;
        public readonly AstToken SwitchVal;

        public Switch(Function function, Tree parent, int offset, Dictionary<int, List<AstToken>> cases, int breakOffset, AstToken switchVal) : base(function, parent, offset)
        {
            Cases = cases;
            BreakOffset = breakOffset;
            SwitchVal = switchVal;

            Case? lastCase = null;

            foreach (var (loc, stmts) in cases)
            {
                foreach (var @case in stmts)
                {
                    @case.HintType(ref SwitchVal.GetTypeContainer());
                }

                var caseStart = Function.CodeOffsetToFunctionOffset(loc);

                if (lastCase != null)
                {
                    lastCase.EndOffset = caseStart;
                }

                lastCase = new Case(Function, this, caseStart, breakOffset, stmts, breakOffset);
                Statements.Add(lastCase);
            }
        }

        /// <returns>Isn't going to be called anyway</returns>
        public override bool IsTreeEnd() => true;

        public override string ToString() => $"switch ({SwitchVal}){Environment.NewLine}{{{Environment.NewLine}{base.ToString()}}}";
    }
}
