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


namespace RefWork.ViewModel
{
    
    public class MainVM : ViewModelBase
    {
        public ObservableCollection<RowUrl> RowsUrl { get; set; }
        public string Path { get; set; } = "./url.txt";
        public string Teg { get; set; } = "a";

        public int Progress { get; set; } = 0;
        private object ProgressSync = new object();
        public int ProgressCompleteCount;

        private readonly IUrlRep urlRep;
       

        public MainVM(IUrlRep urlRep)
        {
            RowsUrl = new ObservableCollection<RowUrl>();
            this.urlRep = urlRep;
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
            Progress = 0;
            int rowsCount = RowsUrl.Count;
            Task.Run(() =>
            { 
                Parallel.ForEach(RowsUrl, new ParallelOptions(), (item, loopState) =>
                {
                    Task.Delay(new Random(DateTime.Now.Millisecond).Next(4000, 20000));
                    ISiteLoader loader = new WebSite();
                    try
                    {
                        string content = loader.GetContent(item.Url);
                        ITegCounter tegCounter = new TegCounter();
                        item.TegsCount = tegCounter.CountTegs(content, Teg);
                        lock (ProgressSync)
                        {
                            ProgressCompleteCount++;
                            Progress = (int)(100 * ((double)ProgressCompleteCount / rowsCount));
                            RaisePropertyChanged("Progress");
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"({item.Name}) Could not get a information by a url");
                    }
                });
            });

        });
    }
}