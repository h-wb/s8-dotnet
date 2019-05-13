using Dep_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.Model
{
    class ComboboxAnneeModel : ViewModelBase
    {
        private ObservableCollection<String> _items = new ObservableCollection<String>();
        private string text;

        public ObservableCollection<String> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }
}
