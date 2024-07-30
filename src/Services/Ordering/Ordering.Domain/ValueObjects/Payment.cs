﻿using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Reflection.Emit;

namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string Cvv { get; }
    public string CVV { get; } = default!;
    public string PaymentMethod { get; } = default!;

    protected Payment()
    {

    }

    private Payment(string cardName, string cardNumber, string expiration, string cvv, string paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, string paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}