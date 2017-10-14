using System;
using System.ComponentModel;

namespace RoulettePlayer
{
    public class Gambler
    {
        private readonly Random _random = new Random();
        private int _money;
        private readonly int _goalMoney;
        private int _moneyLostTotal;
        private int _lastBet;

        private bool _lastGame;
        private int _roundsPlayed;
        private int _lossesInARow;
        private int _longestLossStreak;
        private int _winsInARow;
        private int _longestWinStreak;

        public Gambler() : this (10, 250)
        {
        }

        public Gambler(int startingMoney, int goalMoney)
        {
            _money = startingMoney;
            _goalMoney = goalMoney;
        }

        public void TopUp(int moneyToAdd)
        {
            _money += moneyToAdd;
        }

        public bool Play()
        {
            do
            {
                var betValue = _lastBet == 0 ? 1 : _lastBet * 2;
                betValue = betValue > _money ? 1 : betValue;

                Bet(betValue);
            } while (_money > 0 && !CheckEndConditions());

            PrintGameEndReason();
            PrintResults();

            return _money != 0;
        }

        private void PrintResults()
        {
            Console.WriteLine("You ended the game with: $" + _money + "\nYou have played " + _roundsPlayed + " rounds of roulette.\n Longest loss streak: " + _longestLossStreak + ". Longest win streak: " +_longestLossStreak);
        }

        private void PrintGameEndReason()
        {
            Console.WriteLine("Game has ended because " + (_money == 0 ? "you ran out of money" : "you reached your goal. You lost: $" + _moneyLostTotal + " in total."));
        }

        private bool CheckEndConditions()
        {
            return _money > _goalMoney;
        }

        private void Bet(int moneyToBet)
        {
            ++_roundsPlayed;
            
            if (Roll()) Win(moneyToBet);
            else Lose(moneyToBet);
        }

        //TODO: CLEAN UP 'Lose' AND 'Win' FUNCTIONS
        private void Lose(int moneyBet)
        {
            _lossesInARow = !_lastGame ? _lossesInARow + 1 : 1;
            _longestLossStreak = _lossesInARow > _longestLossStreak ? _lossesInARow : _longestLossStreak;
            _lastGame = false;
            _moneyLostTotal += moneyBet;
            _lastBet = moneyBet;
            _money -= moneyBet;
        }

        private void Win(int moneyBet)
        {
            _winsInARow = _lastGame ? _winsInARow + 1 : 1;
            _longestWinStreak = _winsInARow > _longestWinStreak ? _winsInARow : _longestWinStreak;
            _lastGame = true;
            _moneyLostTotal -= moneyBet;
            _lastBet = 0;
            _money += moneyBet;
        }

        private bool Roll()
        {
            return _random.Next(0, 38) < 18;
        }
    }
}