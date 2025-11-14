using System.Data;

namespace BankSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void ShowTable(DataTable table)
        {
            bindingSource1.DataSource = table;
            dataGridView1.DataSource = bindingSource1;
        }
        private void êë³ºíòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientsTableAdapter1.Fill(bankdbDataSet1.Clients);
            ShowTable(bankdbDataSet1.Clients);
            HighlightMenuColor(êë³ºíòèToolStripMenuItem);
        }

        private void ğàõóíêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            accountsTableAdapter1.Fill(bankdbDataSet1.Accounts);
            ShowTable(bankdbDataSet1.Accounts);
            HighlightMenuColor(ğàõóíêèToolStripMenuItem);
        }

        private void òğàíçàêö³¿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionsTableAdapter1.Fill(bankdbDataSet1.Transactions);
            ShowTable(bankdbDataSet1.Transactions);
            HighlightMenuColor(òğàíçàêö³¿ToolStripMenuItem);
        }

        private void êğåäèòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            creditsTableAdapter1.Fill(bankdbDataSet1.Credits);
            ShowTable(bankdbDataSet1.Credits);
            HighlightMenuColor(êğåäèòèToolStripMenuItem);
        }

        private void ïëàòåæ³ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paymentsTableAdapter1.Fill(bankdbDataSet1.Payments);
            ShowTable(bankdbDataSet1.Payments);
            HighlightMenuColor(ïëàòåæ³ToolStripMenuItem);
        }

        private void ñï³âğîáèòíèêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            employeesTableAdapter1.Fill(bankdbDataSet1.Employees);
            ShowTable(bankdbDataSet1.Employees);
            HighlightMenuColor(ñï³âğîáèòíèêèToolStripMenuItem);
        }

        private void â³ää³ëåííÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bankBranchesTableAdapter1.Fill(bankdbDataSet1.BankBranches);
            ShowTable(bankdbDataSet1.BankBranches);
            HighlightMenuColor(â³ää³ëåííÿToolStripMenuItem);
        }
        private void HighlightMenuColor(ToolStripMenuItem activeItem)
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                ResetMenuColor(item);
            }

            activeItem.BackColor = Color.LightBlue;
        }
        private void ResetMenuColor(ToolStripMenuItem parentItem)
        {
            parentItem.BackColor = SystemColors.Control;
            foreach (ToolStripMenuItem subItem in parentItem.DropDownItems)
            {
                if (subItem is ToolStripMenuItem)
                    ResetMenuColor(subItem);
            }
        }
    }
}
