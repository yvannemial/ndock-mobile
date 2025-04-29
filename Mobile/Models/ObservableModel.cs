using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace Mobile.Models
{
    /// <summary>
    /// Base class for models that need to implement INotifyPropertyChanged
    /// </summary>
    public abstract class ObservableModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;
            
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}