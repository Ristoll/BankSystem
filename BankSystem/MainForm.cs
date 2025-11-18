using AutoMapper;
using BankSystem.ApiClients;
using BLL.Services;
using Core;
using DAL;
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
        private readonly IReportService reportService;
        private readonly ICurrentUserService currentUserService;
        private readonly ClientsApiClient clientsApiClient;
        private readonly AccountsApiClient accountsApiClient;
        private readonly TransactionsApiClient transactionsApiClient;
        private readonly CreditsApiClient creditsApiClient;
        private readonly PaymentsApiClient paymentsApiClient;
        private readonly EmployeesApiClient employeesApiClient;
        private readonly BranchesApiClient branchesApiClient;
        private string currentTable = "";

        public MainForm(IReportService reportService, ICurrentUserService currentUserService)
        {
            InitializeComponent();

            this.reportService = reportService;
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
            PopulateAccountsByCurrencySubMenu();
            PopulateAccountsByStatusSubMenu();
            PopulateCreditStatusesSubMenu();
            this.currentUserService = currentUserService;
        }
        private async void MainForm_Load(object sender, EventArgs e)
        {
            await PopulateAccountTypesComboBoxAsync(comboBox1);
            await PopulateAccountTypesComboBoxAsync(comboBox4);
            await PopulateCurrenciesComboBoxAsync(comboBox2);
            await PopulateCurrenciesComboBoxAsync(comboBox3);
            await PopulateTransactionTypesComboBoxAsync(comboBox5);
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
            PopulateAccountsByCurrencySubMenu();
            PopulateAccountsByStatusSubMenu();
        }

        private async void транзакціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Transactions";
            ShowSubMenuForTable(currentTable);

            var transactions = await transactionsApiClient.LoadTransactionsAsync();
            ShowTable(transactions ?? new List<TransactionDto>());

            HighlightMenuColor(транзакціїToolStripMenuItem);
        }

        private async void кредитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Credits";
            ShowSubMenuForTable(currentTable);

            var credits = await creditsApiClient.LoadCreditsAsync();
            ShowTable(credits ?? new List<CreditDto>());

            HighlightMenuColor(кредитиToolStripMenuItem);
        }

        private async void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";
            ShowSubMenuForTable(currentTable);

            var payments = await paymentsApiClient.LoadPaymentsAsync();
            ShowTable(payments ?? new List<PaymentDto>());

            HighlightMenuColor(платежіToolStripMenuItem);
        }

        private async void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";
            ShowSubMenuForTable(currentTable);

            var employees = await employeesApiClient.LoadEmployeesAsync();
            ShowTable(employees ?? new List<EmployeeDto>());

            HighlightMenuColor(співробитникиToolStripMenuItem);
        }

        private async void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";
            ShowSubMenuForTable(currentTable);

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

            bool result = true;

            if (button1.Text == "Оформити клієнта")
            {
                result = await clientsApiClient.AddClientAsync(clientDto);
            }
            else if (button1.Text == "Підтвердити редагування")
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Будь ласка, оберіть клієнта для редагування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dataGridView1.CurrentRow;
                clientDto.ClientId = Convert.ToInt32(row.Cells["ClientId"].Value);

                result = await clientsApiClient.UpdateClientAsync(clientDto);
            }
            else if (button1.Text == "Додати рахунок")
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Будь ласка, оберіть клієнта для редагування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var row = dataGridView1.CurrentRow;
                AccountDto accountDto = new AccountDto()
                {
                    ClientId = Convert.ToInt32(row.Cells["ClientId"].Value),
                    AccountTypeId = comboBox1.SelectedIndex + 1,
                    CurrencyId = comboBox2.SelectedIndex + 1,
                    BranchId = currentUserService.BankBranchId,
                    EmployeeId = currentUserService.EmployeeId,
                    Balance = 0,
                    OpenDate = DateOnly.FromDateTime(DateTime.Now),
                };
            }
            


                ShowResult(result);
        }

        private void редагуватиКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            button1.Text = "Підтвердити редагування";
            FillFieldsFromSelectedRow();
        }

        private async Task PopulateAccountTypesComboBoxAsync(ComboBox comboBox1)
        {
            comboBox1.Items.Clear();

            var accountTypes = await accountsApiClient.LoadAccountTypesAsync();
            if (accountTypes == null || accountTypes.Count == 0)
            {
                MessageBox.Show("Типи рахунків відсутні.");
                return;
            }

            // Прив'язка через DataSource
            comboBox1.DataSource = accountTypes;
            comboBox1.DisplayMember = "Name";       // що буде показано
            comboBox1.ValueMember = "AccountTypeId"; // значення, яке можна використовувати у коді
        }
        private async Task PopulateCurrenciesComboBoxAsync(ComboBox comboBox2)
        {
            comboBox2.Items.Clear();

            try
            {
                // Використовуємо сервіс / команду для отримання валют
                var currencies = await accountsApiClient.LoadCurrenciesAsync();
                if (currencies == null || currencies.Count == 0)
                {
                    MessageBox.Show("Валюти відсутні.");
                    return;
                }

                // Прив'язуємо до ComboBox
                comboBox2.DataSource = currencies;
                comboBox2.DisplayMember = "Name";        // що буде показано у ComboBox
                comboBox2.ValueMember = "CurrencyId";   // ID для використання у коді
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні валют: {ex.Message}");
            }
        }
        private async Task PopulateTransactionTypesComboBoxAsync(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var transactionTypes = await transactionsApiClient.LoadTransactionTypesAsync();
            if (transactionTypes == null || transactionTypes.Count == 0)
            {
                MessageBox.Show("Типи транзакцій відсутні.");
                return;
            }

            comboBox.DataSource = transactionTypes;
            comboBox.DisplayMember = "Name";               // показуємо назву типу
            comboBox.ValueMember = "TransactionTypeId";    // значення для використання в коді
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
                    await LoadClientsByAccountTypeAsync(accountTypeDto.AccountTypeId);
                };
                клієнтиЗаТипомРахункуToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }
        private async void PopulateAccountsByCurrencySubMenu()
        {
            рахункиЗаВалютоюToolStripMenuItem.DropDownItems.Clear();

            var currencies = await accountsApiClient.LoadCurrenciesAsync(); // Метод API-клієнта для валют
            if (currencies == null || currencies.Count == 0)
            {
                MessageBox.Show("Валюти відсутні.");
                return;
            }

            foreach (var currency in currencies)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(currency.Name);
                subItem.Click += async (s, e) =>
                {
                    await LoadAccountsByCurrencyAsync(currency.CurrencyId);
                };
                рахункиЗаВалютоюToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }
        private void PopulateAccountsByStatusSubMenu()
        {
            рахункиЗаСтатусомToolStripMenuItem.DropDownItems.Clear();

            var statuses = new Dictionary<string, bool>
            {
                { "Активні", true },
                { "Неактивні", false }
            };

            foreach (var status in statuses)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(status.Key);
                subItem.Click += async (s, e) =>
                {
                    await LoadAccountsByStatusAsync(status.Value);
                };
                рахункиЗаСтатусомToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }
        private async void PopulateCreditStatusesSubMenu()
        {
            // Очищаємо попередні пункти меню
            кредитиЗаСтатусомToolStripMenuItem.DropDownItems.Clear();

            // Отримуємо статуси кредитів через API-клієнт
            var creditStatuses = await creditsApiClient.LoadCreditStatusesAsync();
            if (creditStatuses == null || creditStatuses.Count == 0)
            {
                MessageBox.Show("Статуси кредитів відсутні.");
                return;
            }

            // Створюємо підпункти меню
            foreach (var statusDto in creditStatuses)
            {
                ToolStripMenuItem subItem = new ToolStripMenuItem(statusDto.Name);

                subItem.Click += async (s, e) =>
                {
                    await LoadCreditsByStatusAsync(statusDto.StatusId);
                };

                кредитиЗаСтатусомToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }
        private async Task LoadCreditsByStatusAsync(int statusId)
        {
            var credits = await creditsApiClient.FilterByStatusAsync(statusId);

            if (credits != null)
                dataGridView1.DataSource = credits;
            else
                MessageBox.Show("Кредитів з таким статусом не знайдено або сталася помилка.");
        }

        private async Task LoadAccountsByStatusAsync(bool isActive)
        {
            var accounts = await accountsApiClient.FilterByStatusAsync(isActive); // API-клієнт повинен підтримувати фільтр
            if (accounts != null)
                dataGridView1.DataSource = accounts;
            else
                MessageBox.Show("Рахунків з таким статусом не знайдено або сталася помилка.");
        }

        private async Task LoadAccountsByCurrencyAsync(int currencyId)
        {
            var accounts = await accountsApiClient.FilterByCurrencyAsync(currencyId); // Твоє API має підтримувати цей метод
            if (accounts != null)
                dataGridView1.DataSource = accounts;
            else
                MessageBox.Show("Рахунків з цією валютою не знайдено або сталася помилка.");
        }

        private async Task LoadClientsByAccountTypeAsync(int accountTypeId)
        {
            var clients = await clientsApiClient.FilterByAccountTypeAsync(accountTypeId);
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
            clientPanel.Parent = splitContainer1.Panel2;
            accountPanel.Visible = false;
            accountPanel.Parent = splitContainer1.Panel2;
            searchPanel.Visible = false;
            searchPanel.Parent = splitContainer1.Panel2;
            reportPanel.Visible = false;
            reportPanel.Parent = splitContainer1.Panel2;
            reportAccountPanel.Parent = reportPanel;

            accountClientPanel.Visible = false;
            accountClientPanel.Parent = clientPanel;
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
                    {
                        if (row.Cells["DateOfBirth"].Value is DateOnly dateOnly)
                        {
                            dateTimePicker1.Value = dateOnly.ToDateTime(TimeOnly.MinValue);
                        }
                        else
                        {
                            dateTimePicker1.Value = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);
                        }
                    }
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
            await Task.Delay(2000);
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
            додатиРахунокToolStripMenuItem.Visible = false;

            //Рахунки
            редагуватиРахунокToolStripMenuItem.Visible = false;
            рахункиЗаВалютоюToolStripMenuItem.Visible = false;
            рахункиЗаСтатусомToolStripMenuItem.Visible = false;
            рахункуЗаВласникомToolStripMenuItem.Visible = false;
            випискаПоРахункуЗаПеріодToolStripMenuItem.Visible = false;
            //Транзакції
            додатиТранзакціюToolStripMenuItem.Visible = false;
            транзакціїЗаПеріодToolStripMenuItem.Visible = false;
            //Кредити
            додатиКредитToolStripMenuItem.Visible = false;
            редагуватиКредитToolStripMenuItem.Visible = false;
            кредитиЗаСтатусомToolStripMenuItem.Visible = false;
            сумарнийКредитнийПрофільБанкуToolStripMenuItem.Visible = false;
            //Платежі
            додатиПлатіжToolStripMenuItem.Visible = false;
            //Співробітники
            додатиПрацівникаToolStripMenuItem.Visible = false;
            редагуватиПрацівникаToolStripMenuItem.Visible = false;
            видалитиПрацівникаToolStripMenuItem.Visible = false;
            звітПоДіяльностіСпівробітникаToolStripMenuItem.Visible = false;
            //Відділення
            додатиВіддіденняToolStripMenuItem.Visible = false;
            видалитиВідділенняToolStripMenuItem.Visible = false;
            редагуватиВідділенняToolStripMenuItem.Visible = false;
            // Потім показуємо потрібне залежно від таблиці
            switch (tableName)
            {
                case "Clients":
                    додатиКлієнтаToolStripMenuItem.Visible = true;
                    редагуватиКлієнтаToolStripMenuItem.Visible = true;
                    додатиРахунокToolStripMenuItem.Visible = true;
                    клієнтиЗаТипомРахункуToolStripMenuItem.Visible = true;
                    КлієнтаЗаІменемToolStripMenuItem1.Visible = true;
                    клієнтаЗаНомеромТелефонуToolStripMenuItem.Visible = true;
                    списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem.Visible = true;
                    break;
                case "Accounts":
                    редагуватиРахунокToolStripMenuItem.Visible = true;
                    рахункиЗаВалютоюToolStripMenuItem.Visible = true;
                    рахункиЗаСтатусомToolStripMenuItem.Visible = true;
                    рахункуЗаВласникомToolStripMenuItem.Visible = true;
                    випискаПоРахункуЗаПеріодToolStripMenuItem.Visible = true;
                    додатиТранзакціюToolStripMenuItem.Visible = true;
                    break;
                case "BankBranches":
                    додатиВіддіденняToolStripMenuItem.Visible = true;
                    видалитиВідділенняToolStripMenuItem.Visible = true;
                    редагуватиВідділенняToolStripMenuItem.Visible = true;
                    break;
                case "Credits":
                    додатиКредитToolStripMenuItem.Visible = true;
                    редагуватиКредитToolStripMenuItem.Visible = true;
                    кредитиЗаСтатусомToolStripMenuItem.Visible = true;
                    сумарнийКредитнийПрофільБанкуToolStripMenuItem.Visible = true;
                    break;
                case "Employees":
                    додатиПрацівникаToolStripMenuItem.Visible = true;
                    редагуватиПрацівникаToolStripMenuItem.Visible = true;
                    видалитиПрацівникаToolStripMenuItem.Visible = true;
                    звітПоДіяльностіСпівробітникаToolStripMenuItem.Visible = true;
                    break;
                case "Payments":
                    додатиПлатіжToolStripMenuItem.Visible = true;
                    break;
                case "Transactions":
                    транзакціїЗаПеріодToolStripMenuItem.Visible = true;
                    break;
            }
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            List<ClientDto>? clients = null;
            List<AccountDto>? accounts = null;
            List<TransactionDto>? transactions = null;

            // пошук за клієнтами
            if (button2.Text == "Знайти за іменем")
            {
                clients = await clientsApiClient.SearchByFullNameAsync(textBox8.Text);
            }
            else if (button2.Text == "Знайти за номером телефону")
            {
                clients = await clientsApiClient.SearchByPhoneNumberAsync(textBox8.Text);
            }
            // пошук за рахунком
            else if (button2.Text == "Знайти за іменем власника")
            {
                accounts = await accountsApiClient.SearchByOwnerAsync(textBox8.Text);
            }
            // пошук транзакцій за період
            else if (button2.Text == "Знайти за період")
            {
                // ---- ВАЛІДАЦІЯ ----

                if (dateTimePicker5.Value > dateTimePicker4.Value)
                {
                    MessageBox.Show(
                        "Дата початку періоду не може бути пізніше дати завершення.",
                        "Некоректний період",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                transactions = await transactionsApiClient.SearchByPeriodAsync(
                    dateTimePicker5.Value,
                    dateTimePicker4.Value
                );
            }

            // ---- РЕЗУЛЬТАТИ ----

            if (clients != null && clients.Count > 0)
            {
                dataGridView1.DataSource = clients;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            }
            else if (accounts != null && accounts.Count > 0)
            {
                dataGridView1.DataSource = accounts;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            }
            else if (transactions != null && transactions.Count > 0)
            {
                dataGridView1.DataSource = transactions;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
            }
            else
            {
                MessageBox.Show(
                    "Об’єкт не знайдено.",
                    "Результат пошуку",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

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

        private void списокАктивнихРахунківКонкретногоКлієнтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(reportPanel);

            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть клієнта у таблиці.");
                return;
            }

            // Отримуємо ClientId із виділеного рядка
            var cellValue = dataGridView1.CurrentRow.Cells["ClientId"].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("У виділеного клієнта відсутній ClientId.");
                return;
            }

            int clientId = Convert.ToInt32(cellValue);

            // Генеруємо звіт
            string report = reportService.GenerateActiveAccountsReportContent(clientId);
            textBox9.Text = report;
        }

        private void додатиРахунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(clientPanel);
            accountClientPanel.Show();
            button1.Text = "Додати рахунок";
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void редагуватиРахунокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button4.Text = "Внести зміни";
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть рахунок у таблиці.");
                return;
            }
            ShowPanel(accountPanel);
        }

        private void рахункуЗаВласникомToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
            button2.Text = "Знайти за іменем власника";
        }

        private void випискаПоРахункуЗаПеріодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть рахунок у таблиці.");
                return;
            }

            ShowPanel(reportPanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var cellValue = dataGridView1.CurrentRow.Cells["AccountId"].Value;
            int accountId = Convert.ToInt32(cellValue);
            if (dateTimePicker2.Value <= dateTimePicker3.Value)
            {
                var result = reportService.GenerateAccountStatementContent(accountId, dateTimePicker2.Value, dateTimePicker3.Value);
                textBox9.Text = result;
            }
        }

        private void операцToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Внести зміни")
            {
                var row = dataGridView1.CurrentRow;
                if (row == null) return;

                DateOnly? closeDate = checkBox1.Checked ? DateOnly.FromDateTime(DateTime.Now) : null;

                // Заповнюємо комбо-бокси правильно через SelectedValue
                comboBox4.SelectedValue = row.Cells["AccountTypeId"].Value;
                comboBox3.SelectedValue = row.Cells["CurrencyId"].Value;

                // Баланс
                textBox10.Text = row.Cells["Balance"].Value?.ToString();

                AccountDto accountDto = new AccountDto()
                {
                    AccountId = Convert.ToInt32(row.Cells["AccountId"].Value),
                    ClientId = Convert.ToInt32(row.Cells["ClientId"].Value),
                    AccountTypeId = Convert.ToInt32(comboBox4.SelectedValue),
                    CurrencyId = Convert.ToInt32(comboBox3.SelectedValue),
                    BranchId = currentUserService.BankBranchId,
                    EmployeeId = currentUserService.EmployeeId,
                    Balance = decimal.TryParse(textBox10.Text, out var bal) ? bal : 0,
                    OpenDate = DateOnly.FromDateTime(Convert.ToDateTime(row.Cells["OpenDate"].Value)),
                    CloseDate = closeDate
                };

                var result = await accountsApiClient.UpdateAccountAsync(accountDto);
                ShowResult(result);
            }
            else if (button4.Text == "Додати транзакцію")
            {
                var row = dataGridView1.CurrentRow;
                if (row == null) return;

                TransactionDto transactionDto = new TransactionDto()
                {
                    AccountId = Convert.ToInt32(row.Cells["AccountId"].Value),
                    TransactionTypeId = comboBox5.SelectedIndex + 1,
                    Amount = decimal.Parse(textBox11.Text),
                    TransactionDate = DateTime.Now,
                    EmployeeId = currentUserService.EmployeeId
                };

                var result = await transactionsApiClient.AddTransactionAsync(transactionDto);

                ShowResult(result);
            }


        }

        private void транзакціїЗаПеріодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
            textBox8.Hide();
            button2.Text = "Знайти за період";
        }

        private async void додатиТранзакціюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(accountPanel);
            button4.Text = "Додати транзакцію";
        }
    }
}

