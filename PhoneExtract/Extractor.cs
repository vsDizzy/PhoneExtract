using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PhoneExtract.Properties;

namespace PhoneExtract
{
	internal class Extractor
	{
		private readonly Regex _extractEx = new Regex(Settings.Default.Regex, RegexOptions.Multiline);
		private readonly Regex _replaceEx = new Regex(@"[(,)]");

		public IEnumerable<string> GetPhones(string input)
		{
			return from Match match in _extractEx.Matches(input) select FormatPhone(match.Value);
		}

		private string FormatPhone(string phone)
		{
			return _replaceEx.Replace(phone, "");
		}
	}
}