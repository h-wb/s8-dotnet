using Dep_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class TextBlockModel : ViewModelBase
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }
}