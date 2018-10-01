using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI_Editor.Logica
{
    class Calculadora
    {

        public static Object sumar(Object arg1, Object arg2) {
            if (arg1 == null)
                throw new Exception("El primer operando es nulo");
            if (arg2 == null)
                throw new Exception("El segundo operando es nulo");

            if (arg1 is String)
            {
                if (arg2 is String)
                    return (String)((String)arg1 + (String)arg2);
                else if (arg2 is int)
                    return (String)((String)arg1 + Convert.ToString((int)arg2));
                else if (arg2 is double)
                    return (String)((String)arg1 + Convert.ToString((double)arg2));
                else if (arg2 is bool)
                    return (String)((String)arg1 + Convert.ToString((bool)arg2));
            }
            else if (arg1 is int)
            {
                if (arg2 is String)
                    return (String)(Convert.ToString((int)arg1) + (String)arg2);
                else if (arg2 is int)
                    return (int)((int)arg1 + (int)arg2);
                else if (arg2 is double)
                    return (double)(Convert.ToDouble(arg1) + (double)arg2);
                else if (arg2 is bool)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre int y float");
            }
            else if (arg1 is double)
            {
                if (arg2 is String)
                    return (String)(Convert.ToString((double)arg1) + (String)arg2);
                else if (arg2 is double)
                    return (double)((double)arg1 + (double)arg2);
                else if (arg2 is int)
                    return (double)((double)arg1 + Convert.ToDouble(arg2));
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre float y bool");
            }
            else if (arg1 is bool)
            {
                if (arg2 is String)
                    return (String)(Convert.ToString((bool)arg1) + (String)arg2);
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones aritmeticas sobre valores booleanos");
                else if (arg2 is int)
                    throw new Exception("No se puede realizar operacioens aritmeticas entre bool e int");
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre bool y double");
            }

            throw new Exception("La operacion no fue realizada");

        }

        public static Object restar(Object arg1, Object arg2) {

            if (arg1 == null)

                throw new Exception("El primer operando tiene valor nulo");
            if (arg2 == null)
                throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se pueden realizar restas con cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se puede realizar restas con valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)((int)arg1 - (int)arg2);
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y float");
                else if (arg2 is String)
                    throw new Exception("No se puede realizar restas entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar restas entre int y bool");
            }
            else if (arg1 is double) {

                if (arg2 is double)
                    return (double)((double)arg1 - (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar resta entre float y char*");
                else if (arg2 is int)
                    throw new Exception("No se puede realizar resta entre float e int");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar resta entre float y bool");
            }

            throw new Exception("El primer argumento no es de un tipo conocido");
        }

        public static Object multiplicar(Object arg1, Object arg2) {
            if (arg1 == null)
                throw new Exception("El primer operando tiene valor nulo");
            if (arg2 == null)
                throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se pueden realizar multiplicaciones con cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se pueden realizar multiplicaciones con valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)((int)arg1 * (int)arg2);
                else if (arg2 is double)
                    return (double)(Convert.ToDouble(arg1) * (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y bool");
            }
            else if (arg1 is double)
            {

                if (arg2 is double)
                    return (double)((double)arg1 * (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y char*");
                else if (arg2 is int)
                    return (double)((double)arg1 + Convert.ToDouble(arg2));
                else if (arg2 is bool)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y bool");
            }

            throw new Exception("El primer argumento no es de un tipo conocido");
        }

        public static Object dividir(Object arg1, Object arg2) {
            if (arg1 == null)
                throw new Exception("El primer operando tiene valor nulo");
            if (arg2 == null)
                throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se pueden realizar operaciones aritmeticas con cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se pueden realizar operaciones aritmeticas con valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)((int)arg1 / (int)arg2);
                else if (arg2 is double)
                    return (double)(Convert.ToDouble(arg1) / (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)((double)arg1 / (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y char*");
                else if (arg2 is int)
                    return (double)((double)arg1 / Convert.ToDouble(arg2));
                else if (arg2 is bool)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y bool");
            }

            throw new Exception("El primer argumento no es de un tipo conocido");
        }

        public static Object modular(Object arg1, Object arg2) {
            if (arg1 == null)
                throw new Exception("El primer operando tiene valor nulo");
            if (arg2 == null)
                throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se pueden realizar operaciones aritmeticas con cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se pueden realizar operaciones aritmeticas con valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)((int)arg1 % (int)arg2);
                else if (arg2 is double)
                    return (double)(Convert.ToDouble(arg1) % (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones aritmeticas entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)((double)arg1 % (double)arg2);
                else if (arg2 is String)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y char*");
                else if (arg2 is int)
                    return (double)((double)arg1 % Convert.ToDouble(arg2));
                else if (arg2 is bool)
                    throw new Exception("No se pueden realizar operaciones aritmeticas entre float y bool");
            }

            throw new Exception("El primer argumento no es de un tipo conocido");
        }

        public static Object negativo(Object arg1) {

            if (arg1 == null) throw new Exception("El argumento es de valor nulo");
            else if (arg1 is String) throw new Exception("Imposible hacer una cadena negativa");
            else if (arg1 is bool) throw new Exception("No se pueden realizar operaciones" +
                " aritmeticas sobre valores booleanos");
            else if (arg1 is int)
                return -(int)arg1;
            else if (arg1 is double)
                return -(double)arg1;

            throw new Exception("Imposible realizar la operacion");
        }

        public static bool menorQue(Object arg1, Object arg2) {

            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones relaciones " +
                "sobre cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 < (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) < (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 < (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 < Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool mayorQue(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones relaciones " +
                "sobre cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 > (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) > (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 > (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 > Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool menorIgual(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones relaciones " +
                "sobre cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 <= (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) <= (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 <= (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 <= Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool mayorIgual(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones relaciones " +
                "sobre cadenas de caracteres");
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 >= (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) >= (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 >= (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 >= Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool igual(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
            {
                if (arg2 is String)
                    return ((String)arg1).Equals((String)arg2);
                else if (arg2 is int)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* e int");
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* y float");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* y bool");
            }
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 == (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) == (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 == (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 == Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool diferente(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer operando tiene valor nulo");
            else if (arg2 == null) throw new Exception("El segundo operando tiene valor nulo");

            if (arg1 is String)
            {
                if (arg2 is String)
                    return !((String)arg1).Equals((String)arg2);
                else if (arg2 is int)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* e int");
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* y float");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre char* y bool");
            }
            else if (arg1 is bool)
                throw new Exception("No se puede realizar operaciones relacionales sobre valores booleanos");
            else if (arg1 is int)
            {
                if (arg2 is int)
                    return (int)arg1 != (int)arg2;
                else if (arg2 is double)
                    return Convert.ToDouble(arg1) != (double)arg2;
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relaciones entre int y bool");
            }
            else if (arg1 is double)
            {
                if (arg2 is double)
                    return (double)arg1 != (double)arg2;
                else if (arg2 is int)
                    return (double)arg1 != Convert.ToDouble(arg2);
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y char*");
                else if (arg2 is bool)
                    throw new Exception("No se puede realizar operaciones relacionales entre float y bool");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool conjuncion(Object arg1, Object arg2) {

            if (arg1 == null) throw new Exception("El primer argumento es de valor nulo");
            if (arg2 == null) throw new Exception("El segundo argumento es de valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones booleanas con char*");
            else if (arg1 is int)
                throw new Exception("No se puede realizar operaciones booleanas con int");
            else if (arg1 is double)
                throw new Exception("No se puede realizar operaciones booleanas con float");
            else if (arg1 is bool)
            {
                if (arg2 is bool)
                    return (bool)arg1 && (bool)arg2;
                else if (arg2 is int)
                    throw new Exception("No se puede realizar operaciones booleanas entre bool e int");
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operacioens booleanas entre bool y double");
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones booleanas entre bool y char*");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static bool disyuncion(Object arg1, Object arg2) {
            if (arg1 == null) throw new Exception("El primer argumento es de valor nulo");
            if (arg2 == null) throw new Exception("El segundo argumento es de valor nulo");

            if (arg1 is String)
                throw new Exception("No se puede realizar operaciones booleanas con char*");
            else if (arg1 is int)
                throw new Exception("No se puede realizar operaciones booleanas con int");
            else if (arg1 is double)
                throw new Exception("No se puede realizar operaciones booleanas con float");
            else if (arg1 is bool)
            {
                if (arg2 is bool)
                    return (bool)arg1 || (bool)arg2;
                else if (arg2 is int)
                    throw new Exception("No se puede realizar operaciones booleanas entre bool e int");
                else if (arg2 is double)
                    throw new Exception("No se puede realizar operacioens booleanas entre bool y double");
                else if (arg2 is String)
                    throw new Exception("No se puede realizar operaciones booleanas entre bool y char*");
            }
            throw new Exception("Imposible realizar la operacion");
        }

        public static Object incremento(Object arg1) {
            if (arg1 == null)
                throw new Exception("La variable indicada es de valor nulo");

            if (arg1 is String)
                throw new Exception("La operacion de incremento no se puede realizar sobre char*");
            else if (arg1 is bool)
                throw new Exception("La operacion de incremento no se puede realizar sobre valores booleanos");
            else if (arg1 is int)
                return ((int)arg1) + 1;
            else if (arg1 is double)
                return (double)arg1 + 1;

            throw new Exception("Imposible realizar la operacion");
        }

        public static Object decremento(Object arg1) {
            if (arg1 == null)
                throw new Exception("La variable indicada es de valor nulo");

            if (arg1 is String)
                throw new Exception("La operacion de incremento no se puede realizar sobre char*");
            else if (arg1 is bool)
                throw new Exception("La operacion de incremento no se puede realizar sobre valores booleanos");
            else if (arg1 is int)
                return ((int)arg1) - 1;
            else if (arg1 is double)
                return (double)arg1 - 1;

            throw new Exception("Imposible realizar la operacion");
        }
    }
}
