namespace Decompiler.Ast
{
    internal class Global : AstToken
    {
        public readonly uint Index;
        public string Name;

        public Global(Function func, uint index) : base(func)
        {
            Index = index;
            string name = Program.GlobalDB.GetNameFromIndex(index);
            if (name != null)
            {
                Name = name;
            }
        }

        public void SetName(string new_name)
        {
            Name = new_name;
        }

        public override string ToString()
        {
            if (Name != null)
            {
                if (Program.Options != null && Program.Options.GlobalIndexes)
                    return "/*" + "&Global_" + Index + "*/" + "&"+ Name;

                return "&"+Name;
            }

            if (Program.Options != null && Program.Options.Diffmode)
                return "&Global_diffmode";

            return "&Global_" + Index;
        }

        public override string ToPointerString()
        {
            if (Name != null)
            {
                if (Program.Options != null && Program.Options.GlobalIndexes)
                    return "/*" + "Global_" + Index + "*/" + Name;

                return Name;
            }

            if (Program.Options != null && Program.Options.Diffmode)
                return "Global_diffmode";

            return "Global_" + Index;
        }

        public override bool CanGetGlobalIndex() => true;

        public override int GetGlobalIndex() => (int)Index;

        public override bool IsPointer() => true;
    }

    internal class GlobalLoad : AstToken
    {
        public readonly uint Index;
        public string Name;

        public GlobalLoad(Function func, uint index) : base(func)
        {
            Index = index;
            string name = Program.GlobalDB.GetNameFromIndex(index);
            if (name != null)
            {
                Name = name;
            }
        }

        public void SetName(string new_name)
        {
            Name = new_name;
        }

        public override string ToString()
        {
            if (Name != null)
            {
                if (Program.Options != null && Program.Options.GlobalIndexes)
                    return "/*" + "Global_" + Index + "*/" + Name;

                return Name;
            }

            if (Program.Options != null && Program.Options.Diffmode)
                return "Global_diffmode";

            return "Global_" + Index;
        }

        public override bool CanGetGlobalIndex() => true;

        public override int GetGlobalIndex() => (int)Index;
    }

    internal class GlobalStore : AstToken
    {
        public readonly uint Index;
        public readonly AstToken Value;
        public string Name;

        public void SetName(string new_name)
        {
            Name = new_name;
        }

        public GlobalStore(Function func, uint index, AstToken value) : base(func)
        {
            Index = index;
            Value = value;

            Index = index;
            string name = Program.GlobalDB.GetNameFromIndex(index);
            if (name != null)
            {
                Name = name;
            }

            HintType(ref value.GetTypeContainer());
        }

        public override bool IsStatement() => true;

        public override string ToString()
        {
            if (Name != null)
            {
                if (Program.Options != null && Program.Options.GlobalIndexes)
                    return "/*" + "Global_" + Index + "*/" + Name + " = " + Value.ToString() + ";";

                return Name + " = " + Value.ToString() + ";";
            }

            if (Program.Options != null && Program.Options.Diffmode)
                return "Global_diffmode = " + Value.ToString() + ";";

            return "Global_" + Index + " = " + Value.ToString() + ";";
        }

        public override bool CanGetGlobalIndex() => true;

        public override int GetGlobalIndex() => (int)Index;
    }
}
