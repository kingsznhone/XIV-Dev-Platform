using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace 自动进本助手.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = resourceMan = new ResourceManager("自动进本助手.Properties.Resources", typeof(Resources).Assembly);
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static Bitmap 进本
		{
			get
			{
				object @object = ResourceManager.GetObject("进本", resourceCulture);
				return (Bitmap)@object;
			}
		}

		public static Bitmap 问号
		{
			get
			{
				object @object = ResourceManager.GetObject("问号", resourceCulture);
				return (Bitmap)@object;
			}
		}

		internal Resources()
		{
		}
	}
}
