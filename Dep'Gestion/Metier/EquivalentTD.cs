using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class EquivalentTD : ObjetBase
    {
        public Categorie _categorie;
        public TypeCours _typeCours;
        public double _ratio;
        public ObservableCollectionExt<ObjetBase> _tCs;

        public EquivalentTD()
        {
            base.init();
        }
        

        new public void init()
        {
            //this.idCategorieEnseignant = -1;
            //base.init();
            //ratiosCoursTD = new Dictionary<TypeCours, double>();
            //TypeCours TD = new TypeCours("TD");
            //ratiosCoursTD.Add(TD, 1);
        }

        public EquivalentTD(int id, Categorie categ, TypeCours tc, double ratio)
        {
            this.init();
            this.Id = id;
            this.Categorie = categ;
            this.TypeCours = tc;
            this._ratio = ratio;
        }

        public EquivalentTD(Categorie categ, TypeCours tc, double ratio)
        {
            this.init();
            this._categorie = categ;
            this._typeCours = tc;
            this._ratio = ratio;
        }

        public TypeCours TypeCours
        {
            get { return _typeCours; }
            set { SetProperty(ref _typeCours, value); }
        }

        public Categorie Categorie
        {
            get { return _categorie; }
            set { SetProperty(ref _categorie, value); }
        }

        public double Ratio
        {
            get { return _ratio; }
            set { SetProperty(ref _ratio, value); }
        }


        //override public string ToString()
        //{
        //    string res = "";
        //    foreach (KeyValuePair<TypeCours, double> val in ratiosCoursTD)
        //    {
        //        res = String.Concat(res, "\n (typeCours:" + val.Key.ToString() + ", ratio:" + val.Value + ")");
        //    }
        //    return base.ToString() + res;
        //}

        public ObservableCollectionExt<ObjetBase> tCs
        {
            get { return _tCs; }
            set { SetProperty(ref _tCs, value); }
        }

    }
}