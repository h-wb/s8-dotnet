using DAO;
using Dep_Gestion.Metier;
using Metier;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Dep_Gestion.Vues
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class ReglagesVue : Page
    {
        Reglages reglages;
        Categorie categorieSelect;
        TypeCours typeCoursSelect;

        private static AbstractDAOFactory factoSQL = AbstractDAOFactory.getFactory(types.SQL_FACTORY);


        private static DAO<Categorie> categ = factoSQL.getCategorieDAO();
        private static DAO<TypeCours> tps = factoSQL.getTypeCoursDao();

        public ReglagesVue()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            reglages = (Reglages)e.Parameter;
        }


        private void Categorie_ItemClick(object sender, ItemClickEventArgs e)
        {
            categorieSelect = (Categorie)e.ClickedItem;
        }

        private void TypeCours_ItemClick(object sender, ItemClickEventArgs e)
        {
            typeCoursSelect = (TypeCours)e.ClickedItem;
        }

        private void SupprimerCategorie_Tapped(object sender, TappedRoutedEventArgs e)
        {
            categ.delete(categorieSelect);
            reglages.Categorie.Remove(categorieSelect);
        }

        private void SupprimerTypeCours_Tapped(object sender, TappedRoutedEventArgs e)
        {

                tps.delete(typeCoursSelect);
                reglages.TypeCours.Remove(typeCoursSelect);
        }
            
    }
}
