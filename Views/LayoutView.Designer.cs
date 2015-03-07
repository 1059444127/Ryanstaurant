namespace Views
{
    partial class LayoutView
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
            this.plFloor = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // plFloor
            // 
            this.plFloor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFloor.Location = new System.Drawing.Point(0, 0);
            this.plFloor.Name = "plFloor";
            this.plFloor.Size = new System.Drawing.Size(738, 613);
            this.plFloor.TabIndex = 0;
            // 
            // LayoutView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 613);
            this.ControlBox = false;
            this.Controls.Add(this.plFloor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayoutView";
            this.Text = "LayoutView";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plFloor;
    }
}