using Caliburn.Micro;

namespace Jarloo.Tally.Models
{
    public class ProjectFile : PropertyChangedBase
    {
        private string name;
        private string path;
        private int lineCount;
        private int signifigantLineCount;

        public int SignifigantLineCount
        {
            get { return signifigantLineCount; }
            set
            {
                signifigantLineCount = value;
                NotifyOfPropertyChange(() => SignifigantLineCount);
            }
        }

        private string extension;

        public string Extension
        {
            get { return extension; }
            set
            {
                extension = value;
                NotifyOfPropertyChange(() => Extension);
            }
        }


        public int LineCount
        {
            get { return lineCount; }
            set
            {
                lineCount = value;
                NotifyOfPropertyChange(() => LineCount);
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
    }
}