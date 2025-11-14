namespace BankSystem
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            клієнтиToolStripMenuItem = new ToolStripMenuItem();
            рахункиToolStripMenuItem = new ToolStripMenuItem();
            транзакціїToolStripMenuItem = new ToolStripMenuItem();
            кредитиToolStripMenuItem = new ToolStripMenuItem();
            платежіToolStripMenuItem = new ToolStripMenuItem();
            співробитникиToolStripMenuItem = new ToolStripMenuItem();
            відділенняToolStripMenuItem = new ToolStripMenuItem();
            звітиToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            dataGridView1 = new DataGridView();
            bindingSource1 = new BindingSource(components);
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { клієнтиToolStripMenuItem, рахункиToolStripMenuItem, транзакціїToolStripMenuItem, кредитиToolStripMenuItem, платежіToolStripMenuItem, співробитникиToolStripMenuItem, відділенняToolStripMenuItem, звітиToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // клієнтиToolStripMenuItem
            // 
            клієнтиToolStripMenuItem.Name = "клієнтиToolStripMenuItem";
            клієнтиToolStripMenuItem.Size = new Size(75, 24);
            клієнтиToolStripMenuItem.Text = "Клієнти";
            // 
            // рахункиToolStripMenuItem
            // 
            рахункиToolStripMenuItem.Name = "рахункиToolStripMenuItem";
            рахункиToolStripMenuItem.Size = new Size(78, 24);
            рахункиToolStripMenuItem.Text = "Рахунки";
            // 
            // транзакціїToolStripMenuItem
            // 
            транзакціїToolStripMenuItem.Name = "транзакціїToolStripMenuItem";
            транзакціїToolStripMenuItem.Size = new Size(96, 24);
            транзакціїToolStripMenuItem.Text = "Транзакції";
            // 
            // кредитиToolStripMenuItem
            // 
            кредитиToolStripMenuItem.Name = "кредитиToolStripMenuItem";
            кредитиToolStripMenuItem.Size = new Size(81, 24);
            кредитиToolStripMenuItem.Text = "Кредити";
            // 
            // платежіToolStripMenuItem
            // 
            платежіToolStripMenuItem.Name = "платежіToolStripMenuItem";
            платежіToolStripMenuItem.Size = new Size(79, 24);
            платежіToolStripMenuItem.Text = "Платежі";
            // 
            // співробитникиToolStripMenuItem
            // 
            співробитникиToolStripMenuItem.Name = "співробитникиToolStripMenuItem";
            співробитникиToolStripMenuItem.Size = new Size(129, 24);
            співробитникиToolStripMenuItem.Text = "Співробитники";
            // 
            // відділенняToolStripMenuItem
            // 
            відділенняToolStripMenuItem.Name = "відділенняToolStripMenuItem";
            відділенняToolStripMenuItem.Size = new Size(98, 24);
            відділенняToolStripMenuItem.Text = "Відділення";
            // 
            // звітиToolStripMenuItem
            // 
            звітиToolStripMenuItem.Name = "звітиToolStripMenuItem";
            звітиToolStripMenuItem.Size = new Size(58, 24);
            звітиToolStripMenuItem.Text = "Звіти";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 28);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(800, 422);
            splitContainer1.SplitterDistance = 203;
            splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(794, 200);
            dataGridView1.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem клієнтиToolStripMenuItem;
        private ToolStripMenuItem рахункиToolStripMenuItem;
        private ToolStripMenuItem транзакціїToolStripMenuItem;
        private ToolStripMenuItem кредитиToolStripMenuItem;
        private ToolStripMenuItem платежіToolStripMenuItem;
        private ToolStripMenuItem співробитникиToolStripMenuItem;
        private ToolStripMenuItem відділенняToolStripMenuItem;
        private ToolStripMenuItem звітиToolStripMenuItem;
        private SplitContainer splitContainer1;
        private DataGridView dataGridView1;
        private BindingSource bindingSource1;
    }
}
