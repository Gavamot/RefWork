using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using RefWork.Annotations;

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

        private List<string> tegs;

        public List<string> Tegs
        {
            get => tegs;
            set
            {
                tegs = value;
                TegsCount = value?.Count;
                OnPropertyChanged("TegsCount");
            }
        }

        public int? TegsCount { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
