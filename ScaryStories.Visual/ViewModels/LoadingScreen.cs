using System;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Helpers.ProgressIndicator;


namespace ScaryStories.Visual.ViewModels
{
    public class LoadingScreen : Screen
    {
        private bool _indicatorVisible;
        public ProgressIndicator _progressIndicator;
        private bool _isLoading;
        private bool _isAllMenuEnabled = true;

        protected override void OnDeactivate(bool close)
        {
            HideProgressIndicator();
        }


        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; base.NotifyOfPropertyChange(() => IsLoading); }
        }

        public bool IsAllMenuEnabled
        {
            get { return _isAllMenuEnabled; }
            set
            {
                _isAllMenuEnabled = value;
                base.NotifyOfPropertyChange(() => IsAllMenuEnabled);
            }
        }

        public LoadingScreen()
        {
            AddCustomConventions();
        }

        protected virtual void Wait(bool wait)
        {
            IsLoading = wait;
            IsAllMenuEnabled = !wait;
        }

        static void AddCustomConventions()
        {

            // App Bar Conventions
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");

            // ... the rest of your conventions
        }

        public void ShowProgressIndicator(String message)
        {
            if (_indicatorVisible)
            {
                return;
            }

            Wait(true);
            _indicatorVisible = true;

            if (_progressIndicator == null)
            {
                _progressIndicator = new ProgressIndicator();
                _progressIndicator.ProgressType = ProgressTypes.WaitCursor;
                _progressIndicator.ShowLabel = true;
                //  ApplicationBar.IsVisible = false;
            }

            _progressIndicator.Text = message;
            _progressIndicator.Show();
        }

        public void HideProgressIndicator()
        {
            if (!_indicatorVisible)
            {
                return;
            }
            Wait(false);
            _indicatorVisible = false;


            if (_progressIndicator != null)
            {
                _progressIndicator.Hide();
                _progressIndicator = null;
            }

            // ApplicationBar.IsVisible = true;
        }
    }
}
