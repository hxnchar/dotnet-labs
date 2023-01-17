using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr5.Classes
{
    public class Complex
    {
        public double Re { get; private set; }
        public double Im { get; private set; }
        public Complex(double re, double im)
        {
            Re = re;
            Im = im;
        }
        public static Complex operator +(Complex a, Complex b) => new Complex(a.Re + b.Re, a.Im + b.Im);
        public static Complex operator -(Complex a, Complex b) => new Complex(a.Re - b.Re, a.Im - b.Im);
        public static Complex operator *(Complex a, Complex b) => new Complex(a.Re * b.Re - a.Im * b.Im, a.Re * b.Im + a.Im * b.Re);
        public static Complex operator /(Complex a, Complex b)
            => new Complex((a.Re * b.Re + a.Im * b.Im) / (Math.Pow(b.Re, 2) + Math.Pow(b.Im, 2)),
               (a.Im * b.Re - a.Re * b.Im) / (Math.Pow(b.Re, 2) + Math.Pow(b.Im, 2)));
        public static bool operator ==(Complex a, Complex b) => a.Re == b.Re && a.Im == b.Im;
        public static bool operator !=(Complex a, Complex b) => a.Re != b.Re || a.Im != b.Im;
        public override string ToString() => $"{Re} {(Im > 0 ? "+" : "-")} {Im}i";
    }
}
