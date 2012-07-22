using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GameJoltAPI;

namespace SilverJolt
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            Config.game_id = 8763;
            PlaceRectangles();
        }

        private void PlaceRectangles()
        {
            Random r = new Random();
            foreach (UIElement uc in rectarea.Children)
            {
                if (uc is Rectangle)
                {
                    Rectangle rc = (Rectangle)uc;
                    rc.Margin = new Thickness(r.Next(0, 800), r.Next(0, 480), 0, 0);
                    rc.Opacity = (r.NextDouble() + 1) / 16;
                    rc.Width =  Math.Pow(2,r.Next(6, 8));
                    rc.Height = rc.Width;
                }
            }
        }

        public void SetState(string state)
        {
            stateReporter.Text = state;
        }

        private void UserInputLostFocusEvent(object sender, RoutedEventArgs e)
        {
            Config.username = userInput.Text;
        }

        private void TokenInputFocusEvent(object sender, RoutedEventArgs e)
        {
            Config.user_token = tokenInput.Text;
            SetState("Attempting to Authenticate");
            try
            {
                if (GameJoltAPI.Users.User.AuthenticateUser())
                {
                    SetState("Authenticated " + Config.username);
                }
                else SetState("Failed to authenticate " + Config.username + ". Please confirm your identity and try again.");
            }
            catch (GameJoltAPI.Exceptions.APIFailReturned fail) { SetState("Internal Error"); }
        }
    }
}
