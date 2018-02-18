using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RefWork.Annotations;
using RefWork.ViewModel.Class;

namespace RefWork.ViewModel
{
    public class RowUrl : INotifyPropertyChanged
    {
        public RowUrl() { }

        public RowUrl(Uri url)
        {
            Url = url;   
        }

        private Uri url;
        public Uri Url
        {
            get => url;
            set
            {
                this.url = value;
                OnPropertyChanged();
            }
        }

        public string Name => Url.AbsoluteUri;

        private List<ValueWrapper<string>> tegs;
        public List<ValueWrapper<string>> Tegs
        {
            get => tegs;
            set
            {
                tegs = value;
                TegsCount = value?.Count;
                OnPropertyChanged("TegsCount");
            }
        }

        public bool IsLider { get; private set; }
        public int? TegsCount { get; private set; }

        private ValueWrapper<int> maxTegs = new ValueWrapper<int>(-1);
        public ValueWrapper<int> MaxTegs
        {
            get => maxTegs;
            set
            {
                maxTegs = value;
                IsLider = value.Value == TegsCount;
                OnPropertyChanged("IsLider");
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
