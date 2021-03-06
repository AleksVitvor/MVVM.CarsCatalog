﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows.Interactivity;

namespace MVVM.CarsCatalog
{
    class CarViewModel : INotifyPropertyChanged

    {
        private int id = 0;
        private Car _selectedCar;
        public ObservableCollection<Car> Cars { get; set; } = new ObservableCollection<Car>();
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                      {
                          Car car = new Car(id);
                          SqlCommand sqlCommand = new SqlCommand();
                          sqlCommand.CommandText = $"insert into Cars values({id},'',0,0)";
                          id++;
                          sqlCommand.Connection = Singleton.SqlConnection;
                          int number = sqlCommand.ExecuteNonQuery();
                          Cars.Insert(0, car);
                          SelectedCar = car;
                      }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(obj =>
                    {
                        Car car = obj as Car;
                        if (car != null)
                        {
                            SqlCommand sqlCommand = new SqlCommand();
                            sqlCommand.CommandText = $"delete from Cars where id={car.id}";
                            sqlCommand.Connection = Singleton.SqlConnection;
                            sqlCommand.ExecuteNonQuery();
                            Cars.Remove(car);
                        }
                    },
                    (obj) => Cars.Count > 0));
            }
        }
        public CarViewModel()
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "select * from Cars";
            sqlCommand.Connection = Singleton.SqlConnection;
            SqlDataReader reader=sqlCommand.ExecuteReader();

            foreach(var i in reader)
            {
                Car car = new Car(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3));
                id = reader.GetInt32(0);
                Cars.Add(car);
            }
            reader.Close();
            id++;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
