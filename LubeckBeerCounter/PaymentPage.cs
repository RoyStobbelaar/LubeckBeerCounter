using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections;

namespace LubeckBeerCounter
{
    public class PaymentPage : ContentPage
    {
        public ObservableCollection<Beer> beerList;
        private double totalPrice;

        public PaymentPage(IEnumerable beerList)
        {
            ListView view = new ListView();
            view.ItemsSource = beerList;

            var content = new StackLayout();

            //Frontpage layout
            var logo = new Image()
            {
                Aspect = Aspect.AspectFit,
                HeightRequest = WidthRequest = 60
            };
            logo.Source = ImageSource.FromFile("icon");

            var title = new Label()
            {
                Text = "Lübeck Bill",
                FontSize = 22,
                HeightRequest = 100,
                TextColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center
            };

            content.Children.Add(logo);
            content.Children.Add(title);

            foreach (Beer b in view.ItemsSource)
            {
                //Create label for each beer > 0
                if (b.Count > 0)
                {
                    content.Children.Add(new Label()
                        {
                            //Text = b.beerName + " #" + b.Count + " totalcost:" + b.Price,
                            FormattedText = String.Format("{1}x  {0} \t\t Total:{2:C2}", b.beerName, b.Count, b.Price),
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            HeightRequest = 50,
                            FontSize = 18
                        });
                    content.Children.Add(new Label()
                        {
                            HeightRequest = 1,
                            BackgroundColor = Color.FromRgba(255, 255, 100, 100)
                        });
                        
                    totalPrice += b.Price;
                }
            }
            var totalLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 50,
                FontSize = 24,
                TextColor = Color.Red,
                    BackgroundColor = Color.FromRgba(100,100,100,100),
                FormattedText = string.Format("Total: {0:C2}", totalPrice)
            };
            content.Children.Add(totalLabel);
            content.HorizontalOptions = LayoutOptions.CenterAndExpand;
            Content = content;
        }
    }
}


