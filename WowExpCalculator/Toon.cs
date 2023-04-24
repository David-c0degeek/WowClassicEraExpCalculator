using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WowExpCalculator;

public sealed class Toon : INotifyPropertyChanged
{
    private int _level;
    public int Level
    {
        get => _level;
        set
        {
            if (_level == value) return;
            
            _level = value;
            OnPropertyChanged();
        }
    }

    private string _mobColor;
    public string MobColor
    {
        get => _mobColor;
        set
        {
            if (_mobColor == value) return;
            
            _mobColor = value;
            OnPropertyChanged();
        }
    }

    private int _expEarned;
    public int ExpEarned
    {
        get => _expEarned;
        set
        {
            if (_expEarned == value) return;
            
            _expEarned = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
