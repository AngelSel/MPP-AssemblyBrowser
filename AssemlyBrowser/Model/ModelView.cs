using System.Collections.Generic;
using System.ComponentModel;
using AssemblyLibrary;

namespace AssemlyBrowser
{
    class ModelView :INotifyPropertyChanged
    {

        private Comands openFile;

        public Comands OpenFile
        {
            get
            {
                return openFile ?? (openFile = new Comands(obj =>
                {
                    IDialogService dialog = new DialogService();
                    if (dialog.OpenFileDialog())
                    {
                        Path = dialog.FilePath;
                        Browser browser = new Browser();
                        AssemblyInfo result = browser.GetResult(Path);
                        AssemblyName = result.assemblyName;
                        AssemblyInfo = result.namespaces;

                    }
                }));
            }
        }

        private string assemblyName;
        public string AssemblyName
        {
            get 
            { 
                return assemblyName; }

            set
            {
                assemblyName = value;
                OnPropertyChanged("AssemblyName");
                
            }
        }

        private List<AssemblyNamespace> assemblyInfo;
        public List<AssemblyNamespace> AssemblyInfo
        {
            get
            {
                return assemblyInfo;
            }
            set
            {
                assemblyInfo = value;
                OnPropertyChanged("AssemblyInfo");
            }
        }

        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));

        }

    }
}
