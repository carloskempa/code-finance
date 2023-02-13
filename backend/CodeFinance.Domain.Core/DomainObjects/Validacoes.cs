using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CodeFinance.Domain.Core.DomainObjects
{
    public class Validacoes
    {
        public static void ValidarSeIgual(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
                throw new DomainException(mensagem);
        }

        public static void ValidarSeDiferente(object object1, object object2, string mensagem)
        {
            if (!object1.Equals(object2))
                throw new DomainException(mensagem);
        }

        public static void ValidarSeDiferente(string pattern, string valor, string mensagem)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(valor))
                throw new DomainException(mensagem);
        }

        public static void ValidarTamanho(string valor, int maximo, string mensagem)
        {
            var length = valor.Trim().Length;
            if (length > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarTamanho(string valor, int minimo, int maximo, string mensagem)
        {
            var length = valor.Trim().Length;
            if (length < minimo || length > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeVazio(string valor, string mensagem)
        {
            if (valor == null || valor.Trim().Length == 0)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeNulo(object object1, string mensagem)
        {
            if (object1 == null)
                throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(long valor, long minimo, long maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(long valor, long minimo, string mensagem)
        {
            if (valor < minimo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(double valor, double minimo, string mensagem)
        {
            if (valor < minimo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(decimal valor, decimal minimo, string mensagem)
        {
            if (valor < minimo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(int valor, int minimo, string mensagem)
        {
            if (valor < minimo)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorQue(DateTime valor, DateTime minimo, string mensagem)
        {
            if (valor.Date < minimo.Date)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorOuIgualQue(long valor, long comparacao, string mensagem)
        {
            if (valor < comparacao)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorOuIgualQue(double valor, double comparacao, string mensagem)
        {
            if (valor < comparacao)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorOuIgualQue(decimal valor, decimal comparacao, string mensagem)
        {
            if (valor < comparacao)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorOuIgualQue(int valor, int comparacao, string mensagem)
        {
            if (valor < comparacao)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeFalso(bool boolvalor, string mensagem)
        {
            if (!boolvalor)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeVerdadeiro(bool boolvalor, string mensagem)
        {
            if (boolvalor)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeValido(string valor, string regex, string mensagem)
        {
            if (!Regex.IsMatch(valor, regex))
                throw new DomainException(mensagem);
        }

        public static void ValidarSeEmailValido(string valor, string mensagem)
        {
            ValidarSeValido(valor, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", mensagem);
        }

        /// <summary>
        /// Pelo menos 8 caracteres, incluindo pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="mensagem"></param>
        public static void ValidarSeSenhaValido(string valor, string mensagem)
        {
            ValidarSeValido(valor, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,}$", mensagem);
        }
    }
}
