using System.Collections.ObjectModel;
using Caliburn.Micro;

namespace Jarloo.Tally.Models
{
    public class Project : PropertyChangedBase 
    {
        private string name;
        private string path;
        private int totalLineCount;
        private ObservableCollection<ProjectFile> files;

        public ObservableCollection<ProjectFile> Files
        {
            get { return files; }
            set
            {
                files = value;
                NotifyOfPropertyChange(() => Files);
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


        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyOfPropertyChange(() => Path);
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }


        public Project()
        {
            files = new ObservableCollection<ProjectFile>();
        }
    }
}