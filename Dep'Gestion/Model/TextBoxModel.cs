using Dep_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class TextBoxModel : ViewModelBase
    {
        private string text;

        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }
    }
}