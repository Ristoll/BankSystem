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
        private readonly IPasswordHasher passwordHasher;
        private readonly ClientsApiClient clientsApiClient;
        private readonly AccountsApiClient accountsApiClient;
        private readonly TransactionsApiClient transactionsApiClient;
        private readonly CreditsApiClient creditsApiClient;
        private readonly PaymentsApiClient paymentsApiClient;
        private readonly EmployeesApiClient employeesApiClient;
        private readonly BranchesApiClient branchesApiClient;
        private string currentTable = "";

        public MainForm(IReportService reportService, ICurrentUserService currentUserService, IPasswordHasher passwordHasher)
        {
            InitializeComponent();

            this.reportService = reportService;
            this.currentUserService = currentUserService;
            this.passwordHasher = passwordHasher;

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
            HideSubMenus(currentUserService.EmployeeId);
            await PopulateAccountTypesComboBoxAsync(comboBox1);
            await PopulateAccountTypesComboBoxAsync(comboBox4);
            await PopulateCurrenciesComboBoxAsync(comboBox2);
            await PopulateCurrenciesComboBoxAsync(comboBox3);
            await PopulateTransactionTypesComboBoxAsync(comboBox5);
            await PopulateBranchTypesComboBoxAsync(comboBox8);
            await PopulateCreditStatusesComboBoxAsync(comboBox6);
            await PopulatePaymentTypesComboBoxAsync(comboBox7);
            await PopulateBranchTypesComboBoxAsync(comboBox10);
            await PopulateEmployeeRolesComboBoxAsync(comboBox9);
        }
        private void HideSubMenus(int roleId)
        {
            if (!(roleId == 1 && roleId == 5 && roleId == 10)) //якщо не керівник, іт-спеціаліст та охоронець
            {
                співробитникиToolStripMenuItem.Visible = false;
                відділенняToolStripMenuItem.Visible = false;
            }
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
            HidePanels();
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
            HidePanels();
        }

        private async void транзакціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Transactions";
            ShowSubMenuForTable(currentTable);

            var transactions = await transactionsApiClient.LoadTransactionsAsync();
            ShowTable(transactions ?? new List<TransactionDto>());

            HighlightMenuColor(транзакціїToolStripMenuItem);
            HidePanels();
        }

        private async void кредитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Credits";
            ShowSubMenuForTable(currentTable);

            var credits = await creditsApiClient.LoadCreditsAsync();
            ShowTable(credits ?? new List<CreditDto>());

            HighlightMenuColor(кредитиToolStripMenuItem);
            HidePanels();
        }

        private async void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";
            ShowSubMenuForTable(currentTable);

            var payments = await paymentsApiClient.LoadPaymentsAsync();
            ShowTable(payments ?? new List<PaymentDto>());

            HighlightMenuColor(платежіToolStripMenuItem);
            HidePanels();
        }

        private async void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";
            ShowSubMenuForTable(currentTable);

            var employees = await employeesApiClient.LoadEmployeesAsync();
            ShowTable(employees ?? new List<EmployeeDto>());

            HighlightMenuColor(співробитникиToolStripMenuItem);
            HidePanels();
        }

        private async void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";
            ShowSubMenuForTable(currentTable);

            var branches = await branchesApiClient.LoadBranchesAsync();
            ShowTable(branches ?? new List<BankBranchDto>());

            HighlightMenuColor(відділенняToolStripMenuItem);
            HidePanels();
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
                result = await accountsApiClient.AddAccountAsync(accountDto);
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
        private async Task PopulateBranchTypesComboBoxAsync(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var branchTypes = await branchesApiClient.LoadBranchTypesAsync();
            if (branchTypes == null || branchTypes.Count == 0)
            {
                MessageBox.Show("Типи відділень відсутні.");
                return;
            }

            comboBox.DataSource = branchTypes;
            comboBox.DisplayMember = "Name";          // що бачить користувач
            comboBox.ValueMember = "BranchTypeId";    // використовується в коді
        }
        private async Task PopulateEmployeeRolesComboBoxAsync(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var roles = await employeesApiClient.LoadEmployeeRolesAsync();
            if (roles == null || roles.Count == 0)
            {
                MessageBox.Show("Ролі працівників відсутні.");
                return;
            }

            comboBox.DataSource = roles;
            comboBox.DisplayMember = "Name";           // що бачить користувач
            comboBox.ValueMember = "RoleId";   // значення для коду
        }

        private async Task PopulateCreditStatusesComboBoxAsync(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var creditStatuses = await creditsApiClient.LoadCreditStatusesAsync();
            if (creditStatuses == null || creditStatuses.Count == 0)
            {
                MessageBox.Show("Статуси кредитів відсутні.");
                return;
            }

            comboBox.DataSource = creditStatuses;
            comboBox.DisplayMember = "Name";           // що бачить користувач
            comboBox.ValueMember = "StatusId";   // значення для коду
        }
        private async Task PopulatePaymentTypesComboBoxAsync(ComboBox comboBox)
        {
            comboBox.Items.Clear();

            var paymentTypes = await paymentsApiClient.LoadPaymentTypesAsync();
            if (paymentTypes == null || paymentTypes.Count == 0)
            {
                MessageBox.Show("Типи платежів відсутні.");
                return;
            }

            comboBox.DataSource = paymentTypes;
            comboBox.DisplayMember = "Name";           // що бачить користувач
            comboBox.ValueMember = "PaymentTypeId";    // значення для коду
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
            accountPanel.Visible = false;
            branchPanel.Visible = false;
            clientPanel.Visible = false;
            creditPanel.Visible = false;
            creditPaymentPanel.Visible = false;
            employeePanel.Visible = false;
            reportPanel.Visible = false;
            searchPanel.Visible = false;

            panelToShow.Visible = true;
            panelToShow.BringToFront();
            menuStrip2.BringToFront();
        }
        private void HidePanels()
        {
            accountPanel.Visible = false;
            branchPanel.Visible = false;
            clientPanel.Visible = false;
            creditPanel.Visible = false;
            creditPaymentPanel.Visible = false;
            employeePanel.Visible = false;
            reportPanel.Visible = false;
            searchPanel.Visible = false;
            accountClientPanel.Visible = false;
            creditAccountPanel.Visible = false;
            reportAccountPanel.Visible = false;
            transactionAccountPanel.Visible = false;
            searchTimerPanel.Visible = false;
        }

        private void ShowSubPanel(Panel parent, Panel subPanelToShow)
        {
            // Ховаємо всі субпанелі всередині parent
            foreach (Control control in parent.Controls)
            {
                if (control is Panel)
                    control.Visible = false;
            }

            // Показуємо потрібну субпанель
            subPanelToShow.Visible = true;
            subPanelToShow.BringToFront();
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
                    додатиКредитToolStripMenuItem.Visible = true;
                    break;
                case "BankBranches":
                    додатиВіддіденняToolStripMenuItem.Visible = true;
                    видалитиВідділенняToolStripMenuItem.Visible = true;
                    редагуватиВідділенняToolStripMenuItem.Visible = true;
                    break;
                case "Credits":
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

                textBox8.Show();
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
            ShowSubPanel(clientPanel, accountClientPanel);
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
            ShowSubPanel(accountPanel, accountClientPanel);
            var row = dataGridView1.CurrentRow;
            // Заповнюємо комбо-бокси правильно через SelectedValue
            comboBox4.SelectedValue = row.Cells["AccountTypeId"].Value;
            comboBox3.SelectedValue = row.Cells["CurrencyId"].Value;

            // Баланс
            textBox10.Text = row.Cells["Balance"].Value?.ToString();

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
            ShowSubPanel(reportPanel, reportAccountPanel);
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

                var openDateValue = row.Cells["OpenDate"].Value;

                DateOnly openDate = openDateValue switch
                {
                    DateOnly d => d,
                    DateTime dt => DateOnly.FromDateTime(dt),
                    string s when DateTime.TryParse(s, out var dt) => DateOnly.FromDateTime(dt),
                    _ => default
                };

                AccountDto accountDto = new AccountDto()
                {
                    AccountId = Convert.ToInt32(row.Cells["AccountId"].Value),
                    ClientId = Convert.ToInt32(row.Cells["ClientId"].Value),
                    AccountTypeId = Convert.ToInt32(comboBox4.SelectedValue),
                    CurrencyId = Convert.ToInt32(comboBox3.SelectedValue),
                    BranchId = currentUserService.BankBranchId,
                    EmployeeId = currentUserService.EmployeeId,
                    Balance = decimal.TryParse(textBox10.Text, out var bal) ? bal : 0,
                    OpenDate = openDate,
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
            else if (button4.Text == "Додати кредит")
            {
                var cellValue = dataGridView1.CurrentRow.Cells["AccountId"].Value;

                if (cellValue == null || cellValue == DBNull.Value)
                {
                    MessageBox.Show("У виділеного рахунку відсутній AccountId.");
                    return;
                }

                int accountId = Convert.ToInt32(cellValue);
                var accounts = await accountsApiClient.LoadAccountsAsync();

                AccountDto account = null;
                foreach (var acc in accounts)
                {
                    if (acc.AccountId == accountId)
                    {
                        account = acc;
                    }
                }
                var creditDto = new CreditDto()
                {
                    ClientId = account.ClientId,
                    AccountId = account.AccountId,
                    CreditAmount = decimal.Parse(textBox12.Text),
                    InterestRate = decimal.Parse(textBox13.Text),
                    StartDate = DateOnly.FromDateTime(DateTime.Now),
                    StatusId = 1,
                    EmployeeId = currentUserService.EmployeeId
                };

                var result = await creditsApiClient.AddCreditAsync(creditDto);
                ShowResult(result);
            }
        }

        private void транзакціїЗаПеріодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(searchPanel);
            ShowSubPanel(searchPanel, searchTimerPanel);
            button2.Text = "Знайти за період";
            textBox8.Hide();
        }

        private async void додатиТранзакціюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(accountPanel);
            ShowSubPanel(accountPanel, transactionAccountPanel);
            button4.Text = "Додати транзакцію";
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть рахунок у таблиці.");
                return;
            }

            // Отримуємо ClientId із виділеного рядка
            var cellValueAccountTypeId = dataGridView1.CurrentRow.Cells["AccountTypeId"].Value;
            var cellValueCurrencyTypeId = dataGridView1.CurrentRow.Cells["CurrencyId"].Value;
            var cellValueBalance = dataGridView1.CurrentRow.Cells["Balance"].Value;

            comboBox4.SelectedIndex = Convert.ToInt32(cellValueAccountTypeId)+1;
            comboBox3.SelectedIndex = Convert.ToInt32(cellValueCurrencyTypeId) + 1;
            textBox10.Text = Convert.ToString(cellValueBalance);
        }

        private void сумарнийКредитнийПрофільБанкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(reportPanel);
            reportAccountPanel.Hide();
            string report = reportService.GenerateCreditPortfolioReportContent();
            textBox9.Text = report;
        }

        private void звітПоДіяльностіСпівробітникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(reportPanel);

            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть співробітника у таблиці.");
                return;
            }

            // Отримуємо ClientId із виділеного рядка
            var cellValue = dataGridView1.CurrentRow.Cells["EmployeeId"].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("У виділеного співробітника відсутній EmployeeId.");
                return;
            }

            int clientId = Convert.ToInt32(cellValue);

            var result = reportService.GenerateEmployeeActivityReportContent(clientId);
            textBox9.Text = result;
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private async void додатиКредитToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(accountPanel);
            ShowSubPanel(accountPanel, creditAccountPanel);
            button4.Text = "Додати кредит";

            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть рахунок у таблиці.");
                return;
            }
        }

        private async void видалитиПрацівникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть співробітника у таблиці.");
                return;
            }

            // Отримуємо ClientId із виділеного рядка
            var cellValue = dataGridView1.CurrentRow.Cells["EmployeeId"].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("У виділеного співробітника відсутній EmployeeId.");
                return;
            }

            int clientId = Convert.ToInt32(cellValue);
            if (clientId == currentUserService.EmployeeId)
            {
                MessageBox.Show("Ви не можете видалити себе!");
                return;
            }

            var result = await employeesApiClient.DeleteEmployeeAsync(clientId);
            ShowResult(result);
        }

        private async void видалитиВідділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть співробітника у таблиці.");
                return;
            }

            // Отримуємо ClientId із виділеного рядка
            var cellValue = dataGridView1.CurrentRow.Cells["BranchId"].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("У виділеного відділення відсутній BranchId.");
                return;
            }

            int clientId = Convert.ToInt32(cellValue);
            if (clientId == currentUserService.BankBranchId)
            {
                MessageBox.Show("Ви не можете видалити відділення, в якому працюєте!");
                return;
            }

            var result = await branchesApiClient.DeleteBranchAsync(clientId);
            ShowResult(result);
        }

        private void додатиВіддіденняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(branchPanel);
            button7.Text = "Додати відділення";
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            BankBranchDto branchDto = new BankBranchDto()
            {
                BranchName = textBox18.Text,
                Address = textBox20.Text,
                Phone = textBox19.Text,
                BranchTypeId = comboBox8.SelectedIndex + 1
            };
            bool result;
            if (button7.Text == "Додати відділення")
            {
                result = await branchesApiClient.AddBranchAsync(branchDto);
                ShowResult(result);
            }
            else if (button7.Text == "Редагувати відділення")
            {
                var row = dataGridView1.CurrentRow;
                branchDto.BranchId = Convert.ToInt32(row.Cells["BranchId"].Value);
                result = await branchesApiClient.UpdateBranchAsync(branchDto);
                ShowResult(result);
            }

        }

        private void редагуватиВідділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(branchPanel);
            button7.Text = "Редагувати відділення";
            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть відділення у таблиці.");
                return;
            }
            if (dataGridView1.CurrentRow == null) return;

            // Витягуємо дані з поточного рядка
            var row = dataGridView1.CurrentRow;

            textBox18.Text = Convert.ToString(row.Cells["BranchName"].Value);
            textBox20.Text = Convert.ToString(row.Cells["Address"].Value);
            textBox19.Text = Convert.ToString(row.Cells["Phone"].Value);

            int branchTypeId = Convert.ToInt32(row.Cells["BranchTypeId"].Value);

            comboBox8.SelectedValue = branchTypeId - 1;
        }

        private void редагуватиКредитToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(creditPanel);
            // Перевіряємо, що рядок виділений
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть кредит у таблиці.");
                return;
            }
            if (dataGridView1.CurrentRow == null) return;

            // Витягуємо дані з поточного рядка
            var row = dataGridView1.CurrentRow;

            textBox15.Text = row.Cells["CreditAmount"].Value.ToString();
            comboBox6.SelectedIndex = Convert.ToInt32(row.Cells["StatusId"].Value) - 1;
        }

        private void додатиПлатіжToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(creditPaymentPanel);
        }

        private void додатиПрацівникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(employeePanel);
            button8.Text = "Додати співробітника";
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;

            DateOnly ParseDate(object value)
            {
                return value switch
                {
                    DateOnly d => d,
                    DateTime dt => DateOnly.FromDateTime(dt),
                    string s when DateTime.TryParse(s, out var dt) => DateOnly.FromDateTime(dt),
                    _ => default
                };
            }

            var creditDto = new CreditDto()
            {
                CreditId = Convert.ToInt32(row.Cells["CreditId"].Value),
                ClientId = Convert.ToInt32(row.Cells["ClientId"].Value),
                AccountId = Convert.ToInt32(row.Cells["AccountId"].Value),
                CreditAmount = Convert.ToDecimal(textBox15.Text),
                InterestRate = Convert.ToDecimal(row.Cells["InterestRate"].Value),

                StartDate = ParseDate(row.Cells["StartDate"].Value),
                EndDate = ParseDate(row.Cells["EndDate"].Value),

                StatusId = comboBox6.SelectedIndex + 1,
                EmployeeId = row.Cells["EmployeeId"].Value == DBNull.Value
                    ? null
                    : Convert.ToInt32(row.Cells["EmployeeId"].Value)
            };

            var result = await creditsApiClient.UpdateCreditAsync(creditDto);
            ShowResult(result);
        }


        private async void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть кредит у таблиці.");
                return;
            }
            if (dataGridView1.CurrentRow == null) return;

            // Витягуємо дані з поточного рядка
            var row = dataGridView1.CurrentRow;
            PaymentDto paymentDto = new PaymentDto()
            {
                CreditId = Convert.ToInt32(row.Cells["CreditId"].Value),
                PaymentTypeId = Convert.ToInt32(comboBox7.SelectedIndex + 1),
                PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                Amount = Convert.ToDecimal(textBox17.Text)
            };
            var result = await paymentsApiClient.AddPaymentAsync(paymentDto);
            ShowResult(result);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            var row = dataGridView1.CurrentRow;
            if (row == null) return;

            int rowId = Convert.ToInt32(row.Cells["EmployeeId"].Value);
            var employees = await employeesApiClient.LoadEmployeesAsync();
            var existingEmployee = employees.FirstOrDefault(emp => emp.EmployeeId == rowId);

            if (existingEmployee == null)
            {
                MessageBox.Show("Співробітник не знайдений!");
                return;
            }

            EmployeeDto employeeDto = new EmployeeDto()
            {
                EmployeeId = rowId, // обов'язково!
                FirstName = textBox24.Text,
                LastName = textBox23.Text,
                MiddleName = textBox22.Text,
                Phone = textBox16.Text,
                Email = textBox14.Text,
                RoleId = comboBox9.SelectedIndex + 1,
                BranchId = comboBox10.SelectedIndex + 1
            };

            bool result;

            if (button8.Text == "Додати співробітника")
            {
                if (string.IsNullOrEmpty(textBox21.Text))
                {
                    MessageBox.Show("Будь ласка, введіть пароль.");
                    return;
                }
                employeeDto.PasswordHash = passwordHasher.Hash(textBox21.Text);
                result = await employeesApiClient.AddEmployeeAsync(employeeDto);
                ShowResult(result);
            }
            else if (button8.Text == "Редагувати співробітника")
            {
                // Якщо пароль порожній, залишаємо старий хеш
                employeeDto.PasswordHash = string.IsNullOrEmpty(textBox21.Text)
                    ? existingEmployee.PasswordHash
                    : passwordHasher.Hash(textBox21.Text);

                result = await employeesApiClient.UpdateEmployeeAsync(employeeDto);
                ShowResult(result);
            }
        }


        private void редагуватиПрацівникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPanel(employeePanel);
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Будь ласка, виділіть співробітника у таблиці.");
                return;
            }

            var row = dataGridView1.CurrentRow;

            // Заповнюємо текстбокси
            textBox24.Text = Convert.ToString(row.Cells["FirstName"].Value);
            textBox23.Text = Convert.ToString(row.Cells["LastName"].Value);
            textBox22.Text = Convert.ToString(row.Cells["MiddleName"].Value);
            textBox16.Text = Convert.ToString(row.Cells["Phone"].Value);
            textBox14.Text = Convert.ToString(row.Cells["Email"].Value);

            // Пароль зазвичай не підтягуємо, залишаємо пустим
            textBox21.Text = "";

            // ComboBox: EmployeeRole
            int roleId = Convert.ToInt32(row.Cells["RoleId"].Value);
            comboBox9.SelectedValue = roleId;

            // ComboBox: Branch
            int branchId = Convert.ToInt32(row.Cells["BranchId"].Value);
            comboBox10.SelectedValue = branchId;

            // Змінюємо текст кнопки для редагування
            button8.Text = "Редагувати співробітника";
        }
    }
}


