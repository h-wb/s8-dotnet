using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using Metier;

namespace Model
{
    public class ObservableCollectionExt<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ObservableCollectionExt() : base()
        {
            this.CollectionChanged += new NotifyCollectionChangedEventHandler(ObservableCollectionExt_CollectionChanged);
        }

        public ObservableCollectionExt(IEnumerable<T> enumerable) : base()
        {
            this.CollectionChanged += new NotifyCollectionChangedEventHandler(ObservableCollectionExt_CollectionChanged);
        }

        void ObservableCollectionExt_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in e.OldItems)
                {
                    item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in e.NewItems)
                {
                    item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        public void Replace(T item)
        {
            Replace(new T[] { item });
        }

        /// <summary> 
        /// Replaces all elements in existing collection with specified collection of the ObservableCollection(Of T). 
        /// </summary> 
        public void Replace(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            Items.Clear();
            foreach (var i in collection) Items.Add(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public ObjetBase Find(ObservableCollectionExt<ObjetBase> collection, ObjetBase objetBase)
        {
            if(!(objetBase is null)) { 
                return collection.Where(p => p.Id == objetBase.Id).FirstOrDefault();
            }
            else
            {
                return objetBase;
            }
 

        }


        public void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //This will get called when the property of an object inside the collection changes - note you must make it a 'reset' - dunno why
            NotifyCollectionChangedEventArgs args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(args);
        }
    }
}