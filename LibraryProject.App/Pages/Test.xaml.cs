using LibraryProject.App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace LibraryProject.App.Pages
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Test : LibraryProject.App.Common.LayoutAwarePage
    {
        ViewModel ViewModel;

        public Test()
        {
            this.InitializeComponent();

            List<Book> books = new List<Book>();
            populate(books);
        }

        private void populate(List<Book> books)
        {
            ViewModel = new ViewModel();
            
            foreach (Book b in books)
            {
                ViewModel.Books.Add(b);
            }
            this.DataContext = ViewModel;
            ItemDetailFrame.Navigate(typeof(NoItemSelected));
            BookListLabel.Text = String.Format("Books Found ({0})", books.Count());

            ViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SelectedItemIndex")
                {
                    if (ViewModel.SelectedItemIndex == -1)
                    {
                        ItemDetailFrame.Navigate(typeof(NoItemSelected));
                    }
                    else
                    {
                        ItemDetailFrame.Navigate(typeof(ItemDetail), ViewModel);
                    }
                }
            };
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void ListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            ViewModel.SelectedItemIndex = booksList.SelectedIndex;
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            if (ViewModel.SelectedItemIndex > -1)
            {
                Book selectedItem = ViewModel.Books[ViewModel.SelectedItemIndex];
                selectedItem.Save = true;
                ViewModel.SelectedItemIndex = booksList.SelectedIndex;

                Book book = (Book)booksList.SelectedItem;
                UpdateBook(book);
            }
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButtonClick(object sender, RoutedEventArgs e)
        {
            /*
            if (ViewModel.SelectedItemIndex > -1)
            {
                Book selectedItem = ViewModel.Books[ViewModel.SelectedItemIndex];
                selectedItem.Save = false;
                ViewModel.SelectedItemIndex = booksList.SelectedIndex;

                Book book = (Book)booksList.SelectedItem;
                UpdateBook(book);
            }

            //if (ViewModel.SelectedItemIndex > -1)
            //{
            //    ViewModel.Books.RemoveAt(ViewModel.SelectedItemIndex);
            //    ViewModel.SelectedItemIndex = -1;
            //}
            */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GotoSavedButtonClick(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        public async void UpdateBook(Book book)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection("books");
            await conn.UpdateAsync(book);
        }
    }
}
