using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensibilityLibrary
{
	public class ExtensionLoader
    {
		private Type[] _Extensions;
		public Type[] Extensions
		{
			get
			{
				return _Extensions;
			}
		}
		public ExtensionLoader()
		{
			_Extensions = null;
		}
		public ExtensionLoader(string ExtensionsFolder)
        {
			LoadExtensions(ExtensionsFolder);
		}

        public void LoadExtensions(string ExtensionsFolder)
        {
			List<Type> Extensions = new List<Type>();

			string[] DllFiles = Directory.GetFiles(ExtensionsFolder, "*.dll", SearchOption.AllDirectories);
			foreach (string DllFile in DllFiles)
			{
				Assembly a = Assembly.LoadFile(DllFile);
				
				foreach (Type objType in a.GetTypes())
				{
					TypeInfo SubType = objType.UnderlyingSystemType as TypeInfo;
					if (SubType != null && SubType.ImplementedInterfaces.Any())
					{
						Type InterfaceType = SubType.ImplementedInterfaces.First();
						if (InterfaceType.Name == typeof(IMyExtension).Name)
						{
							Extensions.Add(objType);
						}
					}
				}

				_Extensions = Extensions.ToArray();
			}
		}

		public IMyExtension GetExtensionInstance(Type ExtensionType)
		{
			IMyExtension MyExt = Activator.CreateInstance(ExtensionType) as IMyExtension;
			return MyExt;
		}
    }
}
