using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFilters
{
    class ComplexNumber
    {
        double real, imaginary;

        public ComplexNumber(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }

        public double calculateModulus()
        {
            return real * real + imaginary * imaginary;
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real + b.real, a.imaginary + b.imaginary);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.real - b.real, a.imaginary - b.imaginary);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double realValue = (a.real * b.real) + (a.imaginary * b.imaginary * -1);
            double imaginaryValue = (a.real * b.imaginary) + (a.imaginary * b.real);

            return new ComplexNumber(realValue, imaginaryValue);
        }

        public static ComplexNumber operator *(ComplexNumber a, double val)
        {
            return new ComplexNumber(a.real * val, a.imaginary * val);
        }

        public static ComplexNumber operator *(double val, ComplexNumber a)
        {
            return new ComplexNumber(a.real * val, a.imaginary * val);
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            double numeratorReal = (a.real * b.real) + (a.imaginary * (b.imaginary * -1) * -1);
            double numeratorImaginary = (a.real * (b.imaginary * -1)) + (a.imaginary * b.real);
            double denumeratorReal = (b.real * b.real) + (b.imaginary * (b.imaginary * -1) * -1);

            return new ComplexNumber(numeratorReal / denumeratorReal, numeratorImaginary / denumeratorReal);
        }

        public static ComplexNumber operator /(ComplexNumber a, double val)
        {
            return new ComplexNumber(a.real / val, a.imaginary / val);
        }

        public ComplexNumber rectangularFromPolar(double r, double theta)
        {
            return new ComplexNumber(r * Math.Cos(theta), r * Math.Sin(theta));
        }
    }
}
