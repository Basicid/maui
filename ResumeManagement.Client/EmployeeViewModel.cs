using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManagement.Client
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string employeeId;
        private string employeeName;
        private DateTime joinDate;
        private decimal salary;
        private bool isWorking;
        private string imageUrl;

        public string EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; NotifyPropertyChanged(); }
        }

        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; NotifyPropertyChanged(); }
        }

        public DateTime JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; NotifyPropertyChanged(); }
        }

        public decimal Salary
        {
            get { return salary; }
            set { salary = value; NotifyPropertyChanged(); }
        }

        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; NotifyPropertyChanged(); }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; NotifyPropertyChanged(); }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}