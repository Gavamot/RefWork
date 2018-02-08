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

        private int? tegsCount;
        public int? TegsCount
        {
            get => tegsCount;
            set
            {
                this.tegsCount = value;
                OnPropertyChanged();
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
