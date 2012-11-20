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
using LibraryProject.App.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LibraryProject.App.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemDetail : Page
    {
        private ViewModel viewModel;

        public ItemDetail()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = e.Parameter as ViewModel;
            this.DataContext = viewModel;

            viewModel.PropertyChanged += (sender, eventArgs) =>
            {
                if (eventArgs.PropertyName == "SelectedItemIndex")
                {
                    if (viewModel.SelectedItemIndex == -1)
                    {
                        SetItemDetail(null);
                    }
                    else
                    {
                        SetItemDetail(viewModel.Books[viewModel.SelectedItemIndex]);
                    }
                }
            };

            SetItemDetail(viewModel.Books[viewModel.SelectedItemIndex]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void SetItemDetail(Book item)
        {
            ItemDetailTitle.Text = (item == null) ? "" : item.Title;
            ItemDetailAuthor.Text = (item == null) ? "" : item.Author;
            ItemDetailISBN.Text = (item == null) ? "" : item.ISBN.ToString();
            ItemDetailDescription.Text = (item == null) ? "" : item.Description;
            ItemDetailSaved.Text = (item == null) ? "" : YesNoString(item.Save);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleItemChange(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedItemIndex > -1)
            {
                Book selectedItem = viewModel.Books[viewModel.SelectedItemIndex];
                if (sender == ItemDetailTitle) {
                    selectedItem.Title = ItemDetailTitle.Text;
                } else if (sender == ItemDetailAuthor) {
                    selectedItem.Author = ItemDetailAuthor.Text;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string YesNoString(bool value)
        {
            if(value == false)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }
        }
    }
}
