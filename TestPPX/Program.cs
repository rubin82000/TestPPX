using PPX_Pos;


namespace TestPPX
{
    class Program
    {
        static void Main(string[] args)
        {
          //you can change this line
          var Pos = new PassportX_POS(); 
          var italyPassport = new ItalyPassportDecorator(Pos);
          POS_Process.Load(italyPassport);
        }
    }
}
