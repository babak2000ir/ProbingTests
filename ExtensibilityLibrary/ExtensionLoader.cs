using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
			}

			string[] CsFiles = Directory.GetFiles(ExtensionsFolder, "*.csExt", SearchOption.AllDirectories);
			foreach (string CsFile in CsFiles)
			{
				Assembly a = null;
				FileInfo fi = new FileInfo(CsFile);
				bool success = Compile(fi, new CompilerParameters()
				{
					GenerateExecutable = false,
					OutputAssembly = Path.GetFileNameWithoutExtension(CsFile),
					GenerateInMemory = true 
				},
				ref a);

				if (success)
				{
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
				}
			}

			_Extensions = Extensions.ToArray();
		}
		private bool Compile(FileInfo sourceFile, CompilerParameters options, ref Assembly a)
		{
			//options.ReferencedAssemblies.Add("ExtensibilityLibrary.dll");
			options.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
			CompilerResults results = provider.CompileAssemblyFromFile(options, sourceFile.FullName);

			if (results.Errors.Count > 0)
			{
				return false;
			}

			a = results.CompiledAssembly;
			return true;
		}

		public IMyExtension GetExtensionInstance(Type ExtensionType)
		{
			IMyExtension MyExt = Activator.CreateInstance(ExtensionType) as IMyExtension;
			return MyExt;
		}
    }
}
