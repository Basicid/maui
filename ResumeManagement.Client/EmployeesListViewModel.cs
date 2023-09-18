using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManagement.Client
{
    public class EmployeesListViewModel:ObservableCollection<EmployeeViewModel>
    {
        public void AddSampleDataIfEmpty()
        {
            if (Count == 0)
            {
                Add(new EmployeeViewModel
                {
                    EmployeeId = "001",
                    EmployeeName = "John Doe",
                    JoinDate = new DateTime(2020, 5, 15),
                    Salary = 60000,
                    IsWorking = true,
                    ImageUrl = "john-doe.jpg",
                
                });

                Add(new EmployeeViewModel
                {
                    EmployeeId = "002",
                    EmployeeName = "Jane Smith",
                    JoinDate = new DateTime(2019, 8, 10),
                    Salary = 55000,
                    IsWorking = true,
                    ImageUrl = "jane-smith.jpg",
               
                });
            }
        }
    }
}
