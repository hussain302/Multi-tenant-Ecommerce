
namespace Domain.Enums;
public enum PasswordPolicyIssue
{
    MustHaveNumber,
    MustHaveCapitalLetter,
    MustHaveSmallLetter,
    MustBeAtLeast8Characters,
    MustHaveSymbol
}