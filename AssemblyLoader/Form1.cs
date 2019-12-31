using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
			Assembly a = Assembly.Load("DynamicDLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

			string result1 = (string)a.GetTypes()[0].GetMethod("GetClassInfo").Invoke(null, null);
			string result2 = (string)a.GetTypes()[1].GetMethod("GetClassInfo").Invoke(null, null);
		}
	}
}
