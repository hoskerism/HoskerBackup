using System.Collections.Generic;
using System;
using System.IO;

namespace HoskerBackup.Core
{
	static class Helper
	{
		public static int GetMaxFilenameLength(string target)
		{
			// Encrypted drives only support 143 character filenames. I can't find an elegant way of detecting if a drive is encrypted or not, so I'm just going to try writing a longer file.

			const string longFilename = "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890.txt";

			try
			{
				var fullPath = Path.Combine(target, longFilename);
				if (!File.Exists(fullPath))
				{
					var stream = File.Create(fullPath);
					stream.Close();
				}

				File.Delete(fullPath);

				return 255;
			}
			catch (IOException)
			{
				return 103; // 143;
			}
		}
	}
}
