using MediMax.Business.Utils;

namespace MediMax.Business.Validations
{
    public class DefaultErrorMessages
    {
        public const string RequiredField = "Campo obrigatório.";
        public const string InvalidDate = "Informe uma data válida.";
        public const string InvalidEmail = "Informe um email válido.";
        public const string InvalidId = "Informe um número inteiro maior que zero.";
        public const string InvalidTime = "Informe uma hora válida.";
        public const string OneOrZero = "Informe 1 ou 0.";
        public const string InvalidCpjCnpj = "Informe um CPF/CNPJ válido.";

        public static string PasswordOutFormat()
        {
            return
                $"A sua senha deve conter, no mínimo {BusinessRulesConstants.MinimumLengthPassword} dígitos, sendo composta por números, letras maísculas e mínusculas e caracteres especiais.";
        }
        public static string TextOutOfBounds(int min, int max)
        {
            string t = $@"Esse campo deve conter entre {min} e {max} dígitos.";
            return t;
        }

        public static string NumberOutOfBounds(int min, int max)
        {
            return $@"Informe um número inteiro entre {min} e {max}";
        }

        public static string NumberGreaterOrEqualThan(int min)
        {
            return $@"Informe um número inteiro maior que {min} ";
        }
    }
}