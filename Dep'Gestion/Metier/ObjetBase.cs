﻿using Dep_Gestion.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public abstract class ObjetBase : ViewModelBase
    {
        protected int _id;
        protected string _nom;
        protected bool _visibility;
        protected ObservableCollectionExt<ObjetBase> _children = new ObservableCollectionExt<ObjetBase>();
        protected ObjetBase _parent;
        private Type _navigationDestination;
        private object _navigationParameter;

        public void init()
        {
            this.Id = -1;
        }

        override public string ToString()
        { 
            string res = "";
            res += this.GetType().Name + ": id=" + _id + ", nom=" + _nom;
            return res;
        }

        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public string Nom
        {
            get { return _nom; }
            set { SetProperty(ref _nom, value); }
        }

        public bool Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        public ObservableCollectionExt<ObjetBase> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

        public ObjetBase Parent
        {
            get { return _parent; }
            set { SetProperty(ref _parent, value); }
        }

        public Type NavigationDestination
        {
            get { return _navigationDestination; }
            set { SetProperty(ref _navigationDestination, value); }
        }

        public object NavigationParameter
        {
            get { return _navigationParameter; }
            set { SetProperty(ref _navigationParameter, value); }
        }

    }
}