using Dep_Gestion.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.Model
{
    class ButtonModel : ViewModelBase
    {
        private string content;

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }
    }
}