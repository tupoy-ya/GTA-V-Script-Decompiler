using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MsgBox;

namespace Decompiler.Patches
{
    internal class ReturnFunctionPatch : Patch
    {
        private uint ReturnValue = 0;

        public ReturnFunctionPatch(Function function) : base(function)
        {
        }

        public override string GetName(int start, int end) => "Place Function Return";

        public override byte[] GetPatch(int start, int end)
        {
            List<byte> bytes = new();

            if (Function.NumReturns != 0)
            {
                if (ReturnValue <= 7)
                    bytes.Add((byte)(((byte)Opcode.PUSH_CONST_0) + ReturnValue));
                else if (ReturnValue <= 65535)
                {
                    bytes.Add((byte)Opcode.PUSH_CONST_S16);
                    bytes.AddRange(BitConverter.GetBytes((short)ReturnValue));
                }
                else
                {
                    bytes.Add((byte)Opcode.PUSH_CONST_U32);
                    bytes.AddRange(BitConverter.GetBytes(ReturnValue));
                }
            }

            bytes.Add((byte)Opcode.LEAVE);
            bytes.Add((byte)Function.NumParams);
            bytes.Add((byte)Function.NumReturns);

            return bytes.ToArray();
        }

        public override bool ShouldEnablePatch(int start, int end) => end - start == 1 && Function.Instructions[start].OriginalOpcode != Opcode.ENTER;

        public override bool ShouldShowPatch(int start, int end) => true;

        public override async Task<bool> GetData(int start, int end)
        {
            uint value = 0;

            if (Function.Instructions[start].OriginalOpcode == Opcode.ENTER)
            {
                await MessageBox.Show(null, "Cannot place function return directly on an ENTER, try placing it after the ENTER", "Error", MessageBox.MessageBoxButtons.Ok);
                return false;
            }

            if (Function.NumReturns > 1)
            {
                await MessageBox.Show(null, "Cannot apply patch as this function returns multiple values", "Error", MessageBox.MessageBoxButtons.Ok);
                return false;
            }
            else if (Function.NumReturns > 0)
            {
                InputBox box = new();
                if(await box.Show(null, "Enter value to return", "Function Return", InputBox.InputBoxButtons.OkCancel) == InputBox.InputBoxResult.Cancel)
                {
                    return false;
                }

                Console.WriteLine(box.Value);

                if (!uint.TryParse(box.Value, out value))
                {
                    await MessageBox.Show(null, "Integer is invalid", "Error", MessageBox.MessageBoxButtons.Ok);
                    return false;
                }
            }

            ReturnValue = value;

            return true;
        }
    }
}
