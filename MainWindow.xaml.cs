using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
﻿using Newtonsoft.Json;
namespace MovieApp_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Movie> Movies { get; set; } = new List<Movie>
        {
            new Movie
            {
                Name="Ford v Ferrari",
                Genre="biographical sports drama film",
                Director="James Mangold",
                ImagePath="Images/FordvFerrari.png",
            },
            new Movie
            {
                Name="John Wick 4",
                Genre="neo-noir action thriller film",
                Director="Chad Stahelski",
                ImagePath="Images/JohnWick.jpeg",
            },
            new Movie
            {
                Name="Gladiator",
                Genre="epic historical drama film",
                Director="Ridley Scott",
                ImagePath="Images/Gladiator.png",
            },
            new Movie
            {
                Name="Skyfall",
                Genre="spy film",
                Director="Sam Mendes",
                ImagePath="Images/Skyfall.jpg",
            }

        };
        public List<Movie> searchedMovies { get; set; } = new List<Movie> {};
        public dynamic Data { get; set; }
        public dynamic Data2 { get; set; }
        public dynamic Data3 { get; set; }
        public dynamic Data4 { get; set; }
        public dynamic SingleData { get; set; }
        public dynamic SingleData2{ get; set; }
        public dynamic SingleData3{ get; set; }
        public dynamic SingleData4{ get; set; }
        HttpClient httpClient = new HttpClient();

        public MainWindow()
        {
               this.DataContext = this;
              InitializeComponent();
             ListBox_Load();
            
        }

        private void EditMovie(object sender, EventArgs e)
        {
            string movie_name = "";
            if (sender is Button bt && bt.Parent is Grid gr)
            {
                if (gr.Parent is Grid gr1)
                {

                
                foreach (var item in gr1.Children)
                {
                    if (item is Label lbl)
                    {
                        movie_name = lbl.Content.ToString();
                    }
                    if (item is TextBox txtb)
                    {
                        foreach (Movie movie in Movies)
                        {
                            if (movie.Name==movie_name)
                            {
                                MessageBox.Show("Edited");
                                movie.Name = txtb.Text;
                                break;
                            }
                        }
                    }
                }
                }
            }
            movieListBox.Items.Refresh();
        }

        public EventHandler<EventArgs> editButton_Click { get; set; }
        private void ListBox_Load()
        {
            try
            {

                HttpResponseMessage response = new HttpResponseMessage();
                HttpResponseMessage response2 = new HttpResponseMessage();
                HttpResponseMessage response3 = new HttpResponseMessage();
                HttpResponseMessage response4 = new HttpResponseMessage();
                response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={"love"}&plot=full").Result;
                response2 = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={"car"}&plot=full").Result;
                response3 = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={"agent"}&plot=full").Result;
                response4 = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={"scary"}&plot=full").Result;
                var str = response.Content.ReadAsStringAsync().Result;
                Data = JsonConvert.DeserializeObject(str);
                 var str2 = response2.Content.ReadAsStringAsync().Result;
                Data2 = JsonConvert.DeserializeObject(str2);
                 var str3 = response3.Content.ReadAsStringAsync().Result;
                Data3 = JsonConvert.DeserializeObject(str3);
                var str4 = response4.Content.ReadAsStringAsync().Result;
                Data4 = JsonConvert.DeserializeObject(str4);

                for (int i = 0; i < 5; i++)
                {
                    response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data.Search[i].Title}&plot=full").Result;
                    response2= httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data2.Search[i].Title}&plot=full").Result;
                    response3= httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data3.Search[i].Title}&plot=full").Result;
                    response4= httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data4.Search[i].Title}&plot=full").Result;

                    str = response.Content.ReadAsStringAsync().Result;
                    SingleData = JsonConvert.DeserializeObject(str);

                     str2 = response2.Content.ReadAsStringAsync().Result;
                    SingleData2 = JsonConvert.DeserializeObject(str2);

                     str3 = response3.Content.ReadAsStringAsync().Result;
                    SingleData3 = JsonConvert.DeserializeObject(str3);

                    str4 = response4.Content.ReadAsStringAsync().Result;
                    SingleData4 = JsonConvert.DeserializeObject(str4);


                    Movies.Add(new Movie { Name = SingleData.Title, Director = SingleData.Director, Genre = SingleData.Genre, ImagePath = SingleData.Poster });
                    Movies.Add(new Movie { Name = SingleData2.Title, Director = SingleData2.Director, Genre = SingleData2.Genre, ImagePath = SingleData2.Poster });
                    Movies.Add(new Movie { Name = SingleData3.Title, Director = SingleData3.Director, Genre = SingleData3.Genre, ImagePath = SingleData3.Poster });
                    Movies.Add(new Movie { Name = SingleData4.Title, Director = SingleData4.Director, Genre = SingleData4.Genre, ImagePath = SingleData4.Poster });
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                searchedMovies.Clear();
                searchedMovieListBox.Items.Refresh();
                var name = movieTextbox.Text;
                HttpResponseMessage response = new HttpResponseMessage();
                response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={name}&plot=full").Result;
                var str = response.Content.ReadAsStringAsync().Result;
                Data = JsonConvert.DeserializeObject(str);
                for (int i = 0; i < 1; i++)
                {
                    response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data.Search[i].Title}&plot=full").Result;
                    str = response.Content.ReadAsStringAsync().Result;
                    SingleData = JsonConvert.DeserializeObject(str);
                    searchedMovies.Add(new Movie { Name = SingleData.Title, Director = SingleData.Director, Genre = SingleData.Genre, ImagePath = SingleData.Poster });
                }
                searchedMovieListBox.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void plusBtn_Click(object sender, RoutedEventArgs e)
        {
            editButton_Click = new EventHandler<EventArgs>(EditMovie);
            if (sender is Button bt)
            {
                Grid myGrid = bt.Parent as Grid;
                EditMovieUC movieUC = new EditMovieUC();
                movieUC.editButton_Click = editButton_Click;
                foreach (var item in myGrid.Children)
                {
                    if (item is TextBlock txtb)
                    {
                        movieUC.movieNameLbl.Content = txtb.Text;
                    }
                    else if (item is Image img)
                    {
                        movieUC.movieImage.Source = img.Source;
                    }                   
                }
                editMoviegrid.Children.Add(movieUC);

            }


        }

        private void plusBtn2_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button bt)
            {
                Grid myGrid = bt.Parent as Grid;
                foreach (var item in myGrid.Children)
                {
                    if (item is TextBlock txtb)
                    {
                        HttpResponseMessage response = new HttpResponseMessage();
                        response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&s={txtb.Text}&plot=full").Result;
                        var str = response.Content.ReadAsStringAsync().Result;
                        Data = JsonConvert.DeserializeObject(str);
                        response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=a0bc8f02&t={Data.Search[0].Title}&plot=full").Result;
                        str = response.Content.ReadAsStringAsync().Result;
                        SingleData = JsonConvert.DeserializeObject(str);
                        Movies.Add(new Movie { Name = SingleData.Title, Director = SingleData.Director, Genre = SingleData.Genre, ImagePath = SingleData.Poster });
                        movieListBox.Items.Refresh();
                        MessageBox.Show("Movie has been  successfully added !");
                    }
                }
            }
        }
    }
}
