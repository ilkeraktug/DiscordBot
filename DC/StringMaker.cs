using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace DC
{
	public static class StringMaker
	{
		public static string SingleLine(string other)
		{
			return "`" + other + "`\n"; 
		}
		
		public static string Red(string other)
		{
			return "```css\n[" + other + "]```";
		}

		public static string Blue(string other)
		{
			return "```bash\n\"" + other + "\"```";
		}
	}
}
