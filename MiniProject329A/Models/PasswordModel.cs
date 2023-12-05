namespace MiniProject329A.Models
{
    public class PasswordModel
    {
        public bool KurangHurufKecil(string password)
        {
            string hurufK = "abcdefghijklmnopqrstuvwxyz";
            bool komen;

            bool isHurufK = false;

            isHurufK = !hurufK.Any(a => password.Any(b => a == b));

            if (isHurufK)
            {
                komen = false;
            }
            else { komen = true; }

            return komen;
        }
        public bool KurangHurufBesar(string password)
        {
            string hurufK = "abcdefghijklmnopqrstuvwxyz";
            string hurufB = hurufK.ToUpper();
            bool komen;

            bool isHurufB = false;
            isHurufB = !hurufB.Any(a => password.Any(b => a == b));

            if (isHurufB)
            {
                komen = false;
            }
            else { komen = true; }

            return komen;
        }
        public bool KurangSimbol(string password)
        {
            string simbol = "~!@#$%^&*";
            bool komen;

            bool isSimbol = false;

            isSimbol = !simbol.Any(a => password.Any(b => a == b));

            if (isSimbol)
            {
                komen = false;
            }
            else { komen = true; }

            return komen;
        }
        public bool KurangAngka(string password)
        {
            string angka = "1234567890";
            bool komen;

            bool isAngka = false;

            isAngka = !angka.Any(a => password.Any(b => a == b));

            if (isAngka)
            {
                komen = false;
            }
            else { komen = true; }

            return komen;
        }
    }
}
