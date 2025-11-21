using AutoMapper;
using BankSystem.ApiClients;
using BLL.Services;
using Core;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSystem
{
    public partial class RegistrationForm : Form
    {
        EmployeesApiClient employeesApiClient;
        private readonly IReportService reportService;
        private readonly ICurrentUserService currentUserService;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUnitOfWork unitOfWork;
        public RegistrationForm()
        {
            InitializeComponent();
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5136/") // або адреса твого WebAPI
            };
            employeesApiClient = new EmployeesApiClient(httpClient);
            BankDbContext bankDbContext = new BankDbContext();
            unitOfWork = new UnitOfWork(bankDbContext);
            reportService = new ReportService(unitOfWork);
            currentUserService = new CurrentUserService();
            passwordHasher = new PasswordHasher();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await employeesApiClient.LoginEmployeeAsync(textBox1.Text, textBox2.Text);
                if (result != null)
                {
                    currentUserService.SetEmployee(result.EmployeeId, result.BranchId, result.RoleId);
                    MainForm mainForm = new MainForm(reportService, currentUserService, passwordHasher);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Такого працівника немає в базі даних!");
                }
            }
            catch
            {

                MessageBox.Show("Такого користувача немає! Введи коректні дані!");
                return;
            }
        }
    }
}
