namespace FSKview
{
    partial class Form1
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
            this.audioControl1 = new FSKview.AudioControl();
            this.SuspendLayout();
            // 
            // audioControl1
            // 
            this.audioControl1.Location = new System.Drawing.Point(12, 12);
            this.audioControl1.Name = "audioControl1";
            this.audioControl1.SampleRate = 6000;
            this.audioControl1.Size = new System.Drawing.Size(161, 41);
            this.audioControl1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.audioControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FSKview";
            this.ResumeLayout(false);

        }

        #endregion
        private AudioControl audioControl1;
    }
}

