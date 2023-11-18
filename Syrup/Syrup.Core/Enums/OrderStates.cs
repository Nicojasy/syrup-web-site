namespace Syrup.Core.Enums;

[Flags]
public enum OrderStates
{
    New = 1 << 0,
    PendingPayment = 1 << 1,
    Paid = 1 << 2,
    Sent = 1 << 3,
    PendingReceipt = 1 << 4,
    Received = 1 << 5,
    Canceled = 1 << 6,
    Unpaid = 1 << 7
}
