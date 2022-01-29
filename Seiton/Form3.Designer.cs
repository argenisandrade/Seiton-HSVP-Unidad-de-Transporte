namespace Seiton
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ordenDeMovilizaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autorizaciónDeSalidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informeDeMovilizaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordenDeMovilizaciónToolStripMenuItem,
            this.autorizaciónDeSalidaToolStripMenuItem,
            this.informeDeMovilizaciónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1221, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ordenDeMovilizaciónToolStripMenuItem
            // 
            this.ordenDeMovilizaciónToolStripMenuItem.Name = "ordenDeMovilizaciónToolStripMenuItem";
            this.ordenDeMovilizaciónToolStripMenuItem.Size = new System.Drawing.Size(138, 20);
            this.ordenDeMovilizaciónToolStripMenuItem.Text = "Orden de Movilización";
            this.ordenDeMovilizaciónToolStripMenuItem.Click += new System.EventHandler(this.ordenDeMovilizaciónToolStripMenuItem_Click);
            // 
            // autorizaciónDeSalidaToolStripMenuItem
            // 
            this.autorizaciónDeSalidaToolStripMenuItem.Name = "autorizaciónDeSalidaToolStripMenuItem";
            this.autorizaciónDeSalidaToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.autorizaciónDeSalidaToolStripMenuItem.Text = "Autorización de Salida";
            this.autorizaciónDeSalidaToolStripMenuItem.Click += new System.EventHandler(this.autorizaciónDeSalidaToolStripMenuItem_Click);
            // 
            // informeDeMovilizaciónToolStripMenuItem
            // 
            this.informeDeMovilizaciónToolStripMenuItem.Name = "informeDeMovilizaciónToolStripMenuItem";
            this.informeDeMovilizaciónToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.informeDeMovilizaciónToolStripMenuItem.Text = "Informe de Movilización";
            this.informeDeMovilizaciónToolStripMenuItem.Click += new System.EventHandler(this.informeDeMovilizaciónToolStripMenuItem_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 538);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form3";
            this.Text = "Seiton";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ordenDeMovilizaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autorizaciónDeSalidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informeDeMovilizaciónToolStripMenuItem;
    }
}