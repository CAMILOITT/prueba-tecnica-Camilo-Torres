using roulettegame.Controllers;

namespace roulettegame.utils
{
    public enum ETypeBet
    {
        Number, Red, Black, Even, Odd
    }

    public interface IBetClient
    {
        public List<ValueBetDT> Bet { get; set; }
    }

    public class ResultGame
    {
        public bool IsWin { get; set; }
        public decimal Gain { get; set; }
    }

    public class GenerateResultRoulette
    {
        private readonly List<string> _colors = new List<string> { "red", "black" };
        public IBetClient ClientBet;

        private bool _isWin { get; set; } = false;
        private decimal _gain { get; set; } = 0m;

        public string Color { get; set; }
        public int Number { get; set; }

        public GenerateResultRoulette(IBetClient clientBet)
        {
            ClientBet = clientBet;
            Color = GenerateColor();
            Number = GenerateNumber();
            ResultBet();
        }

        private int GenerateNumber()
        {
            int result = new Random().Next(0, 37);
            return result;
        }

        private string GenerateColor()
        {
            int result = new Random().Next(0, _colors.Count);
            return _colors[result];
        }

        private void ResultBet()
        {

            foreach (var bet in ClientBet.Bet)
            {
                if (bet.TypeBet == ETypeBet.Number &&  int.Parse(bet.Value)  == Number)
                {
                    _gain = bet.amount+ (bet.amount * 3);
                    _isWin = true;
                }
                else if (bet.TypeBet == ETypeBet.Red || bet.TypeBet == ETypeBet.Black)
                {
                    _gain = bet.amount + ( bet.amount / 2);
                    _isWin = true;
                }
                else if (bet.TypeBet == ETypeBet.Odd && int.Parse(bet.Value) == 1 || bet.TypeBet == ETypeBet.Even && int.Parse(bet.Value) % 2 == 0)
                {
                    _gain = bet.amount * 2;
                    _isWin = true;
                }
                else
                {
                    if (_isWin == false)
                    {
                        _isWin = false;
                        _gain = -bet.amount;
                    }
                }
            }

        }

        public ResultGame GetResult()
        {
            return new ResultGame
            {
                IsWin = _isWin,
                Gain = _gain
            };
        }

    }

}
