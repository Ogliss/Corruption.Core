using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace FactionColors.Properties
{
	// Token: 0x0200002E RID: 46
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005800 File Offset: 0x00003A00
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000079 RID: 121
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
