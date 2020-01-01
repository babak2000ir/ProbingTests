using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblyLoader
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			textBox1.Text = AppDomain.CurrentDomain.BaseDirectory;
			folderBrowserDialog1.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string[] DllFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Extensions"),"*.dll",SearchOption.AllDirectories);
			//Assembly a = Assembly.Load("DynamicDLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			foreach (string DllFile in DllFiles)
			{
				Assembly a = Assembly.LoadFile(DllFile);
				//string result1 = (string)a.GetTypes()[0].GetMethod("GetClassInfo").Invoke(null, null);
				//string result2 = (string)a.GetTypes()[1].GetMethod("GetClassInfo").Invoke(null, null);

				foreach (Type objType in a.GetTypes())
				{
					TypeInfo SubType = objType.UnderlyingSystemType as TypeInfo;
					if (SubType != null && SubType.ImplementedInterfaces.Any())
					{
						Type InterfaceType = SubType.ImplementedInterfaces.First();
						if (InterfaceType.Name == typeof(IMyExtension).Name)
						{
							IMyExtension MyExt = Activator.CreateInstance(objType) as IMyExtension;
							if (MyExt != null)
							{
								MessageBox.Show(MyExt.GetName() + " " + MyExt.GetDateTime());
							}
						}
					}

					//MessageBox.Show(obj.GetType().ToString());
					//MyExt = obj as IMyExtension;


				}
			}
		}
	}
}
