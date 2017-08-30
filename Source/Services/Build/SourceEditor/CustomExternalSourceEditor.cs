﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBuild.Dashboard.Services.Build.SourceEditor
{
	[ExternalSourceEditor(
		"custom",
		"Custom",
		"Use a custom source editor by specifying its path and arguments.",
		AllowOverridePath = true,
		AllowSpecifyAdditionalArgs = false,
		AllowSpecifyArgs = true)]
	internal class CustomExternalSourceEditor : ExternalSourceEditorBase
	{
		public override bool IsAvailable =>
			!string.IsNullOrEmpty(AppSettings.Default.ExternalSourceEditorPath)
			&& File.Exists(AppSettings.Default.ExternalSourceEditorPath);
		public override bool OpenFile(string file, int lineNumber)
		{
			if (!this.IsAvailable)
			{
				return false;
			}

			var args = AppSettings.Default.ExternalSourceEditorArgs.Replace("{filename}", file)
				.Replace("{linenumber}", lineNumber.ToString());

			var process = Process.Start(AppSettings.Default.ExternalSourceEditorPath, args);
			return process != null;
		}
	}
}