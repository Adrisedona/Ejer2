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
    delegate void Delegate(FileInfo file, string needle, ListBox lst, Form frm);

    public partial class Form1 : Form
    {
        string[] extensions;
        private int corrupts;
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
            if (Directory.Exists(txtDir.Text) && txtString.Text != "")
            {
                lstResult.Items.Clear();
                files = new DirectoryInfo(txtDir.Text).GetFiles().Where(f => extensions.Contains(f.Extension)).ToArray();
                this.Enabled = false;
                corrupts = 0;
                foreach (FileInfo file in files)
                {
                    Thread thread = new Thread(ThreadSearch);
                    thread.IsBackground = true;
                    thread.Start(file);
                }
            }
            else
            {
                MessageBox.Show(!Directory.Exists(txtDir.Text) ? "Invalid directory" : "No string to search", "Searh error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Search(FileInfo file, string needle, ListBox lst, Form frm)
        {
            if (!chkSensitive.Checked)
            {
                needle = needle.ToLower();
            }
            string fileContent;
            int finds;
            try
            {
                using (StreamReader reader = new StreamReader(file.FullName))
                {
                    fileContent = chkSensitive.Checked ? reader.ReadToEnd() : reader.ReadToEnd().ToLower();
                }
                finds = new Regex(Regex.Escape(needle)).Matches(fileContent).Count;
                lock (lst)
                {
                    lst.Items.Add($"{file.Name}-------{finds}");
                }
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is IOException || ex is ArgumentException)
            {
                lock ((object)corrupts)
                {
                    corrupts++;
                }
            }
            lock (files)
            {
                frm.Enabled = files.Length - corrupts == lst.Items.Count;
            }
        }

        private void ThreadSearch(object file)
        {
            Delegate d = Search;
            this.Invoke(d, file, txtString.Text.Trim(), lstResult, this);
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
