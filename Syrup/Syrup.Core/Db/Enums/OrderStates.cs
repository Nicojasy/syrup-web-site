namespace Syrup.Core.Database.Enums;

[Flags]
public enum OrderStates
{
    Basket = 1 << 0,
    New = 1 << 1,
    PendingPayment = 1 << 2,
    Paid = 1 << 3,
    Sent = 1 << 4,
    PendingReceipt = 1 << 5,
    Received = 1 << 6,
    Canceled = 1 << 7,
    Unpaid = 1 << 8
}
