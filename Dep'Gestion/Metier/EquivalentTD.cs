using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class EquivalentTD : ObjetBase
    {

        public Dictionary<TypeCours, double> ratiosCoursTD { get; set; }
        public int idCategorieEnseignant { get; set; }
        public int idTypeCours { get; set; }
        public Categorie categ { get; set; }
        public TypeCours _typeCours;
        public double ratio;

        public EquivalentTD()
        {
            this.init();
        }

        public EquivalentTD(Dictionary<TypeCours, double> ratios)
        {
            this.init();
            //this.ajouterRatiosCoursTD(ratios);
        }

        public EquivalentTD(TypeCours tc, double ratio)
        {
            this.init();
           // this.ajouterRatiosCoursTD(tc, ratio);
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
            this.categ = categ;
            this.TypeCours = tc;
            this.ratio = ratio;
        }


        //public void ajouterRatiosCoursTD(TypeCours tc, double ratio)
        //{
        //    //Vérification des doublons
        //    bool alreadyExists = false;
        //    foreach (KeyValuePair<TypeCours, double> typeCours in ratiosCoursTD)
        //    {
        //        if (tc.Nom == typeCours.Key.Nom)
        //        {
        //            alreadyExists = true;
        //        }
        //    }

        //    if (!alreadyExists)
        //    {
        //        ratiosCoursTD.Add(tc, ratio);
        //    }
        //}



        //Ajoute des ratiosCoursTD à ceux existants

        //public void ajouterRatiosCoursTD(Dictionary<TypeCours, double> ratios)
        //{
        //    ratios.ToList().ForEach(x => ratiosCoursTD[x.Key] = x.Value);
        //}

        public TypeCours TypeCours
        {
            get { return _typeCours; }
            set { SetProperty(ref _typeCours, value); }
        }



        override public string ToString()
        {
            string res = "";
            foreach (KeyValuePair<TypeCours, double> val in ratiosCoursTD)
            {
                res = String.Concat(res, "\n (typeCours:" + val.Key.ToString() + ", ratio:" + val.Value + ")");
            }
            return base.ToString() + res;
        }

    }
}