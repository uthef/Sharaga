﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Task_13.MVVM.Model;
using Task_13.MVVM.ViewModel;

namespace Task_13.MVVM.View.EditWindow
{
    /// <summary>
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditPositionWindow : Window
    {
        public EditPositionWindow(Position position)
        {
            InitializeComponent();
            MainViewModel.SelectedPosition = position;
            MainViewModel.PositionName = position.PositionName;
            MainViewModel.PositionSalary = position.Salary;
            MainViewModel.PositionMaxCountOfEmp = position.MaxCountOfEmployees;
            MainViewModel.PositionDepartment = position.Department;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
