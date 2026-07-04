using System;
using System.Linq;
using EncheresPredict.Domain.Entities;
using EncheresPredict.Domain.Enums;
using EncheresPredict.Domain.Exceptions;
using Xunit;

namespace EncheresPredict.UnitTests;

public class CreditAccountTests
{
    [Fact]
    public void Create_Should_Create_Account_With_Welcome_Credits()
    {
        var userId = Guid.NewGuid().ToString();

        var account = CreditAccount.Create(userId);

        Assert.Equal(userId, account.UserId);
        Assert.Equal(CreditAccount.WelcomeCredits, account.Balance);
        Assert.Single(account.Transactions);

        var transaction = account.Transactions.Single();

        Assert.Equal(CreditTransactionType.Grant, transaction.Type);
        Assert.Equal(CreditAccount.WelcomeCredits, transaction.Amount);
        Assert.Equal(CreditAccount.WelcomeReason, transaction.Reason);
        Assert.Equal(account.Id, transaction.CreditAccountId);
    }

    [Fact]
    public void Grant_Should_Increase_Balance_And_Create_Transaction()
    {
        var userId = Guid.NewGuid().ToString();
        var account = CreditAccount.Create(userId);

        const int grantedCredits = 5;
        const string reason = "Admin bonus";

        account.Grant(grantedCredits, reason);

        Assert.Equal(CreditAccount.WelcomeCredits + grantedCredits, account.Balance);
        Assert.Equal(2, account.Transactions.Count);

        var transaction = account.Transactions.Last();

        Assert.Equal(CreditTransactionType.Grant, transaction.Type);
        Assert.Equal(grantedCredits, transaction.Amount);
        Assert.Equal(reason, transaction.Reason);
    }

    [Fact]
    public void Grant_Should_Throw_When_Amount_Is_Invalid()
    {
        var account = CreditAccount.Create(Guid.NewGuid().ToString());

        Assert.Throws<ArgumentOutOfRangeException>(() =>
            account.Grant(0, "Admin bonus"));
    }

    [Fact]
    public void Grant_Should_Throw_When_Reason_Is_Empty()
    {
        var account = CreditAccount.Create(Guid.NewGuid().ToString());

        var exception = Assert.Throws<ArgumentException>(() =>
            account.Grant(5, ""));

        Assert.Equal("reason", exception.ParamName);
    }

    [Fact]
    public void Consume_Should_Decrease_Balance_And_Create_Transaction()
    {
        var account = CreditAccount.Create(Guid.NewGuid().ToString());
        const string reason = "CCV analysis";

        account.Consume(reason);

        Assert.Equal(CreditAccount.WelcomeCredits - 1, account.Balance);
        Assert.Equal(2, account.Transactions.Count);

        var transaction = account.Transactions.Last();

        Assert.Equal(CreditTransactionType.Consume, transaction.Type);
        Assert.Equal(-1, transaction.Amount);
        Assert.Equal(reason, transaction.Reason);
    }

    [Fact]
    public void Consume_Should_Throw_When_Balance_Is_Zero()
    {
        var account = CreditAccount.Create(Guid.NewGuid().ToString());
        const string reason = "CCV analysis";

        account.Consume(reason);
        account.Consume(reason);
        account.Consume(reason);

        Assert.Throws<InsufficientCreditsException>(() =>
            account.Consume(reason));
    }
}
