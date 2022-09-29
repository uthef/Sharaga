using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_13.MVVM.Core;

namespace Task_13.MVVM.Model
{
    public class Position
    {
        public int ID { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }
        public int MaxCountOfEmployees { get; set; }
        public List<Employee> Employees { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public Department PositionDepartment
        {
            get
            {
                return DataWorker.GetDepartmentByID(DepartmentID);
            }
        }

        [NotMapped]
        public List<Employee> PositionEmployees
        {
            get
            {
                return DataWorker.GetAllEmployeesByPositionID(ID);
            }
        }

        public object DataWorker { get; private set; }
    }
}
