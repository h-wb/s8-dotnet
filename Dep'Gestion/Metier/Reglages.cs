using Dep_Gestion.ViewModel;
using Metier;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dep_Gestion.Metier
{
    class Reglages : ViewModelBase
    {

        protected ObservableCollectionExt<Categorie> _categorie = new ObservableCollectionExt<Categorie>();
        protected ObservableCollectionExt<TypeCours> _type = new ObservableCollectionExt<TypeCours>();

        public ObservableCollectionExt<Categorie> Categorie
        {
            get { return _categorie; }
            set { SetProperty(ref _categorie, value); }
        }

        public ObservableCollectionExt<TypeCours> TypeCours
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

    }
}
