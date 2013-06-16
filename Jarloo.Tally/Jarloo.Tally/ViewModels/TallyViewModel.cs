using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jarloo.Tally.Models;
using Screen = Caliburn.Micro.Screen;

namespace Jarloo.Tally.ViewModels
{
    [Export]
    internal class TallyViewModel : Screen
    {
        #region Properties

        private bool isWorking;
        private Project selectedProject;

        public State State { get; set; }

        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                selectedProject = value;
                NotifyOfPropertyChange(() => SelectedProject);
            }
        }

        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                NotifyOfPropertyChange(() => IsWorking);
            }
        }

        #endregion

        private readonly IProcessor processor;

        private CancellationTokenSource cancelSource = new CancellationTokenSource();

        [ImportingConstructor]
        public TallyViewModel(IProcessor p)
        {
            processor = p;

            State = new State();
        }

        private async void Process()
        {
            Clear();

            try
            {
                IsWorking = true;
                
                await Task.Factory.StartNew(() => processor.Process(State,cancelSource));
            }
            finally
            {
                IsWorking = false;
            }
        }

        public void SelectFolder()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            State.TargetFolder = dlg.SelectedPath;
            Process();
        }

        public void Clear()
        {
            State.TotalFolderCount = 0;
            State.TotalFileCount = 0;
            State.TotalLineCount = 0;
            State.TotalProjectCount = 0;
            State.Projects.Clear();
        }

        public override void TryClose()
        {
            cancelSource.Cancel();
        }
    }
}