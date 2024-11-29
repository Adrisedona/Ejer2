using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejer2
{
    delegate void Delegate(ListBox lst);

    public partial class Form1 : Form
    {
        string[] extensions;
        static string PATH = Environment.GetEnvironmentVariable("userprofile") + "\\extensions.txt";
        FileInfo[] files;
        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.rayquaza;
            SetExtensions(true);
        }

        private void SetExtensions(bool file)
        {
            string[] extensionsAux;
            try
            {
                if (file)
                {
                    using (StreamReader reader = new StreamReader(PATH))
                    {
                        extensionsAux = reader.ReadToEnd().Split(',');
                    }
                }
                else
                {
                    extensionsAux = txtExtensions.Text.Trim().Split(',');
                }
                foreach (string extension in extensionsAux)
                {
                    if (!Regex.IsMatch(extension, @"\.[a-zA-Z0-9]+"))
                    {
                        throw new ArgumentException();
                    }
                }
                extensions = extensionsAux;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException)
            {
                MessageBox.Show(file ? "File not found/corrupt" : "Extensions not valid", "Extensions error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (extensions == null)
                {
                    extensions = new string[] { ".txt" };
                }
            }
            txtExtensions.Text = "";
            foreach (string extension in extensions)
            {
                txtExtensions.Text += extension + ",";
            }
            txtExtensions.Text = txtExtensions.Text.Remove(txtExtensions.Text.Length - 1, 1);
            txtExtensions.Modified = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtExtensions.Modified)
            {
                SetExtensions(false);
            }
            try
            {
                if (Directory.Exists(txtDir.Text) && txtString.Text != "")
                {
                    lstResult.Items.Clear();
                    files = new DirectoryInfo(txtDir.Text).GetFiles().Where(f => extensions.Contains(f.Extension)).ToArray();
                    foreach (FileInfo file in files)
                    {
                        Thread thread = new Thread(Search);
                        thread.IsBackground = true;
                        thread.Start(file);
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentException)
            {
                MessageBox.Show(!Directory.Exists(txtDir.Text) ? "Invalid directory" : (ex is ArgumentException ? "No string to search" : "No permission to access the directory"), "Searh error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Search(object fileInfo)
        {
            FileInfo file = fileInfo as FileInfo;
            string needle = !chkSensitive.Checked ? txtString.Text.ToLower() : txtString.Text;
            int finds = 0;
			string line;
			Regex regex = new Regex(Regex.Escape(needle));

			try
            {
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    //Más correcto que sea linea a linea
					while ((line = reader.ReadLine()) != null)
					{
						finds += regex.Matches(chkSensitive.Checked ? line : line.ToLower()).Count;
					}
                }
                
                lock (lstResult)
                {
                    Delegate d = lst => lst.Items.Add($"{file.Name}-------{finds}");
                    this.Invoke(d, lstResult);
                }
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentException)
            {
                lock (lstResult)
                {
                    Delegate d = lst => lst.Items.Add("Archivo erroneo");
                    this.Invoke(d, lstResult);
                }
            }
            //lock (files)
            //{
            //    frm.Enabled = files.Length - corrupts == lst.Items.Count;
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetExtensions(false);
            try
            {
                using (StreamWriter writer = new StreamWriter(PATH))
                {
                    writer.Write(txtExtensions.Text);
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException)
            {
                MessageBox.Show("File not found/corrupt", "Extensions error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
