// Licensed to the CodeRabbits under one or more agreements.
// The CodeRabbits licenses this file to you under the MIT license.

using CodeRabbits.KaoList.Web.Resrouces;
using Microsoft.AspNetCore.Identity;

namespace CodeRabbits.KaoList.Web.Areas.Identity.Pages
{
    public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(IdentityErrorMessages.DuplicateEmail, email) }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format(IdentityErrorMessages.DuplicateUserName, userName) }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(IdentityErrorMessages.InvalidEmail, email) }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(IdentityErrorMessages.DuplicateRoleName, role) }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(IdentityErrorMessages.InvalidRoleName, role) }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = IdentityErrorMessages.InvalidToken }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format(IdentityErrorMessages.InvalidUserName, userName) }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = IdentityErrorMessages.LoginAlreadyAssociated }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = IdentityErrorMessages.PasswordMismatch }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = IdentityErrorMessages.PasswordRequiresDigit }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = IdentityErrorMessages.PasswordRequiresLower }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = IdentityErrorMessages.PasswordRequiresNonAlphanumeric }; }
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) { return new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = string.Format(IdentityErrorMessages.PasswordRequiresUniqueChars, uniqueChars) }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = IdentityErrorMessages.PasswordRequiresUpper }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(IdentityErrorMessages.PasswordTooShort, length) }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = IdentityErrorMessages.UserAlreadyHasPassword }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(IdentityErrorMessages.UserAlreadyInRole, role) }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(IdentityErrorMessages.UserNotInRole, role) }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = IdentityErrorMessages.UserLockoutNotEnabled }; }
        public override IdentityError RecoveryCodeRedemptionFailed() { return new IdentityError { Code = nameof(RecoveryCodeRedemptionFailed), Description = IdentityErrorMessages.RecoveryCodeRedemptionFailed }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = IdentityErrorMessages.ConcurrencyFailure }; }
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = IdentityErrorMessages.DefaultError }; }
        public IdentityError NicknameRequires(string nickname) { return new IdentityError { Code = nameof(NicknameRequires), Description = string.Format(IdentityErrorMessages.NicknameRequires, nickname) }; }
    }
}
