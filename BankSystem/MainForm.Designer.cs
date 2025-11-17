
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
            searchPanel = new Panel();
            reportPanel = new Panel();
            accountPanel = new Panel();
            button3 = new Button();
            clientPanel = new Panel();
            button1 = new Button();
            textBox7 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            textBox11 = new TextBox();
            textBox10 = new TextBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            textBox9 = new TextBox();
            button2 = new Button();
            label9 = new Label();
            textBox8 = new TextBox();
            menuStrip2 = new MenuStrip();
            операцToolStripMenuItem = new ToolStripMenuItem();
            додатиКлієнтаToolStripMenuItem = new ToolStripMenuItem();
            редагуватиКлієнтаToolStripMenuItem = new ToolStripMenuItem();
            додатиРахунокToolStripMenuItem = new ToolStripMenuItem();
            редагуватиРахунокToolStripMenuItem = new ToolStripMenuItem();
            фільтраціяToolStripMenuItem = new ToolStripMenuItem();
            клієнтиЗаТипомРахункуToolStripMenuItem = new ToolStripMenuItem();
            рахункиЗаВалютоюToolStripMenuItem = new ToolStripMenuItem();
            рахункиЗаСтатусомToolStripMenuItem = new ToolStripMenuItem();
            пошукToolStripMenuItem = new ToolStripMenuItem();
            КлієнтаЗаІменемToolStripMenuItem1 = new ToolStripMenuItem();
            клієнтаЗаНомеромТелефонуToolStripMenuItem = new ToolStripMenuItem();
            рахункуЗаВласникомToolStripMenuItem = new ToolStripMenuItem();
            генераціяЗвітуToolStripMenuItem = new ToolStripMenuItem();
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem = new ToolStripMenuItem();
            випискаПоРахункуЗаПеріодToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            searchPanel.SuspendLayout();
            reportPanel.SuspendLayout();
            accountPanel.SuspendLayout();
            clientPanel.SuspendLayout();
            menuStrip2.SuspendLayout();
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
            клієнтиToolStripMenuItem.Click += клієнтиToolStripMenuItem_Click;
            // 
            // рахункиToolStripMenuItem
            // 
            рахункиToolStripMenuItem.Name = "рахункиToolStripMenuItem";
            рахункиToolStripMenuItem.Size = new Size(78, 24);
            рахункиToolStripMenuItem.Text = "Рахунки";
            рахункиToolStripMenuItem.Click += рахункиToolStripMenuItem_Click;
            // 
            // транзакціїToolStripMenuItem
            // 
            транзакціїToolStripMenuItem.Name = "транзакціїToolStripMenuItem";
            транзакціїToolStripMenuItem.Size = new Size(96, 24);
            транзакціїToolStripMenuItem.Text = "Транзакції";
            транзакціїToolStripMenuItem.Click += транзакціїToolStripMenuItem_Click;
            // 
            // кредитиToolStripMenuItem
            // 
            кредитиToolStripMenuItem.Name = "кредитиToolStripMenuItem";
            кредитиToolStripMenuItem.Size = new Size(81, 24);
            кредитиToolStripMenuItem.Text = "Кредити";
            кредитиToolStripMenuItem.Click += кредитиToolStripMenuItem_Click;
            // 
            // платежіToolStripMenuItem
            // 
            платежіToolStripMenuItem.Name = "платежіToolStripMenuItem";
            платежіToolStripMenuItem.Size = new Size(79, 24);
            платежіToolStripMenuItem.Text = "Платежі";
            платежіToolStripMenuItem.Click += платежіToolStripMenuItem_Click;
            // 
            // співробитникиToolStripMenuItem
            // 
            співробитникиToolStripMenuItem.Name = "співробитникиToolStripMenuItem";
            співробитникиToolStripMenuItem.Size = new Size(129, 24);
            співробитникиToolStripMenuItem.Text = "Співробитники";
            співробитникиToolStripMenuItem.Click += співробитникиToolStripMenuItem_Click;
            // 
            // відділенняToolStripMenuItem
            // 
            відділенняToolStripMenuItem.Name = "відділенняToolStripMenuItem";
            відділенняToolStripMenuItem.Size = new Size(98, 24);
            відділенняToolStripMenuItem.Text = "Відділення";
            відділенняToolStripMenuItem.Click += відділенняToolStripMenuItem_Click;
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
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(searchPanel);
            splitContainer1.Panel2.Controls.Add(menuStrip2);
            splitContainer1.Size = new Size(800, 422);
            splitContainer1.SplitterDistance = 203;
            splitContainer1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(800, 203);
            dataGridView1.TabIndex = 1;
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(reportPanel);
            searchPanel.Controls.Add(button2);
            searchPanel.Controls.Add(label9);
            searchPanel.Controls.Add(textBox8);
            searchPanel.Location = new Point(0, 31);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(791, 181);
            searchPanel.TabIndex = 4;
            searchPanel.Visible = false;
            // 
            // reportPanel
            // 
            reportPanel.Controls.Add(accountPanel);
            reportPanel.Controls.Add(textBox9);
            reportPanel.Location = new Point(3, 22);
            reportPanel.Name = "reportPanel";
            reportPanel.Size = new Size(797, 184);
            reportPanel.TabIndex = 5;
            reportPanel.Visible = false;
            // 
            // accountPanel
            // 
            accountPanel.Controls.Add(button3);
            accountPanel.Controls.Add(clientPanel);
            accountPanel.Controls.Add(comboBox2);
            accountPanel.Controls.Add(comboBox1);
            accountPanel.Controls.Add(textBox11);
            accountPanel.Controls.Add(textBox10);
            accountPanel.Controls.Add(label13);
            accountPanel.Controls.Add(label12);
            accountPanel.Controls.Add(label11);
            accountPanel.Controls.Add(label10);
            accountPanel.Location = new Point(136, 3);
            accountPanel.Name = "accountPanel";
            accountPanel.Size = new Size(797, 184);
            accountPanel.TabIndex = 5;
            accountPanel.Visible = false;
            // 
            // button3
            // 
            button3.Location = new Point(9, 143);
            button3.Name = "button3";
            button3.Size = new Size(157, 29);
            button3.TabIndex = 8;
            button3.Text = "Оформити рахунок";
            button3.UseVisualStyleBackColor = true;
            // 
            // clientPanel
            // 
            clientPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            clientPanel.Controls.Add(button1);
            clientPanel.Controls.Add(textBox7);
            clientPanel.Controls.Add(textBox4);
            clientPanel.Controls.Add(textBox5);
            clientPanel.Controls.Add(textBox6);
            clientPanel.Controls.Add(label8);
            clientPanel.Controls.Add(label7);
            clientPanel.Controls.Add(label6);
            clientPanel.Controls.Add(label5);
            clientPanel.Controls.Add(label4);
            clientPanel.Controls.Add(dateTimePicker1);
            clientPanel.Controls.Add(label3);
            clientPanel.Controls.Add(label2);
            clientPanel.Controls.Add(label1);
            clientPanel.Controls.Add(textBox3);
            clientPanel.Controls.Add(textBox2);
            clientPanel.Controls.Add(textBox1);
            clientPanel.Location = new Point(224, 3);
            clientPanel.Name = "clientPanel";
            clientPanel.Size = new Size(800, 181);
            clientPanel.TabIndex = 1;
            clientPanel.Visible = false;
            // 
            // button1
            // 
            button1.Location = new Point(18, 149);
            button1.Name = "button1";
            button1.Size = new Size(147, 29);
            button1.TabIndex = 15;
            button1.Text = "Оформити клієнта";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(527, 114);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(173, 27);
            textBox7.TabIndex = 14;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(527, 79);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(173, 27);
            textBox4.TabIndex = 13;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(527, 46);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(173, 27);
            textBox5.TabIndex = 12;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(527, 13);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(173, 27);
            textBox6.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(374, 117);
            label8.Name = "label8";
            label8.Size = new Size(59, 20);
            label8.TabIndex = 10;
            label8.Text = "Адреса";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(374, 82);
            label7.Name = "label7";
            label7.Size = new Size(76, 20);
            label7.TabIndex = 9;
            label7.Text = "Ел. пошта";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(374, 49);
            label6.Name = "label6";
            label6.Size = new Size(126, 20);
            label6.TabIndex = 8;
            label6.Text = "Номер телефону";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(374, 16);
            label5.Name = "label5";
            label5.Size = new Size(126, 20);
            label5.TabIndex = 7;
            label5.Text = "Номер паспорта";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 117);
            label4.Name = "label4";
            label4.Size = new Size(133, 20);
            label4.TabIndex = 6;
            label4.Text = "Дата народження";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(157, 112);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(173, 27);
            dateTimePicker1.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 82);
            label3.Name = "label3";
            label3.Size = new Size(92, 20);
            label3.TabIndex = 4;
            label3.Text = "По батькові";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 49);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 3;
            label2.Text = "Прізвище";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 16);
            label1.Name = "label1";
            label1.Size = new Size(35, 20);
            label1.TabIndex = 2;
            label1.Text = "Ім'я";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(157, 79);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(173, 27);
            textBox3.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(157, 46);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(173, 27);
            textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(157, 13);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(173, 27);
            textBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(584, 64);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(151, 28);
            comboBox2.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(151, 64);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 6;
            // 
            // textBox11
            // 
            textBox11.Location = new Point(584, 26);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(151, 27);
            textBox11.TabIndex = 5;
            // 
            // textBox10
            // 
            textBox10.Location = new Point(151, 26);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(151, 27);
            textBox10.TabIndex = 4;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(440, 29);
            label13.Name = "label13";
            label13.Size = new Size(58, 20);
            label13.TabIndex = 3;
            label13.Text = "Баланс";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(438, 67);
            label12.Name = "label12";
            label12.Size = new Size(60, 20);
            label12.TabIndex = 2;
            label12.Text = "Валюта";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(9, 67);
            label11.Name = "label11";
            label11.Size = new Size(93, 20);
            label11.TabIndex = 1;
            label11.Text = "Тип рахунку";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(9, 29);
            label10.Name = "label10";
            label10.Size = new Size(86, 20);
            label10.TabIndex = 0;
            label10.Text = "Номер тел.";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(12, 12);
            textBox9.Multiline = true;
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(776, 160);
            textBox9.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(12, 62);
            button2.Name = "button2";
            button2.Size = new Size(238, 29);
            button2.TabIndex = 4;
            button2.Text = "Знайти за іменем";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 22);
            label9.Name = "label9";
            label9.Size = new Size(55, 20);
            label9.TabIndex = 2;
            label9.Text = "Пошук";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(85, 19);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(703, 27);
            textBox8.TabIndex = 3;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { операцToolStripMenuItem, фільтраціяToolStripMenuItem, пошукToolStripMenuItem, генераціяЗвітуToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(800, 28);
            menuStrip2.TabIndex = 0;
            menuStrip2.Text = "menuStrip2";
            // 
            // операцToolStripMenuItem
            // 
            операцToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { додатиКлієнтаToolStripMenuItem, редагуватиКлієнтаToolStripMenuItem, додатиРахунокToolStripMenuItem, редагуватиРахунокToolStripMenuItem });
            операцToolStripMenuItem.Name = "операцToolStripMenuItem";
            операцToolStripMenuItem.Size = new Size(89, 24);
            операцToolStripMenuItem.Text = "Операція";
            // 
            // додатиКлієнтаToolStripMenuItem
            // 
            додатиКлієнтаToolStripMenuItem.Name = "додатиКлієнтаToolStripMenuItem";
            додатиКлієнтаToolStripMenuItem.Size = new Size(228, 26);
            додатиКлієнтаToolStripMenuItem.Text = "Додати клієнта";
            додатиКлієнтаToolStripMenuItem.Visible = false;
            додатиКлієнтаToolStripMenuItem.Click += додатиКлієнтаToolStripMenuItem_Click;
            // 
            // редагуватиКлієнтаToolStripMenuItem
            // 
            редагуватиКлієнтаToolStripMenuItem.Name = "редагуватиКлієнтаToolStripMenuItem";
            редагуватиКлієнтаToolStripMenuItem.Size = new Size(228, 26);
            редагуватиКлієнтаToolStripMenuItem.Text = "Редагувати клієнта";
            редагуватиКлієнтаToolStripMenuItem.Visible = false;
            редагуватиКлієнтаToolStripMenuItem.Click += редагуватиКлієнтаToolStripMenuItem_Click;
            // 
            // додатиРахунокToolStripMenuItem
            // 
            додатиРахунокToolStripMenuItem.Name = "додатиРахунокToolStripMenuItem";
            додатиРахунокToolStripMenuItem.Size = new Size(228, 26);
            додатиРахунокToolStripMenuItem.Text = "Додати рахунок";
            додатиРахунокToolStripMenuItem.Visible = false;
            // 
            // редагуватиРахунокToolStripMenuItem
            // 
            редагуватиРахунокToolStripMenuItem.Name = "редагуватиРахунокToolStripMenuItem";
            редагуватиРахунокToolStripMenuItem.Size = new Size(228, 26);
            редагуватиРахунокToolStripMenuItem.Text = "Редагувати рахунок";
            редагуватиРахунокToolStripMenuItem.Visible = false;
            // 
            // фільтраціяToolStripMenuItem
            // 
            фільтраціяToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { клієнтиЗаТипомРахункуToolStripMenuItem, рахункиЗаВалютоюToolStripMenuItem, рахункиЗаСтатусомToolStripMenuItem });
            фільтраціяToolStripMenuItem.Name = "фільтраціяToolStripMenuItem";
            фільтраціяToolStripMenuItem.Size = new Size(98, 24);
            фільтраціяToolStripMenuItem.Text = "Фільтрація";
            // 
            // клієнтиЗаТипомРахункуToolStripMenuItem
            // 
            клієнтиЗаТипомРахункуToolStripMenuItem.Name = "клієнтиЗаТипомРахункуToolStripMenuItem";
            клієнтиЗаТипомРахункуToolStripMenuItem.Size = new Size(269, 26);
            клієнтиЗаТипомРахункуToolStripMenuItem.Text = "Клієнти за типом рахунку";
            клієнтиЗаТипомРахункуToolStripMenuItem.Visible = false;
            // 
            // рахункиЗаВалютоюToolStripMenuItem
            // 
            рахункиЗаВалютоюToolStripMenuItem.Name = "рахункиЗаВалютоюToolStripMenuItem";
            рахункиЗаВалютоюToolStripMenuItem.Size = new Size(269, 26);
            рахункиЗаВалютоюToolStripMenuItem.Text = "Рахунки за валютою";
            рахункиЗаВалютоюToolStripMenuItem.Visible = false;
            // 
            // рахункиЗаСтатусомToolStripMenuItem
            // 
            рахункиЗаСтатусомToolStripMenuItem.Name = "рахункиЗаСтатусомToolStripMenuItem";
            рахункиЗаСтатусомToolStripMenuItem.Size = new Size(269, 26);
            рахункиЗаСтатусомToolStripMenuItem.Text = "Рахунки за статусом";
            рахункиЗаСтатусомToolStripMenuItem.Visible = false;
            // 
            // пошукToolStripMenuItem
            // 
            пошукToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { КлієнтаЗаІменемToolStripMenuItem1, клієнтаЗаНомеромТелефонуToolStripMenuItem, рахункуЗаВласникомToolStripMenuItem });
            пошукToolStripMenuItem.Name = "пошукToolStripMenuItem";
            пошукToolStripMenuItem.Size = new Size(69, 24);
            пошукToolStripMenuItem.Text = "Пошук";
            // 
            // КлієнтаЗаІменемToolStripMenuItem1
            // 
            КлієнтаЗаІменемToolStripMenuItem1.Name = "КлієнтаЗаІменемToolStripMenuItem1";
            КлієнтаЗаІменемToolStripMenuItem1.Size = new Size(301, 26);
            КлієнтаЗаІменемToolStripMenuItem1.Text = "Клієнта за іменем";
            КлієнтаЗаІменемToolStripMenuItem1.Visible = false;
            КлієнтаЗаІменемToolStripMenuItem1.Click += КлієнтаЗаІменемToolStripMenuItem1_Click;
            // 
            // клієнтаЗаНомеромТелефонуToolStripMenuItem
            // 
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Name = "клієнтаЗаНомеромТелефонуToolStripMenuItem";
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Size = new Size(301, 26);
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Text = "Клієнта за номером телефону";
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Visible = false;
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Click += клієнтаЗаНомеромТелефонуToolStripMenuItem_Click;
            // 
            // рахункуЗаВласникомToolStripMenuItem
            // 
            рахункуЗаВласникомToolStripMenuItem.Name = "рахункуЗаВласникомToolStripMenuItem";
            рахункуЗаВласникомToolStripMenuItem.Size = new Size(301, 26);
            рахункуЗаВласникомToolStripMenuItem.Text = "Рахунку за власником";
            рахункуЗаВласникомToolStripMenuItem.Visible = false;
            // 
            // генераціяЗвітуToolStripMenuItem
            // 
            генераціяЗвітуToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem, випискаПоРахункуЗаПеріодToolStripMenuItem });
            генераціяЗвітуToolStripMenuItem.Name = "генераціяЗвітуToolStripMenuItem";
            генераціяЗвітуToolStripMenuItem.Size = new Size(129, 24);
            генераціяЗвітуToolStripMenuItem.Text = "Генерація звіту";
            // 
            // списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem
            // 
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Name = "списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem";
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Size = new Size(417, 26);
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Text = "Список активних рахунків конкретного клієнта";
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Visible = false;
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Click += списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem_Click;
            // 
            // випискаПоРахункуЗаПеріодToolStripMenuItem
            // 
            випискаПоРахункуЗаПеріодToolStripMenuItem.Name = "випискаПоРахункуЗаПеріодToolStripMenuItem";
            випискаПоРахункуЗаПеріодToolStripMenuItem.Size = new Size(417, 26);
            випискаПоРахункуЗаПеріодToolStripMenuItem.Text = "Виписка по рахунку за період";
            випискаПоРахункуЗаПеріодToolStripMenuItem.Visible = false;
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
            Text = "Облік клієнтів банку";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            reportPanel.ResumeLayout(false);
            reportPanel.PerformLayout();
            accountPanel.ResumeLayout(false);
            accountPanel.PerformLayout();
            clientPanel.ResumeLayout(false);
            clientPanel.PerformLayout();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
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
        private BindingSource bindingSource1;
        private DataGridView dataGridView1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem операцToolStripMenuItem;
        private ToolStripMenuItem фільтраціяToolStripMenuItem;
        private Panel clientPanel;
        private ToolStripMenuItem додатиКлієнтаToolStripMenuItem;
        private ToolStripMenuItem редагуватиКлієнтаToolStripMenuItem;
        private ToolStripMenuItem пошукToolStripMenuItem;
        private ToolStripMenuItem генераціяЗвітуToolStripMenuItem;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private Label label4;
        private Label label5;
        private Label label7;
        private Label label6;
        private Label label8;
        private TextBox textBox7;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private Button button1;
        private ToolStripMenuItem клієнтиЗаТипомРахункуToolStripMenuItem;
        private ToolStripMenuItem КлієнтаЗаІменемToolStripMenuItem1;
        private ToolStripMenuItem клієнтаЗаНомеромТелефонуToolStripMenuItem;
        private TextBox textBox8;
        private Label label9;
        private Panel searchPanel;
        private Button button2;
        private ToolStripMenuItem списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem;
        private Panel reportPanel;
        private TextBox textBox9;
        private ToolStripMenuItem додатиРахунокToolStripMenuItem;
        private ToolStripMenuItem редагуватиРахунокToolStripMenuItem;
        private ToolStripMenuItem рахункиЗаВалютоюToolStripMenuItem;
        private ToolStripMenuItem рахункиЗаСтатусомToolStripMenuItem;
        private ToolStripMenuItem рахункуЗаВласникомToolStripMenuItem;
        private ToolStripMenuItem випискаПоРахункуЗаПеріодToolStripMenuItem;
        private Panel accountPanel;
        private Label label12;
        private Label label11;
        private Label label10;
        private TextBox textBox11;
        private TextBox textBox10;
        private Label label13;
        private Button button3;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
    }
}
