namespace roulettegame.utils
{
    public enum ETypeBet
    {
      Number,  Red, Black,  Even, Odd
    }

    public interface IBetClient
    {
        ETypeBet TypeBet { get; }
        int Number { get; }
        string Color { get; }
        decimal Bet { get; }
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
            if (ClientBet.TypeBet == ETypeBet.Number && ClientBet.Number == Number)
            {
                _gain = ClientBet.Bet * 3;
                _isWin = true;
            }
            else if (ClientBet.TypeBet == ETypeBet.Red || ClientBet.TypeBet == ETypeBet.Black)
            {
                _gain = ClientBet.Bet / 2;
                _isWin = true;
            }
            else if (ClientBet.TypeBet == ETypeBet.Odd && ClientBet.Number % 2 == 1 || ClientBet.TypeBet == ETypeBet.Even && ClientBet.Number % 2 == 0)
            {
                _gain = ClientBet.Bet;
                _isWin = true;
            } else
            {
                _isWin = false;
                _gain = -ClientBet.Bet;
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
