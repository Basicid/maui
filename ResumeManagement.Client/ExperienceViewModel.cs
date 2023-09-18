using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManagement.Client
{
    public class ExperienceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private string title;
        private string duration;

        public int Id
        {
            get { return id; }
            set { id = value; NotifyPropertyChanged(); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }

        public string Duration
        {
            get { return duration; }
            set { duration = value; NotifyPropertyChanged(); }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
