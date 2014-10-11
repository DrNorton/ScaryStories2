using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using ScaryStories.Visual.ViewModels;

namespace ScaryStories.Visual
{
    public class BootStrapper : PhoneBootstrapperBase
    {
        private PhoneContainer _container;

        public BootStrapper()
        {
            base.StartRuntime();
        }
        protected override void Configure()
        {
            _container = new PhoneContainer();
            _container.RegisterPhoneServices(RootFrame);
            RegisterViewModels();
        }

        private void RegisterViewModels()
        {
            _container.PerRequest<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            // FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }

        protected override void OnActivate(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            //FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }


        protected override void OnClose(object sender, Microsoft.Phone.Shell.ClosingEventArgs e)
        {
            BackgroundAudioPlayer.Instance.Track = null;
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Произошло необработанное исключение; перейти в отладчик
                System.Diagnostics.Debugger.Break();
            }

            MessageBox.Show(e.ExceptionObject.Message, "Неожиданная ошибка", MessageBoxButton.OK);
        } 
    }
}
