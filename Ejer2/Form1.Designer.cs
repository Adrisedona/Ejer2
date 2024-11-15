namespace Ejer2
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDir = new System.Windows.Forms.TextBox();
            this.txtString = new System.Windows.Forms.TextBox();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblString = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListBox();
            this.txtExtensions = new System.Windows.Forms.TextBox();
            this.lblExtensions = new System.Windows.Forms.Label();
            this.chkSensitive = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(16, 42);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(203, 20);
            this.txtDir.TabIndex = 0;
            // 
            // txtString
            // 
            this.txtString.Location = new System.Drawing.Point(225, 42);
            this.txtString.Name = "txtString";
            this.txtString.Size = new System.Drawing.Size(132, 20);
            this.txtString.TabIndex = 1;
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Location = new System.Drawing.Point(13, 23);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(29, 13);
            this.lblDir.TabIndex = 2;
            this.lblDir.Text = "Path";
            // 
            // lblString
            // 
            this.lblString.AutoSize = true;
            this.lblString.Location = new System.Drawing.Point(225, 23);
            this.lblString.Name = "lblString";
            this.lblString.Size = new System.Drawing.Size(75, 13);
            this.lblString.TabIndex = 3;
            this.lblString.Text = "Text to search";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(363, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstResult
            // 
            this.lstResult.FormattingEnabled = true;
            this.lstResult.HorizontalScrollbar = true;
            this.lstResult.Location = new System.Drawing.Point(16, 91);
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(284, 303);
            this.lstResult.TabIndex = 5;
            // 
            // txtExtensions
            // 
            this.txtExtensions.Location = new System.Drawing.Point(306, 374);
            this.txtExtensions.Name = "txtExtensions";
            this.txtExtensions.Size = new System.Drawing.Size(132, 20);
            this.txtExtensions.TabIndex = 6;
            // 
            // lblExtensions
            // 
            this.lblExtensions.AutoSize = true;
            this.lblExtensions.Location = new System.Drawing.Point(306, 358);
            this.lblExtensions.Name = "lblExtensions";
            this.lblExtensions.Size = new System.Drawing.Size(58, 13);
            this.lblExtensions.TabIndex = 7;
            this.lblExtensions.Text = "Extensions";
            // 
            // chkSensitive
            // 
            this.chkSensitive.AutoSize = true;
            this.chkSensitive.Location = new System.Drawing.Point(342, 69);
            this.chkSensitive.Name = "chkSensitive";
            this.chkSensitive.Size = new System.Drawing.Size(96, 17);
            this.chkSensitive.TabIndex = 8;
            this.chkSensitive.Text = "Case Sensitive";
            this.chkSensitive.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 422);
            this.Controls.Add(this.chkSensitive);
            this.Controls.Add(this.lblExtensions);
            this.Controls.Add(this.txtExtensions);
            this.Controls.Add(this.lstResult);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblString);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.txtString);
            this.Controls.Add(this.txtDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Stringfinder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.TextBox txtString;
        private System.Windows.Forms.Label lblDir;
        private System.Windows.Forms.Label lblString;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.TextBox txtExtensions;
        private System.Windows.Forms.Label lblExtensions;
        private System.Windows.Forms.CheckBox chkSensitive;
    }
}

