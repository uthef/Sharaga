using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Task_13.MVVM.Core;
using Task_13.MVVM.Model;
using Task_13.MVVM.View.AddWindow;

namespace Task_13.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Свойства

        #region Отдел
        public static string? DepartmentName { get; set; }
        #endregion

        #region Должность
        public static string? PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxCountOfEmp { get; set; }
        public static Department? PositionDepartment { get; set; }
        #endregion

        #region Сотрудники
        public static string? EmployeeName { get; set; }
        public static string? EmployeeSurname { get; set; }
        public static string? EmployeePhone { get; set; }
        public static Position? EmployeePosition { get; set; }
        #endregion

        #region Выделенный элемент
        public TabItem? SelectedTabItem { get; set; }
        public static Employee? SelectedEmployee { get; set; }
        public static Position? SelectedPosition { get; set; }
        public static Department? SelectedDepartment { get; set; }
        #endregion

        #endregion

        #region Все отделы
        private List<Department> _allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return _allDepartments; }
            set
            {
                _allDepartments = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Все должности
        private List<Position> _allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return _allPositions; }
            set
            {
                _allPositions = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Все сотрудники
        private List<Employee> _allEmployees = DataWorker.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return _allEmployees; }
            set
            {
                _allEmployees = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Открытие и центрирование окон

        #region Окна добавления
        public void OpenAddNewDepartmentWindow()
        {
            AddNewDepartmentWindow addNewDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpenWindow(addNewDepartmentWindow);
        }

        public void OpenAddNewPositionWindow()
        {
            AddNewPositionWindow addNewPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpenWindow(addNewPositionWindow);
        }

        public void OpenAddEmployeeWindow()
        {
            AddEmployeeWIndow addEmployeeWindow = new AddEmployeeWIndow();
            SetCenterPositionAndOpenWindow(addEmployeeWindow);
        }
        #endregion

        #region Центрирование окон
        private void SetCenterPositionAndOpenWindow(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        #region Команды открытия окна

        #region Окна добавления
        private readonly RelayCommand? _openAddDepartmentWindow;

        public RelayCommand OpenAddDepartmentWindow
        {
            get
            {
                return _openAddDepartmentWindow ?? new RelayCommand(e => { OpenAddDepartmentWindow(); });
            }
        }

        private readonly RelayCommand? _openAddPositionWindow;
        public RelayCommand OpenAddPositionWindow
        {
            get
            {
                return _openAddPositionWindow ?? new RelayCommand(e => { OpenAddNewPositionWindow(); });
            }
        }

        private readonly RelayCommand? _openAddEmpWindow;
        public RelayCommand OpenAndEmpWindow
        {
            get
            {
                return _openAddEmpWindow ?? new RelayCommand(e => { OpenAddEmployeeWindow(); });
            }
        }
        #endregion

        #endregion

        private readonly RelayCommand? _openEditItem;
        public RelayCommand OpenEditItem
        {
            get
            {
                return _openEditItem ?? new RelayCommand(e =>
                {
                    #region Работник 
                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        OpenEditEmployeeWindow(SelectedEmployee);
                    }
                    #endregion

                    #region Должность
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindow(SelectedPosition);
                    }
                    #endregion
                });
            }
        }

        private RelayCommand? _createNewDepartment;
        public RelayCommand CreateNewDepartment
        {
            get
            {
                return _createNewDepartment ?? new RelayCommand(c =>
                {
                    Window? window = c as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbDepartmentName");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateDepartmentView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }
        }

        private readonly RelayCommand? _createNewPosition;
        public RelayCommand CreateNewPosition
        {
            get
            {
                return _createNewPosition ?? new RelayCommand(p =>
                {
                    Window? window = p as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbPositionName");
                    }
                    if(PositionDepartment == null)
                    {
                        MessageBox.Show("Отдел не выбран", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxCountOfEmp, PositionDepartment);
                        UpdatePositionView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }
        }

        private readonly RelayCommand? _createNewEmployee;
        public RelayCommand CreateNewEmployee
        {
            get
            {
                return _createNewEmployee ?? new RelayCommand(e =>
                {
                    Window? window = e as Window;
                    string resultStr = "";
                    if (EmployeeName == null || EmployeeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbName");
                    }
                    if (EmployeeSurname == null || EmployeeSurname.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbSurname");
                    }
                    if (EmployeePhone == null || EmployeePhone.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(window, "TxbPhone");
                    }
                    if (EmployeePosition == null)
                    {
                        MessageBox.Show("Должность не выбрана", "Системное сообщение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(EmployeeName, EmployeeSurname, EmployeePhone, EmployeePosition);
                        UpdateEmployeeView();
                        ShowMessageToUser(resultStr);
                        SetNull();
                        window.Close();
                    }
                });
            }
        }

        private readonly RelayCommand? _deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _deleteItem ?? new RelayCommand(d =>
                {
                    string resultString = "Данных для удаления нет!";

                    #region Работник
                    if (SelectedTabItem.Name == "EmployeeTab" && SelectedEmployee != null)
                    {
                        resultString = DataWorker.DeleteEmployee(SelectedEmployee);
                        UpdateAllDataView();
                    }
                    #endregion

                    #region Должность
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultString = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }
                    #endregion

                    #region Департамент
                    if(SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment !=null)
                    {
                        resultString = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }
                    #endregion

                    #region Обновление
                    SetNull();
                    ShowMessageToUser(resultString);
                    #endregion
                });
            }
        }

        private readonly RelayCommand? _editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return _editEmployee ?? new RelayCommand(e =>
                {
                    Window? window = e as Window;
                    string resultString = "Работник не выбран!";
                    string noPositionStr = "Должность не выбрана";
                    if (SelectedEmployee != null)
                    {
                        if (EmployeePosition != null)
                        {
                            resultString = DataWorker.EditEmployee(SelectedEmployee,
                                EmployeeName, EmployeeSurname, EmployeePhone, EmployeePosition);
                            UpdateAllDataView();
                            SetNull();
                            ShowMessageToUser(resultString);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noPositionStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultString);
                    }
                });
            }
        }

        private readonly RelayCommand? _editPosition;
        public RelayCommand EditPosition {
            get
            {
                return _editPosition ?? new RelayCommand(p =>
                {
                    Window? window = p as Window;
                    string resultString = "Должность не выбрана!";
                    string noDepartmentStr = "Отдел не выбран";
                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultString = DataWorker.EditPosition(SelectedPosition,
                                PositionName, PositionSalary, PositionMaxCountOfEmp, PositionDepartment);
                            UpdateAllDataView();
                            SetNull();
                            ShowMessageToUser(resultString);
                            window.Close();
                        }
                        else
                        {
                            ShowMessageToUser(noDepartmentStr);
                        }
                    }
                    else
                    {
                        ShowMessageToUser(resultString);
                    }
                });
            }
        }

        private readonly RelayCommand? _editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return _editDepartment ?? new RelayCommand(d =>
                {
                    Window? window = d as Window;
                    string resultString = "Отдел не выбран";
                    if(SelectedDepartment != null)
                    {
                        resultString = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);
                        UpdateAllDataView();
                        SetNull();
                        ShowMessageToUser(resultString);
                        window.Close();
                    }
                    else
                    {
                        ShowMessageToUser(resultString);
                    }
                });
            }
        }

        #region Обновление ListView в MainWindow
        private void UpdateDepartmentView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.UpdateDepartmentView.ItemsSource = null;
            MainWindow.UpdateDepartmentView.Items.Clear();
            MainWindow.UpdateDepartmentView.ItemsSource = AllDepartments;
            MainWindow.UpdateDepartmentView.Items.Refresh();
        }

        private void UpdatePositionView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.UpdatePositionView.ItemsSource = null;
            MainWindow.UpdatePositionView.Items.Clear();
            MainWindow.UpdatePositionView.ItemsSource = AllPositions;
            MainWindow.UpdatePositionView.Items.Refresh();
        }

        private void UpdateEmployeeView()
        {
            AllEmployees = DataWorker.GetAllEmployees();
            MainWindow.UpdateEmployeeView.ItemsSource = null;
            MainWindow.UpdateEmployeeView.Items.Clear();
            MainWindow.UpdateEmployeeView.ItemsSource = AllPositions;
            MainWindow.UpdateEmployeeView.Items.Refresh();
        }
        
        private void UpdateAllDataView()
        {
            UpdateDepartmentView();
            UpdatePositionView();
            UpdateEmployeeView();
        }
        #endregion

        #region Дополнительные методы
        private void SetRedBlockControl(Window window, string blockName)
        {
            Control? control = window.FindName(blockName) as Control;
            control.BorderBrush = Brushes.DarkRed;
        }

        private void ShowMessageToUser(string message)
        {
            MessageWindow window = new MessageWindow(message);
            SetCenterPositionAndOpenWindow(window);
        }

        private void SetNull()
        {
            DepartmentName = null;

            PositionName = null;
            PositionSalary = 0;
            PositionMaxCountOfEmp = 0;
            PositionDepartment = null;

            EmployeeName = null;
            EmployeeSurname = null;
            EmployeePhone = null;
            EmployeePosition = null;
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion
    }
}