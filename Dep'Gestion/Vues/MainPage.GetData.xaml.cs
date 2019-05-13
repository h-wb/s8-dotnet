using Metier;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGestion
{
    public partial class MainPage
    {
        private ObservableCollectionExt<Departement> GetDepartements()
        {
            ObservableCollectionExt<Departement> departements = new ObservableCollectionExt<Departement>();
            foreach (Departement dpt in depart.findAll())
            {
                departements.Add(new Departement { Id = dpt.Id, Nom = dpt.Nom });
            }
            return departements;
        }

        private ObservableCollectionExt<Enseignant> GetEnseignants()
        {
            ObservableCollectionExt<Enseignant> enseignants = new ObservableCollectionExt<Enseignant>();
            foreach (Enseignant ens in enseignant.findAll())
            {
                enseignants.Add(new Enseignant { Id = ens.Id, Categorie = ens.Categorie, Prenom = ens.Prenom.TrimEnd(), Nom = ens.Nom.TrimEnd(), NavigationDestination = typeof(EnseignantVue) });
            }
            return enseignants;
        }
    }
}
