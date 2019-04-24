using DAO;
using Dep_Gestion.ViewModel;
using Metier;
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
        private ObjetAvecIdEtNom _objet;
        private ObservableCollection<MenuItem> _children = new ObservableCollection<MenuItem>();
        private MenuItem _parent;

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

        public ObjetAvecIdEtNom Objet
        {
            get { return _objet; }
            set { SetProperty(ref _objet, value); }
        }

        public ObservableCollection<MenuItem> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

        public MenuItem Parent
        {
            get { return _parent; }
            set { SetProperty(ref _parent, value); }
        }

        public override string ToString()
        {
            return _text;
        }

    }
}

