using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Decompiler
{
    internal class GlobalModEntry
    {
        public string? Name = null;
        public Types.TypeInfo? Type = null;

        public GlobalModEntry()
        {
        }
    }

    internal class GlobalDB
    {
        private readonly Dictionary<uint, GlobalModEntry> Entries = new();

        public GlobalDB()
        {
            var file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "Assets/globals.db");

            var lines = File.Exists(file)
                ? File.ReadAllLines(file)
                : Encoding.Default.GetString(Properties.Resources.Functions).Trim().Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length == 0)
                    continue;

                var tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                Dictionary<uint, GlobalModEntry> entryDict;

                entryDict = Entries;

                if (!uint.TryParse(tokens[0], System.Globalization.NumberStyles.Number, null, out var hash))
                    throw new FileFormatException("Cannot parse function hash");

                if (!entryDict.ContainsKey(hash))
                    entryDict[hash] = new();

                tokens[1] = tokens[1].ToUpper();

                switch (tokens[1])
                {
                    case "NAME":
                        entryDict[hash].Name = tokens[2];
                        break;
                    case "TYPE":
                        entryDict[hash].Type = Types.GetFromName(tokens[2]);
                        break;
                    default:
                        throw new FileFormatException("Unknown field");
                }
            }
        }

        public string GetNameFromIndex(uint index)
        {
            if (Entries.TryGetValue(index, out var entry))
            {
                return entry.Name;
            }
            return null;
        }
    }
}
