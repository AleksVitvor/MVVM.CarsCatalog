using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.CarsCatalog
{
    class Car : INotifyPropertyChanged
    {
        public int id { get; set; }
        private string _model;
        private int _maxSpeed;
        private decimal _price;
        public string Model
        {
            get { return _model; }
            set
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = $"update Cars set Model='{value}' where id={this.id}";
                sqlCommand.Connection = Singleton.SqlConnection;
                sqlCommand.ExecuteNonQuery();
                _model = value;
                OnPropertyChanged("Model");
            }
        }
        public int MaxSpeed
        {
            get
            {
                return _maxSpeed;
            }
            set
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = $"update Cars set MaxSpeed={value} where id={this.id}";
                sqlCommand.Connection = Singleton.SqlConnection;
                sqlCommand.ExecuteNonQuery();
                _maxSpeed = value;
                OnPropertyChanged("MaxSpeed");
            }
        }
        public decimal Price
        {
            get { return _price; }
            set
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandText = $"update Cars set Price={value} where id={this.id}";
                sqlCommand.Connection = Singleton.SqlConnection;
                sqlCommand.ExecuteNonQuery();
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        public Car(int id)
        {
            this.id = id;
        }
        public Car(int id, string Model, int MaxSpeed, int Price)
        {
            this.id = id;
            this._model = Model;
            this._maxSpeed = MaxSpeed;
            this._price = Price;
        }
        public Car()
        { }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
