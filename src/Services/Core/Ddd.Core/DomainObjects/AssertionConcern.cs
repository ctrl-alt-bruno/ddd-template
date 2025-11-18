using System.Text.RegularExpressions;

namespace Ddd.Core.DomainObjects;

/// <summary>
/// Classe base para validações de invariantes de domínio seguindo os princípios de DDD.
/// Implementa o padrão Assertion Concern para garantir a consistência das entidades de domínio.
/// </summary>
public class AssertionConcern
{
	#region Validações de Igualdade

	public static void ValidateIfEquals(object obj1, object obj2, string message)
	{
		if (obj1 == null || obj2 == null)
			throw new DomainException("Os objetos não podem ser nulos para comparação");

		if (obj1.Equals(obj2))
			throw new DomainException(message);
	}

	public static void ValidateIfNotEquals(object obj1, object obj2, string message)
	{
		if (obj1 == null || obj2 == null)
			throw new DomainException("Os objetos não podem ser nulos para comparação");

		if (!obj1.Equals(obj2))
			throw new DomainException(message);
	}

	public static void ValidateIfDifferent(object obj1, object obj2, string message)
	{
		if (obj1 == null || obj2 == null)
			throw new DomainException("Os objetos não podem ser nulos para comparação");

		if (obj1.Equals(obj2))
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Nulidade

	public static void ValidateIfNull(object obj, string message)
	{
		if (obj == null)
			throw new DomainException(message);
	}

	public static void ValidateIfNotNull(object obj, string message)
	{
		if (obj != null)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de String

	public static void ValidateIfEmpty(string value, string message)
	{
		if (string.IsNullOrWhiteSpace(value))
			throw new DomainException(message);
	}

	public static void ValidateIfNotEmpty(string value, string message)
	{
		if (!string.IsNullOrWhiteSpace(value))
			throw new DomainException(message);
	}

	public static void ValidateCharacterLength(string value, int minLength, int maxLength, string message)
	{
		if (value == null)
			throw new DomainException("O valor não pode ser nulo");

		int length = value.Trim().Length;

		if (length < minLength || length > maxLength)
			throw new DomainException(message);
	}

	public static void ValidateCharacterLength(string value, int maxLength, string message)
	{
		ValidateCharacterLength(value, 0, maxLength, message);
	}

	public static void ValidateMinLength(string value, int minLength, string message)
	{
		if (value == null)
			throw new DomainException("O valor não pode ser nulo");

		if (value.Trim().Length < minLength)
			throw new DomainException(message);
	}

	public static void ValidateMaxLength(string value, int maxLength, string message)
	{
		if (value == null)
			throw new DomainException("O valor não pode ser nulo");

		if (value.Trim().Length > maxLength)
			throw new DomainException(message);
	}

	public static void ValidateExpression(string pattern, string value, string message)
	{
		if (value == null)
			throw new DomainException("O valor não pode ser nulo");

		Regex regex = new Regex(pattern);

		if (!regex.IsMatch(value))
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Intervalo - Int

	public static void ValidateRange(int value, int min, int max, string message)
	{
		if (value < min || value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThan(int value, int min, string message)
	{
		if (value < min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThan(int value, int max, string message)
	{
		if (value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThanOrEqual(int value, int min, string message)
	{
		if (value <= min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThanOrEqual(int value, int max, string message)
	{
		if (value >= max)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Intervalo - Long

	public static void ValidateRange(long value, long min, long max, string message)
	{
		if (value < min || value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThan(long value, long min, string message)
	{
		if (value < min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThan(long value, long max, string message)
	{
		if (value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThanOrEqual(long value, long min, string message)
	{
		if (value <= min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThanOrEqual(long value, long max, string message)
	{
		if (value >= max)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Intervalo - Double

	public static void ValidateRange(double value, double min, double max, string message)
	{
		if (value < min || value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThan(double value, double min, string message)
	{
		if (value < min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThan(double value, double max, string message)
	{
		if (value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThanOrEqual(double value, double min, string message)
	{
		if (value <= min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThanOrEqual(double value, double max, string message)
	{
		if (value >= max)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Intervalo - Float

	public static void ValidateRange(float value, float min, float max, string message)
	{
		if (value < min || value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThan(float value, float min, string message)
	{
		if (value < min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThan(float value, float max, string message)
	{
		if (value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThanOrEqual(float value, float min, string message)
	{
		if (value <= min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThanOrEqual(float value, float max, string message)
	{
		if (value >= max)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Intervalo - Decimal

	public static void ValidateRange(decimal value, decimal min, decimal max, string message)
	{
		if (value < min || value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThan(decimal value, decimal min, string message)
	{
		if (value < min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThan(decimal value, decimal max, string message)
	{
		if (value > max)
			throw new DomainException(message);
	}

	public static void ValidateIfLessThanOrEqual(decimal value, decimal min, string message)
	{
		if (value <= min)
			throw new DomainException(message);
	}

	public static void ValidateIfGreaterThanOrEqual(decimal value, decimal max, string message)
	{
		if (value >= max)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Data

	public static void ValidateIfDateIsInFuture(DateTime date, string message)
	{
		if (date <= DateTime.Now)
			throw new DomainException(message);
	}

	public static void ValidateIfDateIsInPast(DateTime date, string message)
	{
		if (date >= DateTime.Now)
			throw new DomainException(message);
	}

	public static void ValidateIfDateIsToday(DateTime date, string message)
	{
		if (date.Date != DateTime.Now.Date)
			throw new DomainException(message);
	}

	public static void ValidateDateRange(DateTime date, DateTime minDate, DateTime maxDate, string message)
	{
		if (date < minDate || date > maxDate)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Coleções

	public static void ValidateIfEmpty<T>(IEnumerable<T> collection, string message)
	{
		if (collection == null || !collection.Any())
			throw new DomainException(message);
	}

	public static void ValidateIfNotEmpty<T>(IEnumerable<T> collection, string message)
	{
		if (collection != null && collection.Any())
			throw new DomainException(message);
	}

	public static void ValidateCollectionSize<T>(IEnumerable<T> collection, int minSize, int maxSize, string message)
	{
		if (collection == null)
			throw new DomainException("A coleção não pode ser nula");

		int count = collection.Count();

		if (count < minSize || count > maxSize)
			throw new DomainException(message);
	}

	public static void ValidateMinCollectionSize<T>(IEnumerable<T> collection, int minSize, string message)
	{
		if (collection == null)
			throw new DomainException("A coleção não pode ser nula");

		if (collection.Count() < minSize)
			throw new DomainException(message);
	}

	public static void ValidateMaxCollectionSize<T>(IEnumerable<T> collection, int maxSize, string message)
	{
		if (collection == null)
			throw new DomainException("A coleção não pode ser nula");

		if (collection.Count() > maxSize)
			throw new DomainException(message);
	}

	#endregion

	#region Validações de Guid

	public static void ValidateIfEmptyGuid(Guid guid, string message)
	{
		if (guid == Guid.Empty)
			throw new DomainException(message);
	}

	public static void ValidateIfNotEmptyGuid(Guid guid, string message)
	{
		if (guid != Guid.Empty)
			throw new DomainException(message);
	}

	#endregion

	#region Validações Booleanas

	public static void ValidateIfTrue(bool value, string message)
	{
		if (!value)
			throw new DomainException(message);
	}

	public static void ValidateIfFalse(bool value, string message)
	{
		if (value)
			throw new DomainException(message);
	}

	#endregion

	#region Validações Específicas de Domínio

	public static void ValidateEmail(string email, string message)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new DomainException("O e-mail não pode ser vazio");

		const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
		ValidateExpression(emailPattern, email, message);
	}

	public static void ValidateCpf(string cpf, string message)
	{
		if (string.IsNullOrWhiteSpace(cpf))
			throw new DomainException("O CPF não pode ser vazio");

		// Remove caracteres especiais
		cpf = new string(cpf.Where(char.IsDigit).ToArray());

		if (cpf.Length != 11)
			throw new DomainException(message);

		// Validação básica de CPF (pode ser expandida)
		if (cpf.Distinct().Count() == 1)
			throw new DomainException(message);
	}

	public static void ValidateCnpj(string cnpj, string message)
	{
		if (string.IsNullOrWhiteSpace(cnpj))
			throw new DomainException("O CNPJ não pode ser vazio");

		// Remove caracteres especiais
		cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

		if (cnpj.Length != 14)
			throw new DomainException(message);

		// Validação básica de CNPJ (pode ser expandida)
		if (cnpj.Distinct().Count() == 1)
			throw new DomainException(message);
	}

	public static void ValidateUrl(string url, string message)
	{
		if (string.IsNullOrWhiteSpace(url))
			throw new DomainException("A URL não pode ser vazia");

		if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult) ||
			(uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
		{
			throw new DomainException(message);
		}
	}

	public static void ValidatePhoneNumber(string phoneNumber, string message)
	{
		if (string.IsNullOrWhiteSpace(phoneNumber))
			throw new DomainException("O telefone não pode ser vazio");

		// Remove caracteres especiais
		var digits = new string(phoneNumber.Where(char.IsDigit).ToArray());

		// Valida telefones brasileiros (10 ou 11 dígitos)
		if (digits.Length < 10 || digits.Length > 11)
			throw new DomainException(message);
	}

	#endregion
}

public class DomainException : Exception
{
	public DomainException(string message) : base(message)
	{
	}

	public DomainException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
