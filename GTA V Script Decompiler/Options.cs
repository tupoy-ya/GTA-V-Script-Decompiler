using CommandLine;

namespace Decompiler
{
	class Options
	{
		[Value(0, MetaName = "input file", HelpText = "Input file to be processed.", Required = false)]
		public string FileName { get; set; }

		[Option('o', "output file", Default="", HelpText = "Output file name with extension. If recursion is enabled this is output directory name.")]
		public string OutputFile { get; set; }

		[Option('r', "recursive", Default = false, HelpText = "Decompile files recursively.")]
		public bool Recursive { get; set; }

		[Option('n', "native_tables", HelpText = "Don't extract native tables.")]
		public bool DontExtractNativeTables { get; set; }

		[Option('d', "diffmode", Default = false, HelpText = "Removes things that may break diffing.")]
		public bool Diffmode { get; set; }

		[Option('i', "global_indexes", Default = false, HelpText = "Show global indexes.")]
		public bool GlobalIndexes { get; set; }

		[Option('g', "gui", Default = false, HelpText = "Run with gui enabled.")]
		public bool Gui { get; set; }

		[Option('h', "function_hash_names", Default = false, HelpText = "Use function hashes as names.")]
		public bool FunctionHashNames { get; set; }

		[Option('v', "verbose", HelpText = "Show which file is currently being decompiled.")]
		public bool Verbose { get; set; }
	}
}