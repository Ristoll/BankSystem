using BankSystem.ApiClients;
using BankSystem.BankSystemDBDataSetTableAdapters;
using BLL.Commands.ClientsCommands;
using BLL.Services;
using Core.Entities;
using DTO;
using System.Data;
using System.Windows.Forms;
using static BankSystem.BankSystemDBDataSet;

namespace BankSystem
{
    public partial class MainForm : Form
    {
        private ReportService reportService;
        private readonly ClientsApiClient clientsApiClient;
        private string currentTable = "";

        public MainForm()
        {
            InitializeComponent();
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5136/") // або адреса твого WebAPI
            };

            clientsApiClient = new ClientsApiClient(httpClient);
            dataGridView1.AutoGenerateColumns = true;

            bankSystemdbDataSet1 = new BankSystemDBDataSet();
            // Динамічно створюємо підменю
            PopulateAccountTypesSubMenu();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void ShowTable(DataTable table)
        {
            bindingSource1.DataSource = table;
            dataGridView1.DataSource = bindingSource1;
        }
        private void клієнтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Clients";
            ShowSubMenuForTable(currentTable);
            vw_ClientsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Clients);
            ShowTable(bankSystemdbDataSet1.vw_Clients);
            HighlightMenuColor(клієнтиToolStripMenuItem);
            додатиКлієнтаToolStripMenuItem.Visible = true;
            редагуватиКлієнтаToolStripMenuItem.Visible = true;
        }

        private void рахункиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Accounts";
            ShowSubMenuForTable(currentTable);
            vw_AccountsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Accounts);
            ShowTable(bankSystemdbDataSet1.vw_Accounts);
            HighlightMenuColor(рахункиToolStripMenuItem);
        }

        private void транзакціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Transactions";
            vw_TransactionsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Transactions);
            ShowTable(bankSystemdbDataSet1.vw_Transactions);
            HighlightMenuColor(транзакціїToolStripMenuItem);
        }

        private void кредитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Credits";
            ShowSubMenuForTable(currentTable);
            vw_CreditsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Credits);
            ShowTable(bankSystemdbDataSet1.vw_Credits);
            HighlightMenuColor(кредитиToolStripMenuItem);
        }

        private void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";
            ShowSubMenuForTable(currentTable);
            vw_PaymentsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Payments);
            ShowTable(bankSystemdbDataSet1.vw_Payments);
            HighlightMenuColor(платежіToolStripMenuItem);
        }

        private void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";
            ShowSubMenuForTable(currentTable);
            vw_EmployeesTableAdapter1.Fill(bankSystemdbDataSet1.vw_Employees);
            ShowTable(bankSystemdbDataSet1.vw_Employees);
            HighlightMenuColor(співробитникиToolStripMenuItem);
        }

        private void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";
            ShowSubMenuForTable(currentTable);
            vw_BankBranchesTableAdapter1.Fill(bankSystemdbDataSet1.vw_BankBranches);
            ShowTable(bankSystemdbDataSet1.vw_BankBranches);
            HighlightMenuColor(відділенняToolStripMenuItem);
        }

        //Меню операцій
        //1.Клієнти
        private void додатиКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            ClearClientForm();
            button1.Text = "Оформити клієнта";
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            ClientDto clientDto = new ClientDto()
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text,
                MiddleName = textBox3.Text,
                DateOfBirth = DateOnly.FromDateTime(dateTimePicker1.Value),
                PassportNumber = textBox6.Text,
                Phone = textBox5.Text,
                Email = textBox4.Text,
                Address = textBox7.Text,
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now)
            };

            bool result;
            if (button1.Text == "Оформити клієнта")
            {
                result = await clientsApiClient.AddClientAsync(clientDto);

                ShowResult(result);
            }
            else
            {
                result = await clientsApiClient.UpdateClientAsync(clientDto);

                ShowResult(result);
            }
        }
        private void редагуватиКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            button1.Text = "Підтвердити редагування";
            FillFieldsFromSelectedRow();
        }
        //---------------
        //2.Рахунки
        private void додатиРахункиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(accountPanel);
            ClearClientForm();
            button1.Text = "Оформити рахунок";
        }

        private void редагуватиРахунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(accountPanel);
            button1.Text = "Підтвердити редагування";
            FillFieldsFromSelectedRow();
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //Меню фільтрації
        //1.Клієнти
        private void PopulateAccountTypesSubMenu()
        {
            // Очищаємо старі пункти
            клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Clear();

            // Створюємо тимчасову таблицю
            var accountTypesTable = new BankSystemDBDataSet.AccountTypesDataTable();

            // Заповнюємо через TableAdapter
            accountTypesTableAdapter1.Fill(accountTypesTable);

            // Перевіряємо, чи є дані
            if (accountTypesTable.Rows.Count == 0)
            {
                MessageBox.Show("Типи рахунків відсутні.");
                return;
            }

            // Перебираємо рядки і створюємо підменю
            foreach (var row in accountTypesTable)
            {
                string accountTypeName = row.Name; // колонка Name у AccountTypes

                var accountTypeDto = new AccountTypeDto { Name = accountTypeName };

                ToolStripMenuItem subItem = new ToolStripMenuItem(accountTypeName);
                subItem.Click += async (s, e) =>
                {
                    await LoadClientsByAccountTypeAsync(accountTypeDto);
                };

                клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }

        private async Task LoadClientsByAccountTypeAsync(AccountTypeDto accountTypeDto)
        {
            // Викликаємо метод API
            var clients = await clientsApiClient.FilterByAccountTypeAsync(accountTypeDto);

            // Оновлюємо DataGridView
            if (clients != null)
            {
                dataGridView1.DataSource = clients;
            }
            else
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("Клієнтів не знайдено або сталася помилка");
            }
        }
        ///------------------

        //Меню пошуку
        //1.Клієнти
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
        }
        private void клієнтаЗаНомеромТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Text = "Знайти за номером телефону";
        }
        private void клієнтиЗаТипомРахункуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.Text = "Знайти за іменем";
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            List<ClientDto>? result;

            if (button2.Text == "Знайти за іменем")
            {
                result = await clientsApiClient.SearchByFullNameAsync(textBox8.Text);
            }
            else
            {
                result = await clientsApiClient.SearchByPhoneNumberAsync(textBox8.Text);
            }

            if (result != null)
            {
                HighlightClientInDataGrid(result[0]);
            }
            else
            {
                MessageBox.Show("Такого кліжнта нема в БД.");
            }
        }

        //Методи заповнення елементів даними з дата сету
        private void FillClientFields(DataGridViewRow row)
        {
            textBox1.Text = row.Cells["FirstName"].Value?.ToString();
            textBox2.Text = row.Cells["LastName"].Value?.ToString();
            textBox3.Text = row.Cells["MiddleName"].Value?.ToString();
            textBox5.Text = row.Cells["Phone"].Value?.ToString();
            if (row.Cells["DateOfBirth"].Value != null)
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
            textBox4.Text = row.Cells["Email"].Value?.ToString();
            textBox6.Text = row.Cells["PassportNumber"].Value?.ToString();
            textBox7.Text = row.Cells["Address"].Value?.ToString();
        }

        private void FillAccountFields(DataGridView row)
        {
            
        }
        private void ClearClientForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }
        private void FillFieldsFromSelectedRow()
        {
            if (dataGridView1.CurrentRow == null) return;
            var row = dataGridView1.CurrentRow;

            switch (currentTable)
            {
                case "Clients":
                    FillClientFields(row);
                    break;
                case "Accounts":
                    FillClientFields(row);
                    break;
                case "Branches":
                    break;
                    // Додати інші таблиці
            }
        }
        //-------------------------------------

        //Меню звітів
        private async void списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(reportPanel);
            string? phone = GetSelectedClientPhone();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Будь ласка, виділіть клієнта у таблиці.");
                return;
            }

            // Шукаємо клієнта через API
            var clients = await clientsApiClient.SearchByPhoneNumberAsync(phone);

            if (clients == null || clients.Count == 0)
            {
                MessageBox.Show("Клієнта не знайдено через API.");
                return;
            }

            // Беремо Id першого знайденого клієнта
            int clientId = clients[0].ClientId;

            // Генеруємо звіт
            string report = reportService.GenerateActiveAccountsReportContent(clientId);

            // Відображаємо у TextBox
            textBox9.Text = report;
        }

        //Допоміжні методи
        private void ShowPanel(Panel panelToShow)
        {
            clientPanel.Visible = false;
            clientPanel.Parent = splitContainer1.Panel2;
            accountPanel.Visible = false;
            accountPanel.Parent = splitContainer1.Panel2;
            searchPanel.Visible = false;
            searchPanel.Parent = splitContainer1.Panel2;
            reportPanel.Visible = false;
            reportPanel.Parent = splitContainer1.Panel2;

            panelToShow.Visible = true;
            panelToShow.BringToFront();
            menuStrip2.BringToFront();
        }
        private async Task ShowTemporaryMessage(string message, int milliseconds = 3000)
        {
            // Створюємо форму
            Form msgForm = new Form()
            {
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ControlBox = false
            };

            Label lbl = new Label()
            {
                Text = message,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12)
            };

            msgForm.Controls.Add(lbl);

            msgForm.Show();

            await Task.Delay(milliseconds); // чекаємо потрібний час

            msgForm.Close();
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
        private void HighlightClientInDataGrid(ClientDto client)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ClientId"].Value != null &&
                    row.Cells["ClientId"].Value.ToString() == client.ClientId.ToString())
                {
                    row.Selected = true;          // виділяємо рядок
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index; // прокручуємо до нього
                    break;
                }
            }
        }
        private string? GetSelectedClientPhone()
        {
            if (dataGridView1.CurrentRow == null) return null;

            var cellValue = dataGridView1.CurrentRow.Cells["Phone"].Value;
            return cellValue?.ToString();
        }
        private async void ShowResult(bool result)
        {
            if (result)
                await ShowTemporaryMessage("Успішно!", 3000);
            else
                await ShowTemporaryMessage("Помилка.", 3000);
        }
        private void ShowSubMenuForTable(string tableName)
        {
            // Клієнти
            додатиКлієнтаToolStripMenuItem.Visible = false;
            редагуватиКлієнтаToolStripMenuItem.Visible = false;
            клієнтиЗаТипомРахункуToolStripMenuItem.Visible = false;
            КлієнтаЗаІменемToolStripMenuItem1.Visible = false;
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Visible = false;
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Visible = false;

            //Рахунки
            додатиРахунокToolStripMenuItem.Visible = false;
            редагуватиРахунокToolStripMenuItem.Visible = false;
            рахункиЗаВалютоюToolStripMenuItem.Visible = false;
            рахункиЗаСтатусомToolStripMenuItem.Visible = false;
            рахункуЗаВласникомToolStripMenuItem.Visible = false;
            випискаПоРахункуЗаПеріодToolStripMenuItem.Visible = false;

            // Потім показуємо потрібне залежно від таблиці
            switch (tableName)
            {
                case "Clients":
                    додатиКлієнтаToolStripMenuItem.Visible = true;
                    редагуватиКлієнтаToolStripMenuItem.Visible = true;
                    клієнтиЗаТипомРахункуToolStripMenuItem.Visible = true;
                    КлієнтаЗаІменемToolStripMenuItem1.Visible = true;
                    клієнтаЗаНомеромТелефонуToolStripMenuItem.Visible = true;
                    списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Visible = true;
                    break;

                case "Accounts":
                    додатиРахунокToolStripMenuItem.Visible = true;
                    редагуватиРахунокToolStripMenuItem.Visible = true;
                    рахункиЗаВалютоюToolStripMenuItem.Visible = true;
                    рахункиЗаСтатусомToolStripMenuItem.Visible = true;
                    рахункуЗаВласникомToolStripMenuItem.Visible = true;
                    випискаПоРахункуЗаПеріодToolStripMenuItem.Visible = true;
                    break;

                case "Branches":
                    // Меню для філій
                    break;

                case "Credits":
                    // Меню для кредитів
                    break;

                case "Employees":
                    // Меню для співробітників
                    break;

                case "Payments":
                    // Меню для платежів
                    break;

                case "Transactions":
                    // Меню для транзакцій
                    break;
            }
        }

    }
}
