using Dep_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.Model
{
    public class MenuItem : ViewModelBase
    {

        private string _glyph;
        private string _text;
        private int _id;
        private ObservableCollection<MenuItem> _children = new ObservableCollection<MenuItem>();

        public string Glyph
        {
            get { return _glyph; }
            set { SetProperty(ref _glyph, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public ObservableCollection<MenuItem> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

        public override string ToString()
        {
            return _text;
        }

    }
}
