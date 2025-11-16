using BankSystem.ApiClients;
using BLL.Services;
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
                BaseAddress = new Uri("https://localhost:5001/") // або адреса твого WebAPI
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
            vw_CreditsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Credits);
            ShowTable(bankSystemdbDataSet1.vw_Credits);
            HighlightMenuColor(кредитиToolStripMenuItem);
        }

        private void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";
            vw_PaymentsTableAdapter1.Fill(bankSystemdbDataSet1.vw_Payments);
            ShowTable(bankSystemdbDataSet1.vw_Payments);
            HighlightMenuColor(платежіToolStripMenuItem);
        }

        private void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";
            vw_EmployeesTableAdapter1.Fill(bankSystemdbDataSet1.vw_Employees);
            ShowTable(bankSystemdbDataSet1.vw_Employees);
            HighlightMenuColor(співробитникиToolStripMenuItem);
        }

        private void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";
            vw_BankBranchesTableAdapter1.Fill(bankSystemdbDataSet1.vw_BankBranches);
            ShowTable(bankSystemdbDataSet1.vw_BankBranches);
            HighlightMenuColor(відділенняToolStripMenuItem);
        }

        //Меню операцій
        private void додатиКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            button1.Text = "Оформити клієнта";
        }

        //---------------


        //Панель клінтів
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

        //Меню фільтрації
        //1.Клієнти
        private void PopulateAccountTypesSubMenu()
        {
            // Очищаємо старі пункти
            клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Clear();

            // Перевіряємо, що таблиця існує
            var accountTypesTable = bankSystemdbDataSet1.Tables["AccountTypes"];
            if (accountTypesTable == null)
                return;

            // Перебираємо типи рахунків з DataSet
            foreach (DataRow row in accountTypesTable.Rows)
            {
                string accountTypeName = row["Name"].ToString();

                // Створюємо DTO для кожного підменю
                var accountTypeDto = new AccountTypeDto { Name = accountTypeName };

                // Створюємо ToolStripMenuItem
                ToolStripMenuItem subItem = new ToolStripMenuItem(accountTypeName);

                // Додаємо подію натискання
                subItem.Click += async (s, e) =>
                {
                    await LoadClientsByAccountTypeAsync(accountTypeDto);
                };

                // Додаємо до підменю
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
            textBox2.Text = row.Cells["SecondName"].Value?.ToString();
            textBox3.Text = row.Cells["MiddleName"].Value?.ToString();
            textBox5.Text = row.Cells["PhoneNumber"].Value?.ToString();
            if (row.Cells["DateOfBirth"].Value != null)
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
            textBox4.Text = row.Cells["Email"].Value?.ToString();
            textBox6.Text = row.Cells["PassportNumber"].Value?.ToString();
            textBox7.Text = row.Cells["Address"].Value?.ToString();
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
            // Ховаємо всі панелі
            clientPanel.Visible = false;
            searchPanel.Visible = false;
            reportPanel.Visible = false;
            // і т.д.

            // Показуємо потрібну панель
            panelToShow.BringToFront();
            panelToShow.Visible = true;
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

            var cellValue = dataGridView1.CurrentRow.Cells["PhoneNumber"].Value;
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
            // Спочатку ховаємо все
            додатиКлієнтаToolStripMenuItem.Visible = false;
            редагуватиКлієнтаToolStripMenuItem.Visible = false;
            клієнтиЗаТипомРахункуToolStripMenuItem.Visible = false;
            КлієнтаЗаІменемToolStripMenuItem1.Visible = false;
            клієнтаЗаНомеромТелефонуToolStripMenuItem.Visible = false;
            списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Visible = false;

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
                    // Тут аналогічно показуєш меню для рахунків
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

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
