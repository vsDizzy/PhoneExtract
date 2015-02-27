using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace PhoneExtract
{
	internal class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			switch (args.Length)
			{
				case 1:
					if (args[0] == "/c")
					{
						ExtractClipboard();
					}
					else
					{
						Extract(args[0], Path.ChangeExtension(args[0], "out"));
					}
					break;
				case 2:
					Extract(args[0], args[1]);
					break;
				default:
					var path = Path.GetFileName(Assembly.GetEntryAssembly().Location);
					Console.WriteLine("Usage:");
					Console.WriteLine("\t{0} inputFile [outputFile] - extract phones from file", path);
					Console.WriteLine("\t{0} /c - extract phones from clipboard", path);
					break;
			}
		}

		private static void Extract(string inputFilename, string outputFilename)
		{
			var ex = new Extractor();
			var input = File.ReadAllText(inputFilename);
			var phones = ex.GetPhones(input).ToArray();
			if (phones.Length != 0)
			{
				File.WriteAllLines(outputFilename, ex.GetPhones(input));
				Console.WriteLine("{0} -> {1} ({2} numbers)", inputFilename, outputFilename, phones.Length);
			}
			else
			{
				Console.WriteLine("No phone extracted.");
			}
		}

		private static void ExtractClipboard()
		{
			var ex = new Extractor();
			var phones = ex.GetPhones(Clipboard.GetText()).ToArray();
			if (phones.Length != 0)
			{
				var txt = string.Join("\r\n", ex.GetPhones(Clipboard.GetText()));
				Clipboard.SetText(txt);
				Console.WriteLine("Clipboard -> Clipboard ({0} numbers)", phones.Length);
			}
			else
			{
				Console.WriteLine("No phone extracted.");
			}
		}
	}
}