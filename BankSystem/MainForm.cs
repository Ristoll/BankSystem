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
            vw_ClientsTableAdapter1.Fill(bankdbDataSet1.vw_Clients);
            ShowTable(bankdbDataSet1.vw_Clients);
            HighlightMenuColor(êë³ºíòèToolStripMenuItem);
        }

        private void ğàõóíêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_AccountsTableAdapter1.Fill(bankdbDataSet1.vw_Accounts);
            ShowTable(bankdbDataSet1.vw_Accounts);
            HighlightMenuColor(ğàõóíêèToolStripMenuItem);
        }

        private void òğàíçàêö³¿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_TransactionsTableAdapter1.Fill(bankdbDataSet1.vw_Transactions);
            ShowTable(bankdbDataSet1.vw_Transactions);
            HighlightMenuColor(òğàíçàêö³¿ToolStripMenuItem);
        }

        private void êğåäèòèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_CreditsTableAdapter1.Fill(bankdbDataSet1.vw_Credits);
            ShowTable(bankdbDataSet1.vw_Credits);
            HighlightMenuColor(êğåäèòèToolStripMenuItem);
        }

        private void ïëàòåæ³ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_PaymentsTableAdapter1.Fill(bankdbDataSet1.vw_Payments);
            ShowTable(bankdbDataSet1.vw_Payments);
            HighlightMenuColor(ïëàòåæ³ToolStripMenuItem);
        }

        private void ñï³âğîáèòíèêèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_EmployeesTableAdapter1.Fill(bankdbDataSet1.vw_Employees);
            ShowTable(bankdbDataSet1.vw_Employees);
            HighlightMenuColor(ñï³âğîáèòíèêèToolStripMenuItem);
        }

        private void â³ää³ëåííÿToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vw_BankBranchesTableAdapter1.Fill(bankdbDataSet1.vw_BankBranches);
            ShowTable(bankdbDataSet1.vw_BankBranches);
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
