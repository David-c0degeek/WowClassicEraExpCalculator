using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WowExpCalculator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Party = new ObservableCollection<Toon>();
            AddToonCommand = new RelayCommand(AddToon, CanAddToon);
            RemoveToonCommand = new RelayCommand<Toon>(RemoveToon, CanRemoveToon);
        }

        public ObservableCollection<Toon> Party { get; }

        private int _mobLevel;
        public int MobLevel
        {
            get => _mobLevel;
            set
            {
                if (_mobLevel == value) return;
                
                _mobLevel = value;
                OnPropertyChanged();
                RecalculateExperience();
            }
        }

        private int _newToonLevel;
        public int NewToonLevel
        {
            get => _newToonLevel;
            set
            {
                if (_newToonLevel == value) return;
                
                _newToonLevel = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddToonCommand { get; }
        public ICommand RemoveToonCommand { get; }

        private void AddToon()
        {
            var newToon = new Toon { Level = NewToonLevel };
            Party.Add(newToon);
            RecalculateExperience();
        }

        private bool CanAddToon()
        {
            return MobLevel > 0 && NewToonLevel is >= 1 and <= 60 && Party.Count < 5;
        }

        private void RemoveToon(Toon toon)
        {
            Party.Remove(toon);
            RecalculateExperience();
        }

        private static bool CanRemoveToon(Toon? toon)
        {
            return toon != null;
        }

        private void RecalculateExperience()
        {
            ExpCalculationService.CalculateExpForParty(Party, MobLevel);

            foreach (var toon in Party)
            {
                toon.MobColor = MobColorService.GetMobColor(MobLevel, toon.Level);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}