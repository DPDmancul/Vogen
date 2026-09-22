using System.Collections.Generic;

namespace Vogen;

public class Validation
{
    public string ErrorMessage { get; }

    /// <summary>
    /// Contains data related to validation.
    /// </summary>
    public Dictionary<object, object>? Data { get; private set; }

    public static readonly Validation Ok = new Validation(string.Empty);

    private protected Validation(string reason) => ErrorMessage = reason;

    public static Validation Invalid(string reason = "")
    {
        if (string.IsNullOrEmpty(reason))
        {
            return new Validation("[none provided]");
        }

        return new Validation(reason);
    }

    /// <summary>
    /// Adds the specified data to the validation.
    /// This data will be copied to the Data property of the thrown Exception.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>Validation.</returns>
    public Validation WithData(object key, object value)
    {
        Data ??= new();
        Data[key] = value;
        return this;
    }
}

public sealed class Validation<T> : Validation
{
    private readonly T? _value;

    private Validation(string reason, T? value): base(reason) => _value = value;

    public new static Validation<T> Ok(T value) => new Validation<T>(string.Empty, value);

    public new static Validation<T> Invalid(string reason = "")
    {
        if (string.IsNullOrEmpty(reason))
        {
            return new Validation<T>("[none provided]", default);
        }

        return new Validation<T>(reason, default);
    }

    /// <inheritdoc cref="Validation.WithData" />
    public new Validation<T> WithData(object key, object value)
    {
        base.WithData(key, value);
        return this;
    }
}
