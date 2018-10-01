using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FI_Editor.Logica
{
    class Tabla
    {
        public Tabla padre;
        public ArrayList tabla;

        public Tabla() {
            this.padre = null;
            this.tabla = new ArrayList();
        }

        public Tabla(Tabla padre) {
            this.padre = padre;
            this.tabla = new ArrayList();
        }

        public bool contiene(String identificador)
        {
            if (this.tabla.Count < 1) return false;
            if (this.tabla == null) return false;

            foreach (Object simb in this.tabla)
            {
                Simbolo aux = (Simbolo)simb;
                if (aux.identificador.Equals(identificador))
                    return true;
            }
            return false;
        }

        public void insertarSinValor(Simbolo simbolo) {
            if (this.tabla == null) this.tabla = new ArrayList();

            if (contiene(simbolo.identificador))
                throw new Exception("la variable ha sido declarada en el mismo ambito anteriormente");

            this.tabla.Add(simbolo);
        }

        public void insertarConValor(Simbolo simbolo) {
            if (contiene(simbolo.identificador))
                throw new Exception("la variable ha sido declarada en el mismo ambito anteriormente");

            switch (simbolo.tipo) {
                case "int":
                    if (simbolo.valor is int)
                        tabla.Add(simbolo);
                    else throw new Exception("el valor asignado a la variable " + simbolo.identificador
                        + " no es de tipo int");
                    break;

                case "float":
                    if (simbolo.valor is double)
                        tabla.Add(simbolo);
                    else throw new Exception("el valor asignado a la variable " + simbolo.identificador 
                        + " no es de tipo float");
                    break;

                case "char*":
                    if (simbolo.valor is String)
                        tabla.Add(simbolo);
                    else throw new Exception("el valor asignado a la variable " + simbolo.identificador
                        + " no es de tipo char*");
                    break;

                case "bool":
                    if (simbolo.valor is bool)
                        tabla.Add(simbolo);
                    else throw new Exception("el valor asignado a la variable " + simbolo.identificador
                        + " no es de tipo bool");
                    break;
            }
        }

        public void actualizarValor(String identificador, Object valor) {
            if (!(contiene(identificador)))
                throw new Exception("La variable " + identificador + " no existe en el contexto actual");

            foreach (Object sim in this.tabla) {
                if (((Simbolo)sim).identificador.Equals(identificador)) {

                    switch (((Simbolo)sim).tipo) {
                        case "int":
                            if (valor is int)
                                ((Simbolo)sim).valor = valor;
                            else throw new Exception("El valor asignado a la variable " + identificador
                                + " no es de tipo int");
                            break;

                        case "float":
                            if (valor is double)
                                ((Simbolo)sim).valor = valor;
                            else throw new Exception("El valor asignado a la variable " + identificador
                                + " no es de tipo float");
                            break;

                        case "char*":
                            if (valor is String)
                                ((Simbolo)sim).valor = valor;
                            else throw new Exception("El valor asignado a la variable " + identificador
                                + " no es de tipo char*");
                            break;

                        case "bool":
                            if (valor is bool)
                                ((Simbolo)sim).valor = valor;
                            else throw new Exception("El valor asignado a la variable " + identificador
                                + " no es de tipo bool");
                            break;
                    }
                }
            }
        }

        public Object obtenerValor(String identificador) {

            foreach (Simbolo sim in this.tabla) {
                if (sim.identificador.Equals(identificador))
                    return sim.valor;
            }

            throw new Exception("La variable " + identificador + " no ha sido declarada en el contexto actual");
        }

        public void heredar() {

            foreach (Simbolo sim in this.padre.tabla) {
                this.tabla.Add(sim);
            }
        }

        public void actualizarPadre() {

            foreach (Simbolo sim in this.tabla) {
                foreach (Simbolo sim2 in this.padre.tabla) {
                    if (sim.identificador.Equals(sim2.identificador))
                        sim2.valor = sim.valor;
                }
            }
        }
    }
}
