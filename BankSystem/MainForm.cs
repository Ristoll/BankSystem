using BankSystem.ApiClients;
using BLL.Services;
using DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class MainForm : Form
    {
        private readonly ReportService reportService;
        private readonly ClientsApiClient clientsApiClient;
        private readonly AccountsApiClient accountsApiClient;
        private readonly TransactionsApiClient transactionsApiClient;
        private readonly CreditsApiClient creditsApiClient;
        private readonly PaymentsApiClient paymentsApiClient;
        private readonly EmployeesApiClient employeesApiClient;
        private readonly BranchesApiClient branchesApiClient;

        private string currentTable = "";

        public MainForm()
        {
            InitializeComponent();

            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5136/") // або адреса твого WebAPI
            };


            // Ініціалізація всіх API-клієнтів
            clientsApiClient = new ClientsApiClient(httpClient);
            accountsApiClient = new AccountsApiClient(httpClient);
            transactionsApiClient = new TransactionsApiClient(httpClient);
            creditsApiClient = new CreditsApiClient(httpClient);
            paymentsApiClient = new PaymentsApiClient(httpClient);
            employeesApiClient = new EmployeesApiClient(httpClient);
            branchesApiClient = new BranchesApiClient(httpClient);

            dataGridView1.AutoGenerateColumns = true;

            // Динамічно створюємо підменю
            PopulateAccountTypesSubMenu();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        private void ShowTable<T>(List<T> list)
        {
            bindingSource1.DataSource = list;
            dataGridView1.DataSource = bindingSource1;
        }

        // -------------------- Меню --------------------

        private async void клієнтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Clients";
            ShowSubMenuForTable(currentTable);

            var clients = await clientsApiClient.LoadClientsAsync();
            ShowTable(clients ?? new List<ClientDto>());

            HighlightMenuColor(клієнтиToolStripMenuItem);
        }

        private async void рахункиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Accounts";
            ShowSubMenuForTable(currentTable);

            var accounts = await accountsApiClient.LoadAccountsAsync();
            ShowTable(accounts ?? new List<AccountDto>());

            HighlightMenuColor(рахункиToolStripMenuItem);
        }

        private async void транзакціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Transactions";

            var transactions = await transactionsApiClient.LoadTransactionsAsync();
            ShowTable(transactions ?? new List<TransactionDto>());

            HighlightMenuColor(транзакціїToolStripMenuItem);
        }

        private async void кредитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Credits";

            var credits = await creditsApiClient.LoadCreditsAsync();
            ShowTable(credits ?? new List<CreditDto>());

            HighlightMenuColor(кредитиToolStripMenuItem);
        }

        private async void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";

            var payments = await paymentsApiClient.LoadPaymentsAsync();
            ShowTable(payments ?? new List<PaymentDto>());

            HighlightMenuColor(платежіToolStripMenuItem);
        }

        private async void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";

            var employees = await employeesApiClient.LoadEmployeesAsync();
            ShowTable(employees ?? new List<EmployeeDto>());

            HighlightMenuColor(співробитникиToolStripMenuItem);
        }

        private async void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";

            var branches = await branchesApiClient.LoadBranchesAsync();
            ShowTable(branches ?? new List<BankBranchDto>());

            HighlightMenuColor(відділенняToolStripMenuItem);
        }

        // -------------------- Меню операцій --------------------
        // 1. Клієнти
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
            }
            else
            {
                var clients = await clientsApiClient.SearchByPhoneNumberAsync(textBox5.Text);
                if (clients == null || clients.Count == 0)
                {
                    MessageBox.Show("Клієнта з таким номером телефону не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var client = clients[0];
                clientDto.ClientId = client.ClientId;
                result = await clientsApiClient.UpdateClientAsync(clientDto);
            }

            ShowResult(result);
        }

        private void редагуватиКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            button1.Text = "Підтвердити редагування";
            FillFieldsFromSelectedRow();
        }

        // -------------------- Меню фільтрації --------------------
        private async void PopulateAccountTypesSubMenu()
        {
            клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Clear();

            // GET api/accounts/load-accountTypes
            var accountTypes = await accountsApiClient.LoadAccountTypesAsync();
            if (accountTypes == null || accountTypes.Count == 0)
            {
                MessageBox.Show("Типи рахунків відсутні.");
                return;
            }

            foreach (var accountTypeDto in accountTypes)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(accountTypeDto.Name);
                subItem.Click += async (s, e) =>
                {
                    await LoadClientsByAccountTypeAsync(accountTypeDto);
                };
                клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }

        private async Task LoadClientsByAccountTypeAsync(AccountTypeDto accountTypeDto)
        {
            var clients = await clientsApiClient.FilterByAccountTypeAsync(accountTypeDto);
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

        // -------------------- Допоміжні методи --------------------
        private void ShowPanel(Panel panelToShow)
        {
            clientPanel.Visible = false;
            accountPanel.Visible = false;
            searchPanel.Visible = false;
            reportPanel.Visible = false;

            panelToShow.Visible = true;
            panelToShow.BringToFront();
            menuStrip2.BringToFront();
        }

        private void HighlightMenuColor(ToolStripMenuItem activeItem)
        {
            foreach (ToolStripMenuItem item in menuStrip1.Items)
                ResetMenuColor(item);

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
                    textBox1.Text = row.Cells["FirstName"].Value?.ToString();
                    textBox2.Text = row.Cells["LastName"].Value?.ToString();
                    textBox3.Text = row.Cells["MiddleName"].Value?.ToString();
                    textBox5.Text = row.Cells["Phone"].Value?.ToString();
                    if (row.Cells["DateOfBirth"].Value != null)
                        dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                    textBox4.Text = row.Cells["Email"].Value?.ToString();
                    textBox6.Text = row.Cells["PassportNumber"].Value?.ToString();
                    textBox7.Text = row.Cells["Address"].Value?.ToString();
                    break;
            }
        }
        private async void ShowResult(bool result)
        {
            string message = result ? "Успішно!" : "Помилка.";
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
            await Task.Delay(3000);
            msgForm.Close();
        }
        private void ShowSubMenuForTable(string tableName)
        { //Клієнти
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
        private async void button2_Click(object sender, EventArgs e)
        {
            List<ClientDto>? clients = null;

            if (button2.Text == "Знайти за іменем") // шукаємо по повному імені
            {
                clients = await clientsApiClient.SearchByFullNameAsync(textBox8.Text);
            }
            else if (button2.Text == "Знайти за номером телефону") // шукаємо по телефону
            {
                clients = await clientsApiClient.SearchByPhoneNumberAsync(textBox8.Text);
            }

            if (clients != null && clients.Count > 0)
            {
                // Відображаємо результат у DataGridView
                dataGridView1.DataSource = clients;

                // За бажанням виділяємо першого клієнта
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            }
            else
            {
                MessageBox.Show("Клієнта не знайдено.", "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = null;
            }
        }

        private void клієнтаЗаНомеромТелефонуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
            button2.Text = "Знайти за номером телефону";
        }

        private void КлієнтаЗаІменемToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
            button2.Text = "Знайти за іменем";
        }

        private async void списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(reportPanel);

            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть клієнта у таблиці.");
                return;
            }

            // Беремо номер телефону (або будь-яке унікальне поле)
            string? phone = dataGridView1.CurrentRow.Cells["Phone"].Value?.ToString();
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Неможливо визначити клієнта.");
                return;
            }

            // Викликаємо API, щоб отримати реального клієнта
            var clients = await clientsApiClient.SearchByPhoneNumberAsync(phone);
            if (clients == null || clients.Count == 0)
            {
                MessageBox.Show("Клієнта не знайдено через API.");
                return;
            }

            int clientId = clients[0].ClientId;

            string report = reportService.GenerateActiveAccountsReportContent(clientId);
            textBox9.Text = report;
        }

    }
}

