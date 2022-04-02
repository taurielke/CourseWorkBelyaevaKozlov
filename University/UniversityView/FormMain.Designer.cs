
namespace UniversityView
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLearningPlans = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemStudents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAttestations = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1315, 33);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLearningPlans,
            this.toolStripMenuItemStudents,
            this.toolStripMenuItemAttestations});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(139, 29);
            this.toolStripMenuItemHelp.Text = "Справочники";
            // 
            // toolStripMenuItemLearningPlans
            // 
            this.toolStripMenuItemLearningPlans.Name = "toolStripMenuItemLearningPlans";
            this.toolStripMenuItemLearningPlans.Size = new System.Drawing.Size(289, 34);
            this.toolStripMenuItemLearningPlans.Text = "Планы обучения";
            // 
            // toolStripMenuItemStudents
            // 
            this.toolStripMenuItemStudents.Name = "toolStripMenuItemStudents";
            this.toolStripMenuItemStudents.Size = new System.Drawing.Size(289, 34);
            this.toolStripMenuItemStudents.Text = "Студенты";
            // 
            // toolStripMenuItemAttestations
            // 
            this.toolStripMenuItemAttestations.Name = "toolStripMenuItemAttestations";
            this.toolStripMenuItemAttestations.Size = new System.Drawing.Size(289, 34);
            this.toolStripMenuItemAttestations.Text = "Аттестация студентов";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(386, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(674, 700);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 764);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip);
            this.Name = "FormMain";
            this.Text = "Университет \"Все отчислены\"";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLearningPlans;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemStudents;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAttestations;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}