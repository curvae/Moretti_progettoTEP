namespace ServerProvaTask
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textboxinutileAEEW = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_output = new System.Windows.Forms.Label();
            this.label_output_2 = new System.Windows.Forms.Label();
            this.label_output_ball_top = new System.Windows.Forms.Label();
            this.label_output_ball_left = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(365, 196);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textboxinutileAEEW
            // 
            this.textboxinutileAEEW.AutoSize = true;
            this.textboxinutileAEEW.Location = new System.Drawing.Point(365, 255);
            this.textboxinutileAEEW.Name = "textboxinutileAEEW";
            this.textboxinutileAEEW.Size = new System.Drawing.Size(14, 16);
            this.textboxinutileAEEW.TabIndex = 2;
            this.textboxinutileAEEW.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(203, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "aspettando il player:";
            // 
            // label_output
            // 
            this.label_output.AutoSize = true;
            this.label_output.Location = new System.Drawing.Point(206, 121);
            this.label_output.Name = "label_output";
            this.label_output.Size = new System.Drawing.Size(21, 16);
            this.label_output.TabIndex = 4;
            this.label_output.Text = "+0";
            this.label_output.Click += new System.EventHandler(this.label_output_Click);
            // 
            // label_output_2
            // 
            this.label_output_2.AutoSize = true;
            this.label_output_2.Location = new System.Drawing.Point(263, 121);
            this.label_output_2.Name = "label_output_2";
            this.label_output_2.Size = new System.Drawing.Size(28, 16);
            this.label_output_2.TabIndex = 5;
            this.label_output_2.Text = "190";
            // 
            // label_output_ball_top
            // 
            this.label_output_ball_top.AutoSize = true;
            this.label_output_ball_top.Location = new System.Drawing.Point(425, 121);
            this.label_output_ball_top.Name = "label_output_ball_top";
            this.label_output_ball_top.Size = new System.Drawing.Size(28, 16);
            this.label_output_ball_top.TabIndex = 6;
            this.label_output_ball_top.Text = "180";
            // 
            // label_output_ball_left
            // 
            this.label_output_ball_left.AutoSize = true;
            this.label_output_ball_left.Location = new System.Drawing.Point(482, 121);
            this.label_output_ball_left.Name = "label_output_ball_left";
            this.label_output_ball_left.Size = new System.Drawing.Size(28, 16);
            this.label_output_ball_left.TabIndex = 7;
            this.label_output_ball_left.Text = "290";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_output_ball_left);
            this.Controls.Add(this.label_output_ball_top);
            this.Controls.Add(this.label_output_2);
            this.Controls.Add(this.label_output);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxinutileAEEW);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label textboxinutileAEEW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_output;
        private System.Windows.Forms.Label label_output_2;
        private System.Windows.Forms.Label label_output_ball_top;
        private System.Windows.Forms.Label label_output_ball_left;
    }
}

