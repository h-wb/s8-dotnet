using Dep_Gestion.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Service : ViewModelBase
    {
        private double _heures;
        private string _information;


  

        public double Heures
        {
            get { return _heures; }
            set { SetProperty(ref _heures, value); }
        }

        public string Information
        {
            get { return _information; }
            set { SetProperty(ref _information, value); }
        }

    }
}