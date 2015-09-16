using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LubeckBeerCounter
{
    public class LubeckBeerCard : ContentPage
    {
        public LubeckBeerCard()
        {
            //Frontpage layout
            var logo = new Image()
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = WidthRequest = 60
            };
            logo.Source = ImageSource.FromFile("icon");

            var title = new Label()
            {
                Text = "Lübeck BeerApp",
                FontSize = 22,
                HeightRequest = 100,
                TextColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            var beerView = new ListView()
            {
                RowHeight = 80,
            };

            var source = new ObservableCollection<Beer>
            {
                new Beer{ beerName = "Hertog Jan (Tap)", cost = 2.50, imgPath = "hjTap" },
                new Beer{ beerName = "Hertog Jan 0.5l", cost = 5.00, imgPath = "hjTap" },
                new Beer{ beerName = "Amstel Radler", cost = 3.00, imgPath = "radler" },
                new Beer{ beerName = "Leffe Blond", cost = 3.00, imgPath = "blond" },
                new Beer{ beerName = "Leffe Dubbel", cost = 3.50, imgPath = "dubbel" },
                new Beer{ beerName = "Leffe Tripel", cost = 3.75, imgPath = "tripel" },
                new Beer{ beerName = "Hertog Jan Grand Prestige", cost = 4.50, imgPath = "grand" },
                new Beer{ beerName = "Hoegaarden Witbier", cost = 3.25, imgPath = "wit" }
            };
            beerView.ItemsSource = source;

            //Add beers
            beerView.ItemTemplate = new DataTemplate(typeof(BeerCell));
            beerView.BackgroundColor = Color.FromRgb(40, 40, 40);
            beerView.SeparatorColor = Color.FromRgba(255, 255, 100, 100);

            beerView.ItemTapped += (sender, e) =>
            {
                var selectedBeer = (Beer)e.Item;

                foreach (Beer beer in beerView.ItemsSource)
                    if (beer.beerName.Equals(selectedBeer.beerName))
                        beer.Increase();
            };

            var btnPay = new Button()
            {
                Text = "Payment bill",
                TextColor = Color.Yellow
            };
            btnPay.Clicked += async (object sender, EventArgs e) =>
            {

                PaymentPage paymentPage = new PaymentPage(beerView.ItemsSource);
                await Navigation.PushModalAsync(paymentPage);
            };

            Content = new StackLayout
            { 
                Orientation = StackOrientation.Vertical,
                Children = { logo, title, beerView, btnPay }
            };
        }
    }

    public class BeerCell : ViewCell
    {
        public BeerCell()
        {
            //BeerImage
            var beerImage = new Image()
            {
                HorizontalOptions = LayoutOptions.Start,
            };
            beerImage.SetBinding(Image.SourceProperty, new Binding("imgPath"));
            beerImage.WidthRequest = beerImage.HeightRequest = 100;

            var allLayout = CreateNewAllLayout();

            var beerCell = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { beerImage, allLayout }
            };
            View = beerCell;
        }

        public StackLayout CreateNewAllLayout()
        {

            var infoLayout = CreateNewInfoLayout();
            var countLayout = CreateNewCountLayout();


            var allLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { infoLayout,  countLayout }
            };

            return allLayout;
        }

        public StackLayout CreateNewInfoLayout()
        {
            //BeerName
            var beerName = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 12,
            };
            beerName.SetBinding(Label.TextProperty, new Binding("beerName",stringFormat: "{0}"));

            //BeerPrice
            var beerPrice = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 12,
            };
            beerPrice.SetBinding(Label.TextProperty, new Binding("cost", stringFormat: "price: {0}"));

            //Layout
            var infoLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { beerName,  beerPrice }
            };

            return infoLayout;

        }

        public StackLayout CreateNewCountLayout()
        {
            var lblCount = new Label()
            {
                HorizontalOptions = LayoutOptions.Start,
                FontSize = 24,
            };
            lblCount.SetBinding(Label.TextProperty, new Binding("Count" , stringFormat: "#{0}"));

            var lblPrice = new Label()
            {
                HorizontalOptions = LayoutOptions.End,
                FontSize = 24,
            };
            lblPrice.SetBinding(Label.TextProperty, new Binding("Price", stringFormat: "\t\t\t{0:C2}"));

            var countLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { lblCount, lblPrice }
            };

            return countLayout;
        }
    }
}