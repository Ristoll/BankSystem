using AutoMapper;
using BankSystem.ApiClients;
using BLL.Services;
using Core;
using Core.Entities;
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
            HideSubMenus(currentUserService.RoleId);
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
            if (!(roleId == 1 || roleId == 5 || roleId == 10)) //якщо не керівник, іт-спеціаліст та охоронець
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
            if (dataGridView1.Columns.Contains("ClientId"))
                dataGridView1.Columns["ClientId"].Visible = false;
            HighlightMenuColor(клієнтиToolStripMenuItem);
            HidePanels();
        }

        private async void рахункиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Accounts";
            ShowSubMenuForTable(currentTable);

            var accounts = await accountsApiClient.LoadAccountsAsync() ?? new List<AccountDto>();
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();

            var accountTypes = await accountsApiClient.LoadAccountTypesAsync() ?? new List<AccountTypeDto>();
            var currencies = await accountsApiClient.LoadCurrenciesAsync() ?? new List<CurrencyDto>();
            var branches = await branchesApiClient.LoadBranchesAsync() ?? new List<BankBranchDto>();
            var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();

            var clientDict = clients.ToDictionary(c => c.ClientId, c => $"{c.LastName} {c.FirstName} {c.MiddleName}");
            var accountTypeDict = accountTypes.ToDictionary(a => a.AccountTypeId, a => a.Name);
            var currencyDict = currencies.ToDictionary(c => c.CurrencyId, c => c.Name);
            var branchDict = branches.ToDictionary(b => b.BranchId, b => b.BranchName);
            var employeeDict = employees.ToDictionary(e => e.EmployeeId, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");

            var tableData = accounts.Select(a => new
            {
                a.AccountId, // ховаємо
                a.ClientId,  // ховаємо
                a.AccountTypeId,
                a.CurrencyId,
                a.EmployeeId,
                ClientName = clientDict.ContainsKey(a.ClientId) ? clientDict[a.ClientId] : "",
                AccountTypeName = accountTypeDict.ContainsKey(a.AccountTypeId) ? accountTypeDict[a.AccountTypeId] : "",
                CurrencyName = currencyDict.ContainsKey(a.CurrencyId) ? currencyDict[a.CurrencyId] : "",
                BranchName = a.BranchId.HasValue && branchDict.ContainsKey(a.BranchId.Value) ? branchDict[a.BranchId.Value] : "",
                EmployeeName = a.EmployeeId.HasValue && employeeDict.ContainsKey(a.EmployeeId.Value) ? employeeDict[a.EmployeeId.Value] : "",
                a.Balance,
                a.OpenDate,
                a.CloseDate
            }).ToList();

            dataGridView1.DataSource = tableData;

            // Ховаємо ID
            if (dataGridView1.Columns.Contains("AccountId"))
                dataGridView1.Columns["AccountId"].Visible = false;
            if (dataGridView1.Columns.Contains("ClientId"))
                dataGridView1.Columns["ClientId"].Visible = false;
            if (dataGridView1.Columns.Contains("AccountTypeId"))
                dataGridView1.Columns["AccountTypeId"].Visible = false;
            if (dataGridView1.Columns.Contains("CurrencyId"))
                dataGridView1.Columns["CurrencyId"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;

            HighlightMenuColor(рахункиToolStripMenuItem);
            HidePanels();
        }



        private async void транзакціїToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Transactions";
            ShowSubMenuForTable(currentTable);

            // 1. Завантажуємо транзакції
            var transactions = await transactionsApiClient.LoadTransactionsAsync() ?? new List<TransactionDto>();

            // 2. Завантажуємо акаунти, клієнтів, типи транзакцій та співробітників
            var accounts = await accountsApiClient.LoadAccountsAsync() ?? new List<AccountDto>();
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();
            var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();
            var transactionTypes = await transactionsApiClient.LoadTransactionTypesAsync() ?? new List<TransactionTypeDto>();

            // 3. Створюємо словники для швидкого пошуку
            var accountOwnerDict = accounts.ToDictionary(
                a => a.AccountId,
                a =>
                {
                    var client = clients.FirstOrDefault(c => c.ClientId == a.ClientId);
                    return client != null ? $"{client.LastName} {client.FirstName} {client.MiddleName}" : "";
                });

            var transactionTypeDict = transactionTypes.ToDictionary(t => t.TransactionTypeId, t => t.Name);
            var employeeDict = employees.ToDictionary(e => e.EmployeeId, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");

            // 4. Формуємо новий список для таблиці
            var tableData = transactions.Select(t => new
            {
                t.TransactionId,
                AccountId = t.AccountId, // залишаємо для логіки, потім приховаємо
                EmployeeId = t.EmployeeId, // залишаємо для логіки, потім приховаємо
                TransactionTypeId = t.TransactionTypeId, // залишаємо для логіки, потім приховаємо
                AccountOwnerName = accountOwnerDict.ContainsKey(t.AccountId) ? accountOwnerDict[t.AccountId] : "",
                EmployeeName = t.EmployeeId.HasValue && employeeDict.ContainsKey(t.EmployeeId.Value) ? employeeDict[t.EmployeeId.Value] : "",
                TransactionTypeName = transactionTypeDict.ContainsKey(t.TransactionTypeId) ? transactionTypeDict[t.TransactionTypeId] : "",
                t.Amount,
                t.TransactionDate,
                t.Description
            }).ToList();

            // 5. Відображаємо у DataGridView
            dataGridView1.DataSource = tableData;

            // 6. Ховаємо колонки з ID
            if (dataGridView1.Columns.Contains("TransactionId"))
                dataGridView1.Columns["TransactionId"].Visible = false;
            if (dataGridView1.Columns.Contains("AccountId"))
                dataGridView1.Columns["AccountId"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;
            if (dataGridView1.Columns.Contains("TransactionTypeId"))
                dataGridView1.Columns["TransactionTypeId"].Visible = false;

            HighlightMenuColor(транзакціїToolStripMenuItem);
            HidePanels();
        }


        private async void кредитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Credits";
            ShowSubMenuForTable(currentTable);

            // 1. Завантажуємо кредити
            var credits = await creditsApiClient.LoadCreditsAsync() ?? new List<CreditDto>();

            // 2. Завантажуємо клієнтів та співробітників
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();
            var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();

            // 3. Завантажуємо статуси кредитів (якщо є окремий метод)
            var creditStatuses = await creditsApiClient.LoadCreditStatusesAsync() ?? new List<CreditStatusDto>();

            // 4. Створюємо словники для швидкого пошуку
            var clientDict = clients.ToDictionary(c => c.ClientId, c => $"{c.LastName} {c.FirstName} {c.MiddleName}");
            var employeeDict = employees.ToDictionary(e => e.EmployeeId, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");
            var statusDict = creditStatuses.ToDictionary(s => s.StatusId, s => s.Name);

            // 5. Формуємо новий список для таблиці
            var tableData = credits.Select(c => new
            {
                c.CreditId,
                AccountId = c.AccountId,
                ClientId = c.ClientId,
                EmployeeId = c.EmployeeId,
                StatusId = c.StatusId,
                ClientName = clientDict.ContainsKey(c.ClientId) ? clientDict[c.ClientId] : "",
                EmployeeName = c.EmployeeId.HasValue && employeeDict.ContainsKey(c.EmployeeId.Value) ? employeeDict[c.EmployeeId.Value] : "",
                StatusName = statusDict.ContainsKey(c.StatusId) ? statusDict[c.StatusId] : "",
                c.CreditAmount,
                c.InterestRate,
                c.StartDate,
                c.EndDate
            }).ToList();

            // 6. Відображаємо у DataGridView
            dataGridView1.DataSource = tableData;

            // 7. Ховаємо колонки з ID, які не потрібно показувати
            if (dataGridView1.Columns.Contains("CreditId"))
                dataGridView1.Columns["CreditId"].Visible = false;
            if (dataGridView1.Columns.Contains("AccountId"))
                dataGridView1.Columns["AccountId"].Visible = false;
            if (dataGridView1.Columns.Contains("ClientId"))
                dataGridView1.Columns["ClientId"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;
            if (dataGridView1.Columns.Contains("StatusId"))
                dataGridView1.Columns["StatusId"].Visible = false;

            HighlightMenuColor(кредитиToolStripMenuItem);
            HidePanels();
        }


        private async void платежіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Payments";
            ShowSubMenuForTable(currentTable);

            // 1. Завантажуємо платежі
            var payments = await paymentsApiClient.LoadPaymentsAsync()
                           ?? new List<PaymentDto>();

            // 2. Завантажуємо кредити та клієнтів, та типи платежів
            var credits = await creditsApiClient.LoadCreditsAsync()
                          ?? new List<CreditDto>();
            var clients = await clientsApiClient.LoadClientsAsync()
                          ?? new List<ClientDto>();
            var paymentTypes = await paymentsApiClient.LoadPaymentTypesAsync()
                               ?? new List<PaymentTypeDto>();

            // 3. Створюємо словники для швидкого пошуку
            var creditOwnerDict = credits.ToDictionary(
                c => c.CreditId,
                c =>
                {
                    if (c.ClientId == 0) return "";
                    var client = clients.FirstOrDefault(cl => cl.ClientId == c.ClientId);
                    return client != null ? $"{client.LastName} {client.FirstName} {client.MiddleName}" : "";
                });

            var paymentTypeDict = paymentTypes.ToDictionary(pt => pt.PaymentTypeId, pt => pt.Name);

            // 4. Формуємо новий список для таблиці
            var tableData = payments.Select(p => new
            {
                p.PaymentId,
                p.CreditId, // залишаємо для внутрішньої логіки
                CreditOwnerName = creditOwnerDict.ContainsKey(p.CreditId) ? creditOwnerDict[p.CreditId] : "",
                PaymentTypeName = paymentTypeDict.ContainsKey(p.PaymentTypeId) ? paymentTypeDict[p.PaymentTypeId] : "",
                p.PaymentDate,
                p.Amount
            }).ToList();

            // 5. Відображаємо у DataGridView
            dataGridView1.DataSource = tableData;

            // 6. Ховаємо колонку CreditId, щоб користувач її не бачив
            if (dataGridView1.Columns.Contains("PaymentId"))
                dataGridView1.Columns["PaymentId"].Visible = false;
            if (dataGridView1.Columns.Contains("CreditId"))
                dataGridView1.Columns["CreditId"].Visible = false;

            HighlightMenuColor(платежіToolStripMenuItem);
            HidePanels();
        }



        private async void співробитникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "Employees";
            ShowSubMenuForTable(currentTable);

            // 1. Завантажуємо співробітників
            var employees = await employeesApiClient.LoadEmployeesAsync()
                            ?? new List<EmployeeDto>();

            // 2. Завантажуємо список відділень і ролей
            var branches = await branchesApiClient.LoadBranchesAsync()
                           ?? new List<BankBranchDto>();
            var roles = await employeesApiClient.LoadEmployeeRolesAsync()
                        ?? new List<EmployeeRoleDto>(); // RoleDto з RoleId та RoleName

            // 3. Створюємо словники для швидкого пошуку
            var branchDict = branches.ToDictionary(b => b.BranchId, b => b.BranchName);
            var roleDict = roles.ToDictionary(r => r.RoleId, r => r.Name);

            // 4. Формуємо новий список для таблиці
            var tableData = employees.Select(e => new
            {
                e.EmployeeId,
                e.RoleId,
                e.BranchId,
                e.FirstName,
                e.LastName,
                e.MiddleName,
                e.Phone,
                e.Email,
                e.Password,
                e.PasswordHash,
                BranchName = branchDict.ContainsKey(e.BranchId) ? branchDict[e.BranchId] : "",
                RoleName = roleDict.ContainsKey(e.RoleId) ? roleDict[e.RoleId] : ""
            }).ToList();

            // 5. Відображаємо у DataGridView
            dataGridView1.DataSource = tableData;

            // 6. Ховаємо колонки Password та PasswordHash
            if (dataGridView1.Columns.Contains("Password"))
                dataGridView1.Columns["Password"].Visible = false;

            if (dataGridView1.Columns.Contains("PasswordHash"))
                dataGridView1.Columns["PasswordHash"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;
            if (dataGridView1.Columns.Contains("BranchId"))
                dataGridView1.Columns["BranchId"].Visible = false;
            if (dataGridView1.Columns.Contains("RoleId"))
                dataGridView1.Columns["RoleId"].Visible = false;

            HighlightMenuColor(співробитникиToolStripMenuItem);
            HidePanels();
        }


        private async void відділенняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTable = "BankBranches";
            ShowSubMenuForTable(currentTable);

            // 1. Завантажуємо відділення
            var branches = await branchesApiClient.LoadBranchesAsync()
                           ?? new List<BankBranchDto>();

            // 2. Завантажуємо типи відділень
            var types = await branchesApiClient.LoadBranchTypesAsync()
                        ?? new List<BranchTypeDto>();

            // 3. Створюємо словник id → name
            var typeDict = types.ToDictionary(t => t.BranchTypeId, t => t.Name);

            // 4. Формуємо новий список для таблиці
            var tableData = branches.Select(b => new
            {
                b.BranchId,
                b.BranchName,
                b.Address,
                b.Phone,
                BranchTypeId = b.BranchTypeId,                // для логіки
                BranchTypeName = typeDict[b.BranchTypeId]     // для відображення
            }).ToList();

            // 5. Відображаємо
            dataGridView1.DataSource = tableData;

            // 6. Ховаємо ID
            dataGridView1.Columns["BranchTypeId"].Visible = false;
            if (dataGridView1.Columns.Contains("BranchId"))
                dataGridView1.Columns["BranchId"].Visible = false;

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

            var currencies = await accountsApiClient.LoadCurrenciesAsync(); // Завантажуємо валюту
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
                    var accounts = await accountsApiClient.FilterByCurrencyAsync(currency.CurrencyId) ?? new List<AccountDto>();
                    await ShowFilteredAccountsAsync(accounts);
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
                    var accounts = await accountsApiClient.FilterByStatusAsync(status.Value) ?? new List<AccountDto>();
                    await ShowFilteredAccountsAsync(accounts);
                };
                рахункиЗаСтатусомToolStripMenuItem.DropDownItems.Add(subItem);
            }
        }
        private async void PopulateCreditStatusesSubMenu()
        {
            кредитиЗаСтатусомToolStripMenuItem.DropDownItems.Clear();

            // 1. Отримуємо статуси кредитів
            var statuses = await creditsApiClient.LoadCreditStatusesAsync();
            if (statuses == null || statuses.Count == 0)
            {
                MessageBox.Show("Статуси кредитів відсутні.");
                return;
            }

            // 2. Формуємо підменю
            foreach (var status in statuses)
            {
                var subItem = new ToolStripMenuItem(status.Name);

                subItem.Click += async (s, e) =>
                {
                    var credits = await creditsApiClient.FilterByStatusAsync(status.StatusId)
                                   ?? new List<CreditDto>();

                    await ShowFilteredCreditsAsync(credits);
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

            // ---- ПОШУК ----
            if (button2.Text == "Знайти за іменем")
            {
                clients = await clientsApiClient.SearchByFullNameAsync(textBox8.Text);
            }
            else if (button2.Text == "Знайти за номером телефону")
            {
                clients = await clientsApiClient.SearchByPhoneNumberAsync(textBox8.Text);
            }
            else if (button2.Text == "Знайти за іменем власника")
            {
                accounts = await accountsApiClient.SearchByOwnerAsync(textBox8.Text);

                if (accounts != null && accounts.Count > 0)
                {
                    await ShowFilteredAccountsAsync(accounts);
                }
            }
            else if (button2.Text == "Знайти за період")
            {
                if (dateTimePicker5.Value > dateTimePicker4.Value)
                {
                    MessageBox.Show("Дата початку періоду не може бути пізніше дати завершення.",
                        "Некоректний період", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Завантажуємо транзакції за період
                transactions = await transactionsApiClient.SearchByPeriodAsync(dateTimePicker5.Value, dateTimePicker4.Value)
                                    ?? new List<TransactionDto>();

                // 2. Завантажуємо довідники
                accounts = await accountsApiClient.LoadAccountsAsync() ?? new List<AccountDto>();
                clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();
                var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();
                var transactionTypes = await transactionsApiClient.LoadTransactionTypesAsync() ?? new List<TransactionTypeDto>();

                // 3. Готуємо словники
                var accountOwnerDict = accounts.ToDictionary(
                    a => a.AccountId,
                    a =>
                    {
                        var client = clients.FirstOrDefault(c => c.ClientId == a.ClientId);
                        return client != null ? $"{client.LastName} {client.FirstName} {client.MiddleName}" : "";
                    });

                var employeeDict = employees.ToDictionary(e => e.EmployeeId,
                    e => $"{e.LastName} {e.FirstName} {e.MiddleName}");

                var transactionTypeDict = transactionTypes.ToDictionary(
                    t => t.TransactionTypeId, t => t.Name);

                // 4. Формуємо відображення
                var tableData = transactions.Select(t => new
                {
                    t.TransactionId,
                    AccountId = t.AccountId,
                    EmployeeId = t.EmployeeId,
                    TransactionTypeId = t.TransactionTypeId,
                    AccountOwnerName = accountOwnerDict.ContainsKey(t.AccountId) ? accountOwnerDict[t.AccountId] : "",
                    EmployeeName = t.EmployeeId.HasValue && employeeDict.ContainsKey(t.EmployeeId.Value)
                                    ? employeeDict[t.EmployeeId.Value]
                                    : "",
                    TransactionTypeName = transactionTypeDict.ContainsKey(t.TransactionTypeId)
                                    ? transactionTypeDict[t.TransactionTypeId]
                                    : "",
                    t.Amount,
                    t.TransactionDate,
                    t.Description
                }).ToList();

                // 5. Відображення у DataGrid
                dataGridView1.DataSource = tableData;

                // 6. Ховаємо ID-поля
                if (dataGridView1.Columns.Contains("TransactionId"))
                    dataGridView1.Columns["TransactionId"].Visible = false;
                if (dataGridView1.Columns.Contains("AccountId"))
                    dataGridView1.Columns["AccountId"].Visible = false;
                if (dataGridView1.Columns.Contains("EmployeeId"))
                    dataGridView1.Columns["EmployeeId"].Visible = false;
                if (dataGridView1.Columns.Contains("TransactionTypeId"))
                    dataGridView1.Columns["TransactionTypeId"].Visible = false;

                // важливо: додаємо return, щоб нижня частина методу не затирала таблицю
                return;
            }



            // ---- ВІДОБРАЖЕННЯ ----
            if ((clients != null && clients.Count > 0) ||
                (accounts != null && accounts.Count > 0) ||
                (transactions != null && transactions.Count > 0))
            {
                if (clients != null && clients.Count > 0)
                {
                    bindingSource1.DataSource = clients;
                    dataGridView1.DataSource = bindingSource1;

                    if (dataGridView1.Columns.Contains("ClientId"))
                        dataGridView1.Columns["ClientId"].Visible = false;
                }

                // Встановлюємо першу видиму клітинку активною
                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewColumn firstVisibleCol = dataGridView1.Columns
                        .Cast<DataGridViewColumn>()
                        .FirstOrDefault(c => c.Visible)!;

                    if (firstVisibleCol != null)
                        dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[firstVisibleCol.Index];
                }
            }
            else
            {
                MessageBox.Show("Об’єкт не знайдено.", "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                var employees = await employeesApiClient.LoadEmployeesAsync();
                var employeeName = employees.First(e => e.EmployeeId == currentUserService.EmployeeId).LastName + " " + employees.First(e => e.EmployeeId == currentUserService.EmployeeId).FirstName + " " + employees.First(e => e.EmployeeId == currentUserService.EmployeeId) + employees.First(e => e.EmployeeId == currentUserService.EmployeeId).MiddleName;
                TransactionDto transactionDto = new TransactionDto()
                {
                    AccountId = Convert.ToInt32(row.Cells["AccountId"].Value),
                    TransactionTypeId = comboBox5.SelectedIndex + 1,
                    TransactionTypeName = comboBox5.Text,
                    Amount = decimal.Parse(textBox11.Text),
                    TransactionDate = DateTime.Now,
                    EmployeeId = currentUserService.EmployeeId,
                    EmployeeName = employeeName
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
                var statuses = await creditsApiClient.LoadCreditStatusesAsync();
                var neededStatus = statuses.FirstOrDefault(s => s.StatusId == 1);
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
                    StatusName = neededStatus.Name,
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

            comboBox4.SelectedIndex = Convert.ToInt32(cellValueAccountTypeId) + 1;
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
            var employeeAccounts = await accountsApiClient.LoadAccountsAsync();
            var employeeCredits = await creditsApiClient.LoadCreditsAsync();
            var employeeTransactions = await transactionsApiClient.LoadTransactionsAsync();
            bool hasAccounts = employeeAccounts.Any(a => a.EmployeeId == clientId);
            bool hasClients = employeeCredits.Any(c => c.EmployeeId == clientId);
            bool hasTransactions = employeeTransactions.Any(t => t.EmployeeId == clientId);

            if (hasAccounts || hasClients || hasTransactions)
            {
                MessageBox.Show("Не можна видалити працівника, оскільки він створив акаунти або кредити чи провів транзакції. Працівник має передати свої обов'язки іншому працівникові перед тим, як видалити його акаунт.");
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
                BranchTypeId = comboBox8.SelectedIndex + 1,
                BranchTypeName = comboBox8.Text
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
                StatusName = comboBox6.Text,
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

            var row = dataGridView1.CurrentRow;
            int creditId = Convert.ToInt32(row.Cells["CreditId"].Value);

            // 1. Завантажуємо списки кредитів та клієнтів (якщо ще не завантажені)
            var credits = await creditsApiClient.LoadCreditsAsync() ?? new List<CreditDto>();
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();

            // 2. Знаходимо кредит за CreditId
            var credit = credits.FirstOrDefault(c => c.CreditId == creditId);
            if (credit == null)
            {
                MessageBox.Show("Кредит не знайдено.");
                return;
            }

            // 3. Знаходимо клієнта по ClientId
            var client = clients.FirstOrDefault(c => c.ClientId == credit.ClientId);
            if (client == null)
            {
                MessageBox.Show("Власник кредиту не знайдений.");
                return;
            }

            string creditOwnerName = $"{client.LastName} {client.FirstName} {client.MiddleName}";

            // 4. Створюємо DTO платежу
            PaymentDto paymentDto = new PaymentDto()
            {
                CreditId = creditId,
                CreditOwnerName = creditOwnerName,
                PaymentTypeId = comboBox7.SelectedIndex + 1,
                PaymentTypeName = comboBox7.Text,
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
                FirstName = textBox24.Text,
                LastName = textBox23.Text,
                MiddleName = textBox22.Text,
                Phone = textBox16.Text,
                Email = textBox14.Text,
                RoleId = comboBox9.SelectedIndex + 1,
                RoleName = comboBox9.Text,
                BranchId = comboBox10.SelectedIndex + 1,
                BranchName = comboBox10.Text
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
            employeeDto.EmployeeId = existingEmployee.EmployeeId;
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
        private async Task ShowFilteredAccountsAsync(List<AccountDto> filteredAccounts)
        {
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();
            var accountTypes = await accountsApiClient.LoadAccountTypesAsync() ?? new List<AccountTypeDto>();
            var currencies = await accountsApiClient.LoadCurrenciesAsync() ?? new List<CurrencyDto>();
            var branches = await branchesApiClient.LoadBranchesAsync() ?? new List<BankBranchDto>();
            var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();

            var clientDict = clients.ToDictionary(c => c.ClientId, c => $"{c.LastName} {c.FirstName} {c.MiddleName}");
            var accountTypeDict = accountTypes.ToDictionary(a => a.AccountTypeId, a => a.Name);
            var currencyDict = currencies.ToDictionary(c => c.CurrencyId, c => c.Name);
            var branchDict = branches.ToDictionary(b => b.BranchId, b => b.BranchName);
            var employeeDict = employees.ToDictionary(e => e.EmployeeId, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");

            var tableData = filteredAccounts.Select(a => new
            {
                a.AccountId,
                a.ClientId,
                a.AccountTypeId,
                a.CurrencyId,
                a.EmployeeId,
                ClientName = clientDict.ContainsKey(a.ClientId) ? clientDict[a.ClientId] : "",
                AccountTypeName = accountTypeDict.ContainsKey(a.AccountTypeId) ? accountTypeDict[a.AccountTypeId] : "",
                CurrencyName = currencyDict.ContainsKey(a.CurrencyId) ? currencyDict[a.CurrencyId] : "",
                BranchName = a.BranchId.HasValue && branchDict.ContainsKey(a.BranchId.Value) ? branchDict[a.BranchId.Value] : "",
                EmployeeName = a.EmployeeId.HasValue && employeeDict.ContainsKey(a.EmployeeId.Value) ? employeeDict[a.EmployeeId.Value] : "",
                a.Balance,
                a.OpenDate,
                a.CloseDate
            }).ToList();

            dataGridView1.DataSource = tableData;

            // Ховаємо ID
            if (dataGridView1.Columns.Contains("AccountId"))
                dataGridView1.Columns["AccountId"].Visible = false;
            if (dataGridView1.Columns.Contains("ClientId"))
                dataGridView1.Columns["ClientId"].Visible = false;
            if (dataGridView1.Columns.Contains("AccountTypeId"))
                dataGridView1.Columns["AccountTypeId"].Visible = false;
            if (dataGridView1.Columns.Contains("CurrencyId"))
                dataGridView1.Columns["CurrencyId"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;
        }
        private async Task ShowFilteredCreditsAsync(List<CreditDto> filteredCredits)
        {
            currentTable = "Credits";

            // Завантажуємо необхідні дані
            var clients = await clientsApiClient.LoadClientsAsync() ?? new List<ClientDto>();
            var employees = await employeesApiClient.LoadEmployeesAsync() ?? new List<EmployeeDto>();
            var creditStatuses = await creditsApiClient.LoadCreditStatusesAsync() ?? new List<CreditStatusDto>();

            // Створюємо словники для швидкого доступу
            var clientDict = clients.ToDictionary(c => c.ClientId, c => $"{c.LastName} {c.FirstName} {c.MiddleName}");
            var employeeDict = employees.ToDictionary(e => e.EmployeeId, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");
            var statusDict = creditStatuses.ToDictionary(s => s.StatusId, s => s.Name);

            // Формуємо дані для DataGridView
            var tableData = filteredCredits.Select(c => new
            {
                c.CreditId,
                c.AccountId,
                c.ClientId,
                c.EmployeeId,
                c.StatusId,
                ClientName = clientDict.ContainsKey(c.ClientId) ? clientDict[c.ClientId] : "",
                EmployeeName = c.EmployeeId.HasValue && employeeDict.ContainsKey(c.EmployeeId.Value)
                    ? employeeDict[c.EmployeeId.Value]
                    : "",
                StatusName = statusDict.ContainsKey(c.StatusId) ? statusDict[c.StatusId] : "",
                c.CreditAmount,
                c.InterestRate,
                c.StartDate,
                c.EndDate
            }).ToList();

            // Відображаємо у DataGridView
            dataGridView1.DataSource = tableData;

            // Ховаємо колонки з ID
            if (dataGridView1.Columns.Contains("CreditId"))
                dataGridView1.Columns["CreditId"].Visible = false;
            if (dataGridView1.Columns.Contains("AccountId"))
                dataGridView1.Columns["AccountId"].Visible = false;
            if (dataGridView1.Columns.Contains("ClientId"))
                dataGridView1.Columns["ClientId"].Visible = false;
            if (dataGridView1.Columns.Contains("EmployeeId"))
                dataGridView1.Columns["EmployeeId"].Visible = false;
            if (dataGridView1.Columns.Contains("StatusId"))
                dataGridView1.Columns["StatusId"].Visible = false;

            HidePanels();
        }


    }
}


