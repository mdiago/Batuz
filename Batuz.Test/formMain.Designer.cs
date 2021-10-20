
namespace Batuz.Test
{
    partial class formMain
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
            this.btCrearPdfTicketBai = new System.Windows.Forms.Button();
            this.btCrearTicketBai = new System.Windows.Forms.Button();
            this.btCrearTicketBaiFirmado = new System.Windows.Forms.Button();
            this.btValidar = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wBr = new System.Windows.Forms.WebBrowser();
            this.btSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCrearPdfTicketBai
            // 
            this.btCrearPdfTicketBai.Location = new System.Drawing.Point(52, 128);
            this.btCrearPdfTicketBai.Name = "btCrearPdfTicketBai";
            this.btCrearPdfTicketBai.Size = new System.Drawing.Size(144, 23);
            this.btCrearPdfTicketBai.TabIndex = 0;
            this.btCrearPdfTicketBai.Text = "Crear PDF TicketBai";
            this.btCrearPdfTicketBai.UseVisualStyleBackColor = true;
            this.btCrearPdfTicketBai.Click += new System.EventHandler(this.btCrearPdfTicketBai_Click);
            // 
            // btCrearTicketBai
            // 
            this.btCrearTicketBai.Location = new System.Drawing.Point(52, 169);
            this.btCrearTicketBai.Name = "btCrearTicketBai";
            this.btCrearTicketBai.Size = new System.Drawing.Size(144, 23);
            this.btCrearTicketBai.TabIndex = 1;
            this.btCrearTicketBai.Text = "Crear TicketBai";
            this.btCrearTicketBai.UseVisualStyleBackColor = true;
            this.btCrearTicketBai.Click += new System.EventHandler(this.btCrearTicketBai_Click);
            // 
            // btCrearTicketBaiFirmado
            // 
            this.btCrearTicketBaiFirmado.Location = new System.Drawing.Point(52, 208);
            this.btCrearTicketBaiFirmado.Name = "btCrearTicketBaiFirmado";
            this.btCrearTicketBaiFirmado.Size = new System.Drawing.Size(144, 23);
            this.btCrearTicketBaiFirmado.TabIndex = 2;
            this.btCrearTicketBaiFirmado.Text = "Crear TicketBai Firmado";
            this.btCrearTicketBaiFirmado.UseVisualStyleBackColor = true;
            this.btCrearTicketBaiFirmado.Click += new System.EventHandler(this.btCrearTicketBaiFirmado_Click);
            // 
            // btValidar
            // 
            this.btValidar.Location = new System.Drawing.Point(52, 255);
            this.btValidar.Name = "btValidar";
            this.btValidar.Size = new System.Drawing.Size(144, 23);
            this.btValidar.TabIndex = 3;
            this.btValidar.Text = "Valida TicketBai Firmado";
            this.btValidar.UseVisualStyleBackColor = true;
            this.btValidar.Click += new System.EventHandler(this.btValidar_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btSend);
            this.splitContainer1.Panel1.Controls.Add(this.btCrearPdfTicketBai);
            this.splitContainer1.Panel1.Controls.Add(this.btValidar);
            this.splitContainer1.Panel1.Controls.Add(this.btCrearTicketBai);
            this.splitContainer1.Panel1.Controls.Add(this.btCrearTicketBaiFirmado);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wBr);
            this.splitContainer1.Size = new System.Drawing.Size(985, 623);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 4;
            // 
            // wBr
            // 
            this.wBr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wBr.Location = new System.Drawing.Point(0, 0);
            this.wBr.MinimumSize = new System.Drawing.Size(20, 20);
            this.wBr.Name = "wBr";
            this.wBr.Size = new System.Drawing.Size(653, 623);
            this.wBr.TabIndex = 0;
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(52, 299);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(144, 23);
            this.btSend.TabIndex = 4;
            this.btSend.Text = "Envía TicketBai Firmado";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 623);
            this.Controls.Add(this.splitContainer1);
            this.Name = "formMain";
            this.Text = "TEST BATUZ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCrearPdfTicketBai;
        private System.Windows.Forms.Button btCrearTicketBai;
        private System.Windows.Forms.Button btCrearTicketBaiFirmado;
        private System.Windows.Forms.Button btValidar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.WebBrowser wBr;
        private System.Windows.Forms.Button btSend;
    }
}

