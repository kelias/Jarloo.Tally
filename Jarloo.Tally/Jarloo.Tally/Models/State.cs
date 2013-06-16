using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace Jarloo.Tally.Models
{
    internal class State : PropertyChangedBase
    {
        public ObservableCollection<Project> Projects { get; set; }

        
        private int totalLineCount;
        private int totalFileCount;
        private int totalFolderCount;
        private int totalProjectCount;
        private string targetFolder;
        


        public string TargetFolder
        {
            get { return targetFolder; }
            set
            {
                targetFolder = value;
                NotifyOfPropertyChange(() => TargetFolder);
            }
        }


        public int TotalProjectCount
        {
            get { return totalProjectCount; }
            set
            {
                totalProjectCount = value;
                NotifyOfPropertyChange(() => TotalProjectCount);
            }
        }


        public int TotalFolderCount
        {
            get { return totalFolderCount; }
            set
            {
                totalFolderCount = value;
                NotifyOfPropertyChange(() => TotalFolderCount);
            }
        }


        public int TotalFileCount
        {
            get { return totalFileCount; }
            set
            {
                totalFileCount = value;
                NotifyOfPropertyChange(() => TotalFileCount);
            }
        }


        public int TotalLineCount
        {
            get { return totalLineCount; }
            set
            {
                totalLineCount = value;
                NotifyOfPropertyChange(() => TotalLineCount);
            }
        }
        

        public State()
        {
            Projects = new ObservableCollection<Project>();
        }

    }
}