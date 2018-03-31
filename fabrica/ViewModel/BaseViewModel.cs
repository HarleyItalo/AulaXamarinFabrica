using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Threading;
using Plugin.TextToSpeech;
using Plugin.LocalNotifications;

namespace fabrica.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            IsLoading = true;

            var t = new Command(IniciarContador);
            t.Execute(null);
            CommandMudarElemento = new Command(() =>
            {

                if (IsLoading)
                {
                    IsLoading = false;
                    return;
                }
                IsLoading = true;
                t.Execute(null);
            });

            Falar = new Command(() =>
            {
                CrossLocalNotifications.Current.Show("Teste", "Oi Galera", 1, DateTime.Now.AddSeconds(1));
                CrossTextToSpeech.Current.Speak(OqueEpraFalar);
            });
        }

        public async void IniciarContador()
        {
            while (IsLoading)
            {
                await Task.Delay(1000);
                Segundos = DateTime.Now.ToString();
            }
        }

        public ICommand Falar
        {
            get;
            set;
        }

        private string _segundos;
        public string Segundos
        {
            get
            {
                return _segundos;
            }
            set
            {
                _segundos = value;
                Notify();
            }
        }

        private string _oQueEpraFalar;
        public string OqueEpraFalar
        {
            get
            {
                return _oQueEpraFalar;
            }
            set
            {
                _oQueEpraFalar = value;
                Notify();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                Notify();
            }
        }

        public ICommand CommandMudarElemento
        {
            get;
            set;
        }

        public virtual void Notify([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            GC.Collect();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
