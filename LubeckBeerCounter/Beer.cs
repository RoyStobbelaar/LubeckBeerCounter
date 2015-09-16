using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace LubeckBeerCounter
{
    public class Beer : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string count){
            PropertyChangedEventHandler handler = PropertyChanged;        
            if (handler != null) handler(this, new PropertyChangedEventArgs(count));
        }

        protected bool SetField<T>(ref T field, T value, string count)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(count);
            return true;
        }
        #endregion

		public string beerName { get; set; }
		public double cost {get;set;}
		public string imgPath {get;set;}
        private double count;
        public double Count 
        {
            get { return count; }
            set { SetField(ref count, value, "Count"); }
        }

        private double price;
        public double Price 
        {
            get { return price; }
            set { SetField(ref price, value, "Price"); }
        }

        public Beer(){
            count = 0;
            System.Diagnostics.Debug.WriteLine("Load beer");
        }
        public Beer(string beerName, double cost){
            this.beerName = beerName;
            this.cost = cost;
        }
			
		public void Increase(){
            Count++;
            Price = count * cost;
		}

		public void Decrease(){
			count--;
			price = count * cost;
		}

		public double GetTotalPrice(){
			return count * cost;
		}
    }
}

