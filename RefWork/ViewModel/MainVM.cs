using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using RefWork.Model;
using RefWork.ViewModel.Class;


namespace RefWork.ViewModel
{
    
    public class MainVM : ViewModelBase
    {
        public ObservableCollection<RowUrl> RowsUrl { get; set; }

        private RowUrl selectedRowUrl;
        public RowUrl SelectedRowUrl
        {
            get => selectedRowUrl;
            set
            {
                selectedRowUrl = value;
                RaisePropertyChanged("SelectedRowUrl");
            }
        }

        public string Teg { get; set; } = "a";
        
        public ValueWrapper<int> MaxTegs { get; set; }

        private string path = "./url.txt";
        public string Path
        {
            get => path;
            set
            {
                path = value;
                RaisePropertyChanged("Path");
            }
        }
      
        private int progress;
        public int Progress {
            get => progress;
            set
            {
                progress = value < 0 ? 0 : value;
                RaisePropertyChanged("Progress");
            }
        }

        private bool isButtonsEnable = true;
        public bool IsButtonsEnable
        {
            get => isButtonsEnable;
            set
            {
                isButtonsEnable = value;
                RaisePropertyChanged("IsButtonsEnable");
            }
        } 

        private readonly IUrlRep urlRep;
        private readonly ISiteLoader loader;
        private readonly ITegCounter tegCounter;

        public MainVM(IUrlRep urlRep, ISiteLoader loader, ITegCounter tegCounter)
        {
            RowsUrl = new ObservableCollection<RowUrl>();
            this.urlRep = urlRep;
            this.loader = loader;
            this.tegCounter = tegCounter;
        }

        public RelayCommand ObservePath => new RelayCommand(() =>
        {
            var fd = new OpenFileDialog();
            fd.Filter = "All Files (*.txt)|*.txt";
            fd.FilterIndex = 1;
            fd.Multiselect = false;

            if (fd.ShowDialog() == true)
                Path = fd.FileName;
        });

        public RelayCommand ReadFile => new RelayCommand(() =>
        {
            Progress = 0;
            if (string.IsNullOrEmpty(Path)) return;
            try
            {
                RowsUrl.Clear();
                var urls = urlRep.GetUris(Path);
                var urlRow = urls.Select(x => new RowUrl(x));
                foreach (var row in urlRow)
                {
                    RowsUrl.Add(row);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });

        public RelayCommand Analysis => new RelayCommand(() =>
        {
            if (RowsUrl.Count == 0)
                return;
            IsButtonsEnable = false;
            Progress = 0;
            Task.Run((Action)MakeRowsAnalize);
        });

        private void MakeRowsAnalize()
        {
            int rowsCount = RowsUrl.Count;
            int progressCompleteCount = 0;
            object progressSync = new object();
            Parallel.ForEach(RowsUrl, new ParallelOptions(), (item, loopState) =>
            {
                try
                {
                    string content = loader.GetContent(item.Url);
                    item.Tegs = tegCounter.GetTegs(content, Teg)
                        .Select(x => new ValueWrapper<string>(x))
                        .ToList();
                    RaisePropertyChanged("SelectedRowUrl");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    lock (progressSync)
                    {
                        progressCompleteCount++;
                        Progress = (int)(100 * ((double)progressCompleteCount / rowsCount));
                        if (progressCompleteCount == rowsCount)
                            IsButtonsEnable = true;
                    }
                }
            });
            MaxTegs = new ValueWrapper<int>(RowsUrl.Max(x => x.TegsCount ?? -1));
            foreach (var row in RowsUrl)
            {
                row.MaxTegs = MaxTegs;
            }
        }
    }
}